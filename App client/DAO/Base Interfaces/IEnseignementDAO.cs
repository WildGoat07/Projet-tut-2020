﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IEnseignementDAO
    {
        /// <summary>
        /// Créé un nouvel enseignement
        /// </summary>
        /// <param name="value">Détail de l'enseignement à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le nouvel enseignement</returns>
        async Task<Enseignement> CreateAsync(Enseignement value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé de nouveaux enseignements
        /// </summary>
        /// <param name="values">Détails des enseignements à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux enseignements</returns>
        Task<Enseignement[]> CreateAsync(IEnumerable<Enseignement> values);

        /// <summary>
        /// Supprime un enseignement
        /// </summary>
        /// <param name="value">Enseignement à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Enseignement value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des enseignements
        /// </summary>
        /// <param name="value">Enseignements à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Enseignement> value);

        /// <summary>
        /// Récupère tous les enseignements
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les enseignements disponibles</returns>
        async Task<Enseignement[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère un enseignement
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'enseignement correspondant à l'id</returns>
        async Task<Enseignement> GetByIdAsync(string code, string year) => (await GetByIdAsync(new[] { (code, year) })).First();

        /// <summary>
        /// Récupère un enseignement
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'enseignement correspondant à l'id</returns>
        Task<Enseignement[]> GetByIdAsync(IEnumerable<(string, string)> code);

        /// <summary>
        /// Récupère tous les enseignements selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="CmGroups">Min/max du nombre de groupes de CM</param>
        /// <param name="CmGroupsSer">Min/max du nombre de groupes de CM servis</param>
        /// <param name="ec">Code de l'EC des enseignements</param>
        /// <param name="EiGroups">Min/max du nombre de groupes de EI</param>
        /// <param name="EiGroupsSer">Min/max du nombre de groupes de EI servis</param>
        /// <param name="expectedQuantity">Min/max de l'effectif prévu</param>
        /// <param name="PrjGroups">Min/max du nombre de groupes de projet</param>
        /// <param name="PrjGroupsSer">Min/max du nombre de groupes de projet servis</param>
        /// <param name="realQuantity">Min/max de l'effectif réel</param>
        /// <param name="TdGroups">Min/max du nombre de groupes de TD</param>
        /// <param name="TdGroupsSer">Min/max du nombre de groupes de TD servis</param>
        /// <param name="TpGroups">Min/max du nombre de groupes de TP</param>
        /// <param name="TpGroupsSer">Min/max du nombre de groupes de TP servis</param>
        /// <param name="TplGroups">Min/max du nombre de groupes de TPL</param>
        /// <param name="TplGroupsSer">Min/max du nombre de groupes de TPL servis</param>
        /// <param name="year">Année des enseignements</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les enseignements filtrés disponibles</returns>
        Task<Enseignement[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? ec = null, IEnumerable<string>? year = null, (int?, int?)? expectedQuantity = null, (int?, int?)? realQuantity = null, (int?, int?)? CmGroups = null, (int?, int?)? EiGroups = null, (int?, int?)? TdGroups = null, (int?, int?)? TpGroups = null, (int?, int?)? TplGroups = null, (int?, int?)? PrjGroups = null, (int?, int?)? CmGroupsSer = null, (int?, int?)? EiGroupsSer = null, (int?, int?)? TdGroupsSer = null, (int?, int?)? TpGroupsSer = null, (int?, int?)? TplGroupsSer = null, (int?, int?)? PrjGroupsSer = null);

        /// <summary>
        /// Modifie un enseignement
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de l'enseignement</param>
        /// <param name="newValue">Nouvelle valeur de l'enseignement</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'enseignement modifié</returns>
        async Task<Enseignement> UpdateAsync(Enseignement oldValue, Enseignement newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des enseignements
        /// </summary>
        /// <param name="values">Valeurs des enseignements</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les enseignements modifiés</returns>
        Task<Enseignement[]> UpdateAsync(IEnumerable<(Enseignement, Enseignement)> values);
    }
}