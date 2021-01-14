using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditUeModule : Module
    {
        private readonly bool create;

        public EditUeModule(Ue? ue)
        {
            Content = new UI.EditUe(ue, this);
            Title = ue == null ? "Créer un ue" : $"Édition de {ue.libelle_ue}";
            create = ue != null;
        }

        public override bool Closeable => true;

        public override UI.EditUe Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}