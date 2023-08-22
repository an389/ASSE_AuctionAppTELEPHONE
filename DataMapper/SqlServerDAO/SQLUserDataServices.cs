// <copyright file="SQLUserDataServices.cs" company="Transilvania University of Brasov">
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
    ///     The user data services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SQLUserDataServices : IUserDataServices
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        /// <inheritdoc/>
        public bool AddUser(User user)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new user: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("User added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public IList<User> GetAllUsers()
        {
            IList<User> users = new List<User>();

            using (AuctionContext context = new AuctionContext())
            {
                users = context.Users.OrderBy((user) => user.Id).ToList();
            }

            return users;
        }

        /// <inheritdoc/>
        public User GetUserById(int id)
        {
            User user = null;

            using (AuctionContext context = new AuctionContext())
            {
                user = context.Users.Where((user) => user.Id == id).FirstOrDefault();
            }

            return user;
        }

        /// <inheritdoc/>
        public User GetUserByEmailAndPassword(string email, string password)
        {
            User user = null;

            using (AuctionContext context = new AuctionContext())
            {
                user = context.Users.Where((user) => user.Email == email && user.Password == password).FirstOrDefault();
            }

            return user;
        }

        /// <inheritdoc/>
        public bool EmailAlreadyExists(string email)
        {
            User user = null;

            using (AuctionContext context = new AuctionContext())
            {
                user = context.Users.Where((user) => user.Email == email).FirstOrDefault();
            }

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool UsernameAlreadyExists(string username)
        {
            User user = null;

            using (AuctionContext context = new AuctionContext())
            {
                user = context.Users.Where((user) => user.UserName == username).FirstOrDefault();
            }

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool UpdateUser(User user)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Users.Attach(user);
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Warn("Error while updating user: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("User updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteUser(User user)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Users.Attach(user);
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Warn("Error while deleting user: " + exception.Message.ToString() + " " + exception.InnerException.ToString());
                    return false;
                }
            }

            Logger.Info("User deleted successfully!");
            return true;
        }
    }
}
