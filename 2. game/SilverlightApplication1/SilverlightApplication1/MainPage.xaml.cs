using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        // Ganzebord.GanzenbordServiceClient client;
        // Ganzenbordcloud.GanzenbordServiceClient client1;
        //GanzenbordServiceSpel.GanzenbordServiceClient client2;
        ServiceReference1.GanzenbordServiceClient client2;

        DispatcherTimer update = new DispatcherTimer();
        string txtboxnaam;
        string txtboxwachtwoord;

        //private GanzenbordServiceSpel.Lobby _lobbyinfo;
        //public GanzenbordServiceSpel.Lobby LobbyInfo 
        //{
        //    get { return _lobbyinfo; }
        //    set
        //    {
        //        _lobbyinfo = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("LobbyInfo"));
                
        //    }
        //}
        //public event PropertyChangedEventHandler PropertyChanged;

      


        Bord Speelbord;
        ServiceReference1.Player player = new ServiceReference1.Player();
        ServiceReference1.Lobby lobby = new ServiceReference1.Lobby();
        
        
        public MainPage()
        {
            InitializeComponent();
            Speelbord = new Bord();

            //this.DataContext = Speler;
            //client1 = new Ganzenbordcloud.GanzenbordServiceClient();
            //client1.GooiCompleted += client1_GooiCompleted;
            client2 = new ServiceReference1.GanzenbordServiceClient();
            client2.MaakAccountCompleted += client2_MaakAccountCompleted;
            client2.InloggenCompleted +=client2_InloggenCompleted;
            client2.MaakLobbyCompleted += client2_MaakLobbyCompleted;
            client2.BeschikbareLobbysCompleted += client2_BeschikbareLobbysCompleted;
            client2.LobbyInfoCompleted += client2_LobbyInfoCompleted;
            client2.JoinLobbyCompleted += client2_JoinLobbyCompleted;
            client2.StopHostCompleted += client2_StopHostCompleted;
            update.Interval = TimeSpan.FromSeconds(2);
            update.Tick += update_Tick;

         


            //client = new Ganzebord.GanzenbordServiceClient();
            //client.GooiCompleted += client_GooiCompleted;
        }

        void client2_LobbyInfoCompleted(object sender, ServiceReference1.LobbyInfoCompletedEventArgs e)
        {
            try
            {
                LijstSpelersInLobby.ItemsSource = e.Result;

            }
            catch (Exception)
            {

            }
           
        }

        void client2_BeschikbareLobbysCompleted(object sender, ServiceReference1.BeschikbareLobbysCompletedEventArgs e)
        {
            ListAvaibleLobbys.ItemsSource = e.Result;
        }

        void client2_InloggenCompleted(object sender, ServiceReference1.InloggenCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                MessageBox.Show("Niet juist");
            }

            else
            {

                player = e.Result;

                // MessageBox.Show("proficiat ");
                client2.BeschikbareLobbysAsync();
                update.Start();

            }

        }

        void client2_MaakAccountCompleted(object sender, ServiceReference1.MaakAccountCompletedEventArgs e)
        {
            player = e.Result;
        }

        void update_Tick(object sender, EventArgs e)
        {
            client2.LobbyInfoAsync(lobby);
            client2.BeschikbareLobbysAsync();
           
            
        }

        void client2_StopHostCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           

            client2.BeschikbareLobbysAsync();
        }

        void client2_JoinLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           // MessageBox.Show("gejoint");
            update.Start();
            
        }

      

       

     

        void client2_MaakLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           // MessageBox.Show("Lobby toegevoegd");
            
            client2.BeschikbareLobbysAsync();
        }

       

 
      
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

       
        

        private void join_Click(object sender, RoutedEventArgs e)
        {
            update.Stop();
            client2.JoinLobbyAsync(lobby, player);
            
            
            

        }

        private void stopHost_Click(object sender, RoutedEventArgs e)
        {
            client2.StopHostAsync(player);
        }

       

     
        private void ListAvaibleLobbys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lobby = (ServiceReference1.Lobby)ListAvaibleLobbys.SelectedValue;
            client2.LobbyInfoAsync(lobby);
        }

       

       
        
    }
}
