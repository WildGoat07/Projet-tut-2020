using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.modules.UI
{
    /// <summary>
    /// Logique d'interaction pour Parametrage.xaml
    /// </summary>
    public partial class Parametrage : UserControl
    {
        private DAO.CompCourante? initialCompValue;
        private DAO.AnneeUniv? initialAnneeValue;
        private Module module;

        public Parametrage(Module mod)
        {
            InitializeComponent();
            validation.Content = "Sauvegarder et quitter";
            module = mod;
        }

        public async Task RefreshAsync()
        {
            //on mets à jour les combobox pour les clés étrangères
            var selection_comp = comp_courante.SelectedItem;
            comp_courante.Items.Clear();
            comp_courante.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.ComposanteDAO.GetAllAsync(9999, 0))
                comp_courante.Items.Add(new ToStringOverrider<DAO.Composante>(item, item.nom_comp));
            comp_courante.SelectedItem = selection_comp;

            var selection_annee = annee_univ.SelectedItem;
            annee_univ.Items.Clear();
            annee_univ.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.AnneeUnivDAO.GetAllAsync())
                annee_univ.Items.Add(new ToStringOverrider<DAO.AnneeUniv>(item, item.annee));
            annee_univ.SelectedItem = selection_annee;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            return null;
        }

        private async void validation_Click(object sender, RoutedEventArgs e)
        {
            var res = Validate();
            if (res != null)
                MessageBox.Show(res, "Erreur de validation des données", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    /*
                    if (initialCompValue != null)
                        //création d'une composante courante
                        await App.Factory.ComposanteDAO.SetCurrentAsync(new DAO.CompCourante
                            (
                                comp_courante.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Composante>)comp_courante.SelectedItem).Value.id_comp
                            ));

                    if (initialAnneeValue == null)
                        //création d'une composante courante
                        await App.Factory.AnneeUnivDAO. (new DAO.CompCourante
                            (
                                comp_courante.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Composante>)comp_courante.SelectedItem).Value.id_comp
                            ));
                    */
                    module.CloseModule();
                }
                catch (DAO.DAOException exc)
                {
                    switch (exc.Code)
                    {
                        case DAO.DAOException.ErrorCode.UNKNOWN:
                            MessageBox.Show(exc.Message, "Erreur inconnue", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.ENTRY_LINKED:
                            MessageBox.Show("La composante actuelle est lié ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("La composante actuelle existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("La composante actuelle n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}