// <copyright file="IUserScoreAndLimitsDataServices.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper.Interfaces
{
    /// <summary>
    ///     The user score and limits data services.
    /// </summary>
    public interface IUserScoreAndLimitsDataServices
    {
        /// <summary>
        ///     Gets the user score by the user's id.</summary>
        /// <param name="id">The user's id.</param>
        /// <returns>
        ///     the score of the user with the provided id.
        /// </returns>
        double GetUserScoreByUserId(int id);

        /// <summary>
        ///     Gets the user's auctions and bids limit by the user's id.</summary>
        /// <param name="id">The user's id.</param>
        /// <returns>
        ///     the limit on creating auctions and bidding of the user with the provided id.
        /// </returns>
        double GetUserLimitByUserId(int id);

        /// <summary>
        ///     Gets the value of a condition by the condition's name.</summary>
        /// <param name="name">The condition's name.</param>
        /// <returns>
        ///     the value of the condition with the provided name.
        /// </returns>
        int GetConditionalValueByName(string name);
    }
}
