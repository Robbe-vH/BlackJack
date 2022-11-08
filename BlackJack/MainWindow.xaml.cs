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

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int spelerPunten;
        int dealerPunten;
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

            // Kaarten delen
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
