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
        /// Créé des nouvelles catégories
        /// </summary>
        /// <param name="values">Détails des catégories à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles catégories</returns>
        Task<Categorie[]> CreateAsync(ReadOnlySpan<Categorie> values);

        /// <summary>
        /// Supprime une catégorie
        /// </summary>
        /// <param name="value">Catégorie à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(Categorie value);

        /// <summary>
        /// Supprime des catégories
        /// </summary>
        /// <param name="values">Catégories à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Categorie> values);

        /// <summary> Récupère toutes les catégories </summary> <param name="maxCount">Quantité
        /// maximum à récupérer</param> <param name="page"> Les <paramref name="maxCount"/>*
        /// <paramref name="page"/> première valeurs seront évitées <exception
        /// cref="DAOException">Une erreur est survenue</exception> <returns>Toutes les catégories disponibles</returns>
        Task<Categorie[]> GetAllAsync(int maxCount, int page);

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

        /// <summary>
        /// Modifie des catégories
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des catégories</param>
        /// <param name="newValues">Nouvelles valeurs des catégories</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les catégories modifiées</returns>
        Task<Categorie[]> UpdateAsync(ReadOnlySpan<Categorie> oldValues, ReadOnlySpan<Categorie> newValues);
    }
}