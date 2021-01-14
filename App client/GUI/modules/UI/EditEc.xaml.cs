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
            Categorie.Text = (ec.no_cat ?? 0).ToString();
        }

        public async Task RefreshAsync()
        {
            //on mets à jour les combobox pour les clés étrangères
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

            //TODO

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
                            //TODO
                            ));
                    else
                        //modification d'un ec
                        await App.Factory.EcDAO.UpdateAsync(initialValue, new DAO.Ec
                            (
                            //TODO
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