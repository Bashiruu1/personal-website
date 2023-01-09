namespace Personal.Website.Api.GraphQL.Models;

public class GithubResponse<T>
{
    public Viewer<T> Viewer { get; set; }
}

public class Viewer<T>
{
    public Page<T> Repositories { get; set; }
}

public class Repository
{
    public string Url { get; set; }
}
