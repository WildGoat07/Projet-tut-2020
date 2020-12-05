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
        async Task<Diplome> CreateAsync(Diplome value) => (await CreateAsync(new Diplome[] { value })).First();

        /// <summary>
        /// Créé de nouveaux diplômes
        /// </summary>
        /// <param name="values">Détails des diplômes à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouveaux diplômes</returns>
        Task<Diplome[]> CreateAsync(ReadOnlySpan<Diplome> values);

        /// <summary>
        /// Supprime un diplôme
        /// </summary>
        /// <param name="value">Diplôme à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Diplome value) => await DeleteAsync(new Diplome[] { value });

        /// <summary>
        /// Supprime des diplômes
        /// </summary>
        /// <param name="value">Diplômes à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Diplome> value);

        /// <summary>
        /// Récupère tous les diplômes
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les diplômes disponibles</returns>
        Task<Diplome[]> GetAllAsync(int maxCount, int page);

        /// <summary>
        /// Récupère un diplôme
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Le diplôme correspondant à l'id</returns>
        Task<Diplome> GetByIdAsync(string code, int version);

        /// <summary>
        /// Modifie un diplôme
        /// </summary>
        /// <param name="oldValue">Ancienne valeur du diplôme</param>
        /// <param name="newValue">Nouvelle valeur du diplôme</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Le diplôme modifié</returns>
        async Task<Diplome> UpdateAsync(Diplome oldValue, Diplome newValue) => (await UpdateAsync(new Diplome[] { oldValue }, new Diplome[] { newValue })).First();

        /// <summary>
        /// Modifie des diplôme
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des diplômes</param>
        /// <param name="newValues">Nouvelles valeurs des diplômes</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les diplômes modifiés</returns>
        Task<Diplome[]> UpdateAsync(ReadOnlySpan<Diplome> oldValues, ReadOnlySpan<Diplome> newValues);
    }
}