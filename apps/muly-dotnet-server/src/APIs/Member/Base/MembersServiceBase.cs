using Microsoft.EntityFrameworkCore;
using MulyDotnet.APIs;
using MulyDotnet.APIs.Common;
using MulyDotnet.APIs.Dtos;
using MulyDotnet.APIs.Errors;
using MulyDotnet.APIs.Extensions;
using MulyDotnet.Infrastructure;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs;

public abstract class MembersServiceBase : IMembersService
{
    protected readonly MulyDotnetDbContext _context;

    public MembersServiceBase(MulyDotnetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Member
    /// </summary>
    public async Task<MemberDto> CreateMember(MemberCreateInput createDto)
    {
        var member = new Member
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            MembershipDate = createDto.MembershipDate,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName
        };

        if (createDto.Id != null)
        {
            member.Id = createDto.Id;
        }

        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Member>(member.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Member
    /// </summary>
    public async Task DeleteMember(MemberIdDto idDto)
    {
        var member = await _context.Members.FindAsync(idDto.Id);
        if (member == null)
        {
            throw new NotFoundException();
        }

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Members
    /// </summary>
    public async Task<List<MemberDto>> Members(MemberFindMany findManyArgs)
    {
        var members = await _context
            .Members.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return members.ConvertAll(member => member.ToDto());
    }

    /// <summary>
    /// Get one Member
    /// </summary>
    public async Task<MemberDto> Member(MemberIdDto idDto)
    {
        var members = await this.Members(
            new MemberFindMany { Where = new MemberWhereInput { Id = idDto.Id } }
        );
        var member = members.FirstOrDefault();
        if (member == null)
        {
            throw new NotFoundException();
        }

        return member;
    }

    /// <summary>
    /// Meta data about Member records
    /// </summary>
    public async Task<MetadataDto> MembersMeta(MemberFindMany findManyArgs)
    {
        var count = await _context.Members.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one Member
    /// </summary>
    public async Task UpdateMember(MemberIdDto idDto, MemberUpdateInput updateDto)
    {
        var member = updateDto.ToModel(idDto);

        _context.Entry(member).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Members.Any(e => e.Id == member.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
