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

namespace MMSystems5Silverlight
{
    public partial class MainPage : UserControl
    {
        GanzenBordServiceLocal.GanzenbordServiceClient client;
        Bord Speelbord;
        Player Speler;
        public MainPage()
        {
            InitializeComponent();
           client = new GanzenBordServiceLocal.GanzenbordServiceClient();
           client.GooiCompleted += client_GooiCompleted;
           client.GooiAsync();
        }

        void client_GooiCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
            client.GooiAsync();
            //PlaatsOpBord.Text = Dobbelsteen1.Waarde.ToString();
            AantalDobbelsteen.Text = Speler.Locatie.ToString();
            Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.GooiCompleted += client_GooiCompleted;
        }
    }
}
