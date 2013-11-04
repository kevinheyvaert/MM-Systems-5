using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    class ExitLobbyVM
    {

        public ExitLobbyVM()
        {
           App.client1.ExitLobbyCompleted += client1_ExitLobbyCompleted;
        }

        void client1_ExitLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            App.player.Lobby = null;
        }

        public void ExitLobby(GanzenBordServiceCloud.Player player)
        {
            try
            {
                App.client1.ExitLobbyAsync(player);
            }
            catch (Exception)
            {
                
                
            }
            
        }
    }
}
