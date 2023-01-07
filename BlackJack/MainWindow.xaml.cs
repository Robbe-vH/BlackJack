using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.Generic;

namespace BlackJack
{
    /// <summary>
    /// Hoofd programma logica
    /// Bevat de functies voor Nieuwe rondes en spellen,
    /// Win en Lose condities,
    /// Inzet en Kapitaal,
    /// Kaartdeling en de Event Handlers voor de buttons.
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO
        //Schuppen Zeven
        readonly bool isSpeler = true;
        private DispatcherTimer spelerDptmr = new DispatcherTimer();
        private DispatcherTimer dealerDptmr = new DispatcherTimer();
        private DispatcherTimer tijdDptmr = new DispatcherTimer();
        private int rondeTeller;


        public MainWindow()
        {
            InitializeComponent();
            MaakVeldenLeeg();
            BtnHit.Visibility = Visibility.Hidden;
            BtnStand.Visibility = Visibility.Hidden;
            BtnDeel.Visibility = Visibility.Hidden;
            BtnDouble.Visibility = Visibility.Hidden;
            BtnInzetPlus1.Visibility = Visibility.Hidden;
            BtnInzetPlus10.Visibility = Visibility.Hidden;   
            BtnInzetPlus5.Visibility = Visibility.Hidden;
            BtnInzetPlus25.Visibility = Visibility.Hidden;  
            BtnResetInzet.Visibility = Visibility.Hidden;
            LblBudget.Text = "";
            LblInzet.Text = "";
            //MnItHistoriek.Header = string.Empty;
            // timers 
            dealerDptmr.Interval = spelerDptmr.Interval = tijdDptmr.Interval = TimeSpan.FromSeconds(1);
            spelerDptmr.Tick += GeefSpelerKaart;
            dealerDptmr.Tick += GeefDealerKaart;
            tijdDptmr.Tick += TijdDptmr_Tick;
            tijdDptmr.Start();
        }

        private void TijdDptmr_Tick(object? sender, EventArgs e)
        {
            LblTijdstip.Header = DateTime.Now.ToLongTimeString();
        }

        #region Nieuwe rondes en spel
        /// <summary>
        /// Maakt <c>LBSpelerkaarten</c> en <c>LBDealerKaarten</c> leeg en reset <c>LblResultaat</c>.
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
            KaartDeck.VulDeck();
            Speler.Budget = 100;
            Speler.Inzet = 0;
            rondeTeller = 0;
            UpdateBudget();
            UpdateInzet(0);
            BtnDeel.Visibility = Visibility.Visible;
            BtnInzetPlus1.Visibility = Visibility.Visible;
            BtnInzetPlus10.Visibility = Visibility.Visible;
            BtnInzetPlus5.Visibility = Visibility.Visible;
            BtnInzetPlus25.Visibility = Visibility.Visible;
            BtnResetInzet.Visibility = Visibility.Visible;
            BtnNieuwSpel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Functie om een nieuwe ronde te starten. <para>Maakt alle velden en scores leeg en deactiveert hit/stand knoppen. Deelt daarna kaarten uit.</para>
        /// </summary>
        private void NewRound()
        {
            rondeTeller++;
            MaakVeldenLeeg();
            Speler.SpelerPunten = 0;
            Dealer.DealerPunten = 0;
            Speler.aantalAzen = 0;
            Dealer.aantalAzen = 0;
            LblDealerScore.Text = Convert.ToString(Speler.SpelerPunten);
            LblSpelerScore.Text = Convert.ToString(Dealer.DealerPunten);

            BtnDeel.Visibility = Visibility.Hidden;
            BtnHit.Visibility = Visibility.Visible;
            BtnStand.Visibility = Visibility.Visible;
            BtnDouble.Visibility = Visibility.Hidden;
            BtnInzetPlus1.Visibility = Visibility.Hidden;
            BtnInzetPlus5.Visibility = Visibility.Hidden;
            BtnInzetPlus10.Visibility = Visibility.Hidden;
            BtnInzetPlus25.Visibility = Visibility.Hidden;
            BtnResetInzet.Visibility = Visibility.Hidden;
        }

        #endregion


        #region Win lose conditie functies

