namespace SmartEvent.Mobile.Core.Common;

public class PagedResult<T>(IEnumerable<T> items, int totalSize, int pageNumber, int pageSize)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalSize { get; set; } = totalSize;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages =>  (int)Math.Ceiling((double)TotalSize / PageSize); 
    public bool  HasPreviousPage => PageNumber > 1;
    public bool  HasNextPage => PageNumber < TotalPages;
}