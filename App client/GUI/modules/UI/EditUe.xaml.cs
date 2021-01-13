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
    /// Logique d'interaction pour EditUe.xaml
    /// </summary>
    public partial class EditUe : UserControl
    {
        private DAO.Ue? initialValue;
        private Module module;

        public EditUe(DAO.Ue? ue, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = ue;
            if (ue != null)
            {
                //si on donne un ueeignant, c'est qu'on doit modifier un ueeignant existant
                code_ue.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                code_ue.ToolTip = "Modifier cette valeur peut être impossible si cet ue est liée ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si cet ue est liée ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                ue = new DAO.Ue("", "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            code_ue.Text = ue.code_ue;
            Lib_ue.Text = ue.libelle_ue;
            Nature.Text = (ue.nature ?? ' ').ToString();
            ECTS.Text = (ue.ECTS ?? 0).ToString();
        }

        public async Task RefreshAsync()
        {
            //on mets à jour les combobox pour les clés étrangères
            var selection_pere = code_ue_pere.SelectedItem;
            code_ue_pere.Items.Clear();
            code_ue_pere.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.UeDAO.GetAllAsync(9999, 0))
                code_ue_pere.Items.Add(new ToStringOverrider<DAO.Ue>(item, item.code_ue));
            code_ue_pere.SelectedItem = selection_pere;

            var selection_sem = code_sem.SelectedItem;
            code_sem.Items.Clear();
            code_sem.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.SemestreDAO.GetAllAsync(9999, 0))
                code_sem.Items.Add(new ToStringOverrider<DAO.Semestre>(item, item.code_sem));
            code_sem.SelectedItem = selection_sem;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            int dummy = 0;

            if (code_ue.Text.Trim().Length < 1)
                return "Le code ne peut pas être vide";
            if (Lib_ue.Text.Trim().Length < 1)
                return "Le libellé ne peut pas être vide";
            if (Nature.Text.Trim().Length > 1)
                return "La nature doit contenir 1 caractère";
            if (ECTS.Text.Trim().Length > 0 && !int.TryParse(ECTS.Text.Trim(), out dummy))
                return "ECTS incorrecte (pas un nombre)";
            else if (dummy < 0)
                return "ECTS incorrecte (nombre négatif)";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un ueeignant
                if (initialValue != null)
                    await App.Factory.UeDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("L'ue est liée ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("L'ue n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'une ue
                        await App.Factory.UeDAO.CreateAsync(new DAO.Ue
                            (
                                code_ue.Text.Trim(),
                                Lib_ue.Text.Trim(),
                                Nature.Text.Trim().Length == 0 ? null : Nature.Text.Trim().First(),
                                ECTS.Text.Trim().Length == 0 ? 2700 : int.Parse(ECTS.Text.Trim()),
                                code_ue_pere.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Ue>)code_ue_pere.SelectedItem).Value.code_ue_pere,
                                code_sem.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Semestre>)code_sem.SelectedItem).Value.code_sem

                            ));
                    else
                        //modification d'une ue
                        await App.Factory.UeDAO.UpdateAsync(initialValue, new DAO.Ue
                            (
                                code_ue.Text.Trim(),
                                Lib_ue.Text.Trim(),
                                Nature.Text.Trim().Length == 0 ? null : Nature.Text.Trim().First(),
                                ECTS.Text.Trim().Length == 0 ? 2700 : int.Parse(ECTS.Text.Trim()),
                                code_ue_pere.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Ue>)code_ue_pere.SelectedItem).Value.code_ue_pere,
                                code_sem.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Semestre>)code_sem.SelectedItem).Value.code_sem
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
                            MessageBox.Show("L'ue est liée ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("L'ue existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("L'ue n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}