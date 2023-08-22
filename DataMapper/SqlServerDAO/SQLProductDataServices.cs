// <copyright file="SQLProductDataServices.cs" company="Transilvania University of Brasov">
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
    ///     The product data services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SQLProductDataServices : IProductDataServices
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <inheritdoc/>
        public bool AddProduct(Product product)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    product.TerminationDate = product.EndDate;

                    context.Categories.Attach(product.Category);
                    context.Users.Attach(product.Seller);
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new product: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Product added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public IList<Product> GetAllProducts()
        {
            IList<Product> products = new List<Product>();

            using (AuctionContext context = new AuctionContext())
            {
                products = context.Products.Include("Category").OrderBy((product) => product.Id).ToList();
            }

            return products;
        }

        /// <inheritdoc/>
        public Product GetProductById(int id)
        {
            Product product = null;

            using (AuctionContext context = new AuctionContext())
            {
                product = context.Products.Include("Category").Include("Seller").Where((product) => product.Id == id).FirstOrDefault();
            }

            return product;
        }

        /// <inheritdoc/>
        public int GetNoOfActiveAndFutureAuctionsByUserId(int id)
        {
            int noOfActiveAndFutureAuctions;

            using (AuctionContext context = new AuctionContext())
            {
                noOfActiveAndFutureAuctions = context.Products.Include("Seller").Where((product) => product.Seller.Id == id && product.TerminationDate > DateTime.Now).Count();
            }

            return noOfActiveAndFutureAuctions;
        }

        /// <inheritdoc/>
        public int GetNoOfActiveAuctionsOfUserInInterval(int id, DateTime newStart, DateTime newEnd)
        {
            int noOfActiveAuctionsInInterval;

            using (AuctionContext context = new AuctionContext())
            {
                noOfActiveAuctionsInInterval = context.Products.Where((product) => product.Seller.Id == id && newStart <= product.TerminationDate && product.StartDate <= newEnd).Count();
            }

            return noOfActiveAuctionsInInterval;
        }

        /// <inheritdoc/>
        public int GetNoOfActiveAuctionsOfUserInCategory(int id, Category category, DateTime newStart, DateTime newEnd)
        {
            int noOfActiveAuctionsInIntervalInCategory;

            using (AuctionContext context = new AuctionContext())
            {
                noOfActiveAuctionsInIntervalInCategory = context.Products.Include("Category").Where((product) => product.Category.Id == category.Id).Count();
            }

            return noOfActiveAuctionsInIntervalInCategory;
        }

        /// <inheritdoc/>
        public List<string> GetAllProductDescriptions()
        {
            List<string> productDescriptions = new List<string>();

            using (AuctionContext context = new AuctionContext())
            {
                productDescriptions = context.Products.Select((product) => product.Description).ToList();
            }

            return productDescriptions;
        }

        /// <inheritdoc/>
        public bool UpdateProduct(Product product)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Products.Attach(product);
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Warn("Error while updating product: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Product updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteProduct(Product product)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Products.Attach(product);
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting product: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Product deleted successfully!");
            return true;
        }
    }
}
