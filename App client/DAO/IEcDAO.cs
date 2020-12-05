﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IEcDAO
    {
        /// <summary>
        /// Créé une ec
        /// </summary>
        /// <param name="value">Détail de la ec à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La nouvelle ec</returns>
        async Task<Ec> CreateAsync(Ec value) => (await CreateAsync(new Ec[] { value })).First();

        /// <summary>
        /// Créé des nouvelles ec
        /// </summary>
        /// <param name="values">Détails des ec à créer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les nouvelles ec</returns>
        Task<Ec[]> CreateAsync(ReadOnlySpan<Ec> values);

        /// <summary>
        /// Supprime une ec
        /// </summary>
        /// <param name="value">Ec à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        async Task DeleteAsync(Ec value) => await DeleteAsync(new Ec[] { value });

        /// <summary>
        /// Supprime des ec
        /// </summary>
        /// <param name="values">Ec à supprimer</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        Task DeleteAsync(ReadOnlySpan<Ec> values);

        /// <summary>
        /// Récupère toutes les ec
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Toutes les ec disponibles</returns>
        async Task<Ec[]> GetAllAsync(int maxCount, int page) => await GetFilteredAsync(maxCount, page);

        /// <summary>
        /// Récupère une ec
        /// </summary>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>La ec correspondante à l'id</returns>
        Task<Ec> GetByIdAsync(string code);

        /// <summary>
        /// Récupère tous les ec selon des filtres
        /// </summary>
        /// <param name="maxCount">Quantité maximum à récupérer</param>
        /// <param name="page">
        /// Les <paramref name="maxCount"/> * <paramref name="page"/> première valeurs seront évitées
        /// </param>
        /// <param name="owner">Father of the filtered ec</param>
        /// <param name="nature">Nature character of the filtered ec</param>
        /// <param name="ue">Ue linked by the filtered ec</param>
        /// <param name="category">Category id of the filtered ec</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <returns>Tous les diplômes filtrés disponibles</returns>
        Task<Diplome[]> GetFilteredAsync(int maxCount, int page, int? owner = null, char? nature = null, int? ue = null, int? category = null);

        /// <summary>
        /// Modifie une ec
        /// </summary>
        /// <param name="oldValue">Ancienne valeur de la ec</param>
        /// <param name="newValue">Nouvelle valeur de la ec</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>La ec modifiée</returns>
        async Task<Ec> UpdateAsync(Ec oldValue, Ec newValue) => (await UpdateAsync(new Ec[] { oldValue }, new Ec[] { newValue })).First();

        /// <summary>
        /// Modifie des ec
        /// </summary>
        /// <param name="oldValues">Anciennes valeurs des ec</param>
        /// <param name="newValues">Nouvelles valeurs des ec</param>
        /// <exception cref="DAOException">Une erreur est survenue</exception>
        /// <exception cref="ArgumentNullException">Un des paramètres est null</exception>
        /// <returns>Les ec modifiées</returns>
        Task<Ec[]> UpdateAsync(ReadOnlySpan<Ec> oldValues, ReadOnlySpan<Ec> newValues);
    }
}