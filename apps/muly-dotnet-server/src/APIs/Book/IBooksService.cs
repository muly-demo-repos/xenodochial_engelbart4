using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;

namespace MulyDotnet.APIs;

public interface IBooksService
{
    /// <summary>
    /// Get a author record for Book
    /// </summary>
    public Task<AuthorDto> GetAuthor(BookIdDto idDto);

    /// <summary>
    /// Meta data about Book records
    /// </summary>
    public Task<MetadataDto> BooksMeta(BookFindMany findManyArgs);

    /// <summary>
    /// Create one Book
    /// </summary>
    public Task<BookDto> CreateBook(BookCreateInput bookDto);

    /// <summary>
    /// Delete one Book
    /// </summary>
    public Task DeleteBook(BookIdDto idDto);

    /// <summary>
    /// Find many Books
    /// </summary>
    public Task<List<BookDto>> Books(BookFindMany findManyArgs);

    /// <summary>
    /// Get one Book
    /// </summary>
    public Task<BookDto> Book(BookIdDto idDto);

    /// <summary>
    /// Update one Book
    /// </summary>
    public Task UpdateBook(BookIdDto idDto, BookUpdateInput updateDto);
}
