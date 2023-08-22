// <copyright file="UserServicesImplementation.cs" company="Transilvania University of Brasov">
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
    ///     The user services.
    /// </summary>
    public class UserServicesImplementation : IUserServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The user data services.</summary>
        private IUserDataServices userDataServices;

        /// <summary>Initializes a new instance of the <see cref="UserServicesImplementation" /> class.</summary>
        /// <param name="userDataServices">The user data services.</param>
        /// <param name="logger">The logger.</param>
        public UserServicesImplementation(IUserDataServices userDataServices, ILog logger)
        {
            this.userDataServices = userDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddUser(User user)
        {
            if (user != null)
            {
                var context = new ValidationContext(user, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(user, context, results, true))
                {
                    if (!this.userDataServices.EmailAlreadyExists(user.Email)
                        && !this.userDataServices.UsernameAlreadyExists(user.UserName))
                    {
                        return this.userDataServices.AddUser(user);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an already existing user.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid user.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null user.");
                return false;
            }
        }

        /// <inheritdoc/>
        public IList<User> GetAllUsers()
        {
            return this.userDataServices.GetAllUsers();
        }

        /// <inheritdoc/>
        public User GetUserById(int id)
        {
            return this.userDataServices.GetUserById(id);
        }

        /// <inheritdoc/>
        public User GetUserByEmailAndPassword(string email, string password)
        {
            return this.userDataServices.GetUserByEmailAndPassword(email, password);
        }

        /// <inheritdoc/>
        public bool UpdateUser(User user)
        {
            if (user != null)
            {
                var context = new ValidationContext(user, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(user, context, results, true))
                {
                    if (this.userDataServices.GetUserById(user.Id) != null)
                    {
                        return this.userDataServices.UpdateUser(user);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to update a nonexisting user.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to update an invalid user.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to update a null user.");
                return false;
            }
        }

        /// <inheritdoc/>
        public bool DeleteUser(User user)
        {
            if (user != null)
            {
                if (this.userDataServices.GetUserById(user.Id) != null)
                {
                    return this.userDataServices.DeleteUser(user);
                }
                else
                {
                    this.logger.Warn("Attempted to delete a nonexisting user.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to delete a null user.");
                return false;
            }
        }
    }
}
