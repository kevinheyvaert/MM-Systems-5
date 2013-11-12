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
        GanzenBordServiceCloud.GanzenbordServiceClient client;
        Bord Speelbord;
        Player Speler;
        public GameView()
        {
            InitializeComponent();
            
            Speelbord = new Bord();
            Speler = new Player();
            this.DataContext = Speler;

            client = new GanzenBordServiceCloud.GanzenbordServiceClient();
            client.GooiCompleted += client_GooiCompleted;
        }

        void client_GooiCompleted(object sender, GanzenBordServiceCloud.GooiCompletedEventArgs e)
        {
            AantalDobbelsteen.Text = e.Result.ToString();
            gooi.Content = Dice(e.Result);
            Speler.Locatie = Speler.Locatie + e.Result;

            //If (Speler.Locatie == 5, 9, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59)
            //{
            //  NOG IS GOOIEN MET HET GEGOOIDE AANTAL
            //}

            if (Speler.Locatie == 6)
            {
                Speler.Locatie = 12;
                MessageBox.Show("Brug : Ga naar 12");
            }
            if (Speler.Locatie == 19)
            {
                //nog te maken bool Herberg
                MessageBox.Show("Beurt overslaan");
            }
            if (Speler.Locatie == 31)
            {
                //nog te maken bool Put
                MessageBox.Show("Put : Je zit in de put tot er een andere speler in komt");
            }
            if (Speler.Locatie == 42)
            {
                Speler.Locatie = 39;
                MessageBox.Show("Doolhof : Ga naar 39");
            }
            if (Speler.Locatie == 52)
            {
                //nog te maken bool Gevangenis
                MessageBox.Show("Gevangenis : Je zit in de put tot er een andere speler in komt");
            }
            if (Speler.Locatie == 58)
            {
                Speler.Locatie = 0;
                MessageBox.Show("Dood : Terug naar begin");
            }

            if (Speler.Locatie > 63)
            {
                int TijdelijkeLocatie = Speler.Locatie - 63;
                Speler.Locatie = 63 - TijdelijkeLocatie;
            }

            if (Speler.Locatie == 63)
            {
                MessageBox.Show("Einde : U heeft gewonnen!");
                Speler.Locatie = 62;
                //Speler.Score = Speler.Score + 1;
                NavigationService.Navigate(new Uri(string.Format("/Lobby.xaml"), UriKind.Relative));
            }
            
            PlaatsOpBord.Text = Speler.Locatie.ToString();
            Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            Speler.PlaatsR = Speelbord.Plaats[Speler.Locatie, 1];
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
        }

       
        public void Gooi(object sender, RoutedEventArgs e)
        {
            client.GooiAsync(Speler);
            
            
        }


        //http://cespage.com/silverlight/tutorials/wp7tut7.html
        private void Add(Grid grid, int row, int column)
        {
            Ellipse _dot = new Ellipse();
            _dot.Width = 20;
            _dot.Height = 20;
            _dot.Fill = new SolidColorBrush(Colors.Black);
            _dot.SetValue(Grid.ColumnProperty, column);
            _dot.SetValue(Grid.RowProperty, row);
            grid.Children.Add(_dot);
        }

        private Grid Dice(int Value)
        {
            Grid _grid = new Grid();
            _grid.Height = 100;
            _grid.Width = 100;
            for (int index = 0; (index <= 2); index++) // 3 by 3 Grid 
            {
                _grid.RowDefinitions.Add(new RowDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            switch (Value)
            {
                case 0:
                    // No Dots
                    break;
                case 1:
                    Add(_grid, 1, 1); // Middle
                    break;
                case 2:
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 2, 0); // Bottom Left
                    break;
                case 3:
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 1, 1); // Middle
                    Add(_grid, 2, 0); // Bottom Left
                    break;
                case 4:
                    Add(_grid, 0, 0); // Top Left
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 2, 0); // Bottom Left
                    Add(_grid, 2, 2); // Bottom Right
                    break;
                case 5:
                    Add(_grid, 0, 0); // Top Left
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 1, 1); // Middle
                    Add(_grid, 2, 0); // Bottom Left
                    Add(_grid, 2, 2); // Bottom Right
                    break;
                case 6:
                    Add(_grid, 0, 0); // Top Left
                    Add(_grid, 0, 2); // Top Right
                    Add(_grid, 1, 0); // Middle Left
                    Add(_grid, 1, 2); // Middle Right
                    Add(_grid, 2, 0); // Bottom Left
                    Add(_grid, 2, 2); // Bottom Right
                    break;
            }
            return _grid;
        }
    }
}