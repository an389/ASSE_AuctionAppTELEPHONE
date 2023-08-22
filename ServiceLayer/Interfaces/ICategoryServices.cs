// <copyright file="ICategoryServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The category services.
    /// </summary>
    public interface ICategoryServices
    {
        /// <summary>
        ///     Checks if the category is valid and adds it to the database.
        /// </summary>
        /// <param name="category">The category to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the category was added successfully.
        ///     <br/>
        ///     <b>false</b> if the category was null,
        ///         invalid,
        ///         a category with the same name already exists in the database
        ///         or the category could not be added to the database.
        /// </returns>
        bool AddCategory(Category category);

        /// <summary>
        ///     Gets all the categories from the database.
        /// </summary>
        /// <returns>
        ///     a list of all existing categories.
        /// </returns>
        IList<Category> GetAllCategories();

        /// <summary>
        ///     Gets the category with the provided id from the database.
        /// </summary>
        /// <param name="id">The category's id.</param>
        /// <returns>
        ///     the category with the provided id.
        /// </returns>
        Category GetCategoryById(int id);

        /// <summary>
        ///     Gets the category with the provided name from the database.
        /// </summary>
        /// <param name="name">The category's name.</param>
        /// <returns>
        ///     the category with the provided name.
        /// </returns>
        Category GetCategoryByName(string name);

        /// <summary>
        ///     Checks if the category is valid and updates it in the database.
        /// </summary>
        /// <param name="category">The category to be updated in the database.</param>
        /// <returns>
        ///     <b>true</b> if the category was updated successfully.
        ///     <br/>
        ///     <b>false</b> if the category was null,
        ///         invalid,
        ///         the category doesn't exist in the database,
        ///         the category name already exists in the database
        ///         or the category could not be updated in the database.
        /// </returns>
        bool UpdateCategory(Category category);

        /// <summary>
        ///     Checks if the category exists and deletes it from the database.
        /// </summary>
        /// <param name="category">The category to be deleted from the database.</param>
        /// <returns>
        ///     <b>true</b> if the category was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if the category was null,
        ///         the category doesn't exist in the database
        ///         or the category could not be deleted from the database.
        /// </returns>
        bool DeleteCategory(Category category);
    }
}
