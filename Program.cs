using Blog.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
var connectionString = builder.Configuration.GetConnectionString("Blog") ?? "Data Source=Blog.db";

builder.Services
  .AddSqlite<BlogDbContext>(connectionString);
builder.Services
  .AddGraphQLServer()
  .AddGlobalObjectIdentification()
  .RegisterDbContext<BlogDbContext>()
  .AddQueryType<Query>();

var app = builder.Build();
app.MapGraphQL();
app.Run();
