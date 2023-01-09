using Personal.Website.Api.Extensions;
using Personal.Website.Api.GraphQL.Queries;
using Personal.Website.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddGithubGraphQLClient(builder.Configuration.GetSection(nameof(GithubApiSettings)).Get<GithubApiSettings>());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
app.UseRouting();
app.MapGraphQL();

app.Run();
