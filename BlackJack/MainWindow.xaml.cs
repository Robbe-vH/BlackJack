using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;


namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //bool isSpeler;

        public MainWindow()
        {
            InitializeComponent();
            MaakVeldenLeeg();
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = false;
            BtnInzetPlus1.IsEnabled = false;
            BtnInzetPlus10.IsEnabled = false;
            BtnInzetPlus5.IsEnabled = false;
            BtnInzetPlus25.IsEnabled = false;
            BtnResetInzet.IsEnabled = false;
            LblBudget.Text = "";
            LblInzet.Text = "";
        }

        // private algemene functies voor neiuwe rondes/spellen en om de velden leeg te maken
        private void MaakVeldenLeeg()
        {
            LblResultaat.Text = "";
            //TxtDealerKaarten.Clear();
            //TxtSpelerKaarten.Clear();
        }

        // functie om een nieuw spel te starten
        // geeft de speler 100 flippo's
        // zet de speel knoppen op enabled
        // start dan een nieuwe ronde
        private void Newgame()
        {
            Speler.budget = 100;
            Speler.inzet = 0;
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
            Speler.spelerPunten = 0;
            Dealer.dealerPunten = 0;
            LblDealerScore.Text = Convert.ToString(Speler.spelerPunten);
            LblSpelerScore.Text = Convert.ToString(Dealer.dealerPunten);

            // Knoppen veranderen
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;
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
            Speler.budget += Speler.inzet * 2;
            LblBudget.Text = Convert.ToString(Speler.budget);
            Speler.inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.inzet);
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
            Speler.budget += Speler.inzet;
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
            BtnInzetPlus1.IsEnabled = true;
            BtnInzetPlus5.IsEnabled = true;
            BtnInzetPlus10.IsEnabled = true;
            BtnInzetPlus25.IsEnabled = true;
            BtnResetInzet.IsEnabled = true;
            Speler.spelerPunten = 0;
            Dealer.dealerPunten = 0;
            Speler.inzet = 0;
            LblInzet.Text = Convert.ToString(Speler.inzet);
            LblSpelerScore.Text = "0";
            LblDealerScore.Text = "0";
            LblBudget.Text = Convert.ToString(Speler.budget);
            
            Blut();
        }
        private void Push()
        {
            LblResultaat.Text = "Push!";
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
            Speler.budget += Speler.inzet;
            LblBudget.Text = Convert.ToString(Speler.budget);
        }


        // Inzetten en kapitaal functiets
        private void UpdateInzet(int i)
        {
            if (Speler.inzet < Speler.budget)     // inzetten kan tot maximaal het budget
            {
                Speler.inzet += i;
                LblInzet.Text = Convert.ToString(Speler.inzet);
            }
            else if (Speler.inzet > Speler.budget)
            {
                MessageBox.Show("U kan niet meer inzetten dan u heeft!", "Inzet Fout", MessageBoxButton.OK);
            }

            if (Speler.inzet > Speler.budget * 0.1) // kijken of de speler minstens 10% budget inzet
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";
                BtnDeel.IsEnabled = true;
            }

        }
        private void UpdateBudget()
        {
            Speler.budget -= Speler.inzet;
            LblBudget.Text = Convert.ToString(Speler.budget);
        }
        private void Blut()
        {
            // als het geld op is, mesagebox showen
            if (Speler.budget == 0)
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


        // Button Event Handlers
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            if (Speler.inzet > Speler.budget * 0.1) // kijken of de speler minstens 10% budget inzet
            {
                LblResultaat.FontSize = 25;
                LblResultaat.Text = "";
                BtnDeel.IsEnabled = true;

                // Nieuwe ronde starten
                // velden leeg en knoppen veranderen
                NewRound();

                // inzet van budget aftrekken
                UpdateBudget();


                // kaarten delen, dealer 1, speler 
                Dealer.huidigeKaart = KaartDeck.GeefKaart(out Dealer.KaartScore);
                Dealer.dealerPunten += Dealer.KaartScore;

                //TxtDealerKaarten.Text = Dealer.huidigeKaart; // Dealer waardes afdrukken
                LblDealerScore.Text = Convert.ToString(Dealer.dealerPunten);

                Speler.huidigeKaart = KaartDeck.GeefKaart(out Speler.KaartScore);
                Speler.spelerPunten += Speler.KaartScore;

                //TxtSpelerKaarten.Text = $"{Speler.huidigeKaart}"; // Speler waardes afdrukken
                LblSpelerScore.Text = Convert.ToString(Speler.spelerPunten);
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
            Speler.huidigeKaart = Convert.ToString(KaartDeck.GeefKaart(out Speler.KaartScore));
            Speler.spelerPunten += Speler.KaartScore;
            //TxtSpelerKaarten.Text += $"\n{Speler.huidigeKaart}";
            LblSpelerScore.Text = Convert.ToString(Speler.spelerPunten);

            if (Speler.spelerPunten > 21)
            {
                Lose();
            }
            else if (Speler.spelerPunten == 21)
            {
                Win();
                LblResultaat.Text = "Blackjack!!";
            }
        }
        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            while (Dealer.dealerPunten < 17)
            {
                Dealer.huidigeKaart = KaartDeck.GeefKaart(out Dealer.KaartScore);
                Dealer.dealerPunten += Dealer.KaartScore;

                //TxtDealerKaarten.Text += $"\n{Dealer.huidigeKaart}"; // Dealer waardes afdrukken
                LblDealerScore.Text = Convert.ToString(Dealer.dealerPunten);

            }

            if (Dealer.dealerPunten > 21)
            {
                Win();
            }
            else if (Dealer.dealerPunten == Speler.spelerPunten)
            {
                Push();
            }
            else if (Dealer.dealerPunten > Speler.spelerPunten)
            {
                Lose();
            }
            else if (Dealer.dealerPunten < Speler.spelerPunten)
            {
                Win();
            }
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
            Speler.inzet = 0;
            UpdateInzet(0);
        }
        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            // Knop voor een nieuw spel te starten
            // geeft speler 100 pingels
            // zet de knoppen terug uit en maakt de velden leeg
            Newgame();
        }
    }
}
