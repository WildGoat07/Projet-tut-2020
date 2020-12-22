using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IDiplomeDAO
    {
        /// <summary>
        /// Créé un nouveau diplôme
        /// </summary>
        /// <param name="value">Détail du diplôme à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le nouveau diplôme</returns>
        async Task<Diplome> CreateAsync(Diplome value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé de nouveaux diplômes
        /// </summary>
        /// <param name="values">Détails des diplômes à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux diplômes</returns>
        Task<Diplome[]> CreateAsync(IEnumerable<Diplome> values);

        /// <summary>
        /// Supprime un diplôme
        /// </summary>
        /// <param name="value">Diplôme à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Diplome value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des diplômes
        /// </summary>
        /// <param name="values">Diplômes à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Diplome> values);

        /// <summary>
        /// Récupère tous les diplômes
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les diplômes disponibles</returns>
        async Task<Diplome[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère des diplômes
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les diplômes correspondants à l'id</returns>
        Task<Diplome[]> GetByIdAsync(IEnumerable<(string, int)> id);

        /// <summary>
        /// Récupère de diplôme
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le diplôme correspondant à l'id</returns>
        async Task<Diplome> GetByIdAsync(string code, int version) => (await GetByIdAsync(new[] { (code, version) })).First();

        /// <summary>
        /// Récupère tous les diplômes selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="search">Mots-clés à rechercher</param>
        /// <param name="begin">Années de départ du diplôme</param>
        /// <param name="end">Années de fin du diplôme</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les diplômes filtrés disponibles</returns>
        Task<Diplome[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? begin = null, IEnumerable<int>? end = null);

        /// <summary>
        /// Modifie un diplôme
        /// </summary>
        /// <param name="oldValue">Ancienne valeur du diplôme</param>
        /// <param name="newValue">Nouvelle valeur du diplôme</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le diplôme modifié</returns>
        async Task<Diplome> UpdateAsync(Diplome oldValue, Diplome newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des diplômes
        /// </summary>
        /// <param name="values">Valeurs des diplômes</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <exception cref="ArgumentException">Les tableaux sont de taille différente</exception>
        /// <returns>Les diplômes modifiés</returns>
        Task<Diplome[]> UpdateAsync(IEnumerable<(Diplome, Diplome)> values);
    }
}