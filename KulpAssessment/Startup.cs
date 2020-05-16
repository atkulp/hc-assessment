using KulpAssessment.Data;
using KulpAssessment.Data.Entities;
using KulpAssessment.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KulpAssessment
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
            services.AddControllersWithViews();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/atk-assessment";
            });

            // Not much to add, but make sure things are available for DI
            services.AddDbContext<AssessmentDbContext>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Check the database (new'ing doesn't actually touch the DB yet though)
                using (var ctx = new AssessmentDbContext())
                {
                    // Make sure it's created.  On initial creation, we'll also seed it
                    if (ctx.Database.EnsureCreated())
                    {
                        foreach( var p in (new MockPersonRepository()).GetAll())
                        {
                            var newPerson = new Person
                            {
                                // Skip ID since it's identity column
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                DateOfBirth = p.DateOfBirth,
                                DateOfDeath = p.DateOfDeath,
                                Street1 = p.Street1,
                                Street2 = p.Street2,
                                City = p.City,
                                State = p.State,
                                PostalCode = p.PostalCode,
                                Interests = p.Interests,
                                AvatarUrl = p.AvatarUrl
                            };
                            ctx.Add(newPerson);
                        }

                        ctx.SaveChanges();
                    }
                }
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
