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
       // Ganzebord.GanzenbordServiceClient client;
       // Ganzenbordcloud.GanzenbordServiceClient client1;
        GanzenbordServiceSpel.GanzenbordServiceClient client2;
        string txtboxnaam;
        string txtboxwachtwoord;

        Bord Speelbord;
        Player Speler;
        public MainPage()
        {
            InitializeComponent();
            Speelbord = new Bord();
            Speler = new Player();
            //this.DataContext = Speler;
            //client1 = new Ganzenbordcloud.GanzenbordServiceClient();
            //client1.GooiCompleted += client1_GooiCompleted;
            client2 = new GanzenbordServiceSpel.GanzenbordServiceClient();
            client2.MaakAccountCompleted += client2_MaakAccountCompleted;
            
            
            //client = new Ganzebord.GanzenbordServiceClient();
            //client.GooiCompleted += client_GooiCompleted;
        }

        void client2_MaakAccountCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Normaal ist gelukt");
        }

        //void client1_GooiCompleted(object sender, Ganzenbordcloud.GooiCompletedEventArgs e)
        //{
        //    AantalDobbelsteen.Text = e.Result.ToString();
        //    Speler.Locatie = Speler.Locatie + e.Result;
        //    PlaatsOpBord.Text = Speler.Locatie.ToString();
        //    Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
        //    Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
        //}

        //void client_GooiCompleted(object sender, Ganzebord.GooiCompletedEventArgs e)
        //{
        //    AantalDobbelsteen.Text = e.Result.ToString();
        //    Speler.Locatie = Speler.Locatie + e.Result;
        //    PlaatsOpBord.Text = Speler.Locatie.ToString();
        //    Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
        //    Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //client.GooiAsync();
        }

        private void MaakAccount_Click(object sender, RoutedEventArgs e)
        {
           
            client2.MaakAccountAsync("kevin" , "bba");
        }
       
    }
}
