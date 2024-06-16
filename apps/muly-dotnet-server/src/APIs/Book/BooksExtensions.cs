using MulyDotnet.APIs.Dtos;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs.Extensions;

public static class BooksExtensions
{
    public static BookDto ToDto(this Book model)
    {
        return new BookDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Title = model.Title,
            Description = model.Description,
            Isbn = model.Isbn,
            PublishedDate = model.PublishedDate,
            Author = new AuthorIdDto { Id = model.AuthorId },
        };
    }

    public static Book ToModel(this BookUpdateInput updateDto, BookIdDto idDto)
    {
        var book = new Book
        {
            Id = idDto.Id,
            Title = updateDto.Title,
            Description = updateDto.Description,
            Isbn = updateDto.Isbn,
            PublishedDate = updateDto.PublishedDate
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            book.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            book.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return book;
    }
}
