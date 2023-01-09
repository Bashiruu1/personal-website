using GraphQL;
using GraphQL.Client.Abstractions;
using Personal.Website.Api.GraphQL.Models;

namespace Personal.Website.Api.Clients;

public interface IGithubClient
{
    Task<GithubResponse<Repository>> GetRepos(string? after = null);
}

public class GithubClient : IGithubClient
{
    private readonly IGraphQLClient _client;
    public GithubClient(IGraphQLClient client)
    {
        _client = client;
    }

    public async Task<GithubResponse<Repository>> GetRepos(string? after = null)
    {
        var query = @"
        query GetRepositories (
            $after: String
        ){
            viewer {
                repositories(
                    first: 3
                    orderBy: {field: UPDATED_AT, direction: DESC}
                    after: $after
                ) {
                    totalCount
                    pageInfo {
                        endCursor
                        hasNextPage
                        hasPreviousPage
                    }
                    edges {
                        cursor
                        node {
                            url
                        }
                    }
                }
            }
        }
        ";

        var request = new GraphQLRequest
        (
            query,
            new
            {
                after
            }
        );
        var response = await _client.SendQueryAsync<GithubResponse<Repository>>(request);
        if (response.Errors is { Length: > 0 })
        {
            throw new GraphQLException(response.Errors.FirstOrDefault()?.Message ?? "");
        }
        return response.Data;
    }
}
