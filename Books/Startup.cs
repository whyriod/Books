using Books.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books
{
    public class Startup
    {
        //
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        //Configuration from Appsettings.json
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Let us use MVC setup
            services.AddControllersWithViews();

            //Add Db contect
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDbConnection"]);
            });

            //For Identity connection
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            //Use Repo stuff
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<IBuyRepository, EFBuyRepository>();

            //Let us use razor pages.
            services.AddRazorPages();

            //Sessions
            services.AddDistributedMemoryCache();
            services.AddSession();

            //Session Basket
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Blazor
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //use wwwroot
            app.UseStaticFiles();

            //More Sessions Stuff
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //Endpoints are executed in order. Short circuit

                //Category and Page
                endpoints.MapControllerRoute(
                    name: "CategoryPage",
                    pattern:"{category}/Page{PageNum}",
                    defaults: new { controller = "Home", action = "Index" });

                //Pagination View
                endpoints.MapControllerRoute(
                    name: "Pagination",
                    pattern: "Page{PageNum}",
                    defaults: new { controller = "Home", action = "Index"});

                //Only Category
                endpoints.MapControllerRoute(
                    name: "Category",
                    pattern: "{category}/Page{PageNum}",
                    defaults: new { controller = "Home", action = "Index", PageNum = 1 });

                //Default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //Could use
                endpoints.MapDefaultControllerRoute(); 
                // For default

                endpoints.MapRazorPages();

                //Blazor stuff
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            });

            //Seed the admin data
            IdentitySeedData.EnsurePopulated(app);

        }
    }
}


/*
Scaffold DB command
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 3.1.22
dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.22
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 3.1.22
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.22
dotnet ef dbcontext scaffold "Data Source = Bookstore.sqlite" Microsoft.EntityFrameworkCore.Sqlite -o Models
*/