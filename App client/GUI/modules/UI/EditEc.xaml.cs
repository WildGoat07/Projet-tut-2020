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
    /// Logique d'interaction pour EditEc.xaml
    /// </summary>
    public partial class EditEc : UserControl
    {
        private DAO.Ec? initialValue;
        private Module module;

        public EditEc(DAO.Ec? ec, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = ec;
            if (ec != null)
            {
                //si on donne un ec, c'est qu'on doit modifier un ec existant
                code_ec.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                code_ec.ToolTip = "Modifier cette valeur peut être impossible si cet ec est lié ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si cet ec est lié ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                ec = new DAO.Ec("", "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            code_ec.Text = ec.code_ec;
            Libelle.Text = ec.libelle_ec;
            Nature.Text = (ec.nature ?? ' ').ToString();
            HCM.Text = (ec.HCM ?? 0).ToString();
            HEI.Text = (ec.HEI ?? 0).ToString();
            HTD.Text = (ec.HTD ?? 0).ToString();
            HTP.Text = (ec.HTP ?? 0).ToString();
            HTPL.Text = (ec.HTPL ?? 0).ToString();
            HPRJ.Text = (ec.HPRJ ?? 0).ToString();
            NbEpr.Text = (ec.NbEpr ?? 0).ToString();
            CNU.Text = (ec.CNU ?? 0).ToString();
        }

        public async Task RefreshAsync()
        {
            //on mets à jour les combobox pour les clés étrangères
            var selection_categorie = categorie.SelectedItem;
            categorie.Items.Clear();
            categorie.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.CategorieDAO.GetAllAsync(9999, 0))
                categorie.Items.Add(new ToStringOverrider<DAO.Categorie>(item, item.categorie));
            categorie.SelectedItem = selection_categorie;

            var selection_code_ec_pere = code_ec_pere.SelectedItem;
            code_ec_pere.Items.Clear();
            code_ec_pere.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.EcDAO.GetAllAsync(9999, 0))
                code_ec_pere.Items.Add(new ToStringOverrider<DAO.Ec>(item, item.code_ec));
            code_ec_pere.SelectedItem = selection_code_ec_pere;

            var selection_code_ue = code_ue.SelectedItem;
            code_ue.Items.Clear();
            code_ue.Items.Add(new ToStringOverrider<string>("", "Aucune"));
            foreach (var item in await App.Factory.UeDAO.GetAllAsync(9999, 0))
                code_ue.Items.Add(new ToStringOverrider<DAO.Ue>(item, item.code_ue));
            code_ue.SelectedItem = selection_code_ue;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            int dummy = 0;
            if (code_ec.Text.Trim().Length < 1)
                return "Le code EC ne peut pas être vide";
            else if (code_ec.Text.Trim().Length > 10)
                return "Le code EC ne peut pas contenir plus de 10 caractères.";
            if (Libelle.Text.Trim().Length < 1)
                return "Le libellé ne peut pas être vide";
            if (Nature.Text.Trim().Length > 1)
                return "La nature doit contenir 1 caractère";
            if (HCM.Text.Trim().Length > 0 && !int.TryParse(HCM.Text, out dummy))
                return "Heures CM incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures CM incorrectes (nombre négatif)";
            else if (dummy > 999)
                return "Heures CM incorrectes (nombre a plus de 3 chiffres)";
            if (HEI.Text.Trim().Length > 0 && !int.TryParse(HEI.Text, out dummy))
                return "Heures HEI incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures HEI incorrectes (nombre négatif)";
            else if (dummy > 999)
                return "Heures HEI incorrectes (nombre a plus de 3 chiffres)";
            if (HTD.Text.Trim().Length > 0 && !int.TryParse(HTD.Text, out dummy))
                return "Heures TD incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures TD incorrectes (nombre négatif)";
            else if (dummy > 999)
                return "Heures TD incorrectes (nombre a plus de 3 chiffres)";
            if (HTP.Text.Trim().Length > 0 && !int.TryParse(HTP.Text, out dummy))
                return "Heures TP incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures TP incorrectes (nombre négatif)";
            else if (dummy > 999)
                return "Heures TP incorrectes (nombre a plus de 3 chiffres)";
            if (HTPL.Text.Trim().Length > 0 && !int.TryParse(HTPL.Text, out dummy))
                return "Heures TPL incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures TPL incorrectes (nombre négatif)";
            else if (dummy > 999)
                return "Heures TPL incorrectes (nombre a plus de 3 chiffres)";
            if (HPRJ.Text.Trim().Length > 0 && !int.TryParse(HPRJ.Text, out dummy))
                return "Heures Projet incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Heures Projet incorrectes (nombre négatif)";
            else if (dummy > 999)
                return "Heures Projet incorrectes (nombre a plus de 3 chiffres)";
            if (NbEpr.Text.Trim().Length > 0 && !int.TryParse(HTD.Text, out dummy))
                return "Nombre d'épreuves incorrectes (pas un nombre)";
            else if (dummy < 0)
                return "Nombre d'épreuves incorrectes (nombre négatif)";
            else if (dummy > 9)
                return "Nombre d'épreuves trop élevées";
            if (CNU.Text.Trim().Length > 0 && !int.TryParse(CNU.Text, out dummy))
                return "CNU incorrect (pas un nombre)";
            else if (dummy < 0)
                return "CNU incorrect (nombre négatif)";
            else if (dummy > 9999)
                return "CNU incorrect (plus de 4 chiffres)";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un ec
                if (initialValue != null)
                    await App.Factory.EcDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("L'ec est lié ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("L'ec n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'un ec
                        await App.Factory.EcDAO.CreateAsync(new DAO.Ec
                            (
                                code_ec.Text.Trim(),
                                Libelle.Text.Trim(),
                                Nature.Text.Trim().Length == 0 ? null : Nature.Text.First(),
                                HCM.Text.Trim().Length == 0 ? null : int.Parse(HCM.Text.Trim()),
                                HEI.Text.Trim().Length == 0 ? null : int.Parse(HEI.Text.Trim()),
                                HTD.Text.Trim().Length == 0 ? null : int.Parse(HTD.Text.Trim()),
                                HTP.Text.Trim().Length == 0 ? null : int.Parse(HTP.Text.Trim()),
                                HTPL.Text.Trim().Length == 0 ? null : int.Parse(HTPL.Text.Trim()),
                                HPRJ.Text.Trim().Length == 0 ? null : int.Parse(HPRJ.Text.Trim()),
                                NbEpr.Text.Trim().Length == 0 ? 1 : int.Parse(NbEpr.Text.Trim()),
                                CNU.Text.Trim().Length == 0 ? 2700 : int.Parse(CNU.Text.Trim()),
                                categorie.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Categorie>)categorie.SelectedItem).Value.no_cat,
                                code_ec_pere.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Ec>)code_ec_pere.SelectedItem).Value.code_ec,
                                code_ue.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Ue>)code_ue.SelectedItem).Value.code_ue
                            ));
                    else
                        //modification d'un ec
                        await App.Factory.EcDAO.UpdateAsync(initialValue, new DAO.Ec
                            (
                                code_ec.Text.Trim(),
                                Libelle.Text.Trim(),
                                Nature.Text.Trim().Length == 0 ? null : Nature.Text.First(),
                                HCM.Text.Trim().Length == 0 ? null : int.Parse(HCM.Text.Trim()),
                                HEI.Text.Trim().Length == 0 ? null : int.Parse(HEI.Text.Trim()),
                                HTD.Text.Trim().Length == 0 ? null : int.Parse(HTD.Text.Trim()),
                                HTP.Text.Trim().Length == 0 ? null : int.Parse(HTP.Text.Trim()),
                                HTPL.Text.Trim().Length == 0 ? null : int.Parse(HTPL.Text.Trim()),
                                HPRJ.Text.Trim().Length == 0 ? null : int.Parse(HPRJ.Text.Trim()),
                                NbEpr.Text.Trim().Length == 0 ? 1 : int.Parse(NbEpr.Text.Trim()),
                                CNU.Text.Trim().Length == 0 ? 2700 : int.Parse(CNU.Text.Trim()),
                                categorie.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Categorie>)categorie.SelectedItem).Value.no_cat,
                                code_ec_pere.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Ec>)code_ec_pere.SelectedItem).Value.code_ec,
                                code_ue.SelectedIndex < 1 ? null : ((ToStringOverrider<DAO.Ue>)code_ue.SelectedItem).Value.code_ue
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
                            MessageBox.Show("L'ec est lié ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("L'ec existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("L'ec n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}