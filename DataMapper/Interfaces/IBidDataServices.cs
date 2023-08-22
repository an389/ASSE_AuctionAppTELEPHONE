// <copyright file="IBidDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The bid data services.
    /// </summary>
    public interface IBidDataServices
    {
        /// <summary>
        ///     Adds a new bid to the database.
        /// </summary>
        /// <param name="bid">The bid to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the bid was added successfully to the database.
        ///     <br />
        ///     <b>false</b> if an error occurred while adding the bid to the database.
        /// </returns>
        bool AddBid(Bid bid);

        /// <summary>
        ///     Gets all the bids from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing bids.
        /// </returns>
        IList<Bid> GetAllBids();

        /// <summary>
        ///     Gets the bid with the provided id from the database.
        /// </summary>
        /// <param name="id">The bid's id.</param>
        /// <returns>
        ///     the bid with the provided id.
        /// </returns>
        Bid GetBidById(int id);

        /// <summary>
        ///     Gets all the bids on a certain product by the product's id from the database.
        /// </summary>
        /// <param name="id">The product's id.</param>
        /// <returns>
        ///     a list of all bids on the product with the provided id.
        /// </returns>
        IList<Bid> GetBidsByProductId(int id);

        /// <summary>
        ///     Gets the number of active bids of a user by the user's id from the database.</summary>
        /// <param name="id">The user's id.</param>
        /// <returns>
        ///     the number of active bids of the user with the provided id.
        /// </returns>
        int GetNoOfActiveBidsByUserId(int id);

        /// <summary>
        ///     Updates the provided bid in the database.
        /// </summary>
        /// <param name="bid">The bid to be updated.</param>
        /// <returns>
        ///     <b>true</b> if the bid was updated successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while updating the bid.
        /// </returns>
        bool UpdateBid(Bid bid);

        /// <summary>
        ///     Deletes the provided bid from the database.
        /// </summary>
        /// <param name="bid">The bid to be deleted.</param>
        /// <returns>
        ///     <b>true</b> if the bid was deleted successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while deleting the bid.
        /// </returns>
        bool DeleteBid(Bid bid);
    }
}
