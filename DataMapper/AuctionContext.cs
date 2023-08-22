// <copyright file="AuctionContext.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DataMapper
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>The auction context.</summary>
    [ExcludeFromCodeCoverage]
    public class AuctionContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AuctionContext" /> class.
        /// </summary>
        public AuctionContext()
            : base("AuctionDbConnectionString")
        {
        }

        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>Gets or sets the bids.</summary>
        /// <value>The bids.</value>
        public virtual DbSet<Bid> Bids { get; set; }

        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        public virtual DbSet<User> Users { get; set; }
        public DbSet<Utilizator> Utilizatori { get; set; }

        public DbSet<Abonament> Abonaments { get; set; }
        public DbSet<Bonusuri> Bonusuris { get; set; }
        public DbSet<Buisniess> Buisniess { get; set; }
        public DbSet<CentralaTelefonica> CentralaTelefonica { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<AbonamentUser> AbonamentUser { get; set; }
        /// <summary>Gets or sets the ratings.</summary>
        /// <value>The ratings.</value>
        public virtual DbSet<Rating> Ratings { get; set; }

        /// <summary>Gets or sets the conditions.</summary>
        /// <value>The conditions.</value>
        public virtual DbSet<Condition> Conditions { get; set; }

        /// <summary>
        ///         This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        ///         Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances of
        ///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///     property on the given ModelBuilder, but note that this can seriously degrade performance.
        ///     More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///     classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>()
                .HasRequired(s => s.Buyer)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .HasRequired(s => s.RatingUser)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .HasRequired(s => s.RatedUser)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
