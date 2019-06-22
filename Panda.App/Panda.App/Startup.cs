using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panda.Data;
using Panda.Domein;

namespace Panda.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PandaDbContext>(options =>
               options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddIdentity<PandaUser, PandaUserRole>()
                .AddEntityFrameworkStores<PandaDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(identityOptions =>
            {
                identityOptions.Password.RequireDigit = false;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequiredLength = 3;
                identityOptions.Password.RequiredUniqueChars = 0;

                identityOptions.User.RequireUniqueEmail = true;
            });




            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<PandaDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new PandaUserRole()
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                        context.Roles.Add(new PandaUserRole()
                        {
                            Name = "User",
                            NormalizedName = "USER"
                        });
                    }
                  

                    if (!context.PackageStatuses.Any())
                    {
                        context.PackageStatuses.Add(new PackageStatus{Name = "Pending"});
                        context.PackageStatuses.Add(new PackageStatus{Name = "Shipped"});
                        context.PackageStatuses.Add(new PackageStatus{Name = "Delivered"});
                        context.PackageStatuses.Add(new PackageStatus{Name = "Aquired"});
                    }
                    context.SaveChanges();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseExceptionHandler("/Error");

            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            //app.UseMvc();
            app.UseMvcWithDefaultRoute();
        }
    }
}
