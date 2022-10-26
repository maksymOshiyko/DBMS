namespace DBMSApi.Models.Hateoas;

public class HateoasResponse
{
    public IEnumerable<Link> Links { get; set; }
}

public class HateoasResponse<T> : HateoasResponse
{
    public T Data { get; set; }
}