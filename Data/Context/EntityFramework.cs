using Domain.Base.Entities;
using Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class EntityFramework : DbContext
    {
        public EntityFramework(DbContextOptions<EntityFramework> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Card> Card { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<PurchaseCategorie> Categorie { get; set; }
        public DbSet<PurchaseInInstallments> PurchaseInInstallments { get; set; }
        public DbSet<Installments> Installments { get; set; }

    }
}