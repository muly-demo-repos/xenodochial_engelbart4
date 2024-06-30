using Aaa.APIs.Common;
using Aaa.APIs.Dtos;

namespace Aaa.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<User> CreateUser(UserCreateInput user);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<User>> Users(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<User> User(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto);
    public Task<User> UserLogin(UserCreateInput userCreateInputDto);
    public Task<User> UserRegistration(UserCreateInput userCreateInputDto);

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs);
}
