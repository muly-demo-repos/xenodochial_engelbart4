using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulyDotnet.Infrastructure.Models;

[Table("Authors")]
public class Author
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    [StringLength(1000)]
    public string? Biography { get; set; }

    public List<Book>? Books { get; set; } = new List<Book>();
}
