// <copyright file="Bid.cs" company="Transilvania University of Brasov">
// Mihai Andrei Iulian
// </copyright>

namespace DomainModel.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;

    /// <summary>The Bid model.</summary>
    public class Bid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bid"/> class.
        /// </summary>
        /// <param name="product">The product the user is bidding on.</param>
        /// <param name="user">The user who is bidding on the product.</param>
        /// <param name="amount">The amount the user is bidding with.</param>
        /// <param name="currency">The currency of the bidding amount.</param>
        public Bid(Product product, User user, double amount, ECurrency currency)
        {
            this.DateAndTime = DateTime.Now;
            this.Product = product;
            this.Buyer = user;
            this.Amount = amount;
            this.Currency = currency;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bid"/> class.
        /// </summary>
        public Bid()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets the date and time.</summary>
        /// <value>The date and time.</value>
        [Required(ErrorMessage = "[DateAndTime] cannot be null.")]
        public virtual DateTime DateAndTime { get; private set; }

        /// <summary>Gets the product.</summary>
        /// <value>The product.</value>
        [Required(ErrorMessage = "[Product] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Product] must be a valid product.")]
        public virtual Product Product { get; private set; }

        /// <summary>Gets the user.</summary>
        /// <value>The user.</value>
        [Required(ErrorMessage = "[Buyer] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Buyer] must be a valid user.")]
        public virtual User Buyer { get; private set; }

        /// <summary>Gets the amount.</summary>
        /// <value>The amount.</value>
        [Required(ErrorMessage = "[Amount] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[Amount] must be greater than zero.")]
        public virtual double Amount { get; private set; }

        /// <summary>Gets the currency.</summary>
        /// <value>The currency.</value>
        [Required(ErrorMessage = "[Currency] cannot be null.")]
        public virtual ECurrency Currency { get; private set; }
    }
}
