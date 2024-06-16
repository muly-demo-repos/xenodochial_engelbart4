using Microsoft.AspNetCore.Mvc;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;

namespace MulyDotnet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AuthorsControllerBase : ControllerBase
{
    protected readonly IAuthorsService _service;

    public AuthorsControllerBase(IAuthorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Connect multiple Books records to Author
    /// </summary>
    [HttpPost("{Id}/books")]
    public async Task<ActionResult> ConnectBooks(
        [FromRoute()] AuthorIdDto idDto,
        [FromQuery()] BookIdDto[] booksId
    )
    {
        try
        {
            await _service.ConnectBooks(idDto, booksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Books records from Author
    /// </summary>
    [HttpDelete("{Id}/books")]
    public async Task<ActionResult> DisconnectBooks(
        [FromRoute()] AuthorIdDto idDto,
        [FromBody()] BookIdDto[] booksId
    )
    {
        try
        {
            await _service.DisconnectBooks(idDto, booksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Books records for Author
    /// </summary>
    [HttpGet("{Id}/books")]
    public async Task<ActionResult<List<BookDto>>> FindBooks(
        [FromRoute()] AuthorIdDto idDto,
        [FromQuery()] BookFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindBooks(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Author records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AuthorsMeta([FromQuery()] AuthorFindMany filter)
    {
        return Ok(await _service.AuthorsMeta(filter));
    }

    /// <summary>
    /// Update multiple Books records for Author
    /// </summary>
    [HttpPatch("{Id}/books")]
    public async Task<ActionResult> UpdateBooks(
        [FromRoute()] AuthorIdDto idDto,
        [FromBody()] BookIdDto[] booksId
    )
    {
        try
        {
            await _service.UpdateBooks(idDto, booksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Create one Author
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorCreateInput input)
    {
        var author = await _service.CreateAuthor(input);

        return CreatedAtAction(nameof(Author), new { id = author.Id }, author);
    }

    /// <summary>
    /// Delete one Author
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAuthor([FromRoute()] AuthorIdDto idDto)
    {
        try
        {
            await _service.DeleteAuthor(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Authors
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<AuthorDto>>> Authors([FromQuery()] AuthorFindMany filter)
    {
        return Ok(await _service.Authors(filter));
    }

    /// <summary>
    /// Get one Author
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AuthorDto>> Author([FromRoute()] AuthorIdDto idDto)
    {
        try
        {
            return await _service.Author(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Author
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAuthor(
        [FromRoute()] AuthorIdDto idDto,
        [FromQuery()] AuthorUpdateInput authorUpdateDto
    )
    {
        try
        {
            await _service.UpdateAuthor(idDto, authorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
