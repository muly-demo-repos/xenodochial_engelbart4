using Microsoft.EntityFrameworkCore;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;
using MulyDotnet.APIs.Extensions;
using MulyDotnet.Infrastructure;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs;

public abstract class AuthorsServiceBase : IAuthorsService
{
    protected readonly MulyDotnetDbContext _context;

    public AuthorsServiceBase(MulyDotnetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Connect multiple Books records to Author
    /// </summary>
    public async Task ConnectBooks(AuthorIdDto idDto, BookIdDto[] booksId)
    {
        var author = await _context
            .Authors.Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var books = await _context
            .Books.Where(t => booksId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (books.Count == 0)
        {
            throw new NotFoundException();
        }

        var booksToConnect = books.Except(author.Books);

        foreach (var book in booksToConnect)
        {
            author.Books.Add(book);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Books records from Author
    /// </summary>
    public async Task DisconnectBooks(AuthorIdDto idDto, BookIdDto[] booksId)
    {
        var author = await _context
            .Authors.Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var books = await _context
            .Books.Where(t => booksId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var book in books)
        {
            author.Books?.Remove(book);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Books records for Author
    /// </summary>
    public async Task<List<BookDto>> FindBooks(AuthorIdDto idDto, BookFindMany authorFindMany)
    {
        var books = await _context
            .Books.Where(m => m.AuthorId == idDto.Id)
            .ApplyWhere(authorFindMany.Where)
            .ApplySkip(authorFindMany.Skip)
            .ApplyTake(authorFindMany.Take)
            .ApplyOrderBy(authorFindMany.SortBy)
            .ToListAsync();

        return books.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Author records
    /// </summary>
    public async Task<MetadataDto> AuthorsMeta(AuthorFindMany findManyArgs)
    {
        var count = await _context.Authors.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Books records for Author
    /// </summary>
    public async Task UpdateBooks(AuthorIdDto idDto, BookIdDto[] booksId)
    {
        var author = await _context
            .Authors.Include(t => t.Books)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var books = await _context
            .Books.Where(a => booksId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (books.Count == 0)
        {
            throw new NotFoundException();
        }

        author.Books = books;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create one Author
    /// </summary>
    public async Task<AuthorDto> CreateAuthor(AuthorCreateInput createDto)
    {
        var author = new Author
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Dob = createDto.Dob,
            Biography = createDto.Biography
        };

        if (createDto.Id != null)
        {
            author.Id = createDto.Id;
        }
        if (createDto.Books != null)
        {
            author.Books = await _context
                .Books.Where(book => createDto.Books.Select(t => t.Id).Contains(book.Id))
                .ToListAsync();
        }

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Author>(author.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Author
    /// </summary>
    public async Task DeleteAuthor(AuthorIdDto idDto)
    {
        var author = await _context.Authors.FindAsync(idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Authors
    /// </summary>
    public async Task<List<AuthorDto>> Authors(AuthorFindMany findManyArgs)
    {
        var authors = await _context
            .Authors.Include(x => x.Books)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return authors.ConvertAll(author => author.ToDto());
    }

    /// <summary>
    /// Get one Author
    /// </summary>
    public async Task<AuthorDto> Author(AuthorIdDto idDto)
    {
        var authors = await this.Authors(
            new AuthorFindMany { Where = new AuthorWhereInput { Id = idDto.Id } }
        );
        var author = authors.FirstOrDefault();
        if (author == null)
        {
            throw new NotFoundException();
        }

        return author;
    }

    /// <summary>
    /// Update one Author
    /// </summary>
    public async Task UpdateAuthor(AuthorIdDto idDto, AuthorUpdateInput updateDto)
    {
        var author = updateDto.ToModel(idDto);

        if (updateDto.Books != null)
        {
            author.Books = await _context
                .Books.Where(book => updateDto.Books.Select(t => t.Id).Contains(book.Id))
                .ToListAsync();
        }

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Authors.Any(e => e.Id == author.Id))
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
