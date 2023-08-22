// <copyright file="DeleteCategoryTests.cs" company="Transilvania University of Brasov">
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
    ///     Test class for <see cref="CategoryServicesImplementation.DeleteCategory(Category)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class DeleteCategoryTests
    {
        /// <summary>Null category log message.</summary>
        private const string LogDeleteNullCategory = "Attempted to delete a null category.";

        /// <summary>Existing category log message.</summary>
        private const string LogDeleteNonexistingCategory = "Attempted to delete a nonexisting category.";

        /// <summary>
        ///     Test for deleting a null category.
        /// </summary>
        [Test]
        public void DELETE_NullCategory()
        {
            Category category = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.DeleteCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullCategory)));
        }

        /// <summary>
        ///     Test for deleting a non-existing category.
        /// </summary>
        [Test]
        public void DELETE_NonExistingCategory()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(nullCategory);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.DeleteCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNonexistingCategory)));
        }

        /// <summary>
        ///     Test for deleting a category.
        /// </summary>
        [Test]
        public void DELETE_ValidCategory()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(category);
            serviceMock.Setup(x => x.DeleteCategory(category)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(categoryServices.DeleteCategory(category));
        }
    }
}
