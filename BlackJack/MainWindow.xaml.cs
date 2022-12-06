using System;
using System.Windows;
using System.Windows.Threading;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TODO
        //Schuppen Zeven, Ruiten Dame, Harten Dame kaart
        readonly bool isSpeler = true;
        readonly bool isDealer = false;
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

        // private algemene functies voor neiuwe rondes/spellen en om de velden leeg te maken
        private void MaakVeldenLeeg()
        {
            LblResultaat.Text = "";
            LBSpelerKaarten.Items.Clear();
            LBDealerKaarten.Items.Clear();
        }

        // functie om een nieuw spel te starten
        // geeft de speler 100 flippo's
        // zet de speel knoppen op enabled
        // start dan een nieuwe ronde
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


        // functie om een nieuwe ronde te starten
        // maakt alle velden & scores leeg en deactiveert Hit & Stand knoppen
        // Deelt daarna kaarten uit
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
            BtnDouble.IsEnabled = true;
            BtnInzetPlus1.IsEnabled = false;
            BtnInzetPlus5.IsEnabled = false;
            BtnInzetPlus10.IsEnabled = false;
            BtnInzetPlus25.IsEnabled = false;
            BtnResetInzet.IsEnabled = false;
        }


        // Win lose conditie functies
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


        // Inzetten en kapitaal functiets
        private void UpdateInzet(int i)
        {
            if (Speler.Inzet < Speler.Budget)     // inzetten kan tot maximaal het budget
            {
                Speler.Inzet += i;
                LblInzet.Text = Convert.ToString(Speler.Inzet);
            }
            else if (Speler.Inzet > Speler.Budget)
            {
                MessageBox.Show("U kan niet meer inzetten dan u heeft!", "Inzet Fout", MessageBoxButton.OK);
            }

            if (Speler.Inzet > Speler.Budget * 0.1) // kijken of de speler minstens 10% budget inzet
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";
                BtnDeel.IsEnabled = true;
            }

        }
        private void UpdateBudget()
        {
            Speler.Budget -= Speler.Inzet;
            LblBudget.Text = Convert.ToString(Speler.Budget);
        }
        private void Blut()
        {
            // als het geld op is, mesagebox showen
            if (Speler.Budget == 0)
            {
                MessageBox.Show("U bent blut!", "Einde spel", MessageBoxButton.OK);
                // alle knoppen uit als de speler blut is en velden leegmaken
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnInzetPlus1.IsEnabled = false;
                BtnInzetPlus5.IsEnabled = false;
                BtnInzetPlus10.IsEnabled = false;
                BtnInzetPlus25.IsEnabled = false;
                BtnResetInzet.IsEnabled = false;
                MaakVeldenLeeg();

                // enkel nieuw spel btn aanzetten
                BtnNieuwSpel.IsEnabled = true;
            }
        }

        // Kaart functies
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

        private void GeefDealerKaart(object sender, EventArgs e)
        {
            dealerDptmr.Stop();
            LBDealerKaarten.Items.Add(KaartDeck.GeefKaart(out Dealer.KaartScore));  // kaart geven aan dealer in Listbox
            Dealer.DealerPunten += Dealer.KaartScore;                               // punten aan de score toevoegen

            LblDealerScore.Text = Convert.ToString(Dealer.DealerPunten);
            UpdateAantalKaarten();
        }
        private void GeefDealerKaart()                                              // Overload voor eerste kaart
        {
            LBDealerKaarten.Items.Add(KaartDeck.GeefKaart(out Dealer.KaartScore));  // kaart geven aan dealer in Listbox
            Dealer.DealerPunten += Dealer.KaartScore;                               // punten aan de score toevoegen

            LblDealerScore.Text = Convert.ToString(Dealer.DealerPunten);
            UpdateAantalKaarten();
        }

        private void GeefSpelerKaart(object sender, EventArgs e)
        {
            spelerDptmr.Stop();
            LBSpelerKaarten.Items.Add(KaartDeck.GeefKaart(out Speler.KaartScore));  // kaart geven aan dealer in Listbox
            Speler.SpelerPunten += Speler.KaartScore;                               // punten aan de score toevoegen

            LblSpelerScore.Text = Convert.ToString(Speler.SpelerPunten);
            UpdateAantalKaarten();
        }
        private void GeefSpelerKaart()                                              // Overload voor de eerste kaart zonder timer
        {
            LBSpelerKaarten.Items.Add(KaartDeck.GeefKaart(out Speler.KaartScore));  // kaart geven aan dealer in Listbox
            Speler.SpelerPunten += Speler.KaartScore;                               // punten aan de score toevoegen

            LblSpelerScore.Text = Convert.ToString(Speler.SpelerPunten);
            UpdateAantalKaarten();
        }                                   

        public void UpdateAantalKaarten()
        {
            LblAantalKaarten.Text = Convert.ToString(KaartDeck.deck.Count);
        }


        // Btn Event Handlers
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            if (Speler.Inzet > Speler.Budget * 0.1)                                 // kijken of de speler minstens 10% budget inzet
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";

                // Nieuwe ronde starten
                // velden leeg en knoppen veranderen
                NewRound();

                // inzet van budget aftrekken
                UpdateBudget();


                // kaarten delen, dealer 1, speler 2 met 1 sec interval
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
        }
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
        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            // Knop voor een nieuw spel te starten
            // geeft speler 100 pingels
            // zet de knoppen terug uit en maakt de velden leeg
            UpdateAantalKaarten();
            Newgame();
        }

        private void BtnDouble_Click(object sender, RoutedEventArgs e)
        {
            // eerst kijken of er genoeg budget is

        }
    }
}
