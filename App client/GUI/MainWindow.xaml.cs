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
        }

        public Module? CurrentModule => (modules.SelectedItem as TabItem)?.Tag as Module;

        public async Task LoadModuleAsync(Module module)
        {
            var refreshTask = module.RefreshAsync();
            if (module is DateDepedantModule m)
                m.years = yearSelection;
            var item = new TabItem();
            object header;
            if (module.Closeable)
            {
                var closeHeader = new StackPanel { Orientation = Orientation.Horizontal };
                header = closeHeader;
                closeHeader.Children.Add(new Label { Content = module });
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
                closeHeader.Children.Add(closeGrid);
            }
            else
                header = module;
            item.Header = header;
            item.Content = module?.Content;
            item.Tag = module;
            modules.Items.Add(item);
            modules.SelectedItem = item;
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
            foreach (var item in await App.Factory.AnneeUnivDAO.GetAllAsync())
                yearSelection.Items.Add(item.annee);
            if (selection == null)
                yearSelection.SelectedIndex = yearSelection.Items.Count - 1;
            else
                yearSelection.SelectedItem = selection;
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

        private async void Window_Initialized(object sender, EventArgs e)
        {
            await UpdateYearSelectionAsync();
            await LoadModuleAsync(new EnseignementViewModule());
            await LoadModuleAsync(new EditEnseignantModule(null));//! test
            await LoadModuleAsync(new EditEnseignantModule(new DAO.Enseignant("ChK", "un joli nom", "un merveilleux prénom", HOblig: 105.2f)));//!
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
                await RefreshAsync();
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.W)
            {
                var module = CurrentModule;
                if (module?.Closeable ?? false)
                    module.CloseModule();
            }
        }
    }
}