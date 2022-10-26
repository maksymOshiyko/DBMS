namespace DBMSApi.Models.Hateoas;

public class Link
{
    public string? Href { get; init; }
    public string Rel { get; init; }
    public string Method { get; init; }
}