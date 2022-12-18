using System;
using System.Windows;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO
        //Schuppen Zeven
        // foute kaartscores 
        readonly bool isSpeler = true;
        DispatcherTimer spelerDptmr = new DispatcherTimer();
        DispatcherTimer dealerDptmr = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            MaakVeldenLeeg();
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            BtnDouble.IsEnabled = false;
            BtnInzetPlus1.IsEnabled = false;
            BtnInzetPlus10.IsEnabled = false;
            BtnInzetPlus5.IsEnabled = false;
            BtnInzetPlus25.IsEnabled = false;
            BtnResetInzet.IsEnabled = false;
            LblBudget.Text = "";
            LblInzet.Text = "";
            // timers 
            dealerDptmr.Interval = spelerDptmr.Interval = TimeSpan.FromSeconds(1);
            spelerDptmr.Tick += GeefSpelerKaart;
            dealerDptmr.Tick += GeefDealerKaart;
        }

        #region Nieuwe rondes en spel
        /// <summary>
        /// Maakt ListBoxes leeg en reset het Resultaat label.
        /// </summary>
        private void MaakVeldenLeeg()
        {
            LblResultaat.Text = "";
            LBSpelerKaarten.Items.Clear();
            LBDealerKaarten.Items.Clear();
        }

        /// <summary>
        /// Functie om een nieuw spel te starten. <para>Geeft de speler 100 flippo's, zet de speel knoppen op enabled en start dan een nieuwe ronde.</para>
        /// </summary>
        private void Newgame()

        {
            Speler.Budget = 100;
            Speler.Inzet = 0;
            UpdateBudget();
            UpdateInzet(0);
            BtnDeel.IsEnabled = true;
            BtnInzetPlus1.IsEnabled = true;
            BtnInzetPlus10.IsEnabled = true;
            BtnInzetPlus5.IsEnabled = true;
            BtnInzetPlus25.IsEnabled = true;
            BtnResetInzet.IsEnabled = true;
            BtnNieuwSpel.IsEnabled = false;
        }

        /// <summary>
        /// Functie om een nieuwe ronde te starten. <para>Maakt alle velden en scores leeg en deactiveert hit/stand knoppen. Deelt daarna kaarten uit.</para>
        /// </summary>
        private void NewRound()
        {
            // leegmaken
            MaakVeldenLeeg();
            Speler.SpelerPunten = 0;
            Dealer.DealerPunten = 0;
            LblDealerScore.Text = Convert.ToString(Speler.SpelerPunten);
            LblSpelerScore.Text = Convert.ToString(Dealer.DealerPunten);

            // Knoppen veranderen
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
            BtnDouble.IsEnabled = false;
            BtnInzetPlus1.IsEnabled = false;
            BtnInzetPlus5.IsEnabled = false;
            BtnInzetPlus10.IsEnabled = false;
            BtnInzetPlus25.IsEnabled = false;
            BtnResetInzet.IsEnabled = false;
        }

        #endregion

        #region Win lose conditie functies
        /// <summary>
        /// Tesktlabels worden veranderd, <c>Speler.Budget</c> wordt verhoogd met dubbel <c>Speler.Inzet</c>, speelknoppen worden uitgezet en inzetknoppen aangezet.
        /// </summary>
        private void Win()
        {
            LblResultaat.Text = "Gewonnen!";
            Speler.Budget += Speler.Inzet * 2;
            LblBudget.Text = Convert.ToString(Speler.Budget);
            Speler.Inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.Inzet);
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
            BtnInzetPlus1.IsEnabled = true;
            BtnInzetPlus5.IsEnabled = true;
            BtnInzetPlus10.IsEnabled = true;
            BtnInzetPlus25.IsEnabled = true;
            BtnResetInzet.IsEnabled = true;
        }
        /// <summary>
        /// Tesktlabels worden veranderd, <c>Speler.Budget</c> wordt verminderd met <c>Speler.Inzet</c>, speelknoppen worden uitgezet en inzetknoppen aangezet.
        /// </summary>
        private void Lose()
        {
            LblResultaat.Text = "Verloren!";
            LblBudget.Text = Convert.ToString(Speler.Budget);
            Speler.Inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.Inzet);
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
            BtnInzetPlus1.IsEnabled = true;
            BtnInzetPlus5.IsEnabled = true;
            BtnInzetPlus10.IsEnabled = true;
            BtnInzetPlus25.IsEnabled = true;
            BtnResetInzet.IsEnabled = true;
            
            Blut();
        }
        /// <summary>
        /// Tesktlabels worden veranderd, <c>Speler.Budget</c> wordt terug verhoogd <c>Speler.Inzet</c>, speelknoppen worden uitgezet en inzetknoppen aangezet.
        /// </summary>
        private void Push()
        {
            LblResultaat.Text = "Push!";
            Speler.Budget += Speler.Inzet;
            LblBudget.Text = Convert.ToString(Speler.Budget);
            Speler.Inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.Inzet);
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
            BtnInzetPlus1.IsEnabled = true;
            BtnInzetPlus5.IsEnabled = true;
            BtnInzetPlus10.IsEnabled = true;
            BtnInzetPlus25.IsEnabled = true;
            BtnResetInzet.IsEnabled = true;
        }
        #endregion

        #region Inzetten en kapitaal functiets
        /// <summary>
        /// Checkt of <c>Speler.Inzet</c> kleiner is dan <c>Speler.Budget</c> en groter dan 10% van <c>Speler.Budget</c>.
        /// <para>Als de condities kloppen, wordt de inzet geaccepteerd en naar <c>Speler.Inzet</c> geschereven.</para>
        /// <para>Als de conditie niet klopt zal er een <c>MessageBox</c> komen of zal <c>LblResultaat</c> veranderen</para>
        /// </summary>
        /// <param name="inzet">Waarde van <c>BtnInzet</c></param>
        private void UpdateInzet(int inzet)
        {
            if (inzet < Speler.Budget)
            {
                BtnDeel.IsEnabled = true;
                Speler.Inzet += inzet;
                LblInzet.Text = Convert.ToString(Speler.Inzet);
            }
            else if (inzet > Speler.Budget)
            {
                MessageBox.Show("U kan niet meer inzetten dan u heeft!", "Inzet Fout", MessageBoxButton.OK);
            }

            if (inzet > Speler.Budget * 0.1)
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";
                BtnDeel.IsEnabled = true;
            }

        }
        /// <summary>
        /// <c>Speler.Inzet</c> wordt afgetrokken van <c>Speler.Budget</c> en <c>LblBudget wordt veranderd.</c>
        /// </summary>
        private void UpdateBudget()
        {
            Speler.Budget -= Speler.Inzet;
            LblBudget.Text = Convert.ToString(Speler.Budget);
        }
        /// <summary>
        /// Als <c>Speler.Budget</c> gelijk is aan 0, komt een <c>MessageBox</c> en worden de knoppen uitgezet
        /// <para>Daarna worden de velden leeggemaakt en <c>BtnNieuwSpel</c> wordt aangezet</para>
        /// </summary>
        private void Blut()
        {
            if (Speler.Budget == 0)
            {
                MessageBox.Show("U bent blut!", "Einde spel", MessageBoxButton.OK);

                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnInzetPlus1.IsEnabled = false;
                BtnInzetPlus5.IsEnabled = false;
                BtnInzetPlus10.IsEnabled = false;
                BtnInzetPlus25.IsEnabled = false;
                BtnResetInzet.IsEnabled = false;
                MaakVeldenLeeg();

                BtnNieuwSpel.IsEnabled = true;
            }
        }
        #endregion

        #region Kaart functies
        /// <summary>
        /// Start <c>spelerDptmr</c> of <c>dealerDptmr</c>.
        /// </summary>
        /// <param name="isSpeler">Bepaald of de <c>spelerDptmr</c> of <c>dealerDptmr</c> qordt gestart.</param>
        private void VertraagdeKaartDeler(bool isSpeler)
        {
            if (isSpeler)
            {
                spelerDptmr.Start();
            }
            else if (!isSpeler)
            {
                dealerDptmr.Start();
            }
            UpdateAantalKaarten();
        }

        /// <summary>
        /// Stop <c>speler.Dptmr</c>.
        /// Voegt een <c>Kaart</c> toe aan <c>LBDealerKaarten</c>. Voegt daarna <c>Dealer.KaartScore</c> toe aan <c>Dealer.DealerPunten</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeefDealerKaart(object sender, EventArgs e)
        {
            dealerDptmr.Stop();
            LBDealerKaarten.Items.Add(KaartDeck.GeefKaart(out Dealer.KaartScore));  // 
            Dealer.DealerPunten += Dealer.KaartScore;                               // punten aan de score toevoegen

            LblDealerScore.Text = Convert.ToString(Dealer.DealerPunten);
            UpdateAantalKaarten();
        }
        
        /// <summary>
        /// Voegt een <c>Kaart</c> toe aan <c>LBDealerKaarten</c>. Voegt daarna <c>Dealer.KaartScore</c> toe aan <c>Dealer.DealerPunten</c>.
        /// <para>Overload functie zonder argumenten</para>
        /// </summary>
        private void GeefDealerKaart()                                              
        {
            LBDealerKaarten.Items.Add(KaartDeck.GeefKaart(out Dealer.KaartScore));
            Dealer.DealerPunten += Dealer.KaartScore;

            LblDealerScore.Text = Convert.ToString(Dealer.DealerPunten);
            UpdateAantalKaarten();
        }


        /// <summary>
        /// Stop <c>speler.Dptmr</c>.
        /// <para>Voegt een <c>Kaart</c> toe aan <c>LBSpelerKaarten</c>. Voegt daarna <c>Speler.KaartScore</c> toe aan <c>Speler.SpelerPunten</c>.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeefSpelerKaart(object sender, EventArgs e)
        {
            spelerDptmr.Stop();
            LBSpelerKaarten.Items.Add(KaartDeck.GeefKaart(out Speler.KaartScore));  // kaart geven aan dealer in Listbox
            Speler.SpelerPunten += Speler.KaartScore;                               // punten aan de score toevoegen

            LblSpelerScore.Text = Convert.ToString(Speler.SpelerPunten);
            UpdateAantalKaarten();
        }
        /// <summary>
        /// Voegt een <c>Kaart</c> toe aan <c>LBSpelerKaarten</c>. Voegt daarna <c>Speler.KaartScore</c> toe aan <c>Speler.SpelerPunten</c>.
        /// <para>Overload functie zonder argumenten</para>
        /// </summary>
        private void GeefSpelerKaart()                                              // Overload voor de eerste kaart zonder timer
        {
            LBSpelerKaarten.Items.Add(KaartDeck.GeefKaart(out Speler.KaartScore));  // kaart geven aan dealer in Listbox
            Speler.SpelerPunten += Speler.KaartScore;                               // punten aan de score toevoegen

            LblSpelerScore.Text = Convert.ToString(Speler.SpelerPunten);
            UpdateAantalKaarten();
        }


        /// <summary>
        /// Verandert <c>LblaantalKaarten</c> met <c>KaartDeck.deck.Count</c>
        /// </summary>
        public void UpdateAantalKaarten()
        {
            LblAantalKaarten.Text = Convert.ToString(KaartDeck.deck.Count);
        }
        #endregion

        #region Btn Event Handlers
        /// <summary>
        /// Kijkt of <c>Speler.Inzet</c> groter is dan 10% van <c>Speler.Budget</c>.
        /// <para>
        /// Start daarna een nieuwe ronde, maakt velden leeg en veranderd knoppen.
        /// Trekt daarna <c>Speler.Inzet</c> van <c>Speler.Budget</c> af.
        /// Deelt dan 1 gewone <c>Kaart</c> aan de Speler en Dealer en 1 vertraagde <c>Kaart</c> aan de Speler.
        /// </para>
        /// <para>Als <c>Speler.Inzet</c> kleiner is dan 10% van <c>Speler.Budget</c>, laat een <c>MessageBox</c> zien.</para>
        /// <para>Als <c>Speler.Budget</c> groter is dan het dubbele van <c>Speler.Inzet</c>, wordt <c>BtnDouble</c> aangezet.</para>
        /// </summary>
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            if (Speler.Inzet > Speler.Budget * 0.1)
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";

                NewRound();

                UpdateBudget();

                GeefDealerKaart();
                GeefSpelerKaart();
                VertraagdeKaartDeler(isSpeler); 
            }
            else
            {
                LblResultaat.FontSize = 15;
                LblResultaat.Text = "Zet minstens 10% in!";
                BtnDeel.IsEnabled = false;
            }
            if (Speler.Inzet < Speler.Budget / 2)
            {
                BtnDouble.IsEnabled = true;
            }
        }
        /// <summary>
        /// Geeft 1 <c>Kaart</c> aan <c>LbSpelerKaarten</c>.
        /// Checkt daarna of <c>Speler.SpelerPunten</c> groter of gelijk is aan 21 en roept dan de <c>Lose</c> of <c>Win</c> functie op. Schakelt ook <c>BtnDouble</c> uit.
        /// </summary>
        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            // zelfde als verdelen, maar dan maar 1 kaart 
            GeefSpelerKaart();

            if (Speler.SpelerPunten > 21)
            {
                Lose();
            }
            else if (Speler.SpelerPunten == 21)
            {
                Win();
                LblResultaat.Text = "Blackjack!!";
            }

            BtnDouble.IsEnabled = false;
        }
        /// <summary>
        /// Voegt <c>Kaart</c> toe aan <c>LbDealerKaarten</c> toe tot <c>Dealer.DealerPunten</c> groter is dan 17.
        /// Checkt daarna de <c>Win</c> en <c>Lose</c> condities.
        /// </summary>
        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            while (Dealer.DealerPunten < 17)
            {
                GeefDealerKaart();
            }

            if (Dealer.DealerPunten > 21)
            {
                Win();
            }
            else if (Dealer.DealerPunten == Speler.SpelerPunten)
            {
                Push();
            }
            else if (Dealer.DealerPunten > Speler.SpelerPunten)
            {
                Lose();
            }
            else if (Dealer.DealerPunten < Speler.SpelerPunten)
            {
                Win();
            }

            BtnDouble.IsEnabled = false;
        }

        /// <summary>
        /// Deelt kaarten uit aan dealer tot dat hij minimaal 17 punten heeft.
        /// <para>Stand functie zonder argumenten.</para> 
        /// </summary>
        private void BtnStand_Click()
        {
            while (Dealer.DealerPunten < 17)
            {
                GeefDealerKaart();
            }

            if (Dealer.DealerPunten > 21)
            {
                Win();
            }
            else if (Dealer.DealerPunten == Speler.SpelerPunten)
            {
                Push();
            }
            else if (Dealer.DealerPunten > Speler.SpelerPunten)
            {
                Lose();
            }
            else if (Dealer.DealerPunten < Speler.SpelerPunten)
            {
                Win();
            }

            BtnDouble.IsEnabled = false;
        }
        private void BtnInzetPlus1_Click(object sender, RoutedEventArgs e)
        {

            UpdateInzet(1);
        }
        private void BtnInzetPlus5_Click(object sender, RoutedEventArgs e)
        {
            UpdateInzet(5);
        }
        private void BtnInzetPlus10_Click(object sender, RoutedEventArgs e)
        {
            UpdateInzet(10);
        }
        private void BtnInzetPlus25_Click(object sender, RoutedEventArgs e)
        {
            UpdateInzet(25);
        }
        private void BtnResetInzet_Click(object sender, RoutedEventArgs e)
        {
            Speler.Inzet = 0;
            UpdateInzet(0);
        }
        /// <summary>
        /// Start een nieuw spel, geeft <c>Speler</c> 100 pingels en reset de knoppen.
        /// </summary>
        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            UpdateAantalKaarten();
            Newgame();
        }

        /// <summary>
        /// <c>Speler.Inzet</c> verdubbelen en van <c>Speler.Budget</c> aftrekken. 
        /// <para>
        /// Voegt een <c>Kaart</c> toe aan <c>LBSpelerKaarten</c>.
        /// Kijkt na of de Speler BlackJack heeft of verloren is.
        /// Deelt daarna Kaarten uit aan Dealer met <c>BtnStand_Click</c> functie.
        /// </para>
        /// </summary>
        private void BtnDouble_Click(object sender, RoutedEventArgs e)
        {
            UpdateBudget();
            Speler.Inzet *= 2;

            GeefSpelerKaart();

            if (Speler.SpelerPunten > 21)
            {
                Lose();
            }
            else if (Speler.SpelerPunten == 21)
            {
                Win();
                LblResultaat.Text = "Blackjack!!";
            }
            BtnStand_Click();
        }
    }
    #endregion
}
