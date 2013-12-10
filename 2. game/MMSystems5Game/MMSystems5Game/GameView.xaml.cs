using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMSystems5Game
{
    public partial class GameView : PhoneApplicationPage 
    {
       // GanzenBordServiceCloud.GanzenbordServiceClient client;
        
        public GameView()
        {
            InitializeComponent();

            
           // this.DataContext = Speler;
            dobbel.DataContext = App.dice;
            rdpion.DataContext = App.rood;
            blpion.DataContext = App.blauw;
            gpion.DataContext = App.groen;
            glpion.DataContext = App.geel;
            Gif1.DataContext = App.gifviewer;
            Gif2.DataContext = App.gifviewer;
            


            App.gamestate.players=new System.Collections.ObjectModel.ObservableCollection<GanzenBordServiceCloud.Player>();
            //spelers.DataContext = App.gamestate;
           
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
            

        }

        public void Gooi(object sender, RoutedEventArgs e)
        {

            
            App.dice.dobbel(App.player);
            App.KanGooien = false;
            App.plaats = false;

            
        }

        private void spelers_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}