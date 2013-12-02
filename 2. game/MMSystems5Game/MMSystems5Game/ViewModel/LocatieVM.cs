using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSystems5Game
{
   public class LocatieVM:BaseViewModel
    {

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
       

    }
}
