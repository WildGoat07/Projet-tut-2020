using GUI.modules;
using System;
using System.Collections.Generic;
using System.IO;
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
            _ = LoadModuleAsync(new AllEnseignantsModule());//test
            _ = LoadModuleAsync(new DummyDateModule());//test
        }

        public async Task LoadModuleAsync(Module module)
        {
            var refreshTask = module.RefreshAsync();
            if (module is DateDepedantModule m)
                m.years = yearSelection;
            var item = new TabItem();
            var header = new StackPanel { Orientation = Orientation.Horizontal };
            header.Children.Add(new Label { Content = module });
            var image = new Image
            {
                Stretch = Stretch.None,
                Margin = new Thickness(2)
            };
            image.Source = App.CloseTab;
            var closeGrid = new Grid();
            closeGrid.Children.Add(image);
            closeGrid.MouseEnter += (sender, e) => closeGrid.Background = new SolidColorBrush(App.AlphaAccent(25));
            closeGrid.MouseLeave += (sender, e) => closeGrid.Background = new SolidColorBrush(Colors.Transparent);
            module.OnClose += () => modules.Items.Remove(item);
            closeGrid.MouseUp += (sender, e) => module.CloseModule();
            header.Children.Add(closeGrid);
            item.Header = header;
            item.Content = module?.Content;
            item.Tag = module;
            modules.Items.Add(item);
            modules.SelectedItem = item;
            if (refreshTask is not null)
                await refreshTask;
        }

        public async Task RefreshAsync()
        {
            var mod = (modules.SelectedItem as TabItem)?.Tag as Module;
            if (mod is not null)
                await mod.RefreshAsync();
            await UpdateYearSelectionAsync();
        }

        public async Task UpdateYearSelectionAsync()
        {
            var selection = yearSelection.SelectedItem;
            yearSelection.Items.Clear();
            yearSelection.Items.Add("Toutes les années");
            //! AnneeUniv doit d'abord être terminé
            /*
            foreach (var item in await App.Factory.AnneeUnivDAO.GetAllAsync())
                yearSelection.Items.Add(item.annee);
            */
            if (selection != null)
                yearSelection.SelectedItem = selection;
            else
                yearSelection.SelectedIndex = 0;
        }

        private async void Button_Click(object sender, RoutedEventArgs e) => await RefreshAsync();

        private void modules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = modules.SelectedItem as TabItem;
            if (item is not null)
            {
                var module = item.Tag as Module;
                if (module is not null)
                {
                    moduleTitle.Content = module.Title;
                    if (module is DateDepedantModule)
                        yearSelection.Visibility = Visibility.Visible;
                    else
                        yearSelection.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                yearSelection.Visibility = Visibility.Hidden;
                moduleTitle.Content = "";
            }
        }

        private void SideButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid is not null)
                grid.Background = new SolidColorBrush(App.AlphaAccent(75));
        }

        private void SideButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            if (grid is not null)
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
            if (grid is not null)
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
            if (grid is not null)
                grid.Background = new SolidColorBrush(App.AlphaAccent(25));
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
                await RefreshAsync();
        }
    }
}