using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditEcModule : Module
    {
        private readonly bool create;

        public EditEcModule(Ec? ec)
        {
            Content = new UI.EditEc(ec, this);
            Title = ec == null ? "Créer un ec" : $"Édition de {ec.libelle_ec}";
            create = ec != null;
        }

        public override bool Closeable => true;

        public override UI.EditEc Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}