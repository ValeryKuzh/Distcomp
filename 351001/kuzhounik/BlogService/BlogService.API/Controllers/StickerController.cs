using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using BlogService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace BlogService.API.Controllers;

[ApiController]
[Route("api/v1.0/stickers")]
public class StickerController : BaseController<long, StickerRequestToDto<long>, StickerResponseToDto<long>>
{
    public StickerController(IStickerService<long> stickerService) : base(stickerService) { }
}