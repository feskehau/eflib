using HSS.Storage.Core;
using HSS.Storage.Core.Repository.Base;
using HSS.Storage.SqlServer.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(HSS.Job.AISData.Startup))]

namespace HSS.Job.AISData
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<HssDbContext>(optionsAction: options =>
                {
                    SqlServerDbContextOptionsExtensions.UseSqlServer(options, "Data Source = ****; Database = ****; User Id = ****; Password = *****!; MultipleActiveResultSets = True");
                    options.UseModel(new HSSContext().Model);
                },
                contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Transient
            );
            
            builder.Services.AddTransient<IDbContext, HssDbContext>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}