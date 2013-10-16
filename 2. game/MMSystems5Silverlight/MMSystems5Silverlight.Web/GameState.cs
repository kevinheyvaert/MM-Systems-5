using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMSystems5Silverlight.Web
{
    public class Gamestate
    {
        List<Player> Players;
        public Player ActivePlayer;
        GameFase CurrentFase;

    }

    enum GameFase { Ingelogd, Aanbeurt }  
}