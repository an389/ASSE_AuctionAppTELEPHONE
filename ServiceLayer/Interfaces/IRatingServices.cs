// <copyright file="IRatingServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The rating services.
    /// </summary>
    public interface IRatingServices
    {
        /// <summary>
        ///     Checks if the rating is valid and adds it to the database.
        /// </summary>
        /// <param name="rating">The rating to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the rating was added successfully.
        ///     <br/>
        ///     <b>false</b> if the rating was null,
        ///         invalid,
        ///         the auction hasn't ended yet,
        ///         the rating was already given,
        ///         the wrong user is rating or being rated
        ///         or the rating could not be added to the database.
        /// </returns>
        bool AddRating(Rating rating);

        /// <summary>
        ///     Gets all the ratings from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing ratings.
        /// </returns>
        IList<Rating> GetAllRatings();

        /// <summary>
        ///     Gets the rating with the provided id from the database.
        /// </summary>
        /// <param name="id">The rating's id.</param>
        /// <returns>
        ///     the rating with the provided id.
        /// </returns>
        Rating GetRatingById(int id);

        /// <summary>
        ///     Gets all the ratings of a certain user by the user's id from the database.
        /// </summary>
        /// <param name="id">The rated user's id.</param>
        /// <returns>
        ///     a list of all existing ratings given to the user with the provided id.
        /// </returns>
        IList<Rating> GetRatingsByUserId(int id);

        /// <summary>
        ///     Checks if the rating is valid and updates it in the database.
        /// </summary>
        /// <param name="rating">The rating to be updated in the database.</param>
        /// <returns>
        ///     <b>true</b> if the rating was updated successfully.
        ///     <br/>
        ///     <b>false</b> if the rating was null,
        ///         invalid,
        ///         the rating doesn't exist in the database
        ///         or the category could not be updated in the database.
        /// </returns>
        bool UpdateRating(Rating rating);

        /// <summary>
        ///     Checks if the rating exists and deletes it from the database.
        /// </summary>
        /// <param name="rating">The rating to be deleted from the database.</param>
        /// <returns>
        ///     <b>true</b> if the rating was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if the rating was null,
        ///         the rating doesn't exist in the database
        ///         or the rating could not be deleted from the database.
        /// </returns>
        bool DeleteRating(Rating rating);
    }
}
