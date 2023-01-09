using System.Linq;
using System.Net.Http;
using System.Reflection;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using Microsoft.Extensions.DependencyInjection;
using Personal.Website.Api.Clients;
using Personal.Website.Api.Extensions;
using Personal.Website.Api.Models;
using Xunit;
using FluentAssertions;

namespace Personal.Website.Api.Tests;

public class AddGithubGraphQLClientTests
{
    [Fact]
    public void AddGithubGraphQLClient_Adds_Expected_Dependencies()
    {
        // Arrange
        var services = new ServiceCollection();
        var apiSettings = new GithubApiSettings
        {
            Url = "https://api.github.com",
            ApiToken = "abc123"
        };

        // Act
        services.AddGithubGraphQLClient(apiSettings);

        // Assert
        var githubClient = services.BuildServiceProvider().GetService<IGithubClient>();
        var githubHttpClient = GetHttpClientField(githubClient);

        githubHttpClient.BaseAddress.Should().Be("https://api.github.com");
        githubHttpClient.DefaultRequestHeaders.Authorization.Parameter.Should().Be("abc123");
        githubHttpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("Bearer");
        Assert.IsType<GithubClient>(githubClient);
    }

    private static HttpClient GetHttpClientField(IGithubClient githubClient)
    {
        return (githubClient
               .GetType()
               .GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic)
               .FirstOrDefault(x => x.FieldType == typeof(IGraphQLClient))
               .GetValue(githubClient) as GraphQLHttpClient).HttpClient;
    }


}
