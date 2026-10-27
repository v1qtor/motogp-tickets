namespace MotoGP.Domain.Common;

/// <summary>
/// Paging metadata returned to the consumer via the custom "X-Pagination" response header
/// (see <see cref="MotoGP.Api.Controllers.TicketsController"/>). Lives in the Domain layer
/// (our lowest-level shared layer, with no further dependencies) so both Infrastructure
/// (which builds it) and Api (which serializes it) can reference it without a circular
/// project reference - the role the course's separate "Shared" class library plays.
/// </summary>
public class PaginationMetadata
{
    public int TotalItemCount { get; set; }
    public int TotalPageCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }

    public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
    {
        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPageCount = pageSize > 0 ? (int)Math.Ceiling(totalItemCount / (double)pageSize) : 0;
    }
}
