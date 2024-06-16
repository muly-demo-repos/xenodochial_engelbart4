using MulyDotnet.APIs.Dtos;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs.Extensions;

public static class AuthorsExtensions
{
    public static AuthorDto ToDto(this Author model)
    {
        return new AuthorDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Dob = model.Dob,
            Biography = model.Biography,
            Books = model.Books?.Select(x => new BookIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Author ToModel(this AuthorUpdateInput updateDto, AuthorIdDto idDto)
    {
        var author = new Author
        {
            Id = idDto.Id,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Dob = updateDto.Dob,
            Biography = updateDto.Biography
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            author.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            author.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return author;
    }
}
