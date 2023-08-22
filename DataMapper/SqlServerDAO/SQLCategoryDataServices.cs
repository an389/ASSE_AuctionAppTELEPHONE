// <copyright file="SQLCategoryDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>
    ///     The category data services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SQLCategoryDataServices : ICategoryDataServices
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <inheritdoc/>
        public bool AddCategory(Category category)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    if (category.ParentCategory != null)
                    {
                        context.Categories.Attach(category.ParentCategory);
                    }

                    context.Categories.Add(category);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new category: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Category added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public IList<Category> GetAllCategories()
        {
            IList<Category> categories = new List<Category>();

            using (AuctionContext context = new AuctionContext())
            {
              //  categories = context.Categories.Include("ParentCategory").OrderBy((category) => category.Id).ToList();
            }

            return categories;
        }

        /// <inheritdoc/>
        public Category GetCategoryById(int id)
        {
            Category category = null;

            using (AuctionContext context = new AuctionContext())
            {
                category = context.Categories.Include("ParentCategory").Where((category) => category.Id == id).FirstOrDefault();
            }

            return category;
        }

        /// <inheritdoc/>
        public Category GetCategoryByName(string name)
        {
            Category category = null;

            using (AuctionContext context = new AuctionContext())
            {
                category = context.Categories.Include("ParentCategory").Where((category) => category.Name == name).FirstOrDefault();
            }

            return category;
        }

        /// <inheritdoc/>
        public bool UpdateCategory(Category category)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Categories.Attach(category);
                    context.Entry(category).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Warn("Error while updating new category: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Category updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteCategory(Category category)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Categories.Attach(category);
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting category: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Category deleted successfully!");
            return true;
        }
    }
}
