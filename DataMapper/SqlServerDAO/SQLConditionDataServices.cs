// <copyright file="SQLConditionDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>
    ///     The condition data services.
    /// </summary>
    [ExcludeFromCodeCoverage]

    public class SQLConditionDataServices : IConditionDataServices
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <inheritdoc/>
        public bool AddCondition(Condition condition)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Conditions.Add(condition);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new condition: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Condition added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public IList<Condition> GetAllConditions()
        {
            IList<Condition> conditions = new List<Condition>();

            using (AuctionContext context = new AuctionContext())
            {
                conditions = context.Conditions.OrderBy((condition) => condition.Id).ToList();
            }

            return conditions;
        }

        /// <inheritdoc/>
        public Condition GetConditionById(int id)
        {
            Condition condition = null;

            using (AuctionContext context = new AuctionContext())
            {
                condition = context.Conditions.Where((condition) => condition.Id == id).FirstOrDefault();
            }

            return condition;
        }

        /// <inheritdoc/>
        public Condition GetConditionByName(string name)
        {
            Condition condition = null;

            using (AuctionContext context = new AuctionContext())
            {
                condition = context.Conditions.Where((condition) => condition.Name == name).FirstOrDefault();
            }

            return condition;
        }

        /// <inheritdoc/>
        public int GetK()
        {
            Condition condition = this.GetConditionByName("K");

            if (condition != null)
            {
                return condition.Value;
            }
            else
            {
                return 10;
            }
        }

        /// <inheritdoc/>
        public int GetM()
        {
            Condition condition = this.GetConditionByName("M");

            if (condition != null)
            {
                return condition.Value;
            }
            else
            {
                return 5;
            }
        }

        /// <inheritdoc/>
        public int GetS()
        {
            Condition condition = this.GetConditionByName("S");

            if (condition != null)
            {
                return condition.Value;
            }
            else
            {
                return 10;
            }
        }

        /// <inheritdoc/>
        public int GetN()
        {
            Condition condition = this.GetConditionByName("N");

            if (condition != null)
            {
                return condition.Value;
            }
            else
            {
                return 5;
            }
        }

        /// <inheritdoc/>
        public int GetT()
        {
            Condition condition = this.GetConditionByName("T");

            if (condition != null)
            {
                return condition.Value;
            }
            else
            {
                return 20;
            }
        }

        /// <inheritdoc/>
        public bool UpdateCondition(Condition condition)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Conditions.Attach(condition);
                    context.Entry(condition).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating condition: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Condition updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteCondition(Condition condition)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Conditions.Attach(condition);
                    context.Conditions.Remove(condition);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting condition: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("Condition deleted successfully!");
            return true;
        }
    }
}
