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
        
        Player Speler;
        public GameView()
        {
            InitializeComponent();

            Speler = new Player();
           // this.DataContext = Speler;
            dobbel.DataContext = App.dice;
            rdpion.DataContext = App.rood;
            blpion.DataContext = App.blauw;
            gpion.DataContext = App.groen;
            glpion.DataContext = App.geel;
          //blablaba

           // App.client1 = new GanzenBordServiceCloud.GanzenbordServiceClient();
           
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
            App.stophost.StopHost(App.player);
            App.exitlobby.ExitLobby(App.player);
        }

        public void Gooi(object sender, RoutedEventArgs e)
        {

            App.client1.GooiAsync(App.player); 
            
        } 
    }
}