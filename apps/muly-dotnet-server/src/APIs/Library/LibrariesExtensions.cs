using MulyDotnet.APIs.Dtos;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs.Extensions;

public static class LibrariesExtensions
{
    public static LibraryDto ToDto(this Library model)
    {
        return new LibraryDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Location = model.Location,
        };
    }

    public static Library ToModel(this LibraryUpdateInput updateDto, LibraryIdDto idDto)
    {
        var library = new Library
        {
            Id = idDto.Id,
            Name = updateDto.Name,
            Location = updateDto.Location
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            library.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            library.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return library;
    }
}
