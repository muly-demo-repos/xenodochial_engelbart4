using Microsoft.AspNetCore.Mvc;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;

namespace MulyDotnet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MembersControllerBase : ControllerBase
{
    protected readonly IMembersService _service;

    public MembersControllerBase(IMembersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Member
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<MemberDto>> CreateMember(MemberCreateInput input)
    {
        var member = await _service.CreateMember(input);

        return CreatedAtAction(nameof(Member), new { id = member.Id }, member);
    }

    /// <summary>
    /// Delete one Member
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteMember([FromRoute()] MemberIdDto idDto)
    {
        try
        {
            await _service.DeleteMember(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Members
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<MemberDto>>> Members([FromQuery()] MemberFindMany filter)
    {
        return Ok(await _service.Members(filter));
    }

    /// <summary>
    /// Get one Member
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<MemberDto>> Member([FromRoute()] MemberIdDto idDto)
    {
        try
        {
            return await _service.Member(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Member records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MembersMeta([FromQuery()] MemberFindMany filter)
    {
        return Ok(await _service.MembersMeta(filter));
    }

    /// <summary>
    /// Update one Member
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateMember(
        [FromRoute()] MemberIdDto idDto,
        [FromQuery()] MemberUpdateInput memberUpdateDto
    )
    {
        try
        {
            await _service.UpdateMember(idDto, memberUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
