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

namespace GUI.modules
{
    /// <summary>
    /// Logique d'interaction pour AllEnseignants.xaml
    /// </summary>
    public partial class AllEnseignants : UserControl
    {
        public AllEnseignants(Module module, IDictionary<string, filters.IFilter>? preset = null)
        {
            InitializeComponent();
            Module = module;
            Page = 0;
            Filters = new();
            Order = null;
            ReverseOrder = false;
            if (preset == null)
                preset = new Dictionary<string, filters.IFilter>();
            if (preset.ContainsKey("id_ens"))
                Filters.Add("id_ens", (filters.Values)preset["id_ens"] with { Name = "Identifiant" });
            else
                Filters.Add("id_ens", new filters.Values { Name = "Identifiant" });
            if (preset.ContainsKey("fonction"))
                Filters.Add("fonction", (filters.Values)preset["fonction"] with { Name = "Fonction" });
            else
                Filters.Add("fonction", new filters.Values { Name = "Fonction" });
            if (preset.ContainsKey("CRCT"))
                Filters.Add("CRCT", (filters.Values)preset["CRCT"] with { Name = "CRCT" });
            else
                Filters.Add("CRCT", new filters.Values { Name = "CRCT" });
            if (preset.ContainsKey("PES_PEDR"))
                Filters.Add("PES_PEDR", (filters.Values)preset["PES_PEDR"] with { Name = "PES_PEDR" });
            else
                Filters.Add("PES_PEDR", new filters.Values { Name = "PES_PEDR" });
            if (preset.ContainsKey("id_comp"))
                Filters.Add("id_comp", (filters.ListValues)preset["id_comp"] with { Name = "Composante" });
            else
                Filters.Add("id_comp", new filters.ListValues { Name = "Composante" });
            if (preset.ContainsKey("HOblig"))
                Filters.Add("HOblig", (filters.FloatRange)preset["HOblig"] with { Name = "Heures obligatoires" });
            else
                Filters.Add("HOblig", new filters.FloatRange { Name = "Heures obligatoires" });
            if (preset.ContainsKey("HMax"))
                Filters.Add("HMax", (filters.FloatRange)preset["HMax"] with { Name = "Heures maximales" });
            else
                Filters.Add("HMax", new filters.FloatRange { Name = "Heures maximales" });
        }

        public Dictionary<string, filters.IFilter> Filters { get; }
        public Order? Order { get; private set; }
        public int Page { get; private set; }
        public bool ReverseOrder { get; private set; }
        public string Search { get => search.Text; private set => search.Text = value; }
        private Module Module { get; }

        private Order[] Orders { get; } = new[]
            {
                new Order
                {
                    Header = "Identifiant",
                    Value = "id_ens"
                },
                new Order
                {
                    Header = "Nom",
                    Value = "nom"
                },
                new Order
                {
                    Header = "Prénom",
                    Value = "prenom"
                },
                new Order
                {
                    Header = "Fonction",
                    Value = "fonction"
                },
                new Order
                {
                    Header = "Heures obligatoires",
                    Value = "HOblig"
                },
                new Order
                {
                    Header = "Heures maximales",
                    Value = "HMax"
                },
                new Order
                {
                    Header = "CRCT",
                    Value = "CRCT"
                },
                new Order
                {
                    Header = "PES_PEDR",
                    Value = "PES_PEDR"
                }
            };

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FiltersUI(Search, Filters.Values, Orders, Order, ReverseOrder);
            if (dialog.ShowDialog() ?? false)
            {
                Search = dialog.Result.Item1 ?? "";
                foreach (var item in dialog.Result.Item2)
                    Filters[Filters.First(f => f.Value.Name == item.Name).Key] = item;
                Order = dialog.Result.Item3;
                ReverseOrder = dialog.Result.Item4;
                await Module.RefreshAsync();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Module.RefreshAsync();
        }

        private void compo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            App.Log(button.Tag);
        }
    }
}