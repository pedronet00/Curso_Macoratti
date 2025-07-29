namespace APICatalogo.Pagination;

public class ProdutosParameters
{

    const int MAX_PAGE_SIZE = 10;

    public int PageNumber { get; set; } = 1;

    private int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    }
}
