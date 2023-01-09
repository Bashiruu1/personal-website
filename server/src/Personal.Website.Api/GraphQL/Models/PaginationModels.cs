namespace Personal.Website.Api.GraphQL.Models;

public class Page<T>
{
    public List<Edge<T>> Edges { get; set; }
    public PageInfo PageInfo { get; set; }
    public int TotalCount { get; set; }
}

public class PageInfo
{
    public bool HasNextPage { get; set; }
    public bool hasPreviousPage { get; set; }
    public string? EndCursor { get; set; }
}

public class Edge<T>
{
    public string? Cursor { get; set; }
    public T? Node { get; set; }
}
