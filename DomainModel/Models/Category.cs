// <copyright file="Category.cs" company="Transilvania University of Brasov">
// Mihai Andrei Iulian
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>The Category model.</summary>
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="parentCategory">The parent category.</param>
        public Category(string name, Category parentCategory)
        {
            this.Name = name;
            this.ParentCategory = parentCategory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "[Name] must be between 1 and 100 characters.")]
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the parent category.</summary>
        /// <value>The parent category.</value>
        public virtual Category ParentCategory { get; set; }
    }
}
