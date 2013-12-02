using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;

namespace MMSystems5Game
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            App.login.inloggen(Username.Text, Password.Text);
        }

        private void MaakNieuwAccount(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MakeAccount.xaml"), UriKind.Relative));

        }
    
    }
}