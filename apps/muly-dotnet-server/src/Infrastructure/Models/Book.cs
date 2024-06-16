using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulyDotnet.Infrastructure.Models;

[Table("Books")]
public class Book
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(1000)]
    public string? Isbn { get; set; }

    public DateTime? PublishedDate { get; set; }

    public string? AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public Author? Author { get; set; } = null;
}
