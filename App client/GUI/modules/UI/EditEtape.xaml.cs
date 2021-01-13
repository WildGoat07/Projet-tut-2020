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
    /// Logique d'interaction pour EditEtape.xaml
    /// </summary>
    public partial class EditEtape : UserControl
    {
        private DAO.Etape? initialValue;
        private Module module;

        public EditEtape(DAO.Etape? et, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = et;
            if (et != null)
            {
                //si on donne un Etape, c'est qu'on doit modifier un Etape existant
                code_etape.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                code_etape.ToolTip = "Modifier cette valeur peut être impossible si cet Etape est lié ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si cet Etape est lié ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                et = new DAO.Etape("", 0, "", "FB0");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            code_etape.Text = et.code_etape;
            vet.Text = (et.vet).ToString();
            libelle_vet.Text = et.libelle_vet;
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

            var selection3 = vdi.SelectedItem;
            vdi.Items.Clear();
            vdi.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.DiplomeDAO.GetAllAsync(9999, 0))
                vdi.Items.Add(new ToStringOverrider<DAO.Diplome>(item, item.libelle_vdi));
            vdi.SelectedItem = selection3;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            int dummy = 0;
            if (code_etape.Text.Trim().Length == 0)
                return "Le code de l'étape ne peut être vide. ";
            else if (code_etape.Text.Trim().Length > 10)
                return "Le code de l'étape ne peut pas contenir plus de 10 caractères. ";
            if (vet.Text.Trim().Length > 0 && !int.TryParse(vet.Text.Trim(), out dummy))
                return "La version de l'étape doit être un nombre";
            else if (dummy <= 0)
                return "La version de l'étape ne peut être négative ou nulle";
            else if (dummy > 999)
                return "La version du diplôme doit contenir au plus un nombre à 3 chiffres";
            if (libelle_vet.Text.Trim().Length == 0)
                return "Le libellé de l'étape doit contenir au moins un caractère";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un Etape
                if (initialValue != null)
                    await App.Factory.EtapeDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("L'étape est liée ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("L'étape n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'un Etape
                        await App.Factory.EtapeDAO.CreateAsync(new DAO.Etape
                            (
                                code_etape.Text.Trim(),
                                int.Parse(vet.Text.Trim()),
                                libelle_vet.Text.Trim(),
                                id_comp.SelectedIndex < 1 ? "FBO" : ((ToStringOverrider<DAO.Composante>)id_comp.SelectedItem).Value.id_comp,
                                vdi.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Diplome>)vdi.SelectedItem).Value.code_diplome,
                                vdi.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Diplome>)vdi.SelectedItem).Value.vdi
                            ));
                    else
                        //modification d'un Etape
                        await App.Factory.EtapeDAO.UpdateAsync(initialValue, new DAO.Etape
                            (
                                code_etape.Text.Trim(),
                                int.Parse(vet.Text.Trim()),
                                libelle_vet.Text.Trim(),
                                id_comp.SelectedIndex < 1 ? "FB0" : ((ToStringOverrider<DAO.Composante>)id_comp.SelectedItem).Value.id_comp,
                                vdi.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Diplome>)vdi.SelectedItem).Value.code_diplome,
                                vdi.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Diplome>)vdi.SelectedItem).Value.vdi
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
                            MessageBox.Show("L'étape est liée ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("L'étape existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("L'étape n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}