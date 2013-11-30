using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MMSystems5Game
{
    public class PlayersInLobby:BaseViewModel
    {
        private ObservableCollection<GanzenBordServiceCloud.Player> _lijstspelersinlobby;
        public ObservableCollection<GanzenBordServiceCloud.Player> LijstSpelersInLobby
        { 
            get {return _lijstspelersinlobby;}
            set { _lijstspelersinlobby = value; RaisePropChanged("LijstSpelersInLobby"); }
        }

        public PlayersInLobby()
        {
            try
            {
                App.client1.LobbyInfoCompleted += client1_LobbyInfoCompleted;
            }
            catch (Exception)
            {    
                throw;
            }
            
        }

        void client1_LobbyInfoCompleted(object sender, GanzenBordServiceCloud.LobbyInfoCompletedEventArgs e)
        {
            LijstSpelersInLobby = e.Result;
        }

        public void infolobby(GanzenBordServiceCloud.Lobby lobby)
        {
            App.client1.LobbyInfoAsync(lobby);
        }

    }
}
