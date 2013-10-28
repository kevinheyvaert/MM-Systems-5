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
        GanzenbordServiceSpel.Lobby lobby = new GanzenbordServiceSpel.Lobby();
        
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
            client2.LobbyInfoCompleted += client2_LobbyInfoCompleted;
            client2.JoinLobbyCompleted += client2_JoinLobbyCompleted;
            client2.StopHostCompleted += client2_StopHostCompleted;
         


            //client = new Ganzebord.GanzenbordServiceClient();
            //client.GooiCompleted += client_GooiCompleted;
        }

        void client2_StopHostCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           

            client2.BeschikbareLobbysAsync();
        }

        void client2_JoinLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           // MessageBox.Show("gejoint");
            client2.BeschikbareLobbysAsync();
            
        }

        void client2_LobbyInfoCompleted(object sender, GanzenbordServiceSpel.LobbyInfoCompletedEventArgs e)
        {
            LijstSpelersInLobby.ItemsSource = e.Result;
        }

        void client2_MaakAccountCompleted(object sender, GanzenbordServiceSpel.MaakAccountCompletedEventArgs e)
        {
            player = e.Result;
        }

        void client2_BeschikbareLobbysCompleted(object sender, GanzenbordServiceSpel.BeschikbareLobbysCompletedEventArgs e)
        {
            ListAvaibleLobbys.ItemsSource = e.Result;
        }

        void client2_MaakLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           // MessageBox.Show("Lobby toegevoegd");
            
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

               // MessageBox.Show("proficiat ");
                client2.BeschikbareLobbysAsync();

            }

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

        private void ListAvaibleLobbys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           // client2.LobbyInfoAsync((GanzenbordServiceSpel.Lobby)ListAvaibleLobbys.SelectedValue);
        }

        private void join_Click(object sender, RoutedEventArgs e)
        {
            client2.JoinLobbyAsync((GanzenbordServiceSpel.Lobby)ListAvaibleLobbys.SelectedValue, player);
            
            
            

        }

        private void stopHost_Click(object sender, RoutedEventArgs e)
        {
            client2.StopHostAsync(player);
        }
        
    }
}
