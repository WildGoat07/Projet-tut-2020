using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditAnneeUnivModule : Module
    {
        private readonly bool create;

        public EditAnneeUnivModule(AnneeUniv? annee_univ)
        {
            Content = new UI.EditAnneeUniv(annee_univ, this);
            Title = annee_univ == null ? "Créer une année universitaire" : $"Édition de l'année universitaire {annee_univ.annee} ";
            create = annee_univ != null;
        }

        public override bool Closeable => true;

        public override UI.EditAnneeUniv Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Task.CompletedTask;
    }
}