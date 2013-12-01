using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MMSystems5Game
{
    public partial class HighScores : PhoneApplicationPage
    {
        public HighScores()
        {
            InitializeComponent();
            Highscores.DataContext = App.highscore;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
        }
    }
}