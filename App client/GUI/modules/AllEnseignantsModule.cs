using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.modules
{
    internal class AllEnseignantsModule : Module
    {
        public AllEnseignantsModule() => Content = new AllEnseignants(this);

        public override AllEnseignants Content { get; }

        public override string Title => "Enseignants";

        public override async Task RefreshAsync()
        {
            Content.data.Items.Clear();
            foreach (var item in await App.Factory.EnseignantDAO.GetFilteredAsync(50, Content.Page))
                Content.data.Items.Add(new
                {
                    name = item.prenom,
                    surname = item.nom,
                    compo = string.IsNullOrWhiteSpace(item.id_comp) ? "aucune" : item.id_comp, //changer pour le label de composante
                    obj = item
                });
        }
    }
}