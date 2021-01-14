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
    /// Logique d'interaction pour EditCategorie.xaml
    /// </summary>
    public partial class EditCategorie : UserControl
    {
        private DAO.Categorie? initialValue;
        private Module module;

        public EditCategorie(DAO.Categorie? cat, Module mod)
        {
            InitializeComponent();
            module = mod;
            initialValue = cat;
            if (cat != null)
            {
                //si on donne une catégorie, c'est qu'on doit modifier une catégorie existante
                no_cat.BorderBrush = new SolidColorBrush(Colors.Red);
                id_text.Foreground = new SolidColorBrush(Colors.Red);
                no_cat.ToolTip = "Modifier cette valeur peut être impossible si cet catégorie est liée ailleurs";
                id_text.ToolTip = "Modifier cette valeur peut être impossible si cet catégorie est liée ailleurs";
                validation.Content = "Sauvegarder et quitter";
                suppression.Visibility = Visibility.Visible;
            }
            else
            {
                cat = new DAO.Categorie(0, "");
                validation.Content = "Créer";
            }
            //on rempli d'abord les controls
            no_cat.Text = cat.no_cat.ToString();
            Lib_categ.Text = cat.categorie;
        }

        public string? Validate()
        {
            //ici on renvoie un string de l'erreur, ou 'null' si aucune erreur
            int dummy = 0;

            if (!int.TryParse(no_cat.Text, out dummy))
                return "Numéro de catégorie incorrecte (pas un nombre)";
            else if (dummy < 0)
                return "Numéro de catégorie incorrecte (nombre négatif)";
            if (Lib_categ.Text.Trim().Length < 1)
                return "La catégorie ne doit pas être vide";

            return null;
        }

        private async void suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //suppression d'un catégorie
                if (initialValue != null)
                    await App.Factory.CategorieDAO.DeleteAsync(initialValue);
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
                        MessageBox.Show("La catégorie est liée ailleurs", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;

                    case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                        MessageBox.Show("La catégorie n'existe pas", "Erreur de suppression", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        //création d'un catégorie
                        await App.Factory.CategorieDAO.CreateAsync(new DAO.Categorie
                            (
                                int.Parse(no_cat.Text.Trim()),
                                Lib_categ.Text.Trim()
                            ));
                    else
                        //modification d'un catégorie
                        await App.Factory.CategorieDAO.UpdateAsync(initialValue, new DAO.Categorie
                            (
                                int.Parse(no_cat.Text.Trim()),
                                Lib_categ.Text.Trim()
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
                            MessageBox.Show("La catégorie est liée ailleurs", "Erreur de liaison", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.EXISTING_ENTRY:
                            MessageBox.Show("La catégorie existe déjà", "Erreur de duplication", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;

                        case DAO.DAOException.ErrorCode.MISSING_ENTRY:
                            MessageBox.Show("La catégorie n'existe pas", "Erreur de modification", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
        }
    }
}