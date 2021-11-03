namespace FilmsCatalog.Data
{
    using FilmsCatalog.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст базы данных приложения.
    /// </summary>
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Таблица <see cref="Movie"/>
        /// </summary>
        public DbSet<Movie> Movies { get; set; }
    }
}