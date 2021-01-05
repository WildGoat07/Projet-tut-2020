using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IEcDAO
    {
        /// <summary>
        /// Créé une ec
        /// </summary>
        /// <param name="value">Détail de la ec à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle ec</returns>
        async Task<Ec> CreateAsync(Ec value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé des nouvelles ec
        /// </summary>
        /// <param name="values">Détails des ec à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles ec</returns>
        Task<Ec[]> CreateAsync(IEnumerable<Ec> values);

        /// <summary>
        /// Supprime une ec
        /// </summary>
        /// <param name="value">Ec à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Ec value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des ec
        /// </summary>
        /// <param name="values">Ec à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Ec> values);

        /// <summary>
        /// Récupère toutes les ec
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les ec disponibles</returns>
        async Task<Ec[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une ec
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La ec correspondante à l'id</returns>
        async Task<Ec> GetByIdAsync(string code) => (await GetByIdAsync(new[] { code })).First();

        /// <summary>
        /// Récupère des ec
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les ec correspondantes à l'id</returns>
        Task<Ec[]> GetByIdAsync(IEnumerable<string> code);

        /// <summary>
        /// Récupère tous les ec selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="search">Mots-clés à rechercher</param>
        /// <param name="Cnu">Cnu</param>
        /// <param name="owner">Père des ec filtrées</param>
        /// <param name="nature">Nature des ec filtrées</param>
        /// <param name="ue">Ue liée aux ec filtrées</param>
        /// <param name="category">Categorie des ec filtrées</param>
        /// <param name="CmHours">Min/max des heures de CM</param>
        /// <param name="EiHours">Min/max des heures de EI</param>
        /// <param name="PrjHours">Min/max des heures de projet</param>
        /// <param name="TdHours">Min/max des heures de TD</param>
        /// <param name="TpHours">Min/max des heures de TP</param>
        /// <param name="TplHours">Min/max des heures de TPL</param>
        /// <param name="NbEpr">Min/max du nombre d'épreuves</param>
        /// <param name="stepCount">Min/max du nombre d'épreuves</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les ec filtrées disponibles</returns>
        Task<Ec[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? Cnu = null, IEnumerable<int>? owner = null, IEnumerable<char>? nature = null, IEnumerable<int>? ue = null, IEnumerable<int>? category = null, (int?, int?)? CmHours = null, (int?, int?)? EiHours = null, (int?, int?)? TdHours = null, (int?, int?)? TpHours = null, (int?, int?)? TplHours = null, (int?, int?)? PrjHours = null, (int?, int?)? NbEpr = null, (int?, int?)? stepCount = null);

        /// <summary>
        /// Modifie une ec
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la ec</param>
        /// <param name="newValue">Nouvelle valeur de la ec</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La ec modifiée</returns>
        async Task<Ec> UpdateAsync(Ec oldValue, Ec newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des ec
        /// </summary>
        /// <param name="values">Valeurs des ec</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les ec modifiées</returns>
        Task<Ec[]> UpdateAsync(IEnumerable<(Ec, Ec)> values);
    }
}