using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IEnseignantDAO
    {
        /// <summary>
        /// Créé un nouvel enseignant
        /// </summary>
        /// <param name="value">Détail de l'enseignant à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le nouvel enseignant</returns>
        async Task<Enseignant> CreateAsync(Enseignant value) => (await CreateAsync(new Enseignant[] { value })).First();

        /// <summary>
        /// Créé de nouveaux enseignants
        /// </summary>
        /// <param name="values">Détails des enseignants à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux enseignants</returns>
        Task<Enseignant[]> CreateAsync(ReadOnlySpan<Enseignant> values);

        /// <summary>
        /// Supprime un enseignant
        /// </summary>
        /// <param name="value">Enseignant à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Enseignant value) => await DeleteAsync(new Enseignant[] { value });

        /// <summary>
        /// Supprime des enseignants
        /// </summary>
        /// <param name="values">Enseignants à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Enseignant> values);

        /// <summary>
        /// Récupère toutes les enseignants
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les enseignants</returns>
        Task<Enseignant[]> GetAllAsync(int maxCount, int page);

        /// <summary>
        /// Récupère un enseignant
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'enseignant correspondant à l'id</returns>
        Task<Enseignant> GetByIdAsync(string id);

        /// <summary>
        /// Récupère tous les enseignants selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="function">Fonction des enseignants</param>
        /// <param name="comp">Composante ratachée aux enseignants</param>
        /// <param name="CRCT">CRCT des enseignants</param>
        /// <param name="PesPedr">PesPedr des enseignants</param>
        /// <param name="forcedHours">Min/Max des heures obligatoires</param>
        /// <param name="maxHours">Min/Max des heures max</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Tous les enseignants filtrés disponibles</returns>
        Task<Enseignant[]> GetFilteredAsync(int maxCount, int page, ReadOnlyMemory<string>? function = null, ReadOnlyMemory<int>? comp = null, ReadOnlyMemory<char>? CRCT = null, ReadOnlyMemory<char>? PesPedr = null, (float?, float?)? forcedHours = null, (float?, float?)? maxHours = null);

        /// <summary>
        /// Modifie un enseignant
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de l'enseignant</param>
        /// <param name="newValue">Nouvelle valeur de l'enseignant</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'enseignant modifié</returns>
        async Task<Enseignant> UpdateAsync(Enseignant oldValue, Enseignant newValue) => (await UpdateAsync(new Enseignant[] { oldValue }, new Enseignant[] { newValue })).First();

        /// <summary>
        /// Modifie des enseignants
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des enseignants</param>
        /// <param name="newValues">Nouvelles valeurs des enseignants</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les enseignants modifiés</returns>
        Task<Enseignant[]> UpdateAsync(ReadOnlySpan<Enseignant> oldValues, ReadOnlySpan<Enseignant> newValues);
    }
}