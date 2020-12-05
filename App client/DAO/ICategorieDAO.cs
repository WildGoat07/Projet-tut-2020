using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ICategorieDAO
    {
        /// <summary>
        /// Créé une nouvelle catégorie
        /// </summary>
        /// <param name="value">Détail de la catégorie à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle catégorie</returns>
        Task<Categorie> CreateAsync(Categorie value);

        /// <summary>
        /// Supprime une catégorie
        /// </summary>
        /// <param name="value">Catégorie à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(Categorie value);

        /// <summary>
        /// Récupère toutes les catégories
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les catégories disponibles</returns>
        Task<Categorie[]> GetAllAsync();

        /// <summary>
        /// Récupère une catégorie
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>La catégorie correspondante à l'id</returns>
        Task<Categorie> GetByIdAsync(int id);

        /// <summary>
        /// Modifie une catégorie
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la catégorie</param>
        /// <param name="newValue">Nouvelle valeur de la catégorie</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La catégorie modifiée</returns>
        Task<Categorie> UpdateAsync(Categorie oldValue, Categorie newValue);
    }
}