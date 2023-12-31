﻿using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    [ExcludeFromCodeCoverage]

    public class SQLUtilizatorDataServices : IUtilizatorDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);
        public bool AddUtilizator(Utilizator utilizator)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Utilizatori.Add(utilizator);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new user: " + exception);
                    return false;
                }
            }
            Logger.Info("User added successfully!");
            return true;
        }

        public bool DeleteUtilizator(Utilizator utilizator)
        {
            throw new NotImplementedException();
        }

        public bool EmailAlreadyExists(string email)
        {
            Utilizator utilizator = null;
            using(AuctionContext context = new AuctionContext()) { 
                utilizator = context.Utilizatori.Where((utilizator) => utilizator.Emali == email).FirstOrDefault();
            }
            if (utilizator != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Utilizator> GetAllUtilizatori()
        {
            List<Utilizator> utilizatori = new List<Utilizator>();
            using(AuctionContext context = new AuctionContext())
            {
                utilizatori = context.Utilizatori.ToList();
            }
           
            return utilizatori;
        }

        public Utilizator GetUserByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Utilizator GetUtilizatorById(int id)
        {
            Utilizator user = null;

            using (AuctionContext context = new AuctionContext())
            {
                user = context.Utilizatori.Where((user) => user.Id == id).FirstOrDefault();
            }

            return user;
        }

        public bool UpdateUtilizator(Utilizator utilizator)
        {
            throw new NotImplementedException();
        }


        public bool CloseAbonamentForUser(string userEmail, string abonament)
        {
            using (AuctionContext context = new AuctionContext())
            {

                try
                {
                    Utilizator user = context.Utilizatori.Where((user) => user.Emali == userEmail).FirstOrDefault();
                  //  user.AbonamentsId = user.AbonamentsId.Replace(abonament, newValue: string.Empty);
                    AbonamentUser abonamentUser = context.AbonamentUser.Where((abonamentUser) => abonamentUser.Utilizator.Id == user.Id && abonamentUser.Id == int.Parse(abonament)).FirstOrDefault();
                    abonamentUser.Activ = false;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception exception)
                {
                    Logger.Warn("Error while updating user: " + exception.Message.ToString() + " " );
                    return false;
                }
            }

        }
    }
}
