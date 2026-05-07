using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using BlogService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace BlogService.API.Controllers;
[ApiController]
[Route("api/v1.0/comments")]
public class CommentController : BaseController<long, CommentRequestToDto<long>, CommentResponseToDto<long>>
{
    private readonly HttpClient _httpClient;
    private readonly IStoryService<long> _storyService;

    public CommentController(ICommentService<long> commentService, IStoryService<long> storyService, IHttpClientFactory httpClientFactory) 
        : base(commentService) 
    {
        _httpClient = httpClientFactory.CreateClient();
        _storyService = storyService;
        _httpClient.BaseAddress = new Uri("http://localhost:24130/api/v1.0/comments/");
    }
    
    [HttpGet("{id}")]
    public override async Task<CommentResponseToDto<long>> Get(long id)
    {
        var response = await _httpClient.GetAsync($"{id}");
        
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent || 
            response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CommentResponseToDto<long>>();
    }

    [HttpGet]
    public override async Task<IEnumerable<CommentResponseToDto<long>>> GetAll()
    {
        var response = await _httpClient.GetAsync("");
        
        if (!response.IsSuccessStatusCode)
            return Array.Empty<CommentResponseToDto<long>>();

        return await response.Content.ReadFromJsonAsync<IEnumerable<CommentResponseToDto<long>>>() 
               ?? Array.Empty<CommentResponseToDto<long>>();
    }
    
    // [HttpPost]
    // public override async Task<ActionResult<CommentResponseToDto<long>>> Create([FromBody] CommentRequestToDto<long> request)
    // {
    //     var response = await _httpClient.PostAsJsonAsync("", request);
    //     response.EnsureSuccessStatusCode();
    //
    //     var result = await response.Content.ReadFromJsonAsync<CommentResponseToDto<long>>();
    //     return Created(string.Empty, result);
    // }
    
    [HttpPost]
    public override async Task<ActionResult<CommentResponseToDto<long>>> Create([FromBody] CommentRequestToDto<long> request)
    {
        var story = await _storyService.GetAsync(request.StoryID);
        
        if (story == null)
        {
            return BadRequest(new { 
                message = "Validation Error: Story association not found.",
                statusCode = 400 
            });
        }

        var response = await _httpClient.PostAsJsonAsync("", request);
        
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        var result = await response.Content.ReadFromJsonAsync<CommentResponseToDto<long>>();
        return Created(string.Empty, result);
    }

    [HttpPut]
    public override async Task<IActionResult> Update([FromBody] CommentRequestToDto<long> request)
    {
        var response = await _httpClient.PutAsJsonAsync("", request);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<CommentResponseToDto<long>>();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(long id)
    {
        var response = await _httpClient.DeleteAsync($"{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound || 
            response.StatusCode == System.Net.HttpStatusCode.BadRequest) 
        {
            return BadRequest();
        }
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }
        return NoContent();
    }
}