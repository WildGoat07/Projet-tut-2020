﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IHorsCompDAO
    {
        /// <summary>
        /// Créé une nouvelle horsComp
        /// </summary>
        /// <param name="value">Détail de l'horsComp à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle horsComp</returns>
        async Task<HorsComp> CreateAsync(HorsComp value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé de nouvelles horsComp
        /// </summary>
        /// <param name="values">Détails des horsComp à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles horsComp</returns>
        Task<HorsComp[]> CreateAsync(IEnumerable<HorsComp> values);

        /// <summary>
        /// Supprime une horsComp
        /// </summary>
        /// <param name="value">HorsComp à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(HorsComp value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des horsComp
        /// </summary>
        /// <param name="value">HorsComp à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<HorsComp> value);

        /// <summary>
        /// Récupère toutes les horsComp
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les horsComp disponibles</returns>
        async Task<HorsComp[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une horsComp
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'horsComp correspondante à l'id</returns>
        async Task<HorsComp> GetByIdAsync(string id_ens, string id_comp, string annee) => (await GetByIdAsync(new[] { (id_ens, id_comp, annee) })).First();

        /// <summary>
        /// Récupère des horsComp
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les horsComp correspondantes à l'id</returns>
        Task<HorsComp[]> GetByIdAsync(IEnumerable<(string, string, string)> id);

        /// <summary>
        /// Récupère toutes les horsComp selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="year">Année concernée</param>
        /// <param name="comp">Composante exterieure</param>
        /// <param name="teacher">Enseignant concerné</param>
        /// <param name="CmHours">Min/max des heures de CM</param>
        /// <param name="EiHours">Min/max des heures de EI</param>
        /// <param name="equivalentHours">Min/max des heures équivalentes TD</param>
        /// <param name="PrjHours">Min/max des heures de projet</param>
        /// <param name="TdHours">Min/max des heures de TD</param>
        /// <param name="TpHours">Min/max des heures de TP</param>
        /// <param name="TplHours">Min/max des heures de TPL</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les horsComp filtrées disponibles</returns>
        Task<HorsComp[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? teacher = null, IEnumerable<string>? comp = null, IEnumerable<string>? year = null, (int?, int?)? CmHours = null, (int?, int?)? EiHours = null, (int?, int?)? TdHours = null, (int?, int?)? TpHours = null, (int?, int?)? TplHours = null, (int?, int?)? PrjHours = null, (float?, float?)? equivalentHours = null);

        /// <summary>
        /// Modifie une horsComp
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de l'horsComp</param>
        /// <param name="newValue">Nouvelle valeur de l'horsComp</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'horsComp modifiée</returns>
        async Task<HorsComp> UpdateAsync(HorsComp oldValue, HorsComp newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des horsComp
        /// </summary>
        /// <param name="values">Valeurs des horsComp</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les horsComp modifiées</returns>
        Task<HorsComp[]> UpdateAsync(IEnumerable<(HorsComp, HorsComp)> values);
    }
}