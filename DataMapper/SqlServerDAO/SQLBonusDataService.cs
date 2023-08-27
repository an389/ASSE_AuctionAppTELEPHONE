using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    [ExcludeFromCodeCoverage]

    public class SQLBonusDataService : IBonusDataService
    {
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        public bool AddBonus(Bonusuri bonusuri)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Bonusuris.Add(bonusuri);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new bonusuri: " + exception);
                    return false;
                }
            }
            Logger.Info("Nonusuri added successfully!");
            return true;
        }

       

        public bool NameAlreadyExist(string name)
        {
            Bonusuri bonusuri = null;
            using (AuctionContext context = new AuctionContext())
            {
                bonusuri = context.Bonusuris.Where((bonusuri) => bonusuri.Name == name).FirstOrDefault();
            }
            if (bonusuri != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Bonusuri GetBonusById(int id)
        {
            Bonusuri bonusuri = null;
            using (AuctionContext context = new AuctionContext())
            {
                bonusuri = context.Bonusuris.Where((bonusuri) => bonusuri.Id == id).FirstOrDefault();
            }

            return bonusuri;
        }
    }
}
