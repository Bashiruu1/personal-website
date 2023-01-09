using Personal.Website.Api.Clients;
using Personal.Website.Api.GraphQL.Models;

namespace Personal.Website.Api.GraphQL.Queries;

public class Query
{
    public async Task<GithubResponse<Repository>> GetRepositories(
        [Service] IGithubClient githubClient,
        string? after = null)
    {
        return await githubClient.GetRepos(after);
    }
}
