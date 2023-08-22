// <copyright file="Product.cs" company="Transilvania University of Brasov">
// Mihai Andrei Iulian
// </copyright>

namespace DomainModel.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using DomainModel.Enums;

    /// <summary>The Product model.</summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="description">The description of the product.</param>
        /// <param name="category">The category the product is part of.</param>
        /// <param name="startingPrice">The starting price of the product.</param>
        /// <param name="currency">The currency the price is in.</param>
        /// <param name="seller">The user who is selling the product.</param>
        /// <param name="startDate">The date when the auction starts.</param>
        /// <param name="endDate">The date when the auction ends.</param>
        public Product(string name, string description, Category category, double startingPrice, ECurrency currency, User seller, DateTime startDate, DateTime endDate)
        {
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.StartingPrice = startingPrice;
            this.Currency = currency;
            this.Seller = seller;
            this.CreationDate = DateTime.Now;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.TerminationDate = endDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 250, MinimumLength = 1, ErrorMessage = "[Name] must be between 1 and 250 characters.")]
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "[Description] cannot be null.")]
        [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "[Description] must be between 1 and 500 characters.")]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [Required(ErrorMessage = "[Category] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Category] must be a valid category.")]
        public virtual Category Category { get; set; }

        /// <summary>Gets or sets the starting price.</summary>
        /// <value>The starting price.</value>
        [Required(ErrorMessage = "[StartingPrice] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[StartingPrice] cannot be negative.")]
        public virtual double StartingPrice { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        [Required(ErrorMessage = "[Currency] cannot be null.")]
        public virtual ECurrency Currency { get; set; }

        /// <summary>Gets the seller.</summary>
        /// <value>The seller.</value>
        [Required(ErrorMessage = "[Seller] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Seller] must be a valid user.")]
        public virtual User Seller { get; private set; }

        /// <summary>Gets the start date.</summary>
        /// <value>The start date.</value>
        [Required(ErrorMessage = "[CreationDate] cannot be null.")]
        public virtual DateTime CreationDate { get; private set; }

        /// <summary>Gets the start date.</summary>
        /// <value>The start date.</value>
        [Required(ErrorMessage = "[StartDate] cannot be null.")]
        public virtual DateTime StartDate { get; private set; }

        /// <summary>Gets the end date.</summary>
        /// <value>The end date.</value>
        [Required(ErrorMessage = "[EndDate] cannot be null.")]
        [EndDateAfterStartDate(ErrorMessage = "[EndDate] cannot be before [StartDate].")]
        public virtual DateTime EndDate { get; private set; }

        /// <summary>Gets or sets the termination date.</summary>
        /// <value>The termination date.</value>
        [Required(ErrorMessage = "[TerminationDate] cannot be null.")]
        public virtual DateTime TerminationDate { get; set; }
    }
}
