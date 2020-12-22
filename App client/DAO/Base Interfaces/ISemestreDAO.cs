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
        async Task<Semestre> CreateAsync(Semestre value) => (await CreateAsync(new[] { value })).First();

        /// <summary>
        /// Créé de nouveaux semestres
        /// </summary>
        /// <param name="values">Détails des semestres à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux semestres</returns>
        Task<Semestre[]> CreateAsync(IEnumerable<Semestre> values);

        /// <summary>
        /// Supprime un semestre
        /// </summary>
        /// <param name="value">Semestre à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Semestre value) => await DeleteAsync(new[] { value });

        /// <summary>
        /// Supprime des semestres
        /// </summary>
        /// <param name="value">Semestres à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(IEnumerable<Semestre> value);

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
        /// Récupère des semestres
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les semestres correspondants à l'id</returns>
        async Task<Semestre> GetByIdAsync(string code) => (await GetByIdAsync(new[] { code })).First();

        /// <summary>
        /// Récupère des semestres
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les semestres correspondants à l'id</returns>
        Task<Semestre[]> GetByIdAsync(IEnumerable<string> code);

        /// <summary>
        /// Récupère tous les semestres selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="search">Mots-clés à rechercher</param>
        /// <param name="number">Le numéro des semestres</param>
        /// <param name="step">L'étape des semestres</param>
        /// <param name="orderBy">Champ utilisé pour trier</param>
        /// <param name="reverseOrder">True si le tri doit être inversé</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les semestres filtrés disponibles</returns>
        Task<Semestre[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? number = null, IEnumerable<(string?, int?)>? step = null);

        /// <summary>
        /// Modifie un semestre
        /// </summary>
        /// <param name="oldValue">Ancienne valeur du semestre</param>
        /// <param name="newValue">Nouvelle valeur du semestre</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le semestre modifié</returns>
        async Task<Semestre> UpdateAsync(Semestre oldValue, Semestre newValue) => (await UpdateAsync(new[] { (oldValue, newValue) })).First();

        /// <summary>
        /// Modifie des semestres
        /// </summary>
        /// <param name="values">Valeurs des semestres</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <exception cref="ArgumentException">Les tableaux sont de taille différente</exception>
        /// <returns>Les semestres modifiés</returns>
        Task<Semestre[]> UpdateAsync(IEnumerable<(Semestre, Semestre)> values);
    }
}