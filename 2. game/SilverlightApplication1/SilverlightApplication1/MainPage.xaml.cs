using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        GanzenbordServiceSpel.Player player = new GanzenbordServiceSpel.Player();
        ObservableCollection<GanzenbordServiceSpel.Lobby> aList = new ObservableCollection<GanzenbordServiceSpel.Lobby>();
        public MainPage()
        {
            InitializeComponent();
            Speelbord = new Bord();

            //this.DataContext = Speler;
            //client1 = new Ganzenbordcloud.GanzenbordServiceClient();
            //client1.GooiCompleted += client1_GooiCompleted;
            client2 = new GanzenbordServiceSpel.GanzenbordServiceClient();
            client2.MaakAccountCompleted += client2_MaakAccountCompleted;
            client2.InloggenCompleted += client2_InloggenCompleted;
            client2.MaakLobbyCompleted += client2_MaakLobbyCompleted;
            client2.BeschikbareLobbysCompleted += client2_BeschikbareLobbysCompleted;


            //client = new Ganzebord.GanzenbordServiceClient();
            //client.GooiCompleted += client_GooiCompleted;
        }

        void client2_MaakAccountCompleted(object sender, GanzenbordServiceSpel.MaakAccountCompletedEventArgs e)
        {
            player = e.Result;
        }

        void client2_BeschikbareLobbysCompleted(object sender, GanzenbordServiceSpel.BeschikbareLobbysCompletedEventArgs e)
        {
            MessageBox.Show("De lobbys");
           
            aList = e.Result;
        }

        void client2_MaakLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Lobby toegevoegd");
            client2.BeschikbareLobbysAsync();
        }

        void client2_InloggenCompleted(object sender, GanzenbordServiceSpel.InloggenCompletedEventArgs e)
        {

            if (e.Result == null)
            {
                MessageBox.Show("Niet juist");
            }

            else
            {

                player = e.Result;

                MessageBox.Show("[proficiat ");
                client2.BeschikbareLobbysAsync();

            }

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

            txtboxnaam = naam.Text;
            txtboxwachtwoord = wachtwoord.Text;
            client2.MaakAccountAsync(txtboxnaam, txtboxwachtwoord);
            client2.BeschikbareLobbysAsync();
        }

        private void inloggen_Click(object sender, RoutedEventArgs e)
        {
            txtboxnaam = naam.Text;
            txtboxwachtwoord = wachtwoord.Text;
            client2.InloggenAsync(txtboxnaam, txtboxwachtwoord);
            


        }

        private void MaakLobby_Click(object sender, RoutedEventArgs e)
        {

            client2.MaakLobbyAsync(player);


        }
    }
}
