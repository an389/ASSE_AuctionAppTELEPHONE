// <copyright file="IConditionServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The condition services.
    /// </summary>
    public interface IConditionServices
    {
        /// <summary>
        ///     Checks if the condition is valid and adds it to the database.
        /// </summary>
        /// <param name="condition">The condition to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the condition was added successfully.
        ///     <br/>
        ///     <b>false</b> if the condition was null,
        ///         invalid,
        ///         a condition with the same name already exists in the database
        ///         or the category could not be added to the database.
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
        ///     Checks if the condition is valid and updates it in the database.
        /// </summary>
        /// <param name="condition">The condition to be updated in the database.</param>
        /// <returns>
        ///     <b>true</b> if the condition was updated successfully.
        ///     <br/>
        ///     <b>false</b> if the condition was null,
        ///         invalid,
        ///         the condition doesn't exist in the database,
        ///         a condition with the same name already exists in the database
        ///         or the category could not be updated in the database.
        /// </returns>
        bool UpdateCondition(Condition condition);

        /// <summary>
        ///     Checks if the condition exists and deletes it from the database.
        /// </summary>
        /// <param name="condition">The condition to be deleted from the database.</param>
        /// <returns>
        ///     <b>true</b> if the condition was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if the condition was null,
        ///         the condition doesn't exist in the database
        ///         or the condition could not be deleted from the database.
        /// </returns>
        bool DeleteCondition(Condition condition);
    }
}
