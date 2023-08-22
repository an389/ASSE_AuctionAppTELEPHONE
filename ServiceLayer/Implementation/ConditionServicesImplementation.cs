// <copyright file="ConditionServicesImplementation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>
    ///     The condition services.
    /// </summary>
    public class ConditionServicesImplementation : IConditionServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>.The condition data services.</summary>
        private IConditionDataServices conditionDataServices;

        /// <summary>Initializes a new instance of the <see cref="ConditionServicesImplementation" /> class.</summary>
        /// <param name="conditionDataServices">The condition data services.</param>
        /// <param name="logger">The logger.</param>
        public ConditionServicesImplementation(IConditionDataServices conditionDataServices, ILog logger)
        {
            this.conditionDataServices = conditionDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddCondition(Condition condition)
        {
            if (condition != null)
            {
                var context = new ValidationContext(condition, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(condition, context, results, true))
                {
                    if (this.conditionDataServices.GetConditionByName(condition.Name) == null)
                    {
                        return this.conditionDataServices.AddCondition(condition);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an already existing condition.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid condition. " + string.Join(' ', results));
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null condition.");
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<Condition> GetAllConditions()
        {
            return this.conditionDataServices.GetAllConditions();
        }

        /// <inheritdoc/>
        public Condition GetConditionById(int id)
        {
            return this.conditionDataServices.GetConditionById(id);
        }

        /// <inheritdoc/>
        public Condition GetConditionByName(string name)
        {
            return this.conditionDataServices.GetConditionByName(name);
        }

        /// <inheritdoc/>
        public bool UpdateCondition(Condition condition)
        {
            if (condition != null)
            {
                var context = new ValidationContext(condition, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(condition, context, results, true))
                {
                    if (this.conditionDataServices.GetConditionById(condition.Id) != null)
                    {
                        if (condition.Name == this.conditionDataServices.GetConditionById(condition.Id).Name
                            || this.conditionDataServices.GetConditionByName(condition.Name) == null)
                        {
                            return this.conditionDataServices.UpdateCondition(condition);
                        }
                        else
                        {
                            this.logger.Warn("Attempted to update a condition by changing the name to an existing condition name.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to update a nonexisting condition.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to update an invalid condition.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to update a null condition.");
                return false;
            }
        }

        /// <inheritdoc/>
        public bool DeleteCondition(Condition condition)
        {
            if (condition != null)
            {
                if (this.conditionDataServices.GetConditionById(condition.Id) != null)
                {
                    return this.conditionDataServices.DeleteCondition(condition);
                }
                else
                {
                    this.logger.Warn("Attempted to delete a nonexisting condition.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to delete a null condition.");
                return false;
            }
        }
    }
}
