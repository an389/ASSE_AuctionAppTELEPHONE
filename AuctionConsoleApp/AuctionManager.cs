// <copyright file="AuctionManager.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace AuctionConsoleApp
{
    using System;
    using System.Collections.Generic;
    using DataMapper;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Implementation;

    /// <summary>The AuctionManager class.</summary>
    public class AuctionManager
    {
        /// <summary>The current DAO factory.</summary>
        private readonly IDAOFactory currentDAOFactory;

        /// <summary>The logger.</summary>
        private readonly ILog logger;

        /// <summary>The category services.</summary>
        private CategoryServicesImplementation categoryServices;

        /// <summary>The product services.</summary>
        private ProductServicesImplementation productServices;

        /// <summary>The user services.</summary>
        private UserServicesImplementation userServices;

        /// <summary>The bid services.</summary>
        private BidServicesImplementation bidServices;

        /// <summary>The rating services.</summary>
        private RatingServicesImplementation ratingServices;

        /// <summary>The condition services.</summary>
        private ConditionServicesImplementation conditionServices;

        /// <summary>Initializes a new instance of the <see cref="AuctionManager" /> class.</summary>
        /// <param name="currentDAOFactory">The current DAO factory.</param>
        /// <param name="logger">The logger.</param>
        public AuctionManager(IDAOFactory currentDAOFactory, ILog logger)
        {
            this.currentDAOFactory = currentDAOFactory;
            this.logger = logger;

            this.categoryServices = new CategoryServicesImplementation(this.currentDAOFactory.CategoryDataServices, this.logger);
            this.productServices = new ProductServicesImplementation(this.currentDAOFactory.ProductDataServices, this.currentDAOFactory.UserScoreAndLimitsDataServices, this.logger);
            this.userServices = new UserServicesImplementation(this.currentDAOFactory.UserDataServices, this.logger);
            this.bidServices = new BidServicesImplementation(this.currentDAOFactory.BidDataServices, this.currentDAOFactory.UserScoreAndLimitsDataServices, this.logger);
            this.ratingServices = new RatingServicesImplementation(this.currentDAOFactory.RatingDataServices, this.logger);
            this.conditionServices = new ConditionServicesImplementation(this.currentDAOFactory.ConditionDataServices, this.logger);

            IList<Category> categories = this.categoryServices.GetAllCategories();
        }

        public void CategoryManager()
        {
            if (this.categoryServices.GetAllCategories().Count == 0)
            {
                foreach (Category category in this.GetTopLevelCategories())
                {
                    this.categoryServices.AddCategory(category);
                }

                foreach (Category category in this.GetSecondLevelCategories())
                {
                    this.categoryServices.AddCategory(category);
                }

                foreach (Category category in this.GetThirdLevelCategories())
                {
                    this.categoryServices.AddCategory(category);
                }

                Category nullCategory = null;
                Category emptyCategory = new Category();
                this.categoryServices.AddCategory(nullCategory);
                this.categoryServices.AddCategory(emptyCategory);

                foreach (Category category in this.GetThirdLevelCategories())
                {
                    this.categoryServices.AddCategory(category);
                }
            }
        }

        public void ProductManager()
        {
            if (this.productServices.GetAllProducts().Count == 0)
            {
                foreach (Product product in this.GetProducts())
                {
                    this.productServices.AddProduct(product);
                }

                Product nullProduct = null;
                Product emptyProduct = new Product();
                this.productServices.AddProduct(nullProduct);
                this.productServices.AddProduct(emptyProduct);
            }
        }

        public void UserManager()
        {
            foreach (User user in this.GetUsers())
            {
                this.userServices.AddUser(user);
            }

            User nullUser = null;
            User emptyUser = new User();
            this.userServices.AddUser(nullUser);
            this.userServices.AddUser(emptyUser);

            foreach (User user in this.GetUsers())
            {
                this.userServices.AddUser(user);
            }
        }

        public void BidManager()
        {
            foreach (Bid bid in this.GetBids())
            {
                this.bidServices.AddBid(bid);
            }

            Bid nullBid = null;
            Bid emptyBid = new Bid();
            this.bidServices.AddBid(nullBid);
            this.bidServices.AddBid(emptyBid);

            foreach (Bid bid in this.GetBids())
            {
                this.bidServices.AddBid(bid);
            }
        }

        public void RatingManager()
        {
            Console.WriteLine();

            foreach (Rating rating in this.GetRatings())
            {
                this.ratingServices.AddRating(rating);
            }

            Rating nullRating = null;
            Rating emptyRating = new Rating();
            this.ratingServices.AddRating(nullRating);
            this.ratingServices.AddRating(emptyRating);

            foreach (Rating rating in this.GetRatings())
            {
                this.ratingServices.AddRating(rating);
            }
        }

        public void ConditionManager()
        {
            if (this.conditionServices.GetAllConditions().Count == 0)
            {
                foreach (Condition condition in this.GetConditions())
                {
                    this.conditionServices.AddCondition(condition);
                }
            }

            Condition nullCondition = null;
            Condition emptyCondition = new Condition();
            this.conditionServices.AddCondition(nullCondition);
            this.conditionServices.AddCondition(emptyCondition);
        }

        /// <summary>Gets the top level categories.</summary>
        /// <returns>A list of categories.</returns>
        public List<Category> GetTopLevelCategories()
        {
            return new List<Category>
            {
                new Category("Laptop, Tablete & Telefoane", null),
                new Category("PC, Periferice & Software", null),
                new Category("TV, Audio-Video & Foto", null),
                new Category("Electrocasnice & Climatizare", null),
            };
        }

        /// <summary>Gets the second level categories.</summary>
        /// <returns>A list of categories.</returns>
        public List<Category> GetSecondLevelCategories()
        {
            return new List<Category>
            {
                new Category("Laptopuri si accesorii", this.categoryServices.GetCategoryByName("Laptop, Tablete & Telefoane")),
                new Category("Telefoane mobile & accesorii", this.categoryServices.GetCategoryByName("Laptop, Tablete & Telefoane")),
                new Category("Tablete si accesorii", this.categoryServices.GetCategoryByName("Laptop, Tablete & Telefoane")),
                new Category("Desktop PC & Monitoare", this.categoryServices.GetCategoryByName("PC, Periferice & Software")),
                new Category("Componente PC", this.categoryServices.GetCategoryByName("PC, Periferice & Software")),
                new Category("Software", this.categoryServices.GetCategoryByName("PC, Periferice & Software")),
                new Category("Periferice PC", this.categoryServices.GetCategoryByName("PC, Periferice & Software")),
                new Category("Imprimante, scanere & consumabile", this.categoryServices.GetCategoryByName("PC, Periferice & Software")),
                new Category("Televizoare & accesorii", this.categoryServices.GetCategoryByName("TV, Audio-Video & Foto")),
                new Category("Aparate foto & accesorii", this.categoryServices.GetCategoryByName("TV, Audio-Video & Foto")),
                new Category("Aparate frigorifice", this.categoryServices.GetCategoryByName("Electrocasnice & Climatizare")),
                new Category("Masini de spalat rufe", this.categoryServices.GetCategoryByName("Electrocasnice & Climatizare")),
                new Category("Aragazuri, hote si cuptoare", this.categoryServices.GetCategoryByName("Electrocasnice & Climatizare")),
                new Category("Masini de spalat vase", this.categoryServices.GetCategoryByName("Electrocasnice & Climatizare")),
                new Category("Aspiratoare & fiare de calcat", this.categoryServices.GetCategoryByName("Electrocasnice & Climatizare")),
                new Category("Aparate de aer conditionat", this.categoryServices.GetCategoryByName("Electrocasnice & Climatizare")),
            };
        }

        /// <summary>Gets the third level categories.</summary>
        /// <returns>A list of categories.</returns>
        public List<Category> GetThirdLevelCategories()
        {
            return new List<Category>
            {
                new Category("Laptopuri", this.categoryServices.GetCategoryByName("Laptopuri si accesorii")),
                new Category("Accesorii laptop", this.categoryServices.GetCategoryByName("Laptopuri si accesorii")),
                new Category("Telefoane mobile", this.categoryServices.GetCategoryByName("Telefoane mobile & accesorii")),
                new Category("Accesorii telefoane", this.categoryServices.GetCategoryByName("Telefoane mobile & accesorii")),
                new Category("Tablete", this.categoryServices.GetCategoryByName("Tablete si accesorii")),
                new Category("Accesorii tablete", this.categoryServices.GetCategoryByName("Tablete si accesorii")),
            };
        }

        /// <summary>Gets the products.</summary>
        /// <returns>A list of products.</returns>
        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product("Laptop ultraportabil Lenovo IdeaPad 5 Pro 14ACN6", "procesor AMD Ryzen™ 5 5600U, 14\", 2.8K, 90Hz, 16GB, 1TB SSD, AMD Radeon Graphics, No OS, Cloud Grey", this.categoryServices.GetCategoryByName("Laptopuri"), 2999.99, ECurrency.RON, this.userServices.GetUserById(1), DateTime.Today, DateTime.Today.AddDays(30)),
                new Product("Laptop ultraportabil HP 240 G8", "procesor Intel Core i3-1005G1, 14\", HD, 8GB, 256GB SSD, Intel UHD Graphics, Free DOS, Dark Ash Silver", this.categoryServices.GetCategoryByName("Laptopuri"), 299.99, ECurrency.EUR, this.userServices.GetUserById(2), DateTime.Today, DateTime.Today.AddDays(30)),
                new Product("Laptop ASUS X515EA", "procesor Intel® Core™ i5-1135G7, 15.6\", Full HD, 8GB, 512GB SSD, Intel Iris Xᵉ Graphics, No OS, Slate grey", this.categoryServices.GetCategoryByName("Laptopuri"), 2349, ECurrency.RON, this.userServices.GetUserById(2), DateTime.Today, DateTime.Today.AddDays(30)),
                new Product("Laptop ultraportabil Dell Latitude 3410", "procesor Intel Celeron 5205U, 14\" Full HD, Memorie RAM 4GB DDR4, Stocare 128GB SSD, Intel UHD Graphics, Windows 10 Pro, Grey", this.categoryServices.GetCategoryByName("Laptopuri"), 2349.99, ECurrency.RON, this.userServices.GetUserById(2), DateTime.Today, DateTime.Today.AddDays(30)),
            };
        }

        /// <summary>Gets the users.</summary>
        /// <returns>A list of users.</returns>
        public List<User> GetUsers()
        {
            return new List<User>
            {
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Andrei", "Costache", "Costica", null, "andrei.costrache@FakeEmail.com", "@AbCd123"),
                new User("Andrei", "Mihai", "Mihaita", null, "andrei.Mihaita@FakeEmail.com", "@AbCd123")
            };
        }

        /// <summary>Gets the bids.</summary>
        /// <returns>A list of bids.</returns>
        public List<Bid> GetBids()
        {
            return new List<Bid>
            {
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), 300, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("adrian.matei@FakeEmail.com", "P@ssword123"), 325, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), 350, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("adrian.matei@FakeEmail.com", "P@ssword123"), 375, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), 400, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("adrian.matei@FakeEmail.com", "P@ssword123"), 500, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), 750, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("adrian.matei@FakeEmail.com", "P@ssword123"), 1000, ECurrency.EUR),
                new Bid(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), 1100, ECurrency.EUR),
            };
        }

        /// <summary>Gets the ratings.</summary>
        /// <returns>A list of ratings.</returns>
        public List<Rating> GetRatings()
        {
            Random rnd = new Random();

            return new List<Rating>
            {
                new Rating(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), this.userServices.GetUserByEmailAndPassword("dinu.garbuz@FakeEmail.com", "P@rola123"), rnd.Next(5, 10)),
                new Rating(this.productServices.GetProductById(2), this.userServices.GetUserByEmailAndPassword("dinu.garbuz@FakeEmail.com", "P@rola123"), this.userServices.GetUserByEmailAndPassword("andrei.costrache@FakeEmail.com", "@AbCd123"), rnd.Next(5, 10)),
            };
        }

        /// <summary>Gets the conditions.</summary>
        /// <returns>A list of conditions.</returns>
        public List<Condition> GetConditions()
        {
            return new List<Condition>
            {
                new Condition("K", "Maximum number of active auctions.", 10),
                new Condition("M", "Maximum number of active auctions in one category.", 5),
                new Condition("S", "Maximum score.", 10),
                new Condition("N", "Number of ratings used to calculate user score.", 5),
                new Condition("T", "Maximum number of auctions / auctioned products with a perfect score.", 20),
                new Condition("L", "Maximum Levenshtein distance between two product descriptions.", 10),
            };
        }
    }
}
