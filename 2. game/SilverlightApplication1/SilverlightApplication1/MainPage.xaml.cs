using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        Ganzebord.GanzenbordServiceClient client;
        Bord Speelbord;
        Player Speler;
        public MainPage()
        {
            InitializeComponent();
            Speelbord = new Bord();
            Speler = new Player();
            this.DataContext = Speler;
            client = new Ganzebord.GanzenbordServiceClient();
            client.GooiCompleted += client_GooiCompleted;
        }

        void client_GooiCompleted(object sender, Ganzebord.GooiCompletedEventArgs e)
        {
            AantalDobbelsteen.Text = e.Result.ToString();
            Speler.Locatie = Speler.Locatie + e.Result;
            PlaatsOpBord.Text = Speler.Locatie.ToString();
            Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.GooiAsync();
        }
       
    }
}
