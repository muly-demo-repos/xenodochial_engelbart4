namespace MulyDotnet.APIs.Dtos;

public class AuthorUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Biography { get; set; }

    public List<BookIdDto>? Books { get; set; }
}
