using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Net.Http.Headers;
using Personal.Website.Api.Clients;
using Personal.Website.Api.Models;

namespace Personal.Website.Api.Extensions;

public static class ServiceCollectionServiceExtensions
{
    public static IServiceCollection AddGithubGraphQLClient(this IServiceCollection services, GithubApiSettings apiSettings)
    {
        var apiUrl = apiSettings.Url ?? string.Empty;
        var apiToken = apiSettings.ApiToken;
        var clientName = "github";

        services.AddHttpClient(clientName, client =>
        {
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Person.Website.Api", "0.0.1"));
        });

        services.AddScoped<IGithubClient, GithubClient>(sp =>
        {
            var graphQLHttpClient = new GraphQLHttpClient(
                    new GraphQLHttpClientOptions
                    {
                        EndPoint = new Uri(apiUrl)
                    },
                    new SystemTextJsonSerializer(),
                    sp.GetRequiredService<IHttpClientFactory>().CreateClient(clientName)
                );

            return new GithubClient(graphQLHttpClient);
        });

        return services;
    }
}
