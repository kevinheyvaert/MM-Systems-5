using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace MMSystems5Game
{
   public class LocatieVM:BaseViewModel
    {

       public int playerid;
       private int temploc = 0;

      

        private int _plaatsc;
        public int PlaatsC 
        {
            get { return _plaatsc;}
            set { _plaatsc = value; RaisePropChanged("PlaatsC"); } 
        }

        private int _plaatsr;
        public int PlaatsR
        {
            get { return _plaatsr; }
            set { _plaatsr = value; RaisePropChanged("PlaatsR"); }
        }

        public LocatieVM()
        {
            PlaatsC = 0;
            PlaatsR = 6;
            
        }


        public void lopen()
        {
            if (temploc == App.gamestate.turn.Locatie)
            {
                App.pionsetter.Stop();
                App.gametimer.Start();
            }
            else
            {
                PlaatsC = App.bord.Plaats[temploc, 0];
                PlaatsR = App.bord.Plaats[temploc, 1];
                temploc++;
            }

           
            
        }

    }
}
