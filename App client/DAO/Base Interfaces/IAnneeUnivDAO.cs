using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IAnneeUnivDAO
    {
        /// <summary>
        /// Créé une nouvelle année universitaire
        /// </summary>
        /// <param name="value">Détail de l'année à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle année universitaire</returns>
        async Task<AnneeUniv> CreateAsync(AnneeUniv value) => (await CreateAsync(new AnneeUniv[] { value })).First();

        /// <summary>
        /// Créé de nouvelles années universitaires
        /// </summary>
        /// <param name="values">Détails des l'années à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles années universitaires</returns>
        Task<AnneeUniv[]> CreateAsync(ReadOnlySpan<AnneeUniv> values);

        /// <summary>
        /// Supprime une année universitaire
        /// </summary>
        /// <param name="value">Année à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(AnneeUniv value) => await DeleteAsync(new AnneeUniv[] { value });

        /// <summary>
        /// Supprime des années universitaires
        /// </summary>
        /// <param name="values">Années à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<AnneeUniv> values);

        /// <summary>
        /// Récupère toutes les année enregistrées
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les années disponibles</returns>
        Task<AnneeUniv[]> GetAllAsync(int maxCount, int page);

        /// <summary>
        /// Modifie une année universitaire
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de l'année</param>
        /// <param name="newValue">Nouvelle valeur de l'année</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'année modifiée</returns>
        async Task<AnneeUniv> UpdateAsync(AnneeUniv oldValue, AnneeUniv newValue) => (await UpdateAsync(new AnneeUniv[] { oldValue }, new AnneeUniv[] { newValue })).First();

        /// <summary>
        /// Modifie des années universitaires
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des années</param>
        /// <param name="newValues">Nouvelles valeurs des années</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les années modifiées</returns>
        Task<AnneeUniv[]> UpdateAsync(ReadOnlySpan<AnneeUniv> oldValues, ReadOnlySpan<AnneeUniv> newValues);
    }
}