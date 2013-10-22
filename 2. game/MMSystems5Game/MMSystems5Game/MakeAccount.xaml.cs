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
    public partial class MakeAccount : PhoneApplicationPage
    {
        
       
        
        public MakeAccount()
        {
            InitializeComponent();
             (App.Current as App).client1 = new GanzenBordServiceCloud.GanzenbordServiceClient();
             (App.Current as App).client1.MaakAccountCompleted += client1_MaakAccountCompleted;
        }

        void client1_MaakAccountCompleted(object sender, GanzenBordServiceCloud.MaakAccountCompletedEventArgs e)
        {
            (App.Current as App).player = e.Result;
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
            MessageBox.Show("Account aangemaakt");
        }

     
        private void MaakNieuwAan(object sender, RoutedEventArgs e)
        {
             
             (App.Current as App).client1.MaakAccountAsync(Username.Text, Password.Text);
        }
    }
}