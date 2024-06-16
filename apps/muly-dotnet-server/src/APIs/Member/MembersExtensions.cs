using MulyDotnet.APIs.Dtos;
using MulyDotnet.Infrastructure.Models;

namespace MulyDotnet.APIs.Extensions;

public static class MembersExtensions
{
    public static MemberDto ToDto(this Member model)
    {
        return new MemberDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            MembershipDate = model.MembershipDate,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
    }

    public static Member ToModel(this MemberUpdateInput updateDto, MemberIdDto idDto)
    {
        var member = new Member
        {
            Id = idDto.Id,
            MembershipDate = updateDto.MembershipDate,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            member.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            member.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return member;
    }
}
