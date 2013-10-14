using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMSystems5Silverlight.Web
{
    public class Gamestate
    {
        List<Player> Players;
        Player ActivePlayer;
        Bord Mainbord;
        GameFase CurrentFase;

    }

    enum GameFase { Buying, Walking, Bargaining }  //Aanpassen
}