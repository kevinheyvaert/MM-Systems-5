using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MMSystems5Game
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            
            InitializeComponent();
            ListAvaibleLobbys.DataContext = App.lobbylistvm;
            LijstSpelersInLobby.DataContext = App.LobbyInfo;
            play.DataContext = App.join;

            if (App.player.IsHost == false)
            {
                play.IsEnabled = false;
            }
            
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.player.IsHost == true)
                NavigationService.Navigate(new Uri(string.Format("/GameView.xaml"), UriKind.Relative));
            else
                App.join.Join(App.lobbylistvm.TemplateBind, App.player);

        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                App.lobbylistvm.GetLobbys();
                App.timer.Start();  
            }
            catch (Exception)
            {
                
                throw;
            }
         
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.MaakLobby.MaakLobby(App.player);
        }

        private void jn_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}