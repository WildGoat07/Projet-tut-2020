using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditEtapeModule : Module
    {
        private readonly bool create;

        public EditEtapeModule(Etape? et)
        {
            Content = new UI.EditEtape(et, this);
            Title = et == null ? "Créer une étape" : $"Édition de {et.libelle_vet}";
            create = et != null;
        }

        public override bool Closeable => true;

        public override UI.EditEtape Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}