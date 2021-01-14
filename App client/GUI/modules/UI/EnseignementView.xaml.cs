using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour EnseignementView.xaml
    /// </summary>
    public partial class EnseignementView : UserControl
    {
        public List<ToStringOverrider<DAO.Enseignant>> ensList;

        public EnseignementView(Func<DAO.AnneeUniv> getYear)
        {
            InitializeComponent();
            ensList = new List<ToStringOverrider<DAO.Enseignant>>();
            CurrDiplome = null;
            CurrEtape = null;
            CurrSemestre = null;
            CurrUe = null;
            CurrEc = null;
            GetYear = getYear;
        }

        private DAO.Diplome? CurrDiplome { get; set; }
        private DAO.Ec? CurrEc { get; set; }
        private DAO.Etape? CurrEtape { get; set; }
        private DAO.Semestre? CurrSemestre { get; set; }
        private DAO.Ue? CurrUe { get; set; }
        private Func<DAO.AnneeUniv> GetYear { get; }

        public async Task RefreshAsync()
        {
            ensList.Clear();
            foreach (var item in await App.Factory.EnseignantDAO.GetAllAsync(9999, 0))
                ensList.Add(new ToStringOverrider<DAO.Enseignant>(item, $"{item.prenom} {item.nom}"));
            var diplome = diplomes.SelectedItem;
            CurrDiplome = null;
            diplomes.Items.Clear();
            foreach (var item in await App.Factory.DiplomeDAO.GetAllAsync(9999, 0))
                diplomes.Items.Add(new ToStringOverrider<DAO.Diplome>(item, item.libelle_vdi));
            diplomes.SelectedItem = diplome;
        }

        public async Task RefreshDisplay()
        {
            if (CurrEc == null)
            {
                grCM.Text = "";
                grEI.Text = "";
                grTD.Text = "";
                grTP.Text = "";
                nbCM.Text = "";
                nbEI.Text = "";
                nbTD.Text = "";
                nbTP.Text = "";
                equTD.Text = "";
                saveButton.IsEnabled = false;
            }
            else
            {
                try
                {
                    var ens = await App.Factory.EnseignementDAO.GetByIdAsync(CurrEc.code_ec, GetYear().annee);

                    saveButton.IsEnabled = true;
                    nbCM.Text = CurrEc.HCM?.ToString();
                    nbEI.Text = CurrEc.HEI?.ToString();
                    nbTD.Text = CurrEc.HTD?.ToString();
                    nbTP.Text = CurrEc.HEI?.ToString();
                    grCM.Text = ens.GpCMSer?.ToString();
                    grEI.Text = ens.GpEISer?.ToString();
                    grTD.Text = ens.GpTDSer?.ToString();
                    grTP.Text = ens.GpTPSer?.ToString();
                    equTD.Text = ((ens.GpCMSer ?? 0) * (CurrEc.HCM ?? 0) * 1.5f + (CurrEc.HEI ?? 0) * (ens.GpEISer ?? 0) * (7f / 6f) + (CurrEc.HTD ?? 0) * (ens.GpTDSer ?? 0) + (CurrEc.HTP ?? 0) * (ens.GpTPSer ?? 0)).ToString("0.##");
                }
                catch (DAO.DAOException exc)
                {
                    MessageBox.Show(exc.Message, exc.Code switch
                    {
                        DAO.DAOException.ErrorCode.UNKNOWN => "Erreur inconnue",
                        DAO.DAOException.ErrorCode.MISSING_ENTRY => "Aucune entrée trouvée",
                        _ => ""
                    }, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public async Task RefreshEC()
        {
            var ec = ecs.SelectedItem;
            CurrEc = null;
            ecs.Items.Clear();
            if (ues.SelectedItem is ToStringOverrider<DAO.Ue> ue)
                foreach (var item in await App.Factory.EcDAO.GetFilteredAsync(9999, 0, ue: new[] { ue.Value.code_ue }))
                    ecs.Items.Add(new ToStringOverrider<DAO.Ec>(item, item.libelle_ec));
            ecs.SelectedItem = ec;
        }

        public async Task RefreshEtapes()
        {
            var etape = etapes.SelectedItem;
            CurrEtape = null;
            etapes.Items.Clear();
            if (diplomes.SelectedItem is ToStringOverrider<DAO.Diplome> diplome)
                foreach (var item in await App.Factory.EtapeDAO.GetFilteredAsync(9999, 0, diplome: new[] { (diplome.Value.code_diplome, diplome.Value.vdi) }))
                    etapes.Items.Add(new ToStringOverrider<DAO.Etape>(item, item.libelle_vet));
            etapes.SelectedItem = etape;
        }

        public async Task RefreshSemestres()
        {
            var semestre = semestres.SelectedItem;
            CurrSemestre = null;
            semestres.Items.Clear();
            if (etapes.SelectedItem is ToStringOverrider<DAO.Etape> etape)
                foreach (var item in await App.Factory.SemestreDAO.GetFilteredAsync(9999, 0, step: new[] { (etape.Value.code_etape, etape.Value.vet) }))
                    semestres.Items.Add(new ToStringOverrider<DAO.Semestre>(item, item.libelle_sem));
            semestres.SelectedItem = semestre;
        }

        public async Task RefreshUE()
        {
            var ue = ues.SelectedItem;
            CurrUe = null;
            ues.Items.Clear();
            if (semestres.SelectedItem is ToStringOverrider<DAO.Semestre> semestre)
                foreach (var item in await App.Factory.UeDAO.GetFilteredAsync(9999, 0, semester: new[] { semestre.Value }))
                    ues.Items.Add(new ToStringOverrider<DAO.Ue>(item, item.libelle_ue));
            ues.SelectedItem = ue;
        }

        public async Task SaveEC()
        {
            if (CurrEc != null)
            {
                var err = ValidateEC();
                if (err != null)
                    MessageBox.Show(err, "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    try
                    {
                        var ens = await App.Factory.EnseignementDAO.GetByIdAsync(CurrEc.code_ec, GetYear().annee);
                        var t1 = App.Factory.EcDAO.UpdateAsync(CurrEc, CurrEc with
                        {
                            HCM = nbCM.Text.Length > 0 ? int.Parse(nbCM.Text) : null,
                            HEI = nbEI.Text.Length > 0 ? int.Parse(nbEI.Text) : null,
                            HTD = nbTD.Text.Length > 0 ? int.Parse(nbTD.Text) : null,
                            HTP = nbTP.Text.Length > 0 ? int.Parse(nbTP.Text) : null
                        });
                        var t2 = App.Factory.EnseignementDAO.UpdateAsync(ens, ens with
                        {
                            GpCMSer = grCM.Text.Length > 0 ? int.Parse(grCM.Text) : null,
                            GpEISer = grEI.Text.Length > 0 ? int.Parse(grEI.Text) : null,
                            GpTDSer = grTD.Text.Length > 0 ? int.Parse(grTD.Text) : null,
                            GpTPSer = grTP.Text.Length > 0 ? int.Parse(grTP.Text) : null
                        });

                        await t1;
                        await t2;
                    }
                    catch (DAO.DAOException exc)
                    {
                        MessageBox.Show(exc.Message, exc.Code switch
                        {
                            DAO.DAOException.ErrorCode.UNKNOWN => "Erreur inconnue",
                            DAO.DAOException.ErrorCode.MISSING_ENTRY => "Aucune entrée trouvée",
                            _ => ""
                        }, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        public string? ValidateEC()
        {
            int dummy;
            if (!int.TryParse(grCM.Text, out dummy) && grCM.Text.Length > 0)
                return "Groupes CM incorrect";
            if (dummy < 0)
                return "Groupes CM négatif";
            if (!int.TryParse(grEI.Text, out dummy) && grEI.Text.Length > 0)
                return "Groupes EI incorrect";
            if (dummy < 0)
                return "Groupes EI négatif";
            if (!int.TryParse(grTD.Text, out dummy) && grTD.Text.Length > 0)
                return "Groupes TD incorrect";
            if (dummy < 0)
                return "Groupes TD négatif";
            if (!int.TryParse(grTP.Text, out dummy) && grTP.Text.Length > 0)
                return "Groupes TP incorrect";
            if (dummy < 0)
                return "Groupes TP négatif";

            if (!int.TryParse(nbCM.Text, out dummy) && nbCM.Text.Length > 0)
                return "Heures CM incorrect";
            if (dummy < 0)
                return "Heures CM négatif";
            if (!int.TryParse(nbEI.Text, out dummy) && nbEI.Text.Length > 0)
                return "Heures EI incorrect";
            if (dummy < 0)
                return "Heures EI négatif";
            if (!int.TryParse(nbTD.Text, out dummy) && nbTD.Text.Length > 0)
                return "Heures TD incorrect";
            if (dummy < 0)
                return "Heures TD négatif";
            if (!int.TryParse(nbTP.Text, out dummy) && nbTP.Text.Length > 0)
                return "Heures TP incorrect";
            if (dummy < 0)
                return "Heures TP négatif";

            return null;
        }

        private async void diplomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((diplomes.SelectedItem as ToStringOverrider<DAO.Diplome>)?.Value != CurrDiplome)
            {
                CurrDiplome = (diplomes.SelectedItem as ToStringOverrider<DAO.Diplome>)?.Value;
                await RefreshEtapes();
            }
        }

        private async void ecs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ecs.SelectedItem as ToStringOverrider<DAO.Ec>)?.Value != CurrEc)
            {
                CurrEc = (ecs.SelectedItem as ToStringOverrider<DAO.Ec>)?.Value;
                await RefreshDisplay();
            }
        }

        private async void etapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((etapes.SelectedItem as ToStringOverrider<DAO.Etape>)?.Value != CurrEtape)
            {
                CurrEtape = (etapes.SelectedItem as ToStringOverrider<DAO.Etape>)?.Value;
                await RefreshSemestres();
            }
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e) => await SaveEC();

        private async void semestres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((semestres.SelectedItem as ToStringOverrider<DAO.Semestre>)?.Value != CurrSemestre)
            {
                CurrSemestre = (semestres.SelectedItem as ToStringOverrider<DAO.Semestre>)?.Value;
                await RefreshUE();
            }
        }

        private async void ues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ues.SelectedItem as ToStringOverrider<DAO.Ue>)?.Value != CurrUe)
            {
                CurrUe = (ues.SelectedItem as ToStringOverrider<DAO.Ue>)?.Value;
                await RefreshEC();
            }
        }
    }
}