// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace AuctionConsoleApp
{
    using System;
    using DataMapper;
    using log4net;

    /// <summary>The Program class.</summary>
    public class Program
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            AuctionManager auctionManager = new AuctionManager(DAOFactoryMethod.CurrentDAOFactory, Logger);
            ReteaMobilaManager reteaMobilaManager = new ReteaMobilaManager(DAOFactoryMethod.CurrentDAOFactory, Logger);

            auctionManager.ConditionManager();
            auctionManager.CategoryManager();
            auctionManager.UserManager();
            auctionManager.ProductManager();
            auctionManager.BidManager();
            auctionManager.RatingManager();

            reteaMobilaManager.BuisniessManager();
            reteaMobilaManager.AbonamentManager();
            reteaMobilaManager.BonusuriManager();
            reteaMobilaManager.UtilizatorManager();
            reteaMobilaManager.AbonamentAndUserManager();
            reteaMobilaManager.CentralaTelefonicaManager();
            reteaMobilaManager.FacturaManager();

        }
    }
}
