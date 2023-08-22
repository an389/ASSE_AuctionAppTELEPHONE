// <copyright file="IProductServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The product services.
    /// </summary>
    public interface IProductServices
    {
        /// <summary>
        ///     Checks if the product is valid and adds it to the database.
        /// </summary>
        /// <param name="product">The product to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the product was added successfully.
        ///     <br/>
        ///     <b>false</b> if the product was null,
        ///         invalid,
        ///         the user reached their limit of unfinished auctions,
        ///         the user reached their limit of unfinished auctions in a specific interval,
        ///         the user reached their limit of unfinished auctions in a specific category,
        ///         the product description is too similar to existing products' descriptions
        ///         or the product could not be added to the database.
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
        ///     Checks whether the provided product description is too similar to existing product descriptions.
        /// </summary>
        /// <param name="newProductDescription">The description of the new product.</param>
        /// <returns>
        ///     the product with the provided id.
        /// </returns>
        bool IsTooSimilarToOtherProductDescriptions(string newProductDescription);

        /// <summary>
        ///     Checks if the product is valid and updates it in the database.
        /// </summary>
        /// <param name="product">The product to be updated in the database.</param>
        /// <returns>
        ///     <b>true</b> if the product was updated successfully.
        ///     <br/>
        ///     <b>false</b> if the product was null,
        ///         invalid,
        ///         the product doesn't exist in the database,
        ///         the product description is too similar to existing products' descriptions
        ///         or the product could not be updated in the database.
        /// </returns>
        bool UpdateProduct(Product product);

        /// <summary>
        ///     Checks if the product exists and deletes it from the database.
        /// </summary>
        /// <param name="product">The product to be deleted from the database.</param>
        /// <returns>
        ///     <b>true</b> if the product was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if the product was null,
        ///         the product doesn't exist in the database
        ///         or the product could not be deleted from the database.
        /// </returns>
        bool DeleteProduct(Product product);
    }
}
