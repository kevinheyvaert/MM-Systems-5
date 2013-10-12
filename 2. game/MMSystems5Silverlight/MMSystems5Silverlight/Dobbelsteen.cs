using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MMSystems5Silverlight
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
