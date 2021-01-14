using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    public class EditSemestreModule : Module
    {
        private readonly bool create;

        public EditSemestreModule(Semestre? sem)
        {
            Content = new UI.EditSemestre(sem, this);
            Title = sem == null ? "Créer un semestre" : $"Édition de {sem.libelle_sem}";
            create = sem != null;
        }
            
        public override bool Closeable => true;

        public override UI.EditSemestre Content { get; }

        public override string Title { get; }

        public override async Task RefreshAsync() => await Content.RefreshAsync();
    }
}
