using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ICategorieDAO
    {
        /// <summary>
        /// Créé une nouvelle catégorie
        /// </summary>
        /// <param name="value">Détails de la catégorie à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle catégorie</returns>
        async Task<Categorie> CreateAsync(Categorie value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé des nouvelles catégories
        /// </summary>
        /// <param name="values">Détails des catégories à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles catégories</returns>
        Task<Categorie[]> CreateAsync(IEnumerable<Categorie> values);

        /// <summary>
        /// Supprime de catégorie
        /// </summary>
        /// <param name="value">Catégorie à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Categorie value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des catégories
        /// </summary>
        /// <param name="values">Catégories à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Categorie> values);

        /// <summary>
        /// Récupère toutes les catégories
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les catégories disponibles</returns>
        async Task<Categorie[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une catégorie
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La catégorie correspondante à l'id</returns>
        async Task<Categorie> GetByIdAsync(int id) => (await GetByIdAsync(new[] { id })).First();

        /// <summary>
        /// Récupère des catégories
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les catégories correspondantes à l'id</returns>
        Task<Categorie[]> GetByIdAsync(IEnumerable<int> id);

        /// <summary>
        /// Récupère toutes les catégories selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="search">Mots-clés à rechercher</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les catégories filtrées disponibles</returns>
        Task<Categorie[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null);

        /// <summary>
        /// Modifie une catégorie
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la catégorie</param>
        /// <param name="newValue">Nouvelle valeur de la catégorie</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La catégorie modifiée</returns>
        async Task<Categorie> UpdateAsync(Categorie oldValue, Categorie newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des catégories
        /// </summary>
        /// <param name="values">Valeurs des catégories</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <exception cref="ArgumentException">Les tableaux sont de taille différente</exception>
        /// <returns>Les catégories modifiées</returns>
        Task<Categorie[]> UpdateAsync(IEnumerable<(Categorie, Categorie)> values);
    }
}