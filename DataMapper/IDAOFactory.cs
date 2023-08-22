// <copyright file="IDAOFactory.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;

    /// <summary>The service factory.</summary>
    public interface IDAOFactory
    {
        /// <summary>Gets the product data services.</summary>
        /// <value>The product data services.</value>
        [ExcludeFromCodeCoverage]
        IProductDataServices ProductDataServices
        {
            get;
        }

        /// <summary>Gets the category data services.</summary>
        /// <value>The category data services.</value>
        [ExcludeFromCodeCoverage]
        ICategoryDataServices CategoryDataServices
        {
            get;
        }

        /// <summary>Gets the bid data services.</summary>
        /// <value>The bid data services.</value>
        [ExcludeFromCodeCoverage]
        IBidDataServices BidDataServices
        {
            get;
        }

        /// <summary>Gets the user data services.</summary>
        /// <value>The user data services.</value>
        [ExcludeFromCodeCoverage]
        IUserDataServices UserDataServices
        {
            get;
        }

        /// <summary>Gets the rating data services.</summary>
        /// <value>The rating data services.</value>
        [ExcludeFromCodeCoverage]
        IRatingDataServices RatingDataServices
        {
            get;
        }

        /// <summary>Gets the condition data services.</summary>
        /// <value>The condition data services.</value>
        [ExcludeFromCodeCoverage]
        IConditionDataServices ConditionDataServices
        {
            get;
        }

        /// <summary>Gets the user score and limits data services.</summary>
        /// <value>The user score and limits data services.</value>
        [ExcludeFromCodeCoverage]
        IUserScoreAndLimitsDataServices UserScoreAndLimitsDataServices
        {
            get;
        }
        [ExcludeFromCodeCoverage]
        IUtilizatorDataServices UtilizatorDataServices
        {
            get;
        }

        [ExcludeFromCodeCoverage]
        IAbonamentDataServices AbonamentDataServices
        {
            get;
        }
        [ExcludeFromCodeCoverage]
        IBonusDataService BonusDataService
        {
            get;
        }
        [ExcludeFromCodeCoverage]
        IBuisniessDataService BuisniessDataService
        {
            get;
        }
        [ExcludeFromCodeCoverage]
        IFacturaDataService FacturaDataService
        {
            get;
        }
        ICentralaTelefonicaDataService CentralaTelefonicaDataService
        {
            get;
        }
    }
}
