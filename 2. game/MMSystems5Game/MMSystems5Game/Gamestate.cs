using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
    public class Gamestate
    {
        List<Player> Players;
        Player ActivePlayer;
       
        GameFase CurrentFase;

    }

    enum GameFase { Beurt, InLobby, Speleinde }  //Aangepast
}
