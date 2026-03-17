using DiscussionService.Application.DTOs.Requests;
using DiscussionService.Application.DTOs.Responses;
using DiscussionService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace DiscussionService.API.Controllers;

[ApiController]
[Route("api/v1.0/comments")]
public class CommentController : BaseController<long, CommentRequestToDto<long>, CommentResponseToDto<long>>
{
    public CommentController(ICommentService<long> commentService) : base(commentService) { }
    
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}