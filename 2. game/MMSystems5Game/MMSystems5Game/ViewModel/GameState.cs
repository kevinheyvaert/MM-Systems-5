using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class GameState
    {
        GanzenBordServiceCloud.GameState gamestate;
        public GameState()
        {
            App.client1.GamestateCompleted += client1_GamestateCompleted;
            gamestate = new GanzenBordServiceCloud.GameState();
        }


        void client1_GamestateCompleted(object sender, GanzenBordServiceCloud.GamestateCompletedEventArgs e)
        {
            gamestate = e.Result;
        }

        public void status(GanzenBordServiceCloud.Player player)
        {
            App.client1.GamestateAsync(player);
        }
    }
}
