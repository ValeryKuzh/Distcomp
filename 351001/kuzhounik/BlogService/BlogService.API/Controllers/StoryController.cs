using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using BlogService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace BlogService.API.Controllers;

[ApiController]
[Route("api/v1.0/stories")]
public class StoryController : BaseController<long, StoryRequestToDto<long>, StoryResponseToDto<long>>
{
    public StoryController(IStoryService<long> storyService) : base(storyService) { }
}