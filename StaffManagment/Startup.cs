using System;
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
using StaffManagment.Models;

namespace StaffManagment
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("StaffDBConnection")));
            //    services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
    .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        


            services.AddMvc(option =>option.EnableEndpointRouting = false).AddXmlDataContractSerializerFormatters();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateRolePolicy",
                    policy => policy.RequireClaim("Create Role"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateUserPolicy",
                    policy => policy.RequireClaim("Create User"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteUserPolicy",
                    policy => policy.RequireClaim("Delete User"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditUserPolicy",
                    policy => policy.RequireClaim("Edit User"));
            });



            services.AddScoped<IStaffRepository, SQLStaffRepository>();
        }          //Transient

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();


            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
