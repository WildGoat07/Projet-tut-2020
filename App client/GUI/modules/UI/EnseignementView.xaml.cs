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

        public EnseignementView()
        {
            InitializeComponent();
            ensList = new List<ToStringOverrider<DAO.Enseignant>>();
        }

        public async Task RefreshAsync()
        {
            ensList.Clear();
            foreach (var item in await App.Factory.EnseignantDAO.GetAllAsync(9999, 0))
                ensList.Add(new ToStringOverrider<DAO.Enseignant>(item, $"{item.prenom} {item.nom}"));
            var diplome = diplomes.SelectedItem;
            diplomes.Items.Clear();
            foreach (var item in await App.Factory.DiplomeDAO.GetAllAsync(9999, 0))
                diplomes.Items.Add(new ToStringOverrider<DAO.Diplome>(item, item.libelle_vdi));
            diplomes.SelectedItem = diplome;
        }

        public async Task RefreshEC()
        {
            var ec = ecs.SelectedItem;
            ecs.Items.Clear();
            if (ues.SelectedItem is ToStringOverrider<DAO.Ue> ue)
            {
                foreach (var item in await App.Factory.EcDAO.GetFilteredAsync(9999, 0, ue: new[] { ue.Value.code_ue }))
                    ecs.Items.Add(new ToStringOverrider<DAO.Ec>(item, item.libelle_ec));
            }
            ecs.SelectedItem = ec;
        }

        public async Task RefreshEtapes()
        {
            var etape = etapes.SelectedItem;
            etapes.Items.Clear();
            if (diplomes.SelectedItem is ToStringOverrider<DAO.Diplome> diplome)
            {
                foreach (var item in await App.Factory.EtapeDAO.GetFilteredAsync(9999, 0, diplome: new[] { (diplome.Value.code_diplome, diplome.Value.vdi) }))
                    etapes.Items.Add(new ToStringOverrider<DAO.Etape>(item, item.libelle_vet));
            }
            etapes.SelectedItem = etape;
        }

        public async Task RefreshSemestres()
        {
            var semestre = semestres.SelectedItem;
            semestres.Items.Clear();
            if (etapes.SelectedItem is ToStringOverrider<DAO.Etape> etape)
            {
                foreach (var item in await App.Factory.SemestreDAO.GetFilteredAsync(9999, 0, step: new[] { (etape.Value.code_etape, etape.Value.vet) }))
                    semestres.Items.Add(new ToStringOverrider<DAO.Semestre>(item, item.libelle_sem));
            }
            semestres.SelectedItem = semestre;
        }

        public async Task RefreshUE()
        {
            var ue = ues.SelectedItem;
            ues.Items.Clear();
            if (semestres.SelectedItem is ToStringOverrider<DAO.Semestre> semestre)
            {
                foreach (var item in await App.Factory.UeDAO.GetFilteredAsync(9999, 0, semester: new[] { semestre.Value }))
                    ues.Items.Add(new ToStringOverrider<DAO.Ue>(item, item.libelle_ue));
            }
            ues.SelectedItem = ue;
        }

        private async void diplomes_SelectionChanged(object sender, SelectionChangedEventArgs e) => await RefreshEtapes();

        private async void etapes_SelectionChanged(object sender, SelectionChangedEventArgs e) => await RefreshSemestres();

        private async void semestres_SelectionChanged(object sender, SelectionChangedEventArgs e) => await RefreshUE();

        private async void ues_SelectionChanged(object sender, SelectionChangedEventArgs e) => await RefreshEC();
    }
}