// <copyright file="DeleteConditionTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ConditionServiceTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="ConditionServicesImplementation.DeleteCondition(DomainModel.Models.Condition)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class DeleteConditionTests
    {
        /// <summary>Null condition log message.</summary>
        private const string LogDeleteNullCondition = "Attempted to delete a null condition.";

        /// <summary>Existing condition log message.</summary>
        private const string LogDeleteNonexistingCondition = "Attempted to delete a nonexisting condition.";

        /// <summary>
        ///     Test for deleting a null condition.
        /// </summary>
        [Test]
        public void DELETE_NullCondition()
        {
            Condition condition = null;

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.DeleteCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullCondition)));
        }

        /// <summary>
        ///     Test for deleting a non-existing condition.
        /// </summary>
        [Test]
        public void DELETE_NonExistingCondition()
        {
            Condition condition = new Condition("X", "x", 42);
            Condition nullCondition = null;

            var serviceMock = new Mock<IConditionDataServices>();
            serviceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.DeleteCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNonexistingCondition)));
        }

        /// <summary>
        ///     Test for deleting a condition.
        /// </summary>
        [Test]
        public void DELETE_ValidCondition()
        {
            Condition condition = new Condition("X", "x", 42);

            var serviceMock = new Mock<IConditionDataServices>();
            serviceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(condition);
            serviceMock.Setup(x => x.DeleteCondition(condition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.DeleteCondition(condition));
        }
    }
}
