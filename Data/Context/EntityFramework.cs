using Domain.Cards.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class EntityFramework : DbContext
    {
        public EntityFramework(DbContextOptions<EntityFramework> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Card> Cards { get; set; }
    }
}