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
    /// Logique d'interaction pour EditEnseignant.xaml
    /// </summary>
    public partial class EditEnseignant : UserControl
    {
        private DAO.Enseignant? initialValue;
        private Module module;

        public EditEnseignant(DAO.Enseignant? ens, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = ens;
            if (ens != null)
            {
                //si on donne un enseignant, c'est qu'on doit modifier un enseignant existant
                id_ens.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                id_ens.ToolTip = "Modifier cette valeur peut être impossible si cet enseignant est lié ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si cet enseignant est lié ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                ens = new DAO.Enseignant("", "", "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            Nom.Text = ens.nom;
            Prenom.Text = ens.prenom;
            Fonction.Text = ens.fonction ?? "";
            HOblig.Text = (ens.HOblig ?? 0).ToString();
            HMax.Text = (ens.HMax ?? 0).ToString();
            CRCT.Text = (ens.CRCT ?? ' ').ToString();
            PES_PEDR.Text = (ens.PES_PEDR ?? ' ').ToString();
            id_ens.Text = ens.id_ens;
        }

        public async Task RefreshAsync()
        {
            //on mets à jour les combobox pour les clés étrangères
            var selection = id_comp.SelectedItem;
            id_comp.Items.Clear();
            id_comp.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.ComposanteDAO.GetAllAsync(9999, 0))
                id_comp.Items.Add(new ToStringOverrider<DAO.Composante>(item, item.nom_comp));
            id_comp.SelectedItem = selection;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            float dummy = 0;
            if (HOblig.Text.Length > 0 && !float.TryParse(HOblig.Text, out dummy))
                return "Heures obligatoires incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures obligatoires incorrectes (nombre négatif)";
            if (HMax.Text.Length > 0 && !float.TryParse(HMax.Text, out dummy))
                return "Heures maximales incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures maximales incorrectes (nombre négatif)";
            if (CRCT.Text.Length > 1)
                return "Le CRCT doit contenir 1 caractère";
            if (PES_PEDR.Text.Length > 1)
                return "Le PES_PEDR doit contenir 1 caractère";
            if (id_ens.Text.Length != 3)
                return "L'identifiant doit contenir 3 caractères";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un enseignant
                if (initialValue != null)
                    await App.Factory.EnseignantDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("L'enseignant est lié ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("L'enseignant n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
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
                    if (initialValue == null)
                        //création d'un enseignant
                        await App.Factory.EnseignantDAO.CreateAsync(new DAO.Enseignant
                            (
                                id_ens.Text,
                                Nom.Text,
                                Prenom.Text,
                                Fonction.Text.Length == 0 ? null : Fonction.Text,
                                HOblig.Text.Length == 0 ? null : float.Parse(HOblig.Text),
                                HMax.Text.Length == 0 ? null : float.Parse(HMax.Text),
                                CRCT.Text.Length == 0 ? null : CRCT.Text.First(),
                                PES_PEDR.Text.Length == 0 ? null : PES_PEDR.Text.First(),
                                id_comp.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Composante>)id_comp.SelectedItem).Value.id_comp
                            ));
                    else
                        //modification d'un enseignant
                        await App.Factory.EnseignantDAO.UpdateAsync(initialValue, new DAO.Enseignant
                            (
                                id_ens.Text,
                                Nom.Text,
                                Prenom.Text,
                                Fonction.Text.Length == 0 ? null : Fonction.Text,
                                HOblig.Text.Length == 0 ? null : float.Parse(HOblig.Text),
                                HMax.Text.Length == 0 ? null : float.Parse(HMax.Text),
                                CRCT.Text.Length == 0 ? null : CRCT.Text.First(),
                                PES_PEDR.Text.Length == 0 ? null : PES_PEDR.Text.First(),
                                id_comp.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Composante>)id_comp.SelectedItem).Value.id_comp
                            ));
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
                            MessageBox.Show("L'enseignant est lié ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("L'enseignant existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("L'enseignant n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}