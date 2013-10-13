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
        Bord Speelbord;
        Player Speler;
        public GameView()
        {
            InitializeComponent();
            
            Speelbord = new Bord();
            Speler = new Player();
            this.DataContext = Speler;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainGame.xaml"), UriKind.Relative));
        }

       
        public void Gooi(object sender, RoutedEventArgs e)
        {
            
            
            Dobbelsteen Dobbelsteen1 = new Dobbelsteen();
            Dobbelsteen1.GeefWaardeDobbelsteen();
            gooi.Content = Dice(Dobbelsteen1.Waarde);
            // Moet dit hier staan ? 
            Speler.Locatie = Dobbelsteen1.Waarde + Speler.Locatie;
            PlaatsOpBord.Text = Dobbelsteen1.Waarde.ToString();
            AantalDobbelsteen.Text = Speler.Locatie.ToString();
            Speler.PlaatsC = Speelbord.Plaats[Speler.Locatie, 0];
            Speler.PlaatsR =Speelbord.Plaats[Speler.Locatie, 1];
        }

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