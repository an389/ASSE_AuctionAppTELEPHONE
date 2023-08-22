// <copyright file="IUserDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The user data services.
    /// </summary>
    public interface IUserDataServices
    {
        /// <summary>
        ///     Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the user was added successfully to the database.
        ///     <br />
        ///     <b>false</b> if an error occurred while adding the user to the database.
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
        ///     Checks whether a user with the provided email address already exists in the database.
        /// </summary>
        /// <param name="email">The new user's email address.</param>
        /// <returns>
        ///     <b>true</b> if currently there is no user in the database with the provided email address.
        ///     <br />
        ///     <b>false</b> if there already is a user in the database with the provided email address.
        /// </returns>
        bool EmailAlreadyExists(string email);

        /// <summary>
        ///     Checks whether a user with the provided username already exists in the database.
        /// </summary>
        /// <param name="username">The new user's username.</param>
        /// <returns>
        ///     <b>true</b> if currently there is no user in the database with the provided username.
        ///     <br />
        ///     <b>false</b> if there already is a user in the database with the provided username.
        /// </returns>
        bool UsernameAlreadyExists(string username);

        /// <summary>
        ///     Updates the provided user in the database.
        /// </summary>
        /// <param name="user">The user to be updated.</param>
        /// <returns>
        ///     <b>true</b> if the user was updated successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while updating the user.
        /// </returns>
        bool UpdateUser(User user);

        /// <summary>
        ///     Deletes the provided user from the database.
        /// </summary>
        /// <param name="user">The user to be deleted.</param>
        /// <returns>
        ///     <b>true</b> if the user was deleted successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while deleting the user.
        /// </returns>
        bool DeleteUser(User user);
    }
}
