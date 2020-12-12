using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static Color Accent { get; } = Color.FromRgb(39, 101, 189);

        public static Color AlphaAccent(byte a) => Color.FromArgb(a, Accent.R, Accent.G, Accent.B);

        private void SideButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
                grid.Background = new SolidColorBrush(AlphaAccent(75));
        }

        private void SideButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                grid.Background = new SolidColorBrush(AlphaAccent(25));
                if (grid.Children[0] is Label label)
                    label.Foreground = new SolidColorBrush(Accent);
                grid.Children[1].Visibility = Visibility.Visible;
            }
        }

        private void SideButtonMouseLeave(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                grid.Background = new SolidColorBrush(Colors.Transparent);
                if (grid.Children[0] is Label label)
                    label.Foreground = new SolidColorBrush(Colors.Black);
                grid.Children[1].Visibility = Visibility.Hidden;
            }
        }

        private void SideButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
                grid.Background = new SolidColorBrush(AlphaAccent(25));
        }
    }
}