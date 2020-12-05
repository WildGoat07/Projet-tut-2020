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
        Task<Composante> CreateAsync(Composante value);

        /// <summary>
        /// Créé des nouvelles composantes
        /// </summary>
        /// <param name="values">Détails des composantes à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles composantes</returns>
        Task<Composante[]> CreateAsync(ReadOnlySpan<Composante> values);

        /// <summary>
        /// Supprime une composante
        /// </summary>
        /// <param name="value">Composante à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(Composante value);

        /// <summary>
        /// Supprime des composantes
        /// </summary>
        /// <param name="values">Composantes à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Composante> values);

        /// <summary> Récupère toutes les composantes </summary> <param name="maxCount">Quantité
        /// maximum à récupérer</param> <param name="page"> Les <paramref name="maxCount"/>*
        /// <paramref name="page"/> première valeurs seront évitées <exception
        /// cref="DAOException">Une erreur est survenue</exception> <returns>Toutes les composantes disponibles</returns>
        Task<Composante[]> GetAllAsync(int maxCount, int page);

        /// <summary>
        /// Récupère une composante
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>La composante correspondante à l'id</returns>
        Task<Composante> GetByIdAsync(string id);

        /// <summary>
        /// Modifie une composante
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la composante</param>
        /// <param name="newValue">Nouvelle valeur de la composante</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La composante modifiée</returns>
        Task<Composante> UpdateAsync(Composante oldValue, Composante newValue);

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