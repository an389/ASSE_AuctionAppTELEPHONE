// <copyright file="CategoryServicesImplementation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>
    ///     The category services.
    /// </summary>
    public class CategoryServicesImplementation : ICategoryServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The category data services.</summary>
        private ICategoryDataServices categoryDataServices;

        /// <summary>Initializes a new instance of the <see cref="CategoryServicesImplementation" /> class.</summary>
        /// <param name="categoryDataServices">The category data services.</param>
        /// <param name="logger">The logger.</param>
        public CategoryServicesImplementation(ICategoryDataServices categoryDataServices, ILog logger)
        {
            this.categoryDataServices = categoryDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddCategory(Category category)
        {
            if (category != null)
            {
                var context = new ValidationContext(category, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(category, context, results, true))
                {
                    if (this.categoryDataServices.GetCategoryByName(category.Name) == null)
                    {
                        return this.categoryDataServices.AddCategory(category);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an already existing category.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid category.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null category.");
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<Category> GetAllCategories()
        {
            return this.categoryDataServices.GetAllCategories();
        }

        /// <inheritdoc/>
        public Category GetCategoryById(int id)
        {
            return this.categoryDataServices.GetCategoryById(id);
        }

        /// <inheritdoc/>
        public Category GetCategoryByName(string name)
        {
            return this.categoryDataServices.GetCategoryByName(name);
        }

        /// <inheritdoc/>
        public bool UpdateCategory(Category category)
        {
            if (category != null)
            {
                var context = new ValidationContext(category, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(category, context, results, true))
                {
                    if (this.categoryDataServices.GetCategoryById(category.Id) != null)
                    {
                        if (category.Name == this.categoryDataServices.GetCategoryById(category.Id).Name
                            || this.categoryDataServices.GetCategoryByName(category.Name) == null)
                        {
                            return this.categoryDataServices.UpdateCategory(category);
                        }
                        else
                        {
                            this.logger.Warn("Attempted to update a category by changing the name to an existing category name.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to update a nonexisting category.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to update an invalid category.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to update a null category.");
                return false;
            }
        }

        /// <inheritdoc/>
        public bool DeleteCategory(Category category)
        {
            if (category != null)
            {
                if (this.categoryDataServices.GetCategoryById(category.Id) != null)
                {
                    return this.categoryDataServices.DeleteCategory(category);
                }
                else
                {
                    this.logger.Warn("Attempted to delete a nonexisting category.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to delete a null category.");
                return false;
            }
        }
    }
}
