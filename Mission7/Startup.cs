﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission7.Models;

namespace Mission7
{
    public class Startup
    {

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //adding teh connection strings from appssettings.json to connect to the databases
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookConnection"]);
            });
            services.AddDbContext<AppIdentityDBContext>(options =>
            {
                //IdentityUser, IdentityRole
                options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]); 
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<ICheckoutRepository, EFCheckoutRepository>();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            //setting up an instace of the session that lets users do stuff with the carts
            services.AddScoped<Cart>(x => SessionCart.GetCart(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //enables blazor
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //uses wwwroot
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            //do this before endpoints b/c the app will check these first
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoint thaat cleans up how the pages look in the URL
                endpoints.MapControllerRoute(
                    name: "Filtering",
                    pattern: "Category-{bookCategory}/Page-{pageNum}",
                    defaults: new { Controller = "Home", action = "Index" });

                _ = endpoints.MapControllerRoute(
                  name: "Paging",
                  pattern: "Page-{pageNum}",
                  defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute(
                    name: "Category",
                    pattern: "Category-{bookCategory}",
                    defaults: new { Controller = "Home", action = "Index", pageNum=1});

              

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                //enables blazor
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            });

            //runs as program starts. Checks to make sure there is an admin user
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
