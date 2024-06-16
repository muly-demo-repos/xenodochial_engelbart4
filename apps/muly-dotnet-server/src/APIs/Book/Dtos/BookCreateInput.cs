namespace MulyDotnet.APIs.Dtos;

public class BookCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Isbn { get; set; }

    public DateTime? PublishedDate { get; set; }

    public AuthorIdDto? Author { get; set; }
}
