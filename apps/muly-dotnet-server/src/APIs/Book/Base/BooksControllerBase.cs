using Microsoft.AspNetCore.Mvc;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;

namespace MulyDotnet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BooksControllerBase : ControllerBase
{
    protected readonly IBooksService _service;

    public BooksControllerBase(IBooksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get a author record for Book
    /// </summary>
    [HttpGet("{Id}/authors")]
    public async Task<ActionResult<List<AuthorDto>>> GetAuthor([FromRoute()] BookIdDto idDto)
    {
        var author = await _service.GetAuthor(idDto);
        return Ok(author);
    }

    /// <summary>
    /// Meta data about Book records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BooksMeta([FromQuery()] BookFindMany filter)
    {
        return Ok(await _service.BooksMeta(filter));
    }

    /// <summary>
    /// Create one Book
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<BookDto>> CreateBook(BookCreateInput input)
    {
        var book = await _service.CreateBook(input);

        return CreatedAtAction(nameof(Book), new { id = book.Id }, book);
    }

    /// <summary>
    /// Delete one Book
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteBook([FromRoute()] BookIdDto idDto)
    {
        try
        {
            await _service.DeleteBook(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Books
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<BookDto>>> Books([FromQuery()] BookFindMany filter)
    {
        return Ok(await _service.Books(filter));
    }

    /// <summary>
    /// Get one Book
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<BookDto>> Book([FromRoute()] BookIdDto idDto)
    {
        try
        {
            return await _service.Book(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Book
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateBook(
        [FromRoute()] BookIdDto idDto,
        [FromQuery()] BookUpdateInput bookUpdateDto
    )
    {
        try
        {
            await _service.UpdateBook(idDto, bookUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
