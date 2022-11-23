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
        int spelerPunten;
        int dealerPunten;
        bool isSpeler = true;
        int budget;
        int inzet;

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

        private void MaakVeldenLeeg()
        {
            LblResultaat.Text = "";
            TxtDealerKaarten.Clear();
            TxtSpelerKaarten.Clear();
        }

        // functie om een nieuw spel te starten
        // geeft de speler 100 flippo's
        // zet de speel knoppen op enabled
        // start dan een nieuwe ronde
        private void Newgame()
        {
            budget = 100;
            inzet = 0;
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
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            spelerPunten = 0;
            dealerPunten = 0;
            LblDealerScore.Text = Convert.ToString(dealerPunten);
            LblSpelerScore.Text = Convert.ToString(spelerPunten);

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

        // functie voor een nieuwe kaart te genereren 
        private string GeefKaart(bool isSpeler, int punten, out int kaartWaarde)
        {

            kaartWaarde = 0;
            string kaart = "";
            string kaartgetal = "";
            Random rnd = new Random();
            int teken = rnd.Next(1, 5);     // aantal kleuren
            int getal = rnd.Next(1, 14);    // aantal kaarten per kleur

            // switch cases voor de kaarten (moet een dicionary worden of class)
            // Dictionary<int, string> kaartenDeck = new Dictionary<int, string>() { 1, "Harten Aas"};
            switch (teken)
            {
                case 1:
                    kaart = "Klaveren ";
                    break;
                case 2:
                    kaart = "Ruiten ";
                    break;
                case 3:
                    kaart = "Harten ";
                    break;
                case 4:
                    kaart = "Schuppen ";
                    break;
                default:
                    break;
            }

            switch (getal)
            {
                case 1:
                    kaartgetal = "Aas";
                    // Bij een aas kijken of het 10 punten moeten zijn of 1 punt
                    if (punten <= 10)
                    {
                        kaartWaarde = 11;
                    }
                    else
                    {
                        kaartWaarde = 1;
                    }

                    break;
                case 2:
                    kaartgetal = "2";
                    kaartWaarde = 2;
                    break;
                case 3:
                    kaartgetal = "3";
                    kaartWaarde = 3;
                    break;
                case 4:
                    kaartgetal = "4";
                    kaartWaarde = 4;
                    break;
                case 5:
                    kaartgetal = "5";
                    kaartWaarde = 5;
                    break;
                case 6:
                    kaartgetal = "6";
                    kaartWaarde = 6;
                    break;
                case 7:
                    kaartgetal = "7";
                    kaartWaarde = 7;
                    break;
                case 8:
                    kaartgetal = "8";
                    kaartWaarde = 8;
                    break;
                case 9:
                    kaartgetal = "9";
                    kaartWaarde = 9;
                    break;
                case 10:
                    kaartgetal = "10";
                    kaartWaarde = 10;
                    break;
                case 11:
                    kaartgetal = "Boer";
                    kaartWaarde = 10;
                    break;
                case 12:
                    kaartgetal = "Dame";
                    kaartWaarde = 10;
                    break;
                case 13:
                    kaartgetal = "Koning";
                    kaartWaarde = 10;
                    break;
                default:
                    break;
            }

            return kaart + kaartgetal;
        }


        // win/lose/push functies
        private void Win()
        {
            LblResultaat.Text = "Gewonnen!";
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
        }

        private void Lose()
        {
            LblResultaat.Text = "Verloren!";
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
        }
        private void Push()
        {
            LblResultaat.Text = "Push!";
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            BtnDeel.IsEnabled = true;
        }


        // Inzetten en kapitaal functiets
        private void UpdateInzet(int i)
        {
            if (inzet < budget)     // inzetten kan tot maximaal het budget
            {
                inzet += i;
                LblInzet.Text = Convert.ToString(inzet);

                // checken of de speler minimaal 10% van zijn/haar budget heeft ingezet
                if (inzet < budget * 0.1)
                {
                    LblResultaat.FontSize = 15;
                    LblResultaat.Text = "Zet minstens 10% in!";
                    BtnDeel.IsEnabled = false;
                }
                else
                {
                    LblResultaat.FontSize = 25;
                    LblResultaat.Text = "";
                    BtnDeel.IsEnabled = true;
                }
            }
            else if (inzet < budget)
            {
                MessageBox.Show("U kan niet meer inzetten dan u heeft!", "Inzet Fout", MessageBoxButton.OK);
            }

        }

        private void UpdateBudget()
        {
            budget -= inzet;
            LblBudget.Text = Convert.ToString(budget);
        }


        // Button Event Handlers
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            // Nieuwe ronde starten
            // velden leeg en knoppen veranderen
            NewRound();

            // inzet van budget aftrekken
            UpdateBudget();


            // kaarten delen, dealer 1, speler 2
            int dealerKaartscore;
            string dealerKaart = GeefKaart(true, dealerPunten, out dealerKaartscore); // is niet de speler speler, maar de bool staat op true om een kaart te geven
            dealerPunten += dealerKaartscore;

            TxtDealerKaarten.Text = dealerKaart; // Dealer waardes afdrukken
            LblDealerScore.Text = Convert.ToString(dealerPunten);

            int spelerKaartScore;
            int spelerKaart2Score;
            string spelerKaart = GeefKaart(true, spelerPunten, out spelerKaartScore);
            string spelerKaart2 = GeefKaart(true, spelerPunten, out spelerKaart2Score);
            spelerPunten += spelerKaartScore + spelerKaart2Score;

            TxtSpelerKaarten.Text = $"{spelerKaart}\n{spelerKaart2}"; // Speler waardes afdrukken
            LblSpelerScore.Text = Convert.ToString(spelerPunten);

            // als het geld op is, mesagebox showen
            if (budget < 0)
            {
                MessageBox.Show("U bent blut!", "Einde spel", MessageBoxButton.OK);
                // alle knoppen uit als de speler blut is
                BtnDeel.IsEnabled = false;
                BtnHit.IsEnabled = false;
                BtnStand.IsEnabled = false;
                BtnInzetPlus1.IsEnabled = false;
                BtnInzetPlus5.IsEnabled = false;
                BtnInzetPlus10.IsEnabled = false;
                BtnInzetPlus25.IsEnabled = false;
                BtnResetInzet.IsEnabled = false;

                // enkel nieuw spel btn aanzetten
                BtnNieuwSpel.IsEnabled = true;
            }

        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            // zelfde als verdelen, maar dan maar 1 kaart 
            int spelerKaartScore;
            string spelerKaart = GeefKaart(isSpeler, spelerPunten, out spelerKaartScore);
            spelerPunten += spelerKaartScore;
            TxtSpelerKaarten.Text += $"\n{spelerKaart}";
            LblSpelerScore.Text = Convert.ToString(spelerPunten);

            if (spelerPunten > 21)
            {
                Lose();
            }
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            while (dealerPunten < 17)
            {
                int dealerKaartscore;
                string dealerKaart = GeefKaart(true, dealerPunten, out dealerKaartscore);
                dealerPunten += dealerKaartscore;

                TxtDealerKaarten.Text += $"\n{dealerKaart}"; // Dealer waardes afdrukken
                LblDealerScore.Text = Convert.ToString(dealerPunten);

            }

            if (dealerPunten > 21)
            {
                Win();
            }
            else if (dealerPunten == spelerPunten)
            {
                Push();
            }
            else if (dealerPunten > spelerPunten)
            {
                Lose();
            }
            else if (dealerPunten < spelerPunten)
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
            inzet = 0;
            UpdateInzet(0);
        }


        // Knop voor een nieuw spel te starten
        // geeft speler 100 pingels
        // zet de knoppen terug uit en maakt de velden leeg
        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            Newgame();
        }

    }
}
