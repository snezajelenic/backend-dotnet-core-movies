using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesBackend.Repositories;
using MoviesBackend.WebApi.Repositories;

namespace MoviesBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable CORS requests
            services.AddCors();
            services.AddMvc();

            // database context registration.
            // saying to use sqlite in conjunction with entity framework core
            // targeting embedded db file database.db
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite($"Data Source=database.db"));

            // Registering the other dependencies for our classes, using dependency injection
            services.AddTransient<IMovieRepository, MovieRepository>();     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // configure CORS
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc();

        }
    }
}
