using Microsoft.EntityFrameworkCore;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;
using MulyDotnet.APIs.Extensions;
using MulyDotnet.Infrastructure;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs;

public abstract class LibrariesServiceBase : ILibrariesService
{
    protected readonly MulyDotnetDbContext _context;

    public LibrariesServiceBase(MulyDotnetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Library
    /// </summary>
    public async Task<LibraryDto> CreateLibrary(LibraryCreateInput createDto)
    {
        var library = new Library
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name,
            Location = createDto.Location
        };

        if (createDto.Id != null)
        {
            library.Id = createDto.Id;
        }

        _context.Libraries.Add(library);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Library>(library.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Library
    /// </summary>
    public async Task DeleteLibrary(LibraryIdDto idDto)
    {
        var library = await _context.Libraries.FindAsync(idDto.Id);
        if (library == null)
        {
            throw new NotFoundException();
        }

        _context.Libraries.Remove(library);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Libraries
    /// </summary>
    public async Task<List<LibraryDto>> Libraries(LibraryFindMany findManyArgs)
    {
        var libraries = await _context
            .Libraries.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return libraries.ConvertAll(library => library.ToDto());
    }

    /// <summary>
    /// Get one Library
    /// </summary>
    public async Task<LibraryDto> Library(LibraryIdDto idDto)
    {
        var libraries = await this.Libraries(
            new LibraryFindMany { Where = new LibraryWhereInput { Id = idDto.Id } }
        );
        var library = libraries.FirstOrDefault();
        if (library == null)
        {
            throw new NotFoundException();
        }

        return library;
    }

    /// <summary>
    /// Meta data about Library records
    /// </summary>
    public async Task<MetadataDto> LibrariesMeta(LibraryFindMany findManyArgs)
    {
        var count = await _context.Libraries.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Library
    /// </summary>
    public async Task UpdateLibrary(LibraryIdDto idDto, LibraryUpdateInput updateDto)
    {
        var library = updateDto.ToModel(idDto);

        _context.Entry(library).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Libraries.Any(e => e.Id == library.Id))
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
