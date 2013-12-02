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
       
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (App.player.IsHost)
            {
                App.timer.Stop();
                App.start.Start(App.lobbylistvm.TemplateBind);
            }
            else
                play.IsEnabled = false;
               

        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListAvaibleLobbys.DataContext = App.lobbylistvm;
                LijstSpelersInLobby.DataContext = App.LobbyInfo;
              
                join.DataContext = App.lobbylistvm;
                create.DataContext = App.MaakLobby;
                play.IsEnabled = App.Startspel;
                App.timer.Start();
                App.gametimer.Stop();
                App.plaats = true;
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
                play.IsEnabled = false;
                App.plaats = true;
            }
            else
            {
                App.MaakLobby.MaakLobby(App.player);
                play.IsEnabled = true;
                App.plaats = true;
            }
          
            
        }


        private void join_Click(object sender, RoutedEventArgs e)
        {
            
            App.join.Join(App.lobbylistvm.TemplateBind, App.player);
            
           

        }
    }
}