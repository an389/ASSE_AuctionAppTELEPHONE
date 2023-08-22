// <copyright file="UpdateProductTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ProductServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="ProductServicesImplementation.UpdateProduct(Product)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class UpdateProductTests
    {
        /// <summary>Null product log message.</summary>
        private const string LogUpdateNullProduct = "Attempted to update a null product.";

        /// <summary>Invalid product log message.</summary>
        private const string LogUpdateInvalidProduct = "Attempted to update an invalid product.";

        /// <summary>Non-existing product log message.</summary>
        private const string LogUpdateNonexistingProduct = "Attempted to update a nonexisting product.";

        /// <summary>Too many auctions at the same time log message.</summary>
        private const string LogUpdateProductWithDuplicateDescription = "Attempted to update a product with a description too similar to existing product descriptions.";

        /// <summary>
        ///     Test for updating a null product.
        /// </summary>
        [Test]
        public void UPDATE_NullProduct()
        {
            Product nullProduct = null;

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(nullProduct));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullProduct)));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Name_Null()
        {
            Product product = new Product(
                null,
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Name_Empty()
        {
            Product product = new Product(
                string.Empty,
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Name_TooLong()
        {
            Product product = new Product(
                new string('x', 251),
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null description).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Description_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                null,
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty description).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Description_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                string.Empty,
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with description too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Description_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                new string('x', 501),
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null category).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_Category_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                null,
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null category name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidCategory_Name_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category(null, null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty category name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidCategory_Name_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category(string.Empty, null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with category name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidCategory_Name_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category(new string('x', 101), null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with negative starting price).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_StartingPrice_Negative()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                -1,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null seller).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_NullSeller()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                null,
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null seller first name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty seller first name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller first name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User('X' + new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller first name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_NoUpperCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller first name that only has uppercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_NoLowerCaseLetters()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller first name that contains symbols).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_ContainsSymbol()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller first name that contains numbers).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_FirstName_ContainsNumber()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null seller last name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty seller last name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller last name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", 'X' + new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller last name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_NoUpperCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller last name that only has uppercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_NoLowerCaseLetters()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller last name that contains symbols).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_ContainsSymbol()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller last name that contains numbers).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_LastName_ContainsNumber()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null seller username).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_UserName_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty seller username).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_UserName_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller username too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_UserName_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating a valid product (a product with null seller phone number).
        /// </summary>
        [Test]
        public void UPDATE_ValidProduct_ValidSeller_PhoneNumber_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetProductById(product.Id)).Returns(product);
            productServiceMock.Setup(x => x.GetAllProductDescriptions()).Returns(new List<string>());
            productServiceMock.Setup(x => x.UpdateProduct(product)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetConditionalValueByName("L")).Returns(100);
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(productServices.UpdateProduct(product));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty seller phone number).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_PhoneNumber_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller phone number too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_PhoneNumber_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", new string('8', 16), "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with invalid seller phone number).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_PhoneNumber_Invalid()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null seller email address).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Email_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty seller email address).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Email_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller email address too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Email_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 30) + '@' + new string('x', 30), "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with invalid seller email address).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Email_Invalid()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with null seller password).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with empty seller password).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller password too short).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_TooShort()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller password too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20)),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller password that doesn't contain uppercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_MissingUpperCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller password that doesn't contain lowercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_MissingLowerCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller password that doesn't contain numbers).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_MissingNumber()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with seller password that doesn't contain symbols).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_InvalidSeller_Password_MissingSymbol()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating an invalid product (a product with auction end date before auction start date).
        /// </summary>
        [Test]
        public void UPDATE_InvalidProduct_EndDateBeforeStartDate()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today);

            var productServiceMock = new Mock<IProductDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidProduct))));
        }

        /// <summary>
        ///     Test for updating a non-existing product.
        /// </summary>
        [Test]
        public void UPDATE_NonExistingProduct()
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

            var productMock = new Mock<Product>();
            productMock.SetupGet(p => p.Id).Returns(1);
            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetProductById(product.Id)).Returns(nullProduct);
            productServiceMock.Setup(x => x.UpdateProduct(product)).Returns(false);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNonexistingProduct)));
        }

        /// <summary>
        ///     Test for updating a valid product (a product with description too similar to existing product descriptions).
        /// </summary>
        [Test]
        public void UPDATE_ValidProduct_DescriptionTooSimilarToOtherProductDescriptions()
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
            productServiceMock.Setup(x => x.GetAllProductDescriptions()).Returns(new List<string>() { new string('#', 500), product.Description });
            productServiceMock.Setup(x => x.UpdateProduct(product)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetConditionalValueByName("L")).Returns(100);
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(productServices.UpdateProduct(product));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateProductWithDuplicateDescription)));
        }

        /// <summary>
        ///     Test for updating a valid product.
        /// </summary>
        [Test]
        public void UPDATE_ValidProduct()
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
            productServiceMock.Setup(x => x.GetAllProductDescriptions()).Returns(new List<string>());
            productServiceMock.Setup(x => x.UpdateProduct(product)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetConditionalValueByName("L")).Returns(100);
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(productServices.UpdateProduct(product));
        }
    }
}
