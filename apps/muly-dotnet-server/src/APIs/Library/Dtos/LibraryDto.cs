namespace MulyDotnet.APIs.Dtos;

public class LibraryDto : LibraryIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }
}
