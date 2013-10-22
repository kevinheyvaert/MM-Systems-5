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

namespace MMSystems5Game
{
    public partial class MainPage : PhoneApplicationPage
    {
        GanzenBordServiceCloud.GanzenbordServiceClient client1;
        string temp1;
        string temp2;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            client1 = new GanzenBordServiceCloud.GanzenbordServiceClient();
            client1.InloggenCompleted += client1_InloggenCompleted;
        }

        void client1_InloggenCompleted(object sender, GanzenBordServiceCloud.InloggenCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                MessageBox.Show("Succesvol ingelogd");
                NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
            }

            else
            {
                MessageBox.Show("Foute gegevens");
            }


        }

        private void Login(object sender, RoutedEventArgs e)
        {
            temp1 = Username.Text;
            temp2 = Password.Text;
            client1.InloggenAsync(temp1, temp2);
        }

        private void MaakNieuwAccount(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MakeAccount.xaml"), UriKind.Relative));
        }

        
        
    }
}