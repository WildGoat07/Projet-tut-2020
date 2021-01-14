using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    /// Logique d'interaction pour EditSemestre.xaml
    /// </summary>
    public partial class EditSemestre : UserControl
    {
        private DAO.Semestre? initialValue;
        private Module module;

        public EditSemestre(DAO.Semestre? sem, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = sem;
            if (sem != null)
            {
                //si on donne un semestre, c'est qu'on doit modifier un semestre existant
                code_sem.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                code_sem.ToolTip = "Modifier cette valeur peut être impossible si ce semestre est lié ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si ce semestre est lié ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                sem = new DAO.Semestre("", "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            code_sem.Text = sem.code_sem;
            libelle_sem.Text = sem.libelle_sem;
            no_sem.Text = (sem.no_sem ?? 0).ToString();
        }

        public async Task RefreshAsync()
        {
            //on mets à jour les combobox pour les clés étrangères
            var selection_etape = code_etape.SelectedItem;
            code_etape.Items.Clear();
            code_etape.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.EtapeDAO.GetAllAsync(9999, 0))
                code_etape.Items.Add(new ToStringOverrider<DAO.Etape>(item, item.code_etape));
            code_etape.SelectedItem = selection_etape;

            var selection_vet = vet.SelectedItem;
            vet.Items.Clear();
            vet.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.EtapeDAO.GetAllAsync(9999, 0))
                vet.Items.Add(new ToStringOverrider<DAO.Etape>(item, item.vet.ToString()));
            vet.SelectedItem = selection_vet;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            int dummy = 0;
            if (code_sem.Text.Trim().Length < 1)
                return "Le code semestre ne peut pas être vide";
            if (libelle_sem.Text.Trim().Length < 1)
                return "Le libelle semestre ne peut pas être vide";
            if (no_sem.Text.Trim().Length > 0 && !int.TryParse(no_sem.Text.Trim(), out dummy))
                return "numero semestre incorrect (pas un nombre)";
            else if (dummy < 0)
                return "numero semestre incorrect (nombre négatif)";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un semestre
                if (initialValue != null)
                    await App.Factory.SemestreDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("Le semestre est lié ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("Le semestre n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'un semsetre
                        await App.Factory.SemestreDAO.CreateAsync(new DAO.Semestre
                            (
                                code_sem.Text.Trim(),
                                libelle_sem.Text.Trim(),
                                no_sem.Text.Trim().Length == 0 ? null : int.Parse(no_sem.Text.Trim()),
                                code_etape.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Etape>)code_etape.SelectedItem).Value.code_etape,
                                vet.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Etape>)vet.SelectedItem).Value.vet
                            ));
                    else
                        //modification d'un semestre
                        await App.Factory.SemestreDAO.UpdateAsync(initialValue, new DAO.Semestre
                            (
                                code_sem.Text.Trim(),
                                libelle_sem.Text.Trim(),
                                no_sem.Text.Trim().Length == 0 ? null : int.Parse(no_sem.Text.Trim()),
                                code_etape.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Etape>)code_etape.SelectedItem).Value.code_etape,
                                vet.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Etape>)vet.SelectedItem).Value.vet
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
                            MessageBox.Show("Le semestre est lié ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("Le semestre existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("Le semestre n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}