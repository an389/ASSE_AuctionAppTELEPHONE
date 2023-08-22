// <copyright file="DeleteProductTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ProductServiceTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="ProductServicesImplementation.DeleteProduct(Product)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class DeleteProductTests
    {
        /// <summary>Null product log message.</summary>
        private const string LogDeleteNullProduct = "Attempted to delete a null product.";

        /// <summary>Existing product log message.</summary>
        private const string LogDeleteNonexistingProduct = "Attempted to delete a nonexisting product.";

        /// <summary>
        ///     Test for deleting a null product.
        /// </summary>
        [Test]
        public void DELETE_NullProduct()
        {
            Product product = null;

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.DeleteProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullProduct)));
        }

        /// <summary>
        ///     Test for deleting a non-existing product.
        /// </summary>
        [Test]
        public void DELETE_NonExistingProduct()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));
            Product nullProduct = null;

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetProductById(product.Id)).Returns(nullProduct);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.DeleteProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNonexistingProduct)));
        }

        /// <summary>
        ///     Test for deleting a product.
        /// </summary>
        [Test]
        public void DELETE_ValidProduct()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetProductById(product.Id)).Returns(product);
            productServiceMock.Setup(x => x.DeleteProduct(product)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(productServices.DeleteProduct(product));
        }
    }
}
