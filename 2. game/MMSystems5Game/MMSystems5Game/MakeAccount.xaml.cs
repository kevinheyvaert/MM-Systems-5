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
        string temp1;
        string temp2;
        GanzenBordServiceCloud.GanzenbordServiceClient client1;
        
        public MakeAccount()
        {
            InitializeComponent();
            client1 = new GanzenBordServiceCloud.GanzenbordServiceClient();
            client1.MaakAccountCompleted += client1_MaakAccountCompleted;
        }

        void client1_MaakAccountCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           
            NavigationService.Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
        }

        private void MaakNieuwAan(object sender, RoutedEventArgs e)
        {
             temp1 = Username.Text;
            temp2 = Password.Text;
            client1.MaakAccountAsync(temp1, temp2);
        }
    }
}