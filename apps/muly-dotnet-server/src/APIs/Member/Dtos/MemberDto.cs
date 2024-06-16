namespace MulyDotnet.APIs.Dtos;

public class MemberDto : MemberIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? MembershipDate { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
