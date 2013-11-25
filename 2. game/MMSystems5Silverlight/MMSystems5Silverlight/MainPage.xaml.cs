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

namespace MMSystems5Silverlight
{
    public partial class MainPage : UserControl
    {
        //ServiceReference1.GanzenbordServiceClient client1;
        //GanzenBordServiceAzure.GanzenbordServiceClient client1;
        //GanzenBordServiceAzure.Player player = new GanzenBordServiceAzure.Player();
        ServiceLocal.GanzenbordServiceClient client1;
        ServiceLocal.Player player = new ServiceLocal.Player();

  
        ObservableCollection<GanzenBordServiceAzure.Lobby> aList = new ObservableCollection<GanzenBordServiceAzure.Lobby>();

        string txtboxnaam;
        string txtboxwachtwoord;
        
        Bord Speelbord;
        public MainPage()
        {
            InitializeComponent();
            Speelbord = new Bord();
            
            this.DataContext = player;
            //client = new ServiceReference1.GanzenbordServiceClient();
            //client.GooiCompleted += client_GooiCompleted;

            client1 = new ServiceLocal.GanzenbordServiceClient();
            client1.GooiCompleted += client1_GooiCompleted;
            client1.MaakAccountCompleted += client1_MaakAccountCompleted;
            client1.InloggenCompleted += client1_InloggenCompleted;
            client1.MaakLobbyCompleted += client1_MaakLobbyCompleted;
            client1.BeschikbareLobbysCompleted += client1_BeschikbareLobbysCompleted;
            client1.GamestateCompleted += client1_GamestateCompleted;
        
          
        }

        void client1_GamestateCompleted(object sender, ServiceLocal.GamestateCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void client1_BeschikbareLobbysCompleted(object sender, ServiceLocal.BeschikbareLobbysCompletedEventArgs e)
        {
            MessageBox.Show("De lobbys");

            foreach (var item in e.Result)
            {
                lstBox.Items.Add(e.Result);
            }
            
            
        }

        void client1_MaakLobbyCompleted(object sender, ServiceLocal.MaakLobbyCompletedEventArgs e)
        {
            MessageBox.Show("Lobby toegevoegd");
           // client1.BeschikbareLobbysAsync();
            player = e.Result;
        }

        void client1_InloggenCompleted(object sender, ServiceLocal.InloggenCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                MessageBox.Show("Niet juist");
            }

            else
            {
                

                player = e.Result;
                MessageBox.Show("[proficiat ");
            }  
        }

        private void client1_MaakAccountCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Normaal ist gelukt");
        }

        void client1_GooiCompleted(object sender, ServiceLocal.GooiCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            //Speler.Locatie = e.Result;
            //player.Locatie = player.Locatie + e.Result;
            AantalDobbelsteen.Text = e.Result.ToString();
      
            PlaatsOpBord.Text = player.Locatie.ToString();
            //player.PlaatsC = Speelbord.Plaats[player.Locatie, 0];
            //player.PlaatsR = Speelbord.Plaats[player.Locatie, 1];
        }

        //void client_GooiCompleted(object sender, ServiceReference1.GooiCompletedEventArgs e)
        //{

        //    AantalDobbelsteen.Text = e.Result.ToString();
        //    Speler.Locatie = Speler.Locatie + e.Result;
        //    PlaatsOpBord.Text = Speler.Locatie.ToString();
        //    Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
        //    Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
            
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client1.GamestateAsync(player);
        }

        private void MaakAccount(object sender, RoutedEventArgs e)
        {
            txtboxnaam = naam.Text;
            txtboxwachtwoord = wachtwoord.Text;
            client1.MaakAccountAsync(txtboxnaam, txtboxwachtwoord);
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            txtboxnaam = naam.Text;
            txtboxwachtwoord = wachtwoord.Text;
            client1.InloggenAsync(txtboxnaam, txtboxwachtwoord);
        }

        private void MaakLobby_Click(object sender, RoutedEventArgs e)
        {
            client1.MaakLobbyAsync(player);
        }
    }
}
