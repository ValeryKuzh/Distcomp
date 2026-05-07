using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using BlogService.Application.Interfaces.Services;
using BlogService.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Application.Interfaces.Mappers;
using Shared.Application.Services;
using Shared.Domain.Interfaces;

namespace BlogService.Application.Services;

public class UserService<Id> : BaseService<Id, User<Id>, UserRequestToDto<Id>, UserResponseToDto<Id>>, IUserService<Id>
{
    public UserService(IRepository<Id, User<Id>> repository,
        IRequestMapper<UserRequestToDto<Id>, User<Id>> userRequestMapper,
        IResponseMapper<User<Id>, UserResponseToDto<Id>> userResponseMapper) : base(repository, userRequestMapper, userResponseMapper){ }
    
    protected override async Task OnBeforeCreateAsync(UserRequestToDto<Id> request)
    {
        if (await _repository.ExistsAsync(u => u.Login == request.Login))
        {
            throw new HttpRequestException("User with the same login already exists");
        }
    }
}