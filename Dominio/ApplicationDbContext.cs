using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repositorio
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animais> Animais { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