        /// <summary>
        /// Tesktlabels worden veranderd, <c>Speler.Budget</c> wordt verhoogd met dubbel <c>Speler.Inzet</c>, speelknoppen worden uitgezet en inzetknoppen aangezet.
        /// </summary>
        private void Win()
        {
            UpdateHistoriek('+');

            LblResultaat.Text = "Gewonnen!";
            Speler.Budget += Speler.Inzet * 2;
            LblBudget.Text = Convert.ToString(Speler.Budget);
            Speler.Inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.Inzet);
            BtnHit.Visibility = Visibility.Hidden;
            BtnStand.Visibility = Visibility.Hidden;
            BtnDeel.Visibility = Visibility.Visible;
            BtnInzetPlus1.Visibility = Visibility.Visible;
            BtnInzetPlus5.Visibility = Visibility.Visible;
            BtnInzetPlus10.Visibility = Visibility.Visible;
            BtnInzetPlus25.Visibility = Visibility.Visible;
            BtnResetInzet.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Tesktlabels worden veranderd, <c>Speler.Budget</c> wordt verminderd met <c>Speler.Inzet</c>, speelknoppen worden uitgezet en inzetknoppen aangezet.
        /// </summary>
        private void Lose()
        {
            UpdateHistoriek('-');

            LblResultaat.Text = "Verloren!";
            LblBudget.Text = Convert.ToString(Speler.Budget);
            Speler.Inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.Inzet);
            BtnHit.Visibility = Visibility.Hidden;
            BtnStand.Visibility = Visibility.Hidden;
            BtnDeel.Visibility = Visibility.Visible;
            BtnInzetPlus1.Visibility = Visibility.Visible;
            BtnInzetPlus5.Visibility = Visibility.Visible;
            BtnInzetPlus10.Visibility = Visibility.Visible;
            BtnInzetPlus25.Visibility = Visibility.Visible;
            BtnResetInzet.Visibility = Visibility.Visible;

