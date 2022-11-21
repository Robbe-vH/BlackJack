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
        int inzet = 0;

        public MainWindow()
        {
            InitializeComponent();
        }


        // functie om een nieuw spel te starten
        // maakt alle velden & scores leeg en deactiveert Hit & Stand knoppen
        // Deelt daarna kaarten uit
        private void NewGame()
        {
            // leegmaken
            BtnHit.IsEnabled = false;
            BtnStand.IsEnabled = false;
            TxtDealerKaarten.Clear();
            TxtSpelerKaarten.Clear();
            spelerPunten = 0;
            dealerPunten = 0;
            LblDealerScore.Text = Convert.ToString(dealerPunten);
            LblSpelerScore.Text = Convert.ToString(spelerPunten);
            LblResultaat.Text = "";

            // speler terug 100 euro geven
            budget = 100;
            UpdateBudget();

        }

        // functie voor een nieuwe kaart te genereren 
        private string GeefKaart(bool isSpeler, int punten, out int kaartWaarde)
        {
            
            kaartWaarde = 0;
            string kaart = "";
            string kaartgetal = "";
            Random rnd = new Random();
            int teken = rnd.Next(1, 5);
            int getal = rnd.Next(1, 3);

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

        private void UpdateBudget()
        {
            LblBudget.Text = Convert.ToString(budget);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            // alles leegmaken
            NewGame();

            // kaarten delen, dealer 1, speler 2
            int dealerKaartscore;
            string dealerKaart = GeefKaart(true, dealerPunten, out dealerKaartscore); // is geen speler, maar de bool staat op true om een kaart te geven
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

            // Knoppen veranderen
            BtnDeel.IsEnabled = false;
            BtnHit.IsEnabled = true;
            BtnStand.IsEnabled = true;

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

                TxtDealerKaarten.Text += $"\n{ dealerKaart}"; // Dealer waardes afdrukken
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

        }

        private void BtnInzetPlus5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInzetPlus10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInzetPlus25_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
