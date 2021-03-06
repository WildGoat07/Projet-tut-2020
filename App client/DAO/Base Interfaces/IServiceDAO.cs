﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IServiceDAO
    {
        /// <summary>
        /// Créé un nouveau service
        /// </summary>
        /// <param name="value">Détail du service à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le nouveau service</returns>
        async Task<Service> CreateAsync(Service value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé de nouveaux services
        /// </summary>
        /// <param name="values">Détails des services à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux services</returns>
        Task<Service[]> CreateAsync(IEnumerable<Service> values);

        /// <summary>
        /// Supprime un service
        /// </summary>
        /// <param name="value">Service à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Service value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des services
        /// </summary>
        /// <param name="value">Services à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Service> value);

        /// <summary>
        /// Récupère tous les services
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les services disponibles</returns>
        async Task<Service[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère un service
        /// </summary>
        /// <param name="year">Année du service</param>
        /// <param name="teacher">Enseignant du service</param>
        /// <param name="ec">EC du service</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le service correspondant à l'id</returns>
        async Task<Service> GetByIdAsync(string teacher, string ec, string year) => (await GetByIdAsync(new[] { (teacher, ec, year) })).First();

        /// <summary>
        /// Récupère des services
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les services correspondants à l'id</returns>
        Task<Service[]> GetByIdAsync(IEnumerable<(string, string, string)> id);

        /// <summary>
        /// Récupère tous les services selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="CmNumber">Min/max du nombre de cours de CM</param>
        /// <param name="EiNumber">Min/max du nombre de cours de EI</param>
        /// <param name="PrjNumber">Min/max du nombre de cours de projet</param>
        /// <param name="TdNumber">Min/max du nombre de cours de TD</param>
        /// <param name="TplNumber">Min/max du nombre de cours de TPL</param>
        /// <param name="TpNumber">Min/max du nombre de cours de TP</param>
        /// <param name="equivalentHours">Min/max du nombre d'heures équivalentes</param>
        /// <param name="ec">EC rattachée aux services</param>
        /// <param name="teacher">Enseignant rataché aux services</param>
        /// <param name="year">Année des services</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les services filtrés disponibles</returns>
        Task<Service[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? teacher = null, IEnumerable<string>? ec = null, IEnumerable<string>? year = null, (int?, int?)? CmNumber = null, (int?, int?)? EiNumber = null, (int?, int?)? TdNumber = null, (int?, int?)? TpNumber = null, (int?, int?)? TplNumber = null, (int?, int?)? PrjNumber = null, (int?, int?)? equivalentHours = null);

        /// <summary>
        /// Modifie un service
        /// </summary>
        /// <param name="oldValue">Ancienne valeur du service</param>
        /// <param name="newValue">Nouvelle valeur du service</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le service modifié</returns>
        async Task<Service> UpdateAsync(Service oldValue, Service newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des services
        /// </summary>
        /// <param name="values">Valeurs des services</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les services modifiés</returns>
        Task<Service[]> UpdateAsync(IEnumerable<(Service, Service)> values);
    }
}