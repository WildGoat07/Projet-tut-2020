using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditComposanteModule : Module
    {
        private readonly bool create;

        public EditComposanteModule(Composante? comp)
        {
            Content = new UI.EditComposante(comp, this);
            Title = comp == null ? "Créer une composante" : $"Édition de {comp.nom_comp} ";
            create = comp != null;
        }

        public override bool Closeable => true;

        public override UI.EditComposante Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Task.CompletedTask;
    }
}