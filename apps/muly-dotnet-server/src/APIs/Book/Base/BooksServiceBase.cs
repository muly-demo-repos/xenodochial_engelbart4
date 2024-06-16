using Microsoft.EntityFrameworkCore;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;
using MulyDotnet.APIs.Extensions;
using MulyDotnet.Infrastructure;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs;

public abstract class BooksServiceBase : IBooksService
{
    protected readonly MulyDotnetDbContext _context;

    public BooksServiceBase(MulyDotnetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a author record for Book
    /// </summary>
    public async Task<AuthorDto> GetAuthor(BookIdDto idDto)
    {
        var book = await _context
            .Books.Where(book => book.Id == idDto.Id)
            .Include(book => book.Author)
            .FirstOrDefaultAsync();
        if (book == null)
        {
            throw new NotFoundException();
        }
        return book.Author.ToDto();
    }

    /// <summary>
    /// Meta data about Book records
    /// </summary>
    public async Task<MetadataDto> BooksMeta(BookFindMany findManyArgs)
    {
        var count = await _context.Books.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Create one Book
    /// </summary>
    public async Task<BookDto> CreateBook(BookCreateInput createDto)
    {
        var book = new Book
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Title = createDto.Title,
            Description = createDto.Description,
            Isbn = createDto.Isbn,
            PublishedDate = createDto.PublishedDate
        };

        if (createDto.Id != null)
        {
            book.Id = createDto.Id;
        }
        if (createDto.Author != null)
        {
            book.Author = await _context
                .Authors.Where(author => createDto.Author.Id == author.Id)
                .FirstOrDefaultAsync();
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Book>(book.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Book
    /// </summary>
    public async Task DeleteBook(BookIdDto idDto)
    {
        var book = await _context.Books.FindAsync(idDto.Id);
        if (book == null)
        {
            throw new NotFoundException();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Books
    /// </summary>
    public async Task<List<BookDto>> Books(BookFindMany findManyArgs)
    {
        var books = await _context
            .Books.Include(x => x.Author)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return books.ConvertAll(book => book.ToDto());
    }

    /// <summary>
    /// Get one Book
    /// </summary>
    public async Task<BookDto> Book(BookIdDto idDto)
    {
        var books = await this.Books(
            new BookFindMany { Where = new BookWhereInput { Id = idDto.Id } }
        );
        var book = books.FirstOrDefault();
        if (book == null)
        {
            throw new NotFoundException();
        }

        return book;
    }

    /// <summary>
    /// Update one Book
    /// </summary>
    public async Task UpdateBook(BookIdDto idDto, BookUpdateInput updateDto)
    {
        var book = updateDto.ToModel(idDto);

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Books.Any(e => e.Id == book.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
