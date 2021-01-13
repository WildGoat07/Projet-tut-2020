using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditCategorieModule : Module
    {
        private readonly bool create;

        public EditCategorieModule(Categorie? cat)
        {
            Content = new UI.EditCategorie(cat, this);
            Title = cat == null ? "Créer une catégorie" : $"Édition de {cat.categorie} ";
            create = cat != null;
        }

        public override bool Closeable => true;

        public override UI.EditCategorie Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Task.CompletedTask;
    }
}