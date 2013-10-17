using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication1
{
    public class Player : INotifyPropertyChanged
    {
        public Player()
        {


        }
        public string UserName { get; set; }

        public bool Beurt { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        // Plaats van begin op bord(grid)
        private int _plaatsc = 0;
        private int _plaatsr = 6;
        public int PlaatsC
        {
            get { return _plaatsc; }
            set { _plaatsc = value; OnPropertyChanged("PlaatsC"); }
        }
        public int PlaatsR
        {
            get { return _plaatsr; }
            set { _plaatsr = value; OnPropertyChanged("PlaatsR"); }
        }
        public int Id { get; set; }
        public int Locatie { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
