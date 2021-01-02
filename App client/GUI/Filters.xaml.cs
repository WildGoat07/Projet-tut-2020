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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Logique d'interaction pour Filters.xaml
    /// </summary>
    public partial class FiltersUI : Window
    {
        public FiltersUI(string? search, IEnumerable<filters.IFilter> filters, IEnumerable<Order> orders, Order? currOrder, bool reverse)
        {
            InitializeComponent();
            Result = (search, filters, null, false);
            this.search.Text = search ?? "";
            searchGrid.Visibility = search == null ? Visibility.Collapsed : Visibility.Visible;
            foreach (var item in filters)
                this.filters.Children.Add(new GroupFilter(item));
            order.Items.Add("Par défaut");
            foreach (var item in orders)
                order.Items.Add(item);
            if (currOrder == null)
                order.SelectedIndex = 0;
            else
                order.SelectedItem = order;
            this.reverse.IsChecked = reverse;
        }

        public (string?, IEnumerable<filters.IFilter>, Order?, bool) Result { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in filters.Children.Cast<GroupFilter>())
            {
                string? error;
                if ((error = item.Validate()) != null)
                {
                    MessageBox.Show(error, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            Result =
                (
                    string.IsNullOrWhiteSpace(search.Text) ? null : search.Text,
                    from filter in filters.Children.Cast<GroupFilter>()
                    select filter.GetFilter(),
                    order.SelectedIndex > 1 ? order.SelectedItem as Order : null,
                    reverse.IsChecked ?? false
                );
            DialogResult = true;
        }
    }
}