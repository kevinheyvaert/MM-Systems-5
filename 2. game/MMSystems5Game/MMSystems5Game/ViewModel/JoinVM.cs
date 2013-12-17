﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class JoinVM
    {

        public string playjoin { get; set; }
        public JoinVM()
        {
            App.client1.JoinLobbyCompleted += client1_JoinLobbyCompleted;       
        }

        void client1_JoinLobbyCompleted(object sender, GanzenBordServiceCloud.JoinLobbyCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                App.player = e.Result;
                App.plaats = true;
                App.gametimer.Start();
            }
        }

        public void Join(GanzenBordServiceCloud.Lobby lobby, GanzenBordServiceCloud.Player player)
        {
            try
            {
                if (lobby!=null && player!=null)
                {
                    App.client1.JoinLobbyAsync(lobby, player);
                    App.player.Lobby = lobby.HostPlayer;
                    App.player.HostID = lobby.HostID;
                    App.MaakLobby.createstop = "Create Lobby";
                }
                
            }
            catch (Exception)
            {                
                throw;
            }
            
        }
    }
}
