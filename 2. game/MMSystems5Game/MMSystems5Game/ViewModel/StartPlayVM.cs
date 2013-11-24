using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;
using System.Windows;

namespace MMSystems5Game
{
    public class StartPlayVM :PhoneApplicationPage
    {
        public StartPlayVM()
        {
            App.client1.StartCompleted += client1_StartCompleted;
        }

        void client1_StartCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(string.Format("/GameView.xaml"), UriKind.Relative));
        }

        public void Start(GanzenBordServiceCloud.Lobby lobby)
        {
            App.client1.StartAsync(lobby);
        }
    }
}
