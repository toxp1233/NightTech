namespace NightTech.Application.common;

public class PaginationParams
{
    private const int MaxPageSize = 50; // or any upper limit you want
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? Search { get; set; }
}
