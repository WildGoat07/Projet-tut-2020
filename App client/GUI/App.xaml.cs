using DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Color Accent { get; } = Color.FromRgb(39, 101, 189);

        public static IDAOFactory Factory { get; } = null;

        public static Color AlphaAccent(byte a) => Color.FromArgb(a, Accent.R, Accent.G, Accent.B);
    }
}