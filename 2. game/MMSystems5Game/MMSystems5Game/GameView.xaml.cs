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
       // GanzenBordServiceCloud.GanzenbordServiceClient client;
        Bord Speelbord;
        Player Speler;
        public GameView()
        {
            InitializeComponent();
            
            Speelbord = new Bord();
            Speler = new Player();
           // this.DataContext = Speler;
            dobbel.DataContext = App.dice;
          //blablaba

           // App.client1 = new GanzenBordServiceCloud.GanzenbordServiceClient();
            App.client1.GooiCompleted += client_GooiCompleted;
        }

        void client_GooiCompleted(object sender, GanzenBordServiceCloud.GooiCompletedEventArgs e)
        {
           
            //gooi.Content = Dice(e.Result);
            Speler.Locatie = Speler.Locatie + e.Result;

            //If (Speler.Locatie == 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59)
            //{
            //  NOG IS GOOIEN MET HET GEGOOIDE AANTAL
            //}

            if (Speler.Locatie == 6)
            {
                Speler.Locatie = 12;
                MessageBox.Show("Brug : Ga naar 12");
            }
            if (Speler.Locatie == 19)
            {
                //nog te maken bool Herberg
                MessageBox.Show("Beurt overslaan");
            }
            if (Speler.Locatie == 31)
            {
                //nog te maken bool Put
                MessageBox.Show("Put : Je zit in de put tot er een andere speler in komt");
            }
            if (Speler.Locatie == 42)
            {
                Speler.Locatie = 39;
                MessageBox.Show("Doolhof : Ga naar 39");
            }
            if (Speler.Locatie == 52)
            {
                //nog te maken bool Gevangenis
                MessageBox.Show("Gevangenis : Je zit in de put tot er een andere speler in komt");
            }
            if (Speler.Locatie == 58)
            {
                Speler.Locatie = 0;
                MessageBox.Show("Dood : Terug naar begin");
            }

            if (Speler.Locatie > 63)
            {
                int TijdelijkeLocatie = Speler.Locatie - 63;
                Speler.Locatie = 63 - TijdelijkeLocatie;
            }

            if (Speler.Locatie == 63)
            {
                MessageBox.Show("Einde : U heeft gewonnen!");
                Speler.Locatie = 62;
                //Speler.Score = Speler.Score + 1;
                NavigationService.Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
            }
            
            PlaatsOpBord.Text = Speler.Locatie.ToString();
            Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
        }

       
        public void Gooi(object sender, RoutedEventArgs e)
        {
            App.client1.GooiAsync(Speler);
            
            
        }


      
    }
}