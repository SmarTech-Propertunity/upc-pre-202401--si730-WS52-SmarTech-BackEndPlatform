namespace _1_API.Request;

public class SearchRequest
{
    public string SearchInput { set; get; }
    public int Type { set; get; }
    public float PriceMin { set; get; }
    public float PriceMax { set; get; }
}