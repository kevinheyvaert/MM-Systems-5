using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SilverlightApplication1.Web
{
    public class Gamestate
    {

        List<Player> Players;
        Player ActivePlayer;
        Bord Mainbord;
        GameFase CurrentFase;

    }
    enum GameFase { Beurt, InLobby, Speleinde }
}