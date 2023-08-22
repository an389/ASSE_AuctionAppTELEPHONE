// <copyright file="SQLRatingDataServices.cs" company="Transilvania University of Brasov">
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
    ///     The rating data services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SQLRatingDataServices : IRatingDataServices
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <inheritdoc/>
        public bool AddRating(Rating rating)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    Rating newRating = new Rating(
                        context.Products.Find(rating.Product.Id),
                        context.Users.Find(rating.RatingUser.Id),
                        context.Users.Find(rating.RatedUser.Id),
                        rating.Grade);

                    context.Ratings.Add(newRating);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new rating. " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Rating added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public IList<Rating> GetAllRatings()
        {
            IList<Rating> ratings = new List<Rating>();

            using (AuctionContext context = new AuctionContext())
            {
                ratings = context.Ratings.Include("Product").Include("RatingUser").Include("RatedUser").OrderBy((rating) => rating.Id).ToList();
            }

            return ratings;
        }

        /// <inheritdoc/>
        public Rating GetRatingById(int id)
        {
            Rating rating = null;

            using (AuctionContext context = new AuctionContext())
            {
                rating = context.Ratings.Include("Product").Include("RatingUser").Include("RatedUser").Where((rating) => rating.Id == id).FirstOrDefault();
            }

            return rating;
        }

        /// <inheritdoc/>
        public IList<Rating> GetRatingsByUserId(int id)
        {
            IList<Rating> ratings = new List<Rating>();

            using (AuctionContext context = new AuctionContext())
            {
                ratings = context.Ratings.Include("Product").Include("RatingUser").Include("RatedUser").Where((rating) => rating.RatedUser.Id == id).OrderByDescending((rating) => rating.DateAndTime).ToList();
            }

            return ratings;
        }

        /// <inheritdoc/>
        public Rating GetRatingByUserIdAndProductId(int userId, int productId)
        {
            Rating rating = null;

            using (AuctionContext context = new AuctionContext())
            {
                rating = context.Ratings.Include("Product").Include("RatingUser").Include("RatedUser").Where((rating) => rating.RatingUser.Id == userId && rating.Product.Id == productId).FirstOrDefault();
            }

            return rating;
        }

        /// <inheritdoc/>
        public User GetWinningBidUserByProductId(int id)
        {
            Bid winningBid = null;

            using (AuctionContext context = new AuctionContext())
            {
                winningBid = context.Bids.Include("Product").Include("User").Where((bid) => bid.Product.Id == id).OrderByDescending((bid) => bid.DateAndTime).FirstOrDefault();
            }

            if (winningBid != null)
            {
                return winningBid.Buyer;
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public bool UpdateRating(Rating rating)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Ratings.Attach(rating);
                    context.Entry(rating).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating rating. " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Rating updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteRating(Rating rating)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Ratings.Attach(rating);
                    context.Ratings.Remove(rating);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting rating. " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Rating deleted successfully!");
            return true;
        }
    }
}
