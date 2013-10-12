using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMSystems5Silverlight.Web
{
    public class Dobbelsteen
    {
        private int _waarde;

        public int Waarde
        {
            get { return _waarde; }
            set { _waarde = value; }
        }

        private int GooiDobbelsteen()
        {
            int aantal = 0;
            while (aantal < 50)
            {
                Random random = new Random();
                _waarde = (random.Next(6) + 1);
                aantal++;
            }
            return _waarde;
        }

        public int GeefWaardeDobbelsteen()
        {
            return GooiDobbelsteen();
        }

    }
}