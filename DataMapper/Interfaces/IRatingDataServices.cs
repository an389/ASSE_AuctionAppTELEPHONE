// <copyright file="IRatingDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The rating data services.
    /// </summary>
    public interface IRatingDataServices
    {
        /// <summary>
        ///     Adds a new rating to the database.
        /// </summary>
        /// <param name="rating">The rating to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the rating was added successfully to the database.
        ///     <br />
        ///     <b>false</b> if an error occurred while adding the rating to the database.
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
        ///     Gets the rating given by a certain user on a certain product by the user's id and product's id from the database.
        /// </summary>
        /// <param name="userId">The rating user's id.</param>
        /// <param name="productId">The product's id.</param>
        /// <returns>
        ///     the rating given by the user with the provided id on the product with the provided id.
        /// </returns>
        Rating GetRatingByUserIdAndProductId(int userId, int productId);

        /// <summary>
        ///     Gets the winning bid on a product by the product's id from the database.
        /// </summary>
        /// <param name="id">The product's id.</param>
        /// <returns>
        ///     the winning bid on the product with the provided id.
        /// </returns>
        User GetWinningBidUserByProductId(int id);

        /// <summary>
        ///     Updates the provided rating in the database.
        /// </summary>
        /// <param name="rating">The rating to be updated.</param>
        /// <returns>
        ///     <b>true</b> if the rating was updated successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while updating the rating.
        /// </returns>
        bool UpdateRating(Rating rating);

        /// <summary>
        ///     Deletes the provided rating from the database.
        /// </summary>
        /// <param name="rating">The rating to be deleted.</param>
        /// <returns>
        ///     <b>true</b> if the rating was deleted successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while deleting the rating.
        /// </returns>
        bool DeleteRating(Rating rating);
    }
}
