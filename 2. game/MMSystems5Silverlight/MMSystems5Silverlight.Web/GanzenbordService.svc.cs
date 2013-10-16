using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMSystems5Silverlight.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GanzenbordService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GanzenbordService.svc or GanzenbordService.svc.cs at the Solution Explorer and start debugging.
    public class GanzenbordService : IGanzenbordService
    {
        
        public void DoWork()
        {
        }
        Gamestate status = new Gamestate();
        public int  Gooi(Player currentplayer)
        {   
            
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            currentplayer.Locatie = currentplayer.Locatie+Dobbelsteen1.GeefWaardeDobbelsteen();
            status.ActivePlayer = currentplayer;
            
            return Dobbelsteen1.GeefWaardeDobbelsteen();
        }

        


    }
}
