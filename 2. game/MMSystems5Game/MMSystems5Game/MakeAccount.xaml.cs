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
        }

        private void MaakNieuwAan(object sender, RoutedEventArgs e)
        {
            App.maakaccount.MaakAan(Username.Text, Password.Text);  
        }
    }
}