using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    [ExcludeFromCodeCoverage]

    public class SQLBuisniessDataService : IBuisniessDataService
    {
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        public bool AddBuisniess(Buisniess buisniess)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Buisniess.Add(buisniess);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new buisniess: " + exception);
                    return false;
                }
            }
            Logger.Info("Buisniess added successfully!");
            return true;
        }

        public Buisniess GetBuisniess()
        {
            Buisniess buisniess = null;
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    buisniess = context.Buisniess.Where((buisniess) => buisniess != null).FirstOrDefault();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while geting buisniess: " + exception);
                }
            }
            return buisniess;
        }

        public Buisniess GetBuisniessById(int id)
        {
            Buisniess buisniess = null;
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    buisniess = context.Buisniess.Where((buisniess) => buisniess.Id == id).FirstOrDefault();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while geting buisniess: " + exception);
                }
            }
            return buisniess;
        }
    }
}
