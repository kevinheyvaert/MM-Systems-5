using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class GameState
    {
       
        public GameState()
        {
            App.client1.GamestateCompleted += client1_GamestateCompleted;
            
        }


        void client1_GamestateCompleted(object sender, GanzenBordServiceCloud.GamestateCompletedEventArgs e)
        {
            App.gamestate = e.Result;
        }

        public void status(GanzenBordServiceCloud.Player player)
        {
            App.client1.GamestateAsync(player);
        }
    }
}
