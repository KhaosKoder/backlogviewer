
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TheVikingsApp.Backend;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(
#if DEBUG 
    options =>
    options.UseInMemoryDatabase("TheVikingsApp")
#else
    // Db ConnectionString goes here
#endif 
    );
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Viking API", Version = "v1" });
});

var app = builder.Build();

#if DEBUG
var serviceProvider = app.Services;
using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataInitializer.InitializeDatabase(dbContext);
}
#endif

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Viking API V1"));
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/transactions",
[ProducesResponseType(StatusCodes.Status200OK)]
(AppDbContext db, DateTime? from, DateTime? to, string profile) =>
    {
        var query = db.Transactions.AsQueryable();

        if (!string.IsNullOrEmpty(profile))
        {
            query = query.Where(t => t.Surname == profile);
        }

        if (from.HasValue)
        {
            query = query.Where(t => t.EventTime >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(t => t.EventTime <= to.Value);
        }

        return query.ToList();
    })
.WithName("GetTransactions");

app.MapGet("/api/authentications",
[ProducesResponseType(StatusCodes.Status200OK)]
(AppDbContext db, DateTime from, DateTime to) =>
    {
        var query = db.Authentications.Where(a => a.EventTime >= from && a.EventTime <= to);
        return query.ToList();
    })
.WithName("GetAuthentications");
#if DEBUG
app.MapGet("/api/transactions/all", (AppDbContext db) =>
{
    return db.Transactions.ToList();
})
.WithName("GetAllTransactions");

app.MapGet("/api/authentications/all", (AppDbContext db) =>
{
    return db.Authentications.ToList();
})
.WithName("GetAllAuthentications");
#endif


app.Run();