            Blut();
        }

        /// <summary>
        /// Tesktlabels worden veranderd, <c>Speler.Budget</c> wordt terug verhoogd <c>Speler.Inzet</c>, speelknoppen worden uitgezet en inzetknoppen aangezet.
        /// </summary>
        private void Push()
        {
            UpdateHistoriek(' ');

            LblResultaat.Text = "Push!";
            Speler.Budget += Speler.Inzet;
            LblBudget.Text = Convert.ToString(Speler.Budget);
            Speler.Inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.Inzet);
            BtnHit.Visibility = Visibility.Hidden;
            BtnStand.Visibility = Visibility.Hidden;
            BtnDeel.Visibility = Visibility.Visible;
            BtnInzetPlus1.Visibility = Visibility.Visible;
            BtnInzetPlus5.Visibility = Visibility.Visible; 
            BtnInzetPlus10.Visibility = Visibility.Visible;
            BtnInzetPlus25.Visibility = Visibility.Visible;    
            BtnResetInzet.Visibility = Visibility.Visible;
        }

        private void UpdateHistoriek(char c)
        {
            MnItHistoriek.Header = $"{c}{Convert.ToString(Speler.Inzet)} - {Convert.ToString(Speler.SpelerPunten)}/{Convert.ToString(Dealer.DealerPunten)} ";
            // toevoegen aan historieklijst
            string laatstGespeeldeHand = $"Ronde: { rondeTeller.ToString() } Bedrag: {c}{Speler.Inzet} euro  Speler Punten: {Convert.ToString(Speler.SpelerPunten)}  Dealer Punten: {Convert.ToString(Dealer.DealerPunten)}";
            Array.Copy(Speler.historiekArray, 0, Speler.historiekArray, 1, Speler.historiekArray.Length - 1);
            Speler.historiekArray[0] = laatstGespeeldeHand;

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
                BtnDeel.Visibility = Visibility.Visible;
                Speler.Inzet += inzet;
                LblInzet.Text = Convert.ToString(Speler.Inzet);
            }
            else if (inzet > Speler.Budget)
            {
                MessageBox.Show("U kan niet meer inzetten dan u heeft!", "Inzet Fout", MessageBoxButton.OK);
            }

            if (inzet >= Speler.Budget * 0.1)
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";
                BtnDeel.Visibility = Visibility.Visible;
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

                BtnDeel.Visibility = Visibility.Hidden;
                BtnHit.Visibility = Visibility.Hidden;
                BtnStand.Visibility = Visibility.Hidden;
                BtnInzetPlus1.Visibility = Visibility.Hidden;
                BtnInzetPlus5.Visibility = Visibility.Hidden;
                BtnInzetPlus10.Visibility = Visibility.Hidden;
                BtnInzetPlus25.Visibility = Visibility.Hidden;
                BtnResetInzet.Visibility = Visibility.Hidden;
                MaakVeldenLeeg();

                BtnNieuwSpel.Visibility = Visibility.Visible;
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
        /// Voegt een <c>Kaart</c> toe aan <c>LBDealerKaarten</c>. Voegt daarna <c>kaartScore</c> toe aan <c>Dealer.DealerPunten</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeefDealerKaart(object sender, EventArgs e)
        {
            dealerDptmr.Stop();
            GeefDealerKaart();
        }

        /// <summary>
        /// Voegt een <c>Kaart</c> toe aan <c>LBDealerKaarten</c>. Voegt daarna <c>kaartScore</c> toe aan <c>Dealer.DealerPunten</c>.
        /// <para>Overload functie zonder argumenten</para>
        /// </summary>
        private void GeefDealerKaart()
        {
            LBDealerKaarten.Items.Add(KaartDeck.GeefKaart(out int kaartScore));
            if (kaartScore == 11) Dealer.aantalAzen++;
            if (Dealer.DealerPunten > 10) Dealer.DealerPunten += kaartScore - Dealer.aantalAzen * 10;
            else Dealer.DealerPunten += kaartScore;



            LblDealerScore.Text = Convert.ToString(Dealer.DealerPunten);
            UpdateAantalKaarten();
        }


        /// <summary>
        /// Stop <c>speler.Dptmr</c>.
        /// <para>Voegt een <c>Kaart</c> toe aan <c>LBSpelerKaarten</c>. Voegt daarna <c>kaartScore</c> toe aan <c>Speler.SpelerPunten</c>.</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeefSpelerKaart(object sender, EventArgs e)
        {
            spelerDptmr.Stop();
            GeefSpelerKaart();
        }

        /// <summary>
        /// Voegt een <c>Kaart</c> toe aan <c>LBSpelerKaarten</c>. Voegt daarna <c>kaartScore</c> toe aan <c>Speler.SpelerPunten</c>.
        /// <para>Overload functie zonder argumenten</para>
        /// </summary>
        private void GeefSpelerKaart()
        {
            //Kaart kaartObject = KaartDeck.GeefKaart(out int kaartScore);
            //ListBoxItem listboxItemKaart = new ListBoxItem();
            //listboxItemKaart.Content = kaartObject.Foto;
            LBSpelerKaarten.Items.Add(KaartDeck.GeefKaart(out int kaartScore));
            if (kaartScore == 11) Speler.aantalAzen++;
            if (Speler.SpelerPunten > 10) Speler.SpelerPunten += kaartScore - Speler.aantalAzen * 10;
            else Speler.SpelerPunten += kaartScore;


            LblSpelerScore.Text = Convert.ToString(Speler.SpelerPunten);
            if (Speler.SpelerPunten > 21)
            {
                Lose();
            }
            else if (Speler.SpelerPunten == 21)
            {
                Win();
                LblResultaat.Text = "Blackjack!!";
            }
            else
            {
                BtnStand.Visibility = Visibility.Visible;
                BtnHit.Visibility = Visibility.Visible;
                if (Speler.Inzet < Speler.Budget / 2)
                {
                    BtnDouble.Visibility = Visibility.Visible;
                }
            }
            UpdateAantalKaarten();

        }

        ///// <summary>
        ///// Dit zou een omgedraaide kaart moeten toegvoegen aan de Speler Listbox
        ///// </summary>
        //private void GeefSpelerHorizontaleKaart()
        //{
        //    // Dus, eerst een kaart geven
        //    Kaart gewoneKaart = KaartDeck.GeefKaart(out int kaartScore);

        //    // de foto van de gegeven kaart omdraaien
        //    BitmapImage gedraaideKaartFoto = new BitmapImage();
        //    gedraaideKaartFoto.BeginInit();
        //    gedraaideKaartFoto.UriSource = new Uri(gewoneKaart.ImgSource);
        //    gedraaideKaartFoto.Rotation = Rotation.Rotate90;
        //    gedraaideKaartFoto.EndInit();



        //    Kaart gedraaideKaart = gewoneKaart;
        //    // de foto terug in de listbox steken
        //    LBSpelerKaarten.Items.Add(gedraaideKaart);
        //}

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
            if (Speler.Inzet >= Speler.Budget * 0.1)
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";

                NewRound();

                UpdateBudget();

                GeefDealerKaart();
                GeefSpelerKaart();
                BtnHit.Visibility = Visibility.Hidden;
                BtnStand.Visibility = Visibility.Hidden;
                BtnDouble.Visibility = Visibility.Hidden;
                VertraagdeKaartDeler(isSpeler);
            }
            else
            {
                LblResultaat.FontSize = 15;
                LblResultaat.Text = "Zet minstens 10% in!";
                BtnDeel.Visibility = Visibility.Hidden;
                BtnDouble.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Geeft 1 <c>Kaart</c> aan <c>LbSpelerKaarten</c>.
        /// Checkt daarna of <c>Speler.SpelerPunten</c> groter of gelijk is aan 21 en roept dan de <c>Lose</c> of <c>Win</c> functie op. Schakelt ook <c>BtnDouble</c> uit.
        /// </summary>
        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            GeefSpelerKaart();

            BtnDouble.Visibility = Visibility.Hidden;
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

            BtnDouble.Visibility = Visibility.Hidden;
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

            BtnDouble.Visibility = Visibility.Hidden;
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
            Newgame();
            UpdateAantalKaarten();
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

            //GeefSpelerHorizontaleKaart();

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

        /// <summary>
        /// Laat het Historiek.xaml venster zien
        /// </summary>
        private void MnItHistoriek_Click(object sender, RoutedEventArgs e)
        {
            HistoriekWindow historiekWindow = new HistoriekWindow();
            // window op het scherm laten zien
            historiekWindow.Show();
        }
}
    #endregion
}
