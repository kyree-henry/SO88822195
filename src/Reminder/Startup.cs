using FormHelper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using SO88822195.Module.SchedulEmail.Data.Core.Hubs;
using SO88822195.Module.SchedulEmail.Data.Presistance;
using SO88822195.Module.SchedulEmail.Data.Services;

namespace SO88822195.Module.SchedulEmail
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddRazorPages().AddFormHelper();
            services.AddDbContext<DataContext>(options =>
                        options.UseSqlServer(_configuration.GetConnectionString("SQlConnectionString")));

            services.AddHangfire(configuration =>
           configuration.UseSqlServerStorage(_configuration.GetConnectionString("SQlConnectionString")));
            services.AddHangfireServer();

            services.AddScoped<ReminderService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseFormHelper();
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ReminderHub>("/reminderHub");
            });
        }
    }
}
