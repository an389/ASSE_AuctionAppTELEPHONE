// <copyright file="GetConditionTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ConditionServiceTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for
    ///         <see cref="ConditionServicesImplementation.GetAllConditions()"/>,
    ///         <see cref="ConditionServicesImplementation.GetConditionById(int)"/> and
    ///         <see cref="ConditionServicesImplementation.GetConditionByName(string)"/>
    ///     methods.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class GetConditionTests
    {
        /// <summary>
        ///     Test for retrieving all existing conditions.
        /// </summary>
        [Test]
        public void GET_AllConditions()
        {
            List<Condition> conditions = GetSampleConditions();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetAllConditions()).Returns(conditions);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            var expected = conditions;
            var actual = conditionServices.GetAllConditions();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                Assert.AreEqual(expected[i].Description, actual[i].Description);
                Assert.AreEqual(expected[i].Value, actual[i].Value);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing conditions but none were found.
        /// </summary>
        [Test]
        public void GET_AllConditions_NoneFound()
        {
            List<Condition> emptyConditionList = new List<Condition>();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetAllConditions()).Returns(emptyConditionList);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(conditionServices.GetAllConditions());
        }

        /// <summary>
        ///     Test for retrieving an existing condition with the specified id.
        /// </summary>
        [Test]
        public void GET_ConditionById()
        {
            Condition condition = GetSampleCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(condition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            var expected = condition;
            var actual = conditionServices.GetConditionById(condition.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        /// <summary>
        ///     Test for retrieving an existing condition with the specified id but no such condition was found.
        /// </summary>
        [Test]
        public void GET_ConditionById_NotFound()
        {
            Condition condition = GetSampleCondition();
            Condition nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(conditionServices.GetConditionById(condition.Id));
        }

        /// <summary>
        ///     Test for retrieving an existing condition with the specified name.
        /// </summary>
        [Test]
        public void GET_ConditionByName()
        {
            Condition condition = GetSampleCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionByName(condition.Name)).Returns(condition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            var expected = condition;
            var actual = conditionServices.GetConditionByName(condition.Name);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        /// <summary>
        ///     Test for retrieving an existing condition with the specified name but no such category was found.
        /// </summary>
        [Test]
        public void GET_ConditionByName_NotFound()
        {
            Condition condition = GetSampleCondition();
            Condition nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionByName(condition.Name)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(conditionServices.GetConditionByName(condition.Name));
        }

        /// <summary>Gets a sample condition.</summary>
        /// <returns>a sample condition.</returns>
        private static Condition GetSampleCondition()
        {
            return new Condition("X", "x", 42);
        }

        /// <summary>Gets sample conditions.</summary>
        /// <returns>a list of sample conditions.</returns>
        private static List<Condition> GetSampleConditions()
        {
            return new List<Condition>
            {
                new Condition("A", "aaa", 1),
                new Condition("B", "bbb", 2),
                new Condition("C", "ccc", 3),
            };
        }
    }
}
