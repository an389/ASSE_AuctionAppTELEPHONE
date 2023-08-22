// <copyright file="BidServicesImplementation.cs" company="Transilvania University of Brasov">
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
    ///     The bid services.
    /// </summary>
    public class BidServicesImplementation : IBidServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The bid data services.</summary>
        private IBidDataServices bidDataServices;

        /// <summary>The user score and limits data services.</summary>
        private IUserScoreAndLimitsDataServices userScoreAndLimitsDataServices;

        /// <summary>Initializes a new instance of the <see cref="BidServicesImplementation" /> class.</summary>
        /// <param name="bidDataServices">The bid data services.</param>
        /// <param name="userScoreAndLimitsDataServices">The user score and limits data services.</param>
        /// <param name="logger">The logger.</param>
        public BidServicesImplementation(IBidDataServices bidDataServices, IUserScoreAndLimitsDataServices userScoreAndLimitsDataServices, ILog logger)
        {
            this.bidDataServices = bidDataServices;
            this.userScoreAndLimitsDataServices = userScoreAndLimitsDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddBid(Bid bid)
        {
            if (bid != null)
            {
                var context = new ValidationContext(bid, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(bid, context, results, true))
                {
                    if (this.userScoreAndLimitsDataServices.GetUserLimitByUserId(bid.Buyer.Id)
                        - this.bidDataServices.GetNoOfActiveBidsByUserId(bid.Buyer.Id) > 0)
                    {
                        if (bid.DateAndTime > bid.Product.StartDate && bid.DateAndTime < bid.Product.EndDate && bid.DateAndTime < bid.Product.TerminationDate)
                        {
                            if (bid.Buyer.Id != bid.Product.Seller.Id)
                            {
                                if (bid.Currency == bid.Product.Currency)
                                {
                                    var existingBids = this.bidDataServices.GetBidsByProductId(bid.Product.Id);

                                    if ((existingBids.Count == 0 && bid.Amount >= bid.Product.StartingPrice)
                                        || (existingBids.Count > 0 && bid.Amount > existingBids[0].Amount && bid.Buyer.Id != existingBids[0].Buyer.Id))
                                    {
                                        return this.bidDataServices.AddBid(bid);
                                    }
                                    else
                                    {
                                        this.logger.Warn("Attempted to bid with not enough money or user already has the winning bid.");
                                        return false;
                                    }
                                }
                                else
                                {
                                    this.logger.Warn("Attempted to bid with different currency.");
                                    return false;
                                }
                            }
                            else
                            {
                                this.logger.Warn("Attempted to bid on an owned product.");
                                return false;
                            }
                        }
                        else
                        {
                            this.logger.Warn("Attempted to bid when the auction didn't start or has already ended.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to bid over limit.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid bid. " + string.Join(' ', results));
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null bid.");
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<Bid> GetAllBids()
        {
            return this.bidDataServices.GetAllBids();
        }

        /// <inheritdoc/>
        public Bid GetBidById(int id)
        {
            return this.bidDataServices.GetBidById(id);
        }

        /// <inheritdoc/>
        public IList<Bid> GetBidsByProductId(int id)
        {
            return this.bidDataServices.GetBidsByProductId(id);
        }

        /*
        public bool UpdateBid(Bid bid)
        {
            if (bid != null)
            {
                var context = new ValidationContext(bid, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(bid, context, results, true))
                {
                    if (_bidDataServices.GetBidById(bid.Id) != null)
                    {
                        return _bidDataServices.UpdateBid(bid);
                    }
                    else
                    {
                        _logger.Warn("Attempted to update a nonexisting bid.");
                        return false;
                    }
                }
                else
                {
                    _logger.Warn("Attempted to update an invalid bid.");
                    return false;
                }
            }
            else
            {
                _logger.Warn("Attempted to update a null bid.");
                return false;
            }
        }

        public bool DeleteBid(Bid bid)
        {
            if (bid != null)
            {
                if (_bidDataServices.GetBidById(bid.Id) != null)
                {
                    return _bidDataServices.DeleteBid(bid);
                }
                else
                {
                    _logger.Warn("Attempted to delete a nonexisting bid.");
                    return false;
                }
            }
            else
            {
                _logger.Warn("Attempted to delete a null bid.");
                return false;
            }
        }
        */
    }
}
