using Microsoft.EntityFrameworkCore;
using MoviesBackend.Models;

namespace MoviesBackend.Repositories
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            // When you change a model (add new properties), create a new model
            // make sure you delete the database first so it will generated with Database.EnsureCreated();
            //Database.EnsureDeleted();

            // If the database doesn't exist will create one with the appropriate schema as defined in the models 
            // If the database exists it won't do anything
            Database.EnsureCreated();
        }


    }
}
