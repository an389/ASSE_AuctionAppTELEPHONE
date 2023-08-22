// <copyright file="AddCategoryTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace CategoryServiceTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="CategoryServicesImplementation.AddCategory(Category)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class AddCategoryTests
    {
        /// <summary>Null category log message.</summary>
        private const string LogAddNullCategory = "Attempted to add a null category.";

        /// <summary>Invalid category log message.</summary>
        private const string LogAddInvalidCategory = "Attempted to add an invalid category.";

        /// <summary>Existing category log message.</summary>
        private const string LogAddExistingCategory = "Attempted to add an already existing category.";

        /// <summary>
        ///     Test for adding a null category.
        /// </summary>
        [Test]
        public void ADD_NullCategory()
        {
            Category category = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.AddCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullCategory)));
        }

        /// <summary>
        ///     Test for adding an invalid category (a category with null name).
        /// </summary>
        [Test]
        public void ADD_InvalidCategory_Name_Null()
        {
            Category category = new Category(null, new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.AddCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCategory))));
        }

        /// <summary>
        ///     Test for adding an invalid category (a category with empty name).
        /// </summary>
        [Test]
        public void ADD_InvalidCategory_Name_Empty()
        {
            Category category = new Category(string.Empty, new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.AddCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCategory))));
        }

        /// <summary>
        ///     Test for adding an invalid category (a category with name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidCategory_Name_TooLong()
        {
            Category category = new Category(new string('x', 101), new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.AddCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCategory))));
        }

        /// <summary>
        ///     Test for adding an invalid category (a category with null parent category).
        /// </summary>
        [Test]
        public void ADD_ValidCategory_ParentCategory_Null()
        {
            Category category = new Category("Aparat foto", null);

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.AddCategory(category)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(categoryServices.AddCategory(category));
        }

        /// <summary>
        ///     Test for adding an already existing category.
        /// </summary>
        [Test]
        public void ADD_ValidCategory_ExistingCategory()
        {
            Category category = new Category("Aparat foto", null);

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryByName(category.Name)).Returns(category);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.AddCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddExistingCategory)));
        }

        /// <summary>
        ///     Test for adding a valid category.
        /// </summary>
        [Test]
        public void ADD_ValidCategory()
        {
            Category category = new Category("Aparat foto", null);
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryByName(category.Name)).Returns(nullCategory);
            serviceMock.Setup(x => x.AddCategory(category)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(categoryServices.AddCategory(category));
        }
    }
}
