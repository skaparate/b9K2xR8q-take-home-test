using Fundo.Core.Interfaces;
using Fundo.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fundo.Infrastructure.Repositories;
using Fundo.Services;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Applications.WebApi
{
    public class Startup
    {
        private const string CorsPolicyName = "AllowLocalhost";

        public Startup(IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyName,
                    policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
            var builder = WebApplication.CreateBuilder();
            services.AddControllers();
            services.AddScoped<IAccountHolderRepository, AccountHolderRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<AccountHolderService>();

            services.AddDbContext<DatabaseContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(CorsPolicyName);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}