using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    internal class EditDiplomeModule : Module
    {
        private readonly bool create;

        public EditDiplomeModule(Diplome? dip)
        {
            Content = new UI.EditDiplome(dip, this);
            Title = dip == null ? "Créer un diplôme" : $"Édition de {dip.libelle_diplome}";
            create = dip != null;
        }

        public override bool Closeable => true;

        public override UI.EditDiplome Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Task.CompletedTask;
    }
}