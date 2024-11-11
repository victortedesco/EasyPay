using Microsoft.EntityFrameworkCore;
using Polly;
using Users.API.Application.Extensions;
using Users.API.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressMapClientErrors = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

_ = Task.Run(() => ExecuteMigrationsPeriodically(app));

app.Run();

static async Task ExecuteMigrationsPeriodically(WebApplication app)
{
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
