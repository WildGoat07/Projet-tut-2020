using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IComposanteDAO
    {
        /// <summary>
        /// Créé une nouvelle composante
        /// </summary>
        /// <param name="value">Détail de la composante à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle composante</returns>
        async Task<Composante> CreateAsync(Composante value) => (await CreateAsync(new Composante[] { value })).First();

        /// <summary>
        /// Créé des nouvelles composantes
        /// </summary>
        /// <param name="values">Détails des composantes à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles composantes</returns>
        Task<Composante[]> CreateAsync(ReadOnlySpan<Composante> values);

        /// <summary>
        /// Récupère la composante en cours
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>La composante en cours</returns>
        Task<Composante> CurrentAsync();

        /// <summary>
        /// Supprime une composante
        /// </summary>
        /// <param name="value">Composante à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Composante value) => await DeleteAsync(new Composante[] { value });

        /// <summary>
        /// Supprime des composantes
        /// </summary>
        /// <param name="values">Composantes à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Composante> values);

        /// <summary>
        /// Récupère toutes les composantes
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les composantes disponibles</returns>
        async Task<Composante[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une composante
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La composante correspondante à l'id</returns>
        Task<Composante> GetByIdAsync(string id);

        /// <summary>
        /// Récupère toutes les composantes
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="location">Lieu de la composante</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Toutes les composantes disponibles</returns>
        Task<Composante[]> GetFilteredAsync(int maxCount, int page, ReadOnlyMemory<string>? location = null);

        /// <summary>
        /// Modifie une composante
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la composante</param>
        /// <param name="newValue">Nouvelle valeur de la composante</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La composante modifiée</returns>
        async Task<Composante> UpdateAsync(Composante oldValue, Composante newValue) => (await UpdateAsync(new Composante[] { oldValue }, new Composante[] { newValue })).First();

        /// <summary>
        /// Modifie des composantes
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des composantes</param>
        /// <param name="newValues">Nouvelles valeurs des composantes</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les composantes modifiées</returns>
        Task<Composante[]> UpdateAsync(ReadOnlySpan<Composante> oldValues, ReadOnlySpan<Composante> newValues);
    }
}