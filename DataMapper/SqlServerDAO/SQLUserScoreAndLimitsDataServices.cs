// <copyright file="SQLUserScoreAndLimitsDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using DataMapper.Interfaces;
    using DomainModel.Models;

    /// <summary>
    ///     The user score and limits data services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SQLUserScoreAndLimitsDataServices : IUserScoreAndLimitsDataServices
    {
        /// <inheritdoc/>
        public double GetUserScoreByUserId(int id)
        {
            int n = 5, s = 10;
            List<int> grades = new List<int>();

            using (AuctionContext context = new AuctionContext())
            {
                n = this.GetConditionalValueByName("N");
                s = this.GetConditionalValueByName("S");
                grades = context.Ratings.Where((rating) => rating.RatedUser.Id == id).OrderByDescending((rating) => rating.DateAndTime).Select((rating) => rating.Grade).Take(n).ToList();
            }

            if (n == -1)
            {
                n = 5;
            }

            if (s == -1)
            {
                s = 10;
            }

            if (grades.Count > 0)
            {
                return grades.Average();
            }
            else
            {
                return s;
            }
        }

        /// <inheritdoc/>
        public double GetUserLimitByUserId(int id)
        {
            int s;
            double userScore, a, b, c, d, fx;

            userScore = this.GetUserScoreByUserId(id);
            s = this.GetConditionalValueByName("S");
            a = (double)Math.Floor((decimal)s / 2);
            b = s;
            c = 1;
            d = this.GetConditionalValueByName("T");
            decimal threshold = s / 2;

            if ((decimal)userScore < Math.Floor(threshold))
            {
                return 0;
            }
            else
            {
                fx = c + ((d - c) / (b - a) * (userScore - a));
                return (int)Math.Round(fx);
            }
        }

        /// <inheritdoc/>
        public int GetConditionalValueByName(string name)
        {
            Condition condition = null;

            using (AuctionContext context = new AuctionContext())
            {
                condition = context.Conditions.Where((condition) => condition.Name == name).FirstOrDefault();
            }

            if (condition != null)
            {
                return condition.Value;
            }
            else
            {
                return -1;
            }
        }
    }
}
