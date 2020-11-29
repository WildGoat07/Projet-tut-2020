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
        Task<AnneeUniv> CreateAsync(AnneeUniv value);

        /// <summary>
        /// Supprime une année universitaire
        /// </summary>
        /// <param name="value">Année à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>True si supprimé avec succès, False autrement</returns>
        Task<bool> DeleteAsync(AnneeUniv value);

        /// <summary>
        /// Récupère toutes les année enregistrées
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les années disponibles</returns>
        Task<AnneeUniv[]> GetAllAsync();

        /// <summary>
        /// Modifie une année universitaire
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de l'année</param>
        /// <param name="newValue">Nouvelle valeur de l'année</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>L'année modifiée</returns>
        Task<AnneeUniv> UpdateAsync(AnneeUniv oldValue, AnneeUniv newValue);
    }
}