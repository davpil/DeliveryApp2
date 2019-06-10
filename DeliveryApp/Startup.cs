using DeliveryApp.Attributes;
using DeliveryApp.Data;
using DeliveryApp.Mapping.Implementation;
using DeliveryApp.Mapping.Interfaces;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Implementation;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Services.Implementation;
using DeliveryApp.Services.Interfaces;
using DeliveryApp.Synchro.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;

namespace DeliveryApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.Value.StartsWith("/api"))
                    {
                        context.Response.StatusCode = 401;
                        return Task.FromResult(0);
                    }
                    context.Response.Redirect(context.RedirectUri);
                    return Task.FromResult(0);
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            }).AddApiExplorer();
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            if (CurrentEnvironment.IsEnvironment("Testing"))
            {
                // If Testing environment, set in memory database
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestingDB")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            services.AddSwaggerGen(
                 options =>
                 {
                     options.SwaggerDoc("v1", new Info { Title = "Delivery API", Description = "Swagger Delivery API", Version = "v1" });
                     // options.IncludeXmlComments(GetXmlCommentsPath());
                 });

            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IActivityMappingService, ActivityMappingService>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleMappingService, RoleMappingService>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeMappingService, EmployeeMappingService>();

            services.AddScoped<ActivitySyncService, ActivitySyncService>();
            services.AddScoped<ISynchroService, SynchroService>();

            services.AddScoped<IDynamicMenuService, DynamicMenuService>();
            services.AddScoped<IDynamicMenuRepository, DynamicMenuRepository>();
            services.AddScoped<IDynamicMenuMappingService, DynamicMenuMappingService>();

            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IPositionMappingService, PositionMappingService>();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery API");
                }
            );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}

