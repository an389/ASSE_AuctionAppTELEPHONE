// <copyright file="ProductServicesImplementation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>
    ///     The product services.
    /// </summary>
    public class ProductServicesImplementation : IProductServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The product data services.</summary>
        private IProductDataServices productDataServices;

        /// <summary>The user score and limits data services.</summary>
        private IUserScoreAndLimitsDataServices userScoreAndLimitsDataServices;

        /// <summary>Initializes a new instance of the <see cref="ProductServicesImplementation" /> class.</summary>
        /// <param name="productDataServices">The product data services.</param>
        /// <param name="userScoreAndLimitsDataServices">The user score and limits data services.</param>
        /// <param name="logger">The logger.</param>
        public ProductServicesImplementation(IProductDataServices productDataServices, IUserScoreAndLimitsDataServices userScoreAndLimitsDataServices, ILog logger)
        {
            this.productDataServices = productDataServices;
            this.userScoreAndLimitsDataServices = userScoreAndLimitsDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddProduct(Product product)
        {
            if (product != null)
            {
                var context = new ValidationContext(product, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(product, context, results, true))
                {
                    if (this.userScoreAndLimitsDataServices.GetUserLimitByUserId(product.Seller.Id)
                        - this.productDataServices.GetNoOfActiveAndFutureAuctionsByUserId(product.Seller.Id) > 0)
                    {
                        if (this.productDataServices.GetNoOfActiveAuctionsOfUserInInterval(product.Seller.Id, product.StartDate, product.TerminationDate)
                            < this.userScoreAndLimitsDataServices.GetConditionalValueByName("K"))
                        {
                            if (this.productDataServices.GetNoOfActiveAuctionsOfUserInCategory(product.Seller.Id, product.Category, product.StartDate, product.TerminationDate)
                                < this.userScoreAndLimitsDataServices.GetConditionalValueByName("M"))
                            {
                                if (!this.IsTooSimilarToOtherProductDescriptions(product.Description))
                                {
                                    return this.productDataServices.AddProduct(product);
                                }
                                else
                                {
                                    this.logger.Warn("Attempted to create a product with a description too similar to existing product descriptions.");
                                    return false;
                                }
                            }
                            else
                            {
                                this.logger.Warn("Attempted to create too many active auctions at the same time in the same category.");
                                return false;
                            }
                        }
                        else
                        {
                            this.logger.Warn("Attempted to create too many active auctions at the same time.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to create too many auctions.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid product. " + string.Join(' ', results));
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null product.");
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<Product> GetAllProducts()
        {
            return this.productDataServices.GetAllProducts();
        }

        /// <inheritdoc/>
        public Product GetProductById(int id)
        {
            return this.productDataServices.GetProductById(id);
        }

        /// <inheritdoc/>
        public bool IsTooSimilarToOtherProductDescriptions(string newProductDescription)
        {
            List<string> productDescriptions = this.productDataServices.GetAllProductDescriptions();
            int l = this.userScoreAndLimitsDataServices.GetConditionalValueByName("L");

            foreach (string productDescription in productDescriptions)
            {
                int n = newProductDescription.Length;
                int m = productDescription.Length;
                int[,] d = new int[n + 1, m + 1];

                // if (n == 0 && m < l)
                // {
                //    return true;
                // }

                // if (m == 0 && n < l)
                // {
                //    return true;
                // }

                // if (n != 0 && m != 0)
                // {
                for (int i = 0; i <= n; d[i, 0] = i++)
                {
                }

                for (int j = 0; j <= m; d[0, j] = j++)
                {
                }

                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= m; j++)
                    {
                        int cost = (productDescription[j - 1] == newProductDescription[i - 1]) ? 0 : 1;
                        d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                    }
                }

                if (d[n, m] < l)
                {
                    return true;
                }

                // }
            }

            return false;
        }

        /// <inheritdoc/>
        public bool UpdateProduct(Product product)
        {
            if (product != null)
            {
                var context = new ValidationContext(product, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(product, context, results, true))
                {
                    if (this.productDataServices.GetProductById(product.Id) != null)
                    {
                        if (!this.IsTooSimilarToOtherProductDescriptions(product.Description))
                        {
                            return this.productDataServices.UpdateProduct(product);
                        }
                        else
                        {
                            this.logger.Warn("Attempted to update a product with a description too similar to existing product descriptions.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to update a nonexisting product.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to update an invalid product. " + string.Join(' ', results));
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to update a null product.");
                return false;
            }
        }

        /// <inheritdoc/>
        public bool DeleteProduct(Product product)
        {
            if (product != null)
            {
                if (this.productDataServices.GetProductById(product.Id) != null)
                {
                    return this.productDataServices.DeleteProduct(product);
                }
                else
                {
                    this.logger.Warn("Attempted to delete a nonexisting product.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to delete a null product.");
                return false;
            }
        }
    }
}
