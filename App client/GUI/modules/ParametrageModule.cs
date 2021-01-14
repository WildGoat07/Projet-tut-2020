using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class ParametrageModule : Module
    {
        public ParametrageModule()
        {
            Content = new UI.Parametrage(this);
            Title = "Parametrage";
        }

        public override bool Closeable => true;

        public override UI.Parametrage Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}