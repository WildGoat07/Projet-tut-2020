using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EnseignementViewModule : DateDepedantModule
    {
        public EnseignementViewModule()
        {
            Content = new UI.EnseignementView();
        }

        public override bool Closeable => false;

        public override UI.EnseignementView Content { get; }

        public override string Title => "Enseignements";

        public override void DateChanged(AnneeUniv? year)
        {
        }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}