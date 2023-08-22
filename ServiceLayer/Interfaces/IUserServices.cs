// <copyright file="IUserServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The user services.
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        ///     Checks if the user is valid and adds it to the database.
        /// </summary>
        /// <param name="user">The user to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the user was added successfully.
        ///     <br/>
        ///     <b>false</b> if the user was null,
        ///         invalid,
        ///         a user with the same username or email already exists in the database
        ///         or the user could not be added to the database.
        /// </returns>
        bool AddUser(User user);

        /// <summary>
        ///     Gets all the users from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing users.
        /// </returns>
        IList<User> GetAllUsers();

        /// <summary>
        ///     Gets the user with the provided id from the database.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>
        ///     the user with the provided id.
        /// </returns>
        User GetUserById(int id);

        /// <summary>
        ///     Gets the user with the provided email and password from the database.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>
        ///     the user with the provided email and password.
        /// </returns>
        User GetUserByEmailAndPassword(string email, string password);

        /// <summary>
        ///     Checks if the user is valid and updates it in the database.
        /// </summary>
        /// <param name="user">The user to be updated in the database.</param>
        /// <returns>
        ///     <b>true</b> if the user was updated successfully.
        ///     <br/>
        ///     <b>false</b> if the user was null,
        ///         invalid,
        ///         the user doesn't exists in the database
        ///         or the category could not be updated in the database.
        /// </returns>
        bool UpdateUser(User user);

        /// <summary>
        ///     Checks if the user exists and deletes it from the database.
        /// </summary>
        /// <param name="user">The user to be deleted from the database.</param>
        /// <returns>
        ///     <b>true</b> if the user was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if the user was null,
        ///         the user doesn't exist in the database
        ///         or the user could not be deleted from the database.
        /// </returns>
        bool DeleteUser(User user);
    }
}
