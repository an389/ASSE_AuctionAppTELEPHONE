// <copyright file="DAOFactoryMethod.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>The DAOFactoryMethod class.</summary>
    [ExcludeFromCodeCoverage]
    public static class DAOFactoryMethod
    {
        /// <summary>
        ///     Initializes static members of the <see cref="DAOFactoryMethod"/> class.
        /// </summary>
        static DAOFactoryMethod()
        {
            string currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            CurrentDAOFactory = string.IsNullOrWhiteSpace(currentDataProvider)
                ? null
                : currentDataProvider.ToLower(culture: CultureInfo.CurrentCulture).Trim() switch
                {
                    "sqlserver" => new SQLServerDAOFactory(),
                    _ => new SQLServerDAOFactory(),
                };
        }

        /// <summary>Gets the current DAO factory.</summary>
        /// <value>The current DAO factory.</value>
        public static IDAOFactory CurrentDAOFactory { get; private set; }
    }
}
