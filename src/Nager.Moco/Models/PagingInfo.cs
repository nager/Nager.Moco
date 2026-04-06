namespace Nager.Moco.Models
{
    /// <summary>
    /// Represents paginated query results, including the total number of available items and the items on the current page.
    /// </summary>
    /// <typeparam name="T">The type of the items in the result set.</typeparam>
    public class PagingInfo<T>
    {
        /// <summary>
        /// Gets the current page number (1-based index).
        /// </summary>
        public int CurrentPage { get; init; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int PageSize { get; init; } = 100;

        /// <summary>
        /// Gets or sets the total number of items available across all pages.
        /// </summary>
        public int Total { get; init; }

        /// <summary>
        /// Gets the total number of pages based on the total item count and page size.
        /// </summary>
        public int TotalPages => PageSize == 0
            ? 0
            : (int)Math.Ceiling((double)Total / PageSize);

        /// <summary>
        /// Gets or sets the items returned for the current page.
        /// </summary>
        public T[] Items { get; init; } = [];
    }
}
