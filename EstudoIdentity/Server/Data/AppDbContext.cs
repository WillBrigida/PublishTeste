using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EstudoIdentity.Server.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        //public DbSet<ProjCondominioSmart.Shared.Models.Condomino> Condomino { get; set; }
    }
}
