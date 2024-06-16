using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;

namespace MulyDotnet.APIs;

public interface IMembersService
{
    /// <summary>
    /// Create one Member
    /// </summary>
    public Task<MemberDto> CreateMember(MemberCreateInput memberDto);

    /// <summary>
    /// Delete one Member
    /// </summary>
    public Task DeleteMember(MemberIdDto idDto);

    /// <summary>
    /// Find many Members
    /// </summary>
    public Task<List<MemberDto>> Members(MemberFindMany findManyArgs);

    /// <summary>
    /// Get one Member
    /// </summary>
    public Task<MemberDto> Member(MemberIdDto idDto);

    /// <summary>
    /// Meta data about Member records
    /// </summary>
    public Task<MetadataDto> MembersMeta(MemberFindMany findManyArgs);

    /// <summary>
    /// Update one Member
    /// </summary>
    public Task UpdateMember(MemberIdDto idDto, MemberUpdateInput updateDto);
}
