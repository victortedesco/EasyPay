using Microsoft.EntityFrameworkCore;
using Polly;
using Transactions.API.Application.Extensions;
using Transactions.Infrastructure.Data;
using Transactions.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddApplication(configuration);
builder.Services.AddInfrastructure(connectionString!);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

_ = Task.Run(() => ExecuteMigrationsPeriodically(app));

app.Run();

static async Task ExecuteMigrationsPeriodically(WebApplication app)
{
    await Task.Delay(TimeSpan.FromSeconds(5));

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryForeverAsync(retryAttempt =>
        {
            return TimeSpan.FromSeconds(5);
        });

    await retryPolicy.ExecuteAsync(async () =>
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    });
}