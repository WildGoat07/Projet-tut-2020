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
        /// Supprime une composante
        /// </summary>
        /// <param name="value">Composante à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(Composante value);

        /// <summary>
        /// Récupère toutes les composantes
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les composantes disponibles</returns>
        Task<Composante[]> GetAllAsync();

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
    }
}