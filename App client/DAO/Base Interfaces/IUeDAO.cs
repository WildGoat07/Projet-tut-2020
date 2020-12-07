using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IUeDAO
    {
        /// <summary>
        /// Créé une ue
        /// </summary>
        /// <param name="value">Détail de la ue à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle ue</returns>
        async Task<Ue> CreateAsync(Ue value) => (await CreateAsync(new Ue[] { value })).First();

        /// <summary>
        /// Créé des nouvelles ue
        /// </summary>
        /// <param name="values">Détails des ue à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles ue</returns>
        Task<Ue[]> CreateAsync(ArraySegment<Ue> values);

        /// <summary>
        /// Supprime une ue
        /// </summary>
        /// <param name="value">Ue à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Ue value) => await DeleteAsync(new Ue[] { value });

        /// <summary>
        /// Supprime des ue
        /// </summary>
        /// <param name="values">Ue à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ArraySegment<Ue> values);

        /// <summary>
        /// Récupère toutes les ue
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les ue disponibles</returns>
        async Task<Ue[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une ue
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La ue correspondante à l'id</returns>
        Task<Ue> GetByIdAsync(string code);

        /// <summary>
        /// Récupère tous les ue selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="ECTS">ECTS des UE</param>
        /// <param name="nature">Nature des UE</param>
        /// <param name="parent">Parent des UE</param>
        /// <param name="semester">Semestre des UE</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Toutes les ue filtrées disponibles</returns>
        Task<Ue[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, ArraySegment<char>? nature = null, ArraySegment<int>? ECTS = null, ArraySegment<string>? parent = null, ArraySegment<Semestre>? semester = null);

        /// <summary>
        /// Modifie une ue
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la ue</param>
        /// <param name="newValue">Nouvelle valeur de la ue</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La ue modifiée</returns>
        async Task<Ue> UpdateAsync(Ue oldValue, Ue newValue) => (await UpdateAsync(new Ue[] { oldValue }, new Ue[] { newValue })).First();

        /// <summary>
        /// Modifie des ue
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des ue</param>
        /// <param name="newValues">Nouvelles valeurs des ue</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <exception cref="ArgumentException">Les tableaux sont de taille différente</exception>
        /// <returns>Les ue modifiées</returns>
        Task<Ue[]> UpdateAsync(ReadOnlyMemory<Ue> oldValues, ReadOnlyMemory<Ue> newValues);
    }
}