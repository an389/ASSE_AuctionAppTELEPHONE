// <copyright file="SQLBidDataServices.cs" company="Transilvania University of Brasov">
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
    ///     The bid data services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SQLBidDataServices : IBidDataServices
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <inheritdoc/>
        public bool AddBid(Bid bid)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Products.Attach(bid.Product);
                    context.Users.Attach(bid.Buyer);
                    context.Bids.Add(bid);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new bid: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Bid added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public IList<Bid> GetAllBids()
        {
            IList<Bid> bids = new List<Bid>();

            using (AuctionContext context = new AuctionContext())
            {
                bids = context.Bids.Include("Product").Include("Buyer").OrderBy((bid) => bid.Id).ToList();
            }

            return bids;
        }

        /// <inheritdoc/>
        public Bid GetBidById(int id)
        {
            Bid bid = null;

            using (AuctionContext context = new AuctionContext())
            {
                bid = context.Bids.Include("Product").Include("Buyer").Where((bid) => bid.Id == id).FirstOrDefault();
            }

            return bid;
        }

        /// <inheritdoc/>
        public IList<Bid> GetBidsByProductId(int id)
        {
            IList<Bid> productBids = new List<Bid>();

            using (AuctionContext context = new AuctionContext())
            {
                productBids = context.Bids.Include("Product").Include("Buyer").Where((bid) => bid.Product.Id == id).OrderByDescending((bid) => bid.DateAndTime).ToList();
            }

            return productBids;
        }

        /// <inheritdoc/>
        public int GetNoOfActiveBidsByUserId(int id)
        {
            int activeBids;

            using (AuctionContext context = new AuctionContext())
            {
                activeBids = context.Bids.Include("Product").Include("Buyer").Where((bid) => bid.Buyer.Id == id && bid.Product.TerminationDate > DateTime.Now).Select((bid) => bid.Product.Id).Distinct().Count();
            }

            return activeBids;
        }

        /// <inheritdoc/>
        public bool UpdateBid(Bid bid)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Bids.Attach(bid);
                    context.Entry(bid).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating bid: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Bid updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteBid(Bid bid)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Bids.Attach(bid);
                    context.Bids.Remove(bid);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting bid: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Bid deleted successfully!");
            return true;
        }
    }
}
