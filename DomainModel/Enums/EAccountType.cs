// <copyright file="EAccountType.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DomainModel.Enums
{
    /// <summary>The type of account.</summary>
    public enum EAccountType
    {
        /// <summary>Unknown account type.</summary>
        Unknown,

        /// <summary>New account.</summary>
        New,

        /// <summary>Buyer account.</summary>
        Buyer,

        /// <summary>Seller account.</summary>
        Seller,

        /// <summary>Account for buying and selling.</summary>
        Both,
    }
}
