using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transactions.Domain;
using Transactions.Infrastructure.Abstractions;
using Transactions.Infrastructure.Data;
using Transactions.Infrastructure.Repositories;
using Transactions.Infrastructure.Services;

namespace Transactions.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable("__TransactionsMigrationHistory", "transactions"));
        });
        var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();

        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
