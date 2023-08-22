// <copyright file="RatingServicesImplementation.cs" company="Transilvania University of Brasov">
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
    ///     The rating services.
    /// </summary>
    public class RatingServicesImplementation : IRatingServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The rating data services.</summary>
        private IRatingDataServices ratingDataServices;

        /// <summary>Initializes a new instance of the <see cref="RatingServicesImplementation" /> class.</summary>
        /// <param name="ratingDataServices">The rating data services.</param>
        /// <param name="logger">The logger.</param>
        public RatingServicesImplementation(IRatingDataServices ratingDataServices, ILog logger)
        {
            this.ratingDataServices = ratingDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddRating(Rating rating)
        {
            if (rating != null)
            {
                var context = new ValidationContext(rating, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(rating, context, results, true))
                {
                    if (rating.DateAndTime >= rating.Product.TerminationDate)
                    {
                        if (this.ratingDataServices.GetRatingByUserIdAndProductId(rating.RatingUser.Id, rating.Product.Id) == null)
                        {
                            User winningBidUser = this.ratingDataServices.GetWinningBidUserByProductId(rating.Product.Id);

                            if ((rating.RatingUser.Id == rating.Product.Seller.Id && rating.RatedUser.Id == winningBidUser.Id)
                                || (rating.RatingUser.Id == winningBidUser.Id && rating.RatedUser.Id == rating.Product.Seller.Id))
                            {
                                return this.ratingDataServices.AddRating(rating);
                            }
                            else
                            {
                                this.logger.Warn("Attempted to add rating to other user.");
                                return false;
                            }
                        }
                        else
                        {
                            this.logger.Warn("Attempted to add rating again on an auction.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add rating on active auction.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid rating. " + string.Join(' ', results));
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null rating.");
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<Rating> GetAllRatings()
        {
            return this.ratingDataServices.GetAllRatings();
        }

        /// <inheritdoc/>
        public Rating GetRatingById(int id)
        {
            return this.ratingDataServices.GetRatingById(id);
        }

        /// <inheritdoc/>
        public IList<Rating> GetRatingsByUserId(int id)
        {
            return this.ratingDataServices.GetRatingsByUserId(id);
        }

        /// <inheritdoc/>
        public bool UpdateRating(Rating rating)
        {
            if (rating != null)
            {
                var context = new ValidationContext(rating, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(rating, context, results, true))
                {
                    if (this.ratingDataServices.GetRatingById(rating.Id) != null)
                    {
                        return this.ratingDataServices.UpdateRating(rating);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to update a nonexisting rating.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to update an invalid rating. " + string.Join(' ', results));
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to update a null rating.");
                return false;
            }
        }

        /// <inheritdoc/>
        public bool DeleteRating(Rating rating)
        {
            if (rating != null)
            {
                if (this.ratingDataServices.GetRatingById(rating.Id) != null)
                {
                    return this.ratingDataServices.DeleteRating(rating);
                }
                else
                {
                    this.logger.Warn("Attempted to delete a nonexisting rating.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to delete a null rating.");
                return false;
            }
        }
    }
}
