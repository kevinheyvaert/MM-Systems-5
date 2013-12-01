using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class GameState:BaseViewModel
    {
       
        public GameState()
        {
            App.client1.GamestateCompleted += client1_GamestateCompleted;
        }


        void client1_GamestateCompleted(object sender, GanzenBordServiceCloud.GamestateCompletedEventArgs e)
        {
            App.gamestate = e.Result;

            if (App.player.PlayerId==e.Result.turn.PlayerId)
            {
                App.dice.Turn = true;
                App.KanGooien = true;
            }

            else if (App.player.PlayerId!=e.Result.turn.PlayerId)

            {
                App.dice.Turn = false;
            }

            //Plaatsen van de pionen.
            //Kleur van pionen wordt toegewezen aan de rangschikking van players in database
           
            App.geel.PlaatsC = App.bord.Plaats[e.Result.players[0].Locatie, 0];
            App.geel.PlaatsR = App.bord.Plaats[e.Result.players[0].Locatie, 1];
             

                if (e.Result.players.Count > 1)
                {
                    App.blauw.PlaatsC = App.bord.Plaats[e.Result.players[1].Locatie, 0];
                    App.blauw.PlaatsR = App.bord.Plaats[e.Result.players[1].Locatie, 1];

                    if (e.Result.players.Count > 2)
                    {
                        
                        App.rood.PlaatsC = App.bord.Plaats[e.Result.players[2].Locatie, 0];
                        App.rood.PlaatsR = App.bord.Plaats[e.Result.players[2].Locatie, 1];


                        if (e.Result.players.Count > 3)
                        {
                            App.groen.PlaatsC = App.bord.Plaats[e.Result.players[3].Locatie, 0];
                            App.groen.PlaatsR = App.bord.Plaats[e.Result.players[3].Locatie, 1];
                        }
                    }

                }
        }

        public void status(GanzenBordServiceCloud.Player player)
        {
            App.client1.GamestateAsync(player);
        }
    }
}
