using DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Color Accent { get; } = Color.FromRgb(39, 101, 189);

        public static ImageSource CloseTab
        {
            get
            {
                using var stream = new MemoryStream(GUI.Properties.Resources.closeTab);
                return LoadImage(stream);
            }
        }

        public static IDAOFactory Factory { get; } = null; //à mettre la DAO de l'API

        public static Color AlphaAccent(byte a) => Color.FromArgb(a, Accent.R, Accent.G, Accent.B);

        public static ImageSource LoadImage(Stream stream)
        {
            var img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = stream;
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
            img.Freeze();
            return img;
        }
    }
}