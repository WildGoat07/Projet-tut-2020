using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IEtapeDAO
    {
        /// <summary>
        /// Créé une nouvelle étape
        /// </summary>
        /// <param name="value">Détail de l'étape à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle étape</returns>
        async Task<Etape> CreateAsync(Etape value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé de nouvelles étapes
        /// </summary>
        /// <param name="values">Détails des étapes à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles étapes</returns>
        Task<Etape[]> CreateAsync(IEnumerable<Etape> values);

        /// <summary>
        /// Supprime une étape
        /// </summary>
        /// <param name="value">Étape à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Etape value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des étapes
        /// </summary>
        /// <param name="value">Étapes à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Etape> value);

        /// <summary>
        /// Récupère toutes les étapes
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les étape disponibles</returns>
        async Task<Etape[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une étape
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'étape correspondante à l'id</returns>
        async Task<Etape> GetByIdAsync(string code, int version) => (await GetByIdAsync(new[] { (code, version) })).First();

        /// <summary>
        /// Récupère des étapes
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les étapes correspondantes à l'id</returns>
        Task<Etape[]> GetByIdAsync(IEnumerable<(string, int)> id);

        /// <summary>
        /// Récupère toutes les étapes selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="search">Mots-clés à rechercher</param>
        /// <param name="comp">Composante des étapes</param>
        /// <param name="diplome">Diplôme des étapes</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Toutes les étape filtrées disponibles</returns>
        Task<Etape[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<string>? comp = null, IEnumerable<(string, int)>? diplome = null);

        /// <summary>
        /// Modifie une étape
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de l'étape</param>
        /// <param name="newValue">Nouvelle valeur de l'étape</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'étape modifiée</returns>
        async Task<Etape> UpdateAsync(Etape oldValue, Etape newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des étapes
        /// </summary>
        /// <param name="values">Valeurs des étapes</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <exception cref="ArgumentException">Les tableaux sont de taille différente</exception>
        /// <returns>Les étapes modifiées</returns>
        Task<Etape[]> UpdateAsync(IEnumerable<(Etape, Etape)> values);
    }
}