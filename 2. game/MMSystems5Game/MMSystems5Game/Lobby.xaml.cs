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
            play.DataContext = App.player;
            join.DataContext = App.lobbylistvm;
            create.DataContext = App.MaakLobby;
  
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

                App.timer.Stop();
                App.start.Start(App.lobbylistvm.TemplateBind);

        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                App.timer.Start();  
            }
            catch (Exception)
            {
                
                throw;
            }
         
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (App.player.IsHost)
            {
                App.stophost.StopHost(App.player);
            }
            else
             App.MaakLobby.MaakLobby(App.player);
            
        }


        private void join_Click(object sender, RoutedEventArgs e)
        {
            
            App.join.Join(App.lobbylistvm.TemplateBind, App.player);
            App.gametimer.Start();

        }
    }
}