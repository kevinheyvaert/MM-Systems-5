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

        public int  Gooi()
        {

            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
           
            
            // Moet dit hier staan ? 
            //Speler.Locatie = Dobbelsteen1.Waarde + Speler.Locatie;
            //PlaatsOpBord.Text = Dobbelsteen1.Waarde.ToString();
            //AantalDobbelsteen.Text = Speler.Locatie.ToString();
            //Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            //Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
            return Dobbelsteen1.GeefWaardeDobbelsteen();
        }
    }
}
