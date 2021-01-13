using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditEnseignantModule : Module
    {
        private readonly bool create;

        public EditEnseignantModule(Enseignant? ens)
        {
            Content = new UI.EditEnseignant(ens, this);
            Title = ens == null ? "Créer un enseignant" : $"Édition de {ens.prenom} {ens.nom}";
            create = ens != null;
        }

        public override bool Closeable => true;

        public override UI.EditEnseignant Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}