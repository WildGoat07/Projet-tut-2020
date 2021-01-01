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
        public int page;

        public AllEnseignants()
        {
            InitializeComponent();
            page = 0;
        }

        private void compo_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            App.Log(button.Tag);
        }
    }
}