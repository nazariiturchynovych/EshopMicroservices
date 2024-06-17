using Discount.Grpc;
using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<DiscountService>();


app.UseMigration();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();