using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScholarSyncMVC.Data;
using ScholarSyncMVC.Helper;
using ScholarSyncMVC.map_application;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.Repository;
using ScholarSyncMVC.Repository.Contract;

namespace ScholarSyncMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
           // builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));



			//Context Services
			builder.Services.AddDbContext<ScholarSyncConext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("conn")));

            //Identity Services 
            builder.Services.AddIdentity<AppUser, IdentityRole>(options=>
            {
                options.Password.RequiredUniqueChars = 0 ;
                options.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<ScholarSyncConext>();
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddScoped<IScholarship, ScholarshipRepository>();
            builder.Services.AddScoped<IRequirement, RequirementRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
