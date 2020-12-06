using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ISemestreDAO
    {
        /// <summary>
        /// Créé un nouveau semestre
        /// </summary>
        /// <param name="value">Détail du semestre à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le nouveau semestre</returns>
        async Task<Semestre> CreateAsync(Semestre value) => (await CreateAsync(new Semestre[] { value })).First();

        /// <summary>
        /// Créé de nouveaux semestres
        /// </summary>
        /// <param name="values">Détails des semestres à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux semestres</returns>
        Task<Semestre[]> CreateAsync(ReadOnlySpan<Semestre> values);

        /// <summary>
        /// Supprime un semestre
        /// </summary>
        /// <param name="value">Semestre à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Semestre value) => await DeleteAsync(new Semestre[] { value });

        /// <summary>
        /// Supprime des semestres
        /// </summary>
        /// <param name="value">Semestres à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Semestre> value);

        /// <summary>
        /// Récupère tous les semestres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les semestres disponibles</returns>
        async Task<Semestre[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère un semestre
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le semestre correspondant à l'id</returns>
        Task<Semestre> GetByIdAsync(string code);

        /// <summary>
        /// Récupère tous les semestres selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="number">Le numéro des semestres</param>
        /// <param name="step">L'étape des semestres</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les semestres filtrés disponibles</returns>
        Task<Semestre[]> GetFilteredAsync(int maxCount, int page, ReadOnlyMemory<int>? number = null, ReadOnlyMemory<(string?, int?)>? step = null);

        /// <summary>
        /// Modifie un semestre
        /// </summary>
        /// <param name="oldValue">Ancienne valeur du semestre</param>
        /// <param name="newValue">Nouvelle valeur du semestre</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le semestre modifié</returns>
        async Task<Semestre> UpdateAsync(Semestre oldValue, Semestre newValue) => (await UpdateAsync(new Semestre[] { oldValue }, new Semestre[] { newValue })).First();

        /// <summary>
        /// Modifie des semestres
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des semestres</param>
        /// <param name="newValues">Nouvelles valeurs des semestres</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les semestres modifiés</returns>
        Task<Semestre[]> UpdateAsync(ReadOnlySpan<Semestre> oldValues, ReadOnlySpan<Semestre> newValues);
    }
}