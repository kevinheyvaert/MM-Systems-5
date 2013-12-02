using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class StopHostVM
    {
        public StopHostVM()
        {
            App.client1.StopHostCompleted += client1_StopHostCompleted;
        }

        void client1_StopHostCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            App.player.Lobby = null;
            App.player.IsHost = false;           
        }

        public void StopHost(GanzenBordServiceCloud.Player player)
        {
            try
            {
               App.client1.StopHostAsync(player);
               App.gametimer.Stop();
            }
            catch (Exception)
            {               
                throw;
            }
           
        }


    }
}
