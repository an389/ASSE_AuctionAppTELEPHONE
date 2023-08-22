// <copyright file="IProductDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    using System;
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The product data services.
    /// </summary>
    public interface IProductDataServices
    {
        /// <summary>
        ///     Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the product was added successfully to the database.
        ///     <br />
        ///     <b>false</b> if an error occurred while adding the product to the database.
        /// </returns>
        bool AddProduct(Product product);

        /// <summary>
        ///     Gets all the products from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing products.
        /// </returns>
        IList<Product> GetAllProducts();

        /// <summary>
        ///     Gets the product with the provided id from the database.
        /// </summary>
        /// <param name="id">The product's id.</param>
        /// <returns>
        ///     the product with the provided id.
        /// </returns>
        Product GetProductById(int id);

        /// <summary>
        ///     Gets the number of unfinished auctions of a user by the user's id from the database.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns>
        ///     the number of unfinished auctions of the user with the provided id.
        /// </returns>
        int GetNoOfActiveAndFutureAuctionsByUserId(int id);

        /// <summary>
        ///     Gets the number of unfinished auctions of a user in a given interval by the user's id from the database.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="newStart">The date the auction starts for the new product.</param>
        /// <param name="newEnd">The date the auction ends for the new product.</param>
        /// <returns>
        ///     the number of unfinished auctions in a given interval of the user with the provided id.
        /// </returns>
        int GetNoOfActiveAuctionsOfUserInInterval(int id, DateTime newStart, DateTime newEnd);

        /// <summary>
        ///     Gets the number of unfinished auctions of a user in a certain category by the user's id from the database.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="category">The category the new product is in.</param>
        /// <param name="newStart">The date the auction starts for the new product.</param>
        /// <param name="newEnd">The date the auction ends for the new product.</param>
        /// <returns>
        ///     the number of unfinished auctions in a given interval of the user with the provided id.
        /// </returns>
        int GetNoOfActiveAuctionsOfUserInCategory(int id, Category category, DateTime newStart, DateTime newEnd);

        /// <summary>
        ///     Gets the description of each product from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing product descriptions.
        /// </returns>
        List<string> GetAllProductDescriptions();

        /// <summary>
        ///     Updates the provided product in the database.
        /// </summary>
        /// <param name="product">The product to be updated.</param>
        /// <returns>
        ///     <b>true</b> if the product was updated successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while updating the product.
        /// </returns>
        bool UpdateProduct(Product product);

        /// <summary>
        ///     Deletes the provided product from the database.
        /// </summary>
        /// <param name="product">The product to be deleted.</param>
        /// <returns>
        ///     <b>true</b> if the product was deleted successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while deleting the product.
        /// </returns>
        bool DeleteProduct(Product product);
    }
}
