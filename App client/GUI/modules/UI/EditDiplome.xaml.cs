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
    /// Logique d'interaction pour EditDiplome.xaml
    /// </summary>
    public partial class EditDiplome : UserControl
    {
        private DAO.Diplome? initialValue;
        private Module module;

        public EditDiplome(DAO.Diplome? dip, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = dip;
            if (dip != null)
            {
                //si on donne un Diplome, c'est qu'on doit modifier un existant
                code_diplome.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                code_diplome.ToolTip = "Modifier cette valeur peut être impossible si ce Diplome est lié ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si ce Diplome est lié ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                dip = new DAO.Diplome("", "", 0, "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            code_diplome.Text = dip.code_diplome;
            Libelle.Text = dip.libelle_diplome;
            Version.Text = (dip.vdi).ToString();
            Lib_vers.Text = dip.libelle_vdi;
            AnneeDeb.Text = (dip.annee_deb).ToString();
            AnneeFin.Text = (dip.annee_fin).ToString();
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            int dummy = 0;
            if (code_diplome.Text.Trim().Length == 0)
                return "Le code du diplôme ne peut être vide. ";
            else if (code_diplome.Text.Trim().Length > 100)
                return "Le code du diplôme ne peut pas contenir plus de 10 caractères. ";
            if (Libelle.Text.Trim().Length == 0)
                return "Le libellé du diplôme doit contenir au moins un caractère";
            if (Version.Text.Trim().Length > 0 && !int.TryParse(Version.Text.Trim(), out dummy))
                return "La version du diplôme doit être un nombre";
            else if (dummy <= 0)
                return "La version du diplôme ne peut être négative ou nulle";
            else if (dummy > 999)
                return "La version du diplôme doit contenir au plus un nombre à 3 chiffres";
            else if (dummy < 0)
                return "La version du diplôme ne peut être un nombre négatif";
            if (!(Lib_vers.Text.Trim().Length > 0))
                return "Le libellé de la version du diplôme doit contenir au moins un caractère";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un Diplome
                if (initialValue != null)
                    await App.Factory.DiplomeDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("Le diplôme est lié ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("Le diplôme n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'un Diplome
                        await App.Factory.DiplomeDAO.CreateAsync(new DAO.Diplome
                            (
                                code_diplome.Text.Trim(),
                                Libelle.Text.Trim(),
                                int.Parse(Version.Text),
                                Lib_vers.Text.Trim(),
                                AnneeDeb.Text.Length == 0 ? null : int.Parse(AnneeDeb.Text),
                                AnneeFin.Text.Length == 0 ? null : int.Parse(AnneeFin.Text)
                            ));
                    else
                        //modification d'un Diplome
                        await App.Factory.DiplomeDAO.UpdateAsync(initialValue, new DAO.Diplome
                            (
                                code_diplome.Text.Trim(),
                                Libelle.Text.Trim(),
                                int.Parse(Version.Text),
                                Lib_vers.Text.Trim(),
                                AnneeDeb.Text.Length == 0 ? null : int.Parse(AnneeDeb.Text),
                                AnneeFin.Text.Length == 0 ? null : int.Parse(AnneeFin.Text)
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
                            MessageBox.Show("Le diplôme est lié ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("Le diplôme existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("Le diplôme n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}