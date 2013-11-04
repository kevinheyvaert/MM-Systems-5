using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class MaakLobbyVM
    {
        public MaakLobbyVM()
        {
            App.client1.MaakLobbyCompleted += client1_MaakLobbyCompleted;
        
        }

        void client1_MaakLobbyCompleted(object sender, GanzenBordServiceCloud.MaakLobbyCompletedEventArgs e)
        {
            App.player.Lobby = App.player.PlayerNaam;
            App.player.IsHost = true;
        }

        public void MaakLobby(GanzenBordServiceCloud.Player player)
        {
            try
            {
                App.client1.MaakLobbyAsync(player);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
