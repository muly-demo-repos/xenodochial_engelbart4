using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;

namespace MulyDotnet.APIs;

public interface ILibrariesService
{
    /// <summary>
    /// Create one Library
    /// </summary>
    public Task<LibraryDto> CreateLibrary(LibraryCreateInput libraryDto);

    /// <summary>
    /// Delete one Library
    /// </summary>
    public Task DeleteLibrary(LibraryIdDto idDto);

    /// <summary>
    /// Find many Libraries
    /// </summary>
    public Task<List<LibraryDto>> Libraries(LibraryFindMany findManyArgs);

    /// <summary>
    /// Get one Library
    /// </summary>
    public Task<LibraryDto> Library(LibraryIdDto idDto);

    /// <summary>
    /// Meta data about Library records
    /// </summary>
    public Task<MetadataDto> LibrariesMeta(LibraryFindMany findManyArgs);

    /// <summary>
    /// Update one Library
    /// </summary>
    public Task UpdateLibrary(LibraryIdDto idDto, LibraryUpdateInput updateDto);
}
