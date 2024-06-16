using Microsoft.AspNetCore.Mvc;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;

namespace MulyDotnet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class LibrariesControllerBase : ControllerBase
{
    protected readonly ILibrariesService _service;

    public LibrariesControllerBase(ILibrariesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Library
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<LibraryDto>> CreateLibrary(LibraryCreateInput input)
    {
        var library = await _service.CreateLibrary(input);

        return CreatedAtAction(nameof(Library), new { id = library.Id }, library);
    }

    /// <summary>
    /// Delete one Library
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteLibrary([FromRoute()] LibraryIdDto idDto)
    {
        try
        {
            await _service.DeleteLibrary(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Libraries
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<LibraryDto>>> Libraries(
        [FromQuery()] LibraryFindMany filter
    )
    {
        return Ok(await _service.Libraries(filter));
    }

    /// <summary>
    /// Get one Library
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<LibraryDto>> Library([FromRoute()] LibraryIdDto idDto)
    {
        try
        {
            return await _service.Library(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Library records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> LibrariesMeta([FromQuery()] LibraryFindMany filter)
    {
        return Ok(await _service.LibrariesMeta(filter));
    }

    /// <summary>
    /// Update one Library
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateLibrary(
        [FromRoute()] LibraryIdDto idDto,
        [FromQuery()] LibraryUpdateInput libraryUpdateDto
    )
    {
        try
        {
            await _service.UpdateLibrary(idDto, libraryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
