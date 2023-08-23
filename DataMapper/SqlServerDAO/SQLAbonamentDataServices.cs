using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataMapper.SqlServerDAO
{
    public class SQLAbonamentDataServices : IAbonamentDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        public bool AddAbonament(Abonament abonament)
        {
            using(AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Abonaments.Add(abonament);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error while adding new abonament: " + ex);
                    return false;
                }
                Logger.Info("Abonament added successfully!");
                return true;
            }
        }

        public bool AddAbonamentForUtilizator(string userEmail, string abonamentName)
        {
            Abonament abonament = null;
            Utilizator utilizator = null;
            AbonamentUser abonamentUser = null;
            using (AuctionContext context = new AuctionContext())
            {
                try
                {

                    abonament = context.Abonaments.Where((abonament) => abonament.Name == abonamentName).FirstOrDefault();
                    utilizator = context.Utilizatori.Where((utilizator) => utilizator.Emali == userEmail).FirstOrDefault();
                    abonamentUser = new AbonamentUser(utilizator, abonament);
                    context.AbonamentUser.Add(abonamentUser);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error while adding new abonament for user: " + ex);
                    return false;
                }

                Logger.Info("Abonament for user added successfully!");
                return true;
            }
        }

        public bool FindAbonamentByName(string abonamentName)
        {
            Abonament abonament = null;
            using (AuctionContext context = new AuctionContext())
            {
                abonament = context.Abonaments.Where((abonament) => abonament.Name == abonamentName).FirstOrDefault();
            }

            return abonament != null ? true : false;
        }

        public Abonament GetAbonamentById(int id)
        {
            Abonament abonament = null;
            using (AuctionContext context = new AuctionContext())
            {
                abonament = context.Abonaments.Where((abonament) => abonament.Id == id).FirstOrDefault();
            }

            return abonament;
        }

        public List<AbonamentUser> GetAbonamentsByUserId(int id)
        {
            Abonament abonament = null;
            Utilizator utilizator = null;
            List<AbonamentUser> abonamentUser = null;
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    abonamentUser = context.AbonamentUser.Where((abonamentUser) => abonamentUser.Utilizator.Id == id).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error while adding new abonament for user: " + ex);
                }

                Logger.Info("Abonament for user added successfully!");
                return abonamentUser;
            }


        }

        public bool NameAlreadyExist(string name)
        {
            Abonament abonament = null;
            using (AuctionContext context = new AuctionContext())
            {
                abonament = context.Abonaments.Where((abonament) => abonament.Name == name).FirstOrDefault();
            }

            if (abonament != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
