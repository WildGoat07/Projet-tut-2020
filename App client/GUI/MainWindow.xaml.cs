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
            _ = UpdateYearSelectionAsync();
        }

        public void LoadModule(Module module)
        {
            if (module is DateDepedantModule m)
                m.years = yearSelection;
            var item = new TabItem();
            var header = new StackPanel();
            header.Children.Add(new Label { Content = module.Title });
            header.Children.Add(new Image());
            item.Header = header;
            item.Content = module.Content;
            item.Tag = module;
            modules.Items.Add(item);
        }

        public async Task UpdateYearSelectionAsync()
        {
            var selection = yearSelection.SelectedItem;
            yearSelection.Items.Clear();
            yearSelection.Items.Add("Toutes les années");
            if (App.Factory != null)
                foreach (var item in await App.Factory.AnneeUnivDAO.GetAllAsync())
                    yearSelection.Items.Add(item.Annee);
            yearSelection.SelectedItem = selection ?? yearSelection.Items[0];
        }

        private void modules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = modules.SelectedItem as TabItem;
            if (item != null)
            {
                var module = item.Tag as Module;
                if (module != null)
                {
                    moduleTitle.Content = module.Title;
                    if (module is DateDepedantModule)
                        yearSelection.Visibility = Visibility.Visible;
                    else
                        yearSelection.Visibility = Visibility.Hidden;
                }
            }
        }

        private void SideButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
                grid.Background = new SolidColorBrush(App.AlphaAccent(75));
        }

        private void SideButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                grid.Background = new SolidColorBrush(App.AlphaAccent(25));
                if (grid.Children[0] is Label label)
                    label.Foreground = new SolidColorBrush(App.Accent);
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
                grid.Background = new SolidColorBrush(App.AlphaAccent(25));
        }
    }
}