using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using BlogService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace BlogService.API.Controllers;

[ApiController]
[Route("api/v1.0/users")]
public class UserController : BaseController<long, UserRequestToDto<long>, UserResponseToDto<long>>
{
    public UserController(IUserService<long> userService) : base(userService) { }
}