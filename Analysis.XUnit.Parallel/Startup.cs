using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Analysis.XUnit.Parallel.API;

namespace Analysis.XUnit.Parallel
{
    public class Startup
    {

        public static SqliteConnection? SqliteConnectionOverride { get; internal set; }
        public IConfiguration Configuration { get; }



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

           services.AddDbContext<CustomerDbContext>(options => CreateDbContextOptionsBuilder(options, Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static DbContextOptionsBuilder CreateDbContextOptionsBuilder(DbContextOptionsBuilder options, string connectionString) =>
            SqliteConnectionOverride == null ? options.UseSqlite(connectionString) : options.UseSqlite(SqliteConnectionOverride);
    }
}
