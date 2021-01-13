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
    /// Logique d'interaction pour EditComposante.xaml
    /// </summary>
    public partial class EditComposante : UserControl
    {
        private DAO.Composante? initialValue;
        private Module module;

        public EditComposante(DAO.Composante? comp, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = comp;
            if (comp != null)
            {
                //si on donne une composante, c'est qu'on doit modifier une composante existante
                id_comp.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                id_comp.ToolTip = "Modifier cette valeur peut être impossible si cet composante est liée ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si cet composante est liée ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                comp = new DAO.Composante("", "", "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            id_comp.Text = comp.id_comp;
            Nom.Text = comp.nom_comp;
            Lieu.Text = comp.lieu_comp;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            if (id_comp.Text.Length != 3)
                return "L'identifiant doit contenir 3 caractères";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un compeignant
                if (initialValue != null)
                    await App.Factory.ComposanteDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("La composante est liée ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("La composante n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'une composante
                        await App.Factory.ComposanteDAO.CreateAsync(new DAO.Composante
                            (
                                id_comp.Text,
                                Nom.Text,
                                Lieu.Text
                            ));
                    else
                        //modification d'une composante
                        await App.Factory.ComposanteDAO.UpdateAsync(initialValue, new DAO.Composante
                            (
                                id_comp.Text,
                                Nom.Text,
                                Lieu.Text
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
                            MessageBox.Show("La composante est liée ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("La composante existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("La composante n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}