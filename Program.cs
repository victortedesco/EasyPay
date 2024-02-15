using EasyPay.Data;
using EasyPay.Repositories;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        SetupBuilder(builder);

        var app = builder.Build();
        SetupApp(app);
        app.Run();
    }

    private static void SetupBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DataContext>(o =>
            o.UseSqlite("Data Source=EasyPay.db")
        );

        builder.Services.AddTransient<UserRepository>();
        builder.Services.AddTransient<TransactionRepository>();
    }

    private static void SetupApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapControllers();
        app.UseHttpsRedirection();
    }
}