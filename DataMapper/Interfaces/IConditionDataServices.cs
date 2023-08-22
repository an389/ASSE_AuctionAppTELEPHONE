// <copyright file="IConditionDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The condition data services.
    /// </summary>
    public interface IConditionDataServices
    {
        /// <summary>
        ///     Adds a new condition to the database.
        /// </summary>
        /// <param name="condition">The condition to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the condition was added successfully to the database.
        ///     <br />
        ///     <b>false</b> if an error occurred while adding the condition to the database.
        /// </returns>
        bool AddCondition(Condition condition);

        /// <summary>
        ///     Gets all the conditions from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing conditions.
        /// </returns>
        IList<Condition> GetAllConditions();

        /// <summary>
        ///     Gets the condition with the provided id from the database.
        /// </summary>
        /// <param name="id">The condition's id.</param>
        /// <returns>
        ///     the condition with the provided id.
        /// </returns>
        Condition GetConditionById(int id);

        /// <summary>
        ///     Gets the condition with the provided name from the database.
        /// </summary>
        /// <param name="name">The condition's name.</param>
        /// <returns>
        ///     the condition with the provided name.
        /// </returns>
        Condition GetConditionByName(string name);

        /// <summary>
        ///     Gets the maximum number of active auctions a seller can have at any given time (K) from the database.
        /// </summary>
        /// <returns>
        ///     the maximum number of active auctions a seller can have at any given time (K) from the database.
        /// </returns>
        int GetK();

        /// <summary>
        ///     Gets the maximum number of active auctions a seller can have in one category at any given time (M) from the database.
        /// </summary>
        /// <returns>
        ///     the maximum number of active auctions a seller can have in one category at any given time (M) from the database.
        /// </returns>
        int GetM();

        /// <summary>
        ///     Gets the maximum score a user can have (S) from the database.
        /// </summary>
        /// <returns>
        ///     the maximum score a user can have (S) from the database.
        /// </returns>
        int GetS();

        /// <summary>
        ///     Gets the number of most recent ratings to be used in calculating user scores (N) from the database.
        /// </summary>
        /// <returns>
        ///     the number of most recent ratings to be used in calculating user scores (N) from the database.
        /// </returns>
        int GetN();

        /// <summary>
        ///     Gets the maximum number of active auctions/bids a user can have with a perfect score (T) from the database.
        /// </summary>
        /// <returns>
        ///     the maximum number of active auctions/bids a user can have with a perfect score (T) from the database.
        /// </returns>
        int GetT();

        /// <summary>
        ///     Updates the provided condition in the database.
        /// </summary>
        /// <param name="condition">The condition to be updated.</param>
        /// <returns>
        ///     <b>true</b> if the condition was updated successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while updating the condition.
        /// </returns>
        bool UpdateCondition(Condition condition);

        /// <summary>
        ///     Deletes the provided condition from the database.
        /// </summary>
        /// <param name="condition">The condition to be deleted.</param>
        /// <returns>
        ///     <b>true</b> if the condition was deleted successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while deleting the condition.
        /// </returns>
        bool DeleteCondition(Condition condition);
    }
}
