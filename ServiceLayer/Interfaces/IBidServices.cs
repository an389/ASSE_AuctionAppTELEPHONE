// <copyright file="IBidServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The bid services.
    /// </summary>
    public interface IBidServices
    {
        /// <summary>
        ///     Checks if the bid is valid and adds it to the database.
        /// </summary>
        /// <param name="bid">The bid to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the bid was added successfully.
        ///     <br/>
        ///     <b>false</b> if the bid was null,
        ///         invalid,
        ///         the user reached their limit of active bids,
        ///         the auction hasn't started yet or has already ended,
        ///         the buyer is also the seller,
        ///         the currency is not the same as the one used in the auction,
        ///         the amount is less than the starting price or the previous bid
        ///         or the bid could not be added to the database.
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

        // bool UpdateBid(Bid bid);

        // bool DeleteBid(Bid bid);
    }
}
