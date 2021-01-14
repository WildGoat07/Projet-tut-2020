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
    /// Logique d'interaction pour EditAnneeUniv.xaml
    /// </summary>
    public partial class EditAnneeUniv : UserControl
    {
        private DAO.AnneeUniv? initialValue;
        private Module module;

        public EditAnneeUniv(DAO.AnneeUniv? annee, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = annee;
            if (annee != null)
            {
                //si on donne une année, c'est qu'on doit modifier une année existante
                annee_univ.ToolTip = "L'année doit s'écrire XXXX/ XXXX";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                annee = new DAO.AnneeUniv("");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            annee_univ.Text = annee.annee.ToString();
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            if (annee_univ.Text.Trim().Length != 9)
                return "L'année doit s'écrire XXXX/XXXX";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un année
                if (initialValue != null)
                    await App.Factory.AnneeUnivDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("L'année est liée ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("L'année n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'un année
                        await App.Factory.AnneeUnivDAO.CreateAsync(new DAO.AnneeUniv
                            (
                                annee_univ.Text.Trim()
                            ));
                    else
                        //modification d'un année
                        await App.Factory.AnneeUnivDAO.UpdateAsync(initialValue, new DAO.AnneeUniv
                            (
                                annee_univ.Text.Trim()
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
                            MessageBox.Show("L'année est liée ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("L'année existe déjà", "Erreur de duplianneeion", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("L'année n'existe pas", "Erreur de modifianneeion", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}