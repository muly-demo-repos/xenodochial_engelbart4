using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;

namespace MulyDotnet.APIs;

public interface IAuthorsService
{
    /// <summary>
    /// Connect multiple Books records to Author
    /// </summary>
    public Task ConnectBooks(AuthorIdDto idDto, BookIdDto[] booksId);

    /// <summary>
    /// Disconnect multiple Books records from Author
    /// </summary>
    public Task DisconnectBooks(AuthorIdDto idDto, BookIdDto[] booksId);

    /// <summary>
    /// Find multiple Books records for Author
    /// </summary>
    public Task<List<BookDto>> FindBooks(AuthorIdDto idDto, BookFindMany BookFindMany);

    /// <summary>
    /// Meta data about Author records
    /// </summary>
    public Task<MetadataDto> AuthorsMeta(AuthorFindMany findManyArgs);

    /// <summary>
    /// Update multiple Books records for Author
    /// </summary>
    public Task UpdateBooks(AuthorIdDto idDto, BookIdDto[] booksId);

    /// <summary>
    /// Create one Author
    /// </summary>
    public Task<AuthorDto> CreateAuthor(AuthorCreateInput authorDto);

    /// <summary>
    /// Delete one Author
    /// </summary>
    public Task DeleteAuthor(AuthorIdDto idDto);

    /// <summary>
    /// Find many Authors
    /// </summary>
    public Task<List<AuthorDto>> Authors(AuthorFindMany findManyArgs);

    /// <summary>
    /// Get one Author
    /// </summary>
    public Task<AuthorDto> Author(AuthorIdDto idDto);

    /// <summary>
    /// Update one Author
    /// </summary>
    public Task UpdateAuthor(AuthorIdDto idDto, AuthorUpdateInput updateDto);
}
