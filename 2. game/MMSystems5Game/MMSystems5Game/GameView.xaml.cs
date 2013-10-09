using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMSystems5Game
{
    public partial class GameView : PhoneApplicationPage 
    {
        Bord Speelbord;
        Player Speler;
        public GameView()
        {
            InitializeComponent();
            
            Speelbord = new Bord();
            Speler = new Player();
            this.DataContext = Speler;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
        }

       
        public void Gooi(object sender, RoutedEventArgs e)
        {
            
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            Dobbelsteen1.GeefWaardeDobbelsteen();
            // Moet dit hier staan ? 
            Speler.Locatie = Dobbelsteen1.Waarde + Speler.Locatie;
            PlaatsOpBord.Text = Dobbelsteen1.Waarde.ToString();
            AantalDobbelsteen.Text = Speler.Locatie.ToString();
            Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            Speler.PlaatsR =Speelbord.Plaats[Speler.Locatie, 1];
        }
    }
}