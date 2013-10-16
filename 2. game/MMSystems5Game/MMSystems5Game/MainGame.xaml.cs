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
    public partial class MainGame : PhoneApplicationPage
    {
        public MainGame()
        {
            InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
        }

        private void View_Scores(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/HighScores.xaml"), UriKind.Relative));
        }

        private void Exit_Game(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml"), UriKind.Relative));
        }
    }
}