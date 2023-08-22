// <copyright file="ICategoryDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    using System.Collections.Generic;
    using DomainModel.Models;

    /// <summary>
    ///     The category data services.
    /// </summary>
    public interface ICategoryDataServices
    {
        /// <summary>
        ///     Adds a new category to the database.
        /// </summary>
        /// <param name="category">The category to be added to the database.</param>
        /// <returns>
        ///     <b>true</b> if the category was added successfully to the database.
        ///     <br />
        ///     <b>false</b> if an error occurred while adding the category to the database.
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
        ///     Updates the provided category in the database.
        /// </summary>
        /// <param name="category">The category to be updated.</param>
        /// <returns>
        ///     <b>true</b> if the category was updated successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while updating the category.
        /// </returns>
        bool UpdateCategory(Category category);

        /// <summary>
        ///     Deletes the provided category from the database.
        /// </summary>
        /// <param name="category">The category to be deleted.</param>
        /// <returns>
        ///     <b>true</b> if the category was deleted successfully.
        ///     <br />
        ///     <b>false</b> if an error occurred while deleting the category.
        /// </returns>
        bool DeleteCategory(Category category);
    }
}
