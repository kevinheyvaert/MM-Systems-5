using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GanzenbordWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        private int _plaatsc;
        private int _plaatsr;
        public int PlaatsC 
        { 
            get {return _plaatsc;}
            set { _plaatsc = value; OnPropertyChanged("PlaatsC"); } 
        }
        public int PlaatsR
        {
            get {return _plaatsr;}
            set { _plaatsr = value; OnPropertyChanged("PlaatsR"); } 
        }
       
        int locatie = 0;
        int[,] plaats = new int[63, 2]{
        {0,6},{1,6},{2,6},{3,6},{4,6},{5,6},{6,6},{7,6},{8,6},
        {8,5},{8,4},{8,3},{8,2},{8,1},{8,0},
        {7,0},{6,0},{5,0},{4,0},{3,0},{2,0},{1,0},{0,0},
        {0,1},{0,2},{0,3},{0,4},{0,5},
        {1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},
        {7,4},{7,3},{7,2},{7,1},
        {6,1},{5,1},{4,1},{3,1},{2,1},{1,1},
        {1,2},{1,3},{1,4},
        {2,4},{3,4},{4,4},{5,4},{6,4},
        {6,3},{6,2},
        {5,2},{4,2},{3,2},{2,2},
        {2,3},
        {3,3},{4,3},{5,3}};


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void beweeg()
        {
            locatie++;
            PlaatsC = plaats[locatie, 0];
            PlaatsR = plaats[locatie, 1];
           

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            beweeg();
        }

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
