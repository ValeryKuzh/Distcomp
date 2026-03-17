using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using BlogService.Application.Interfaces.Services;
using BlogService.Domain.Entities;
using Shared.Application.Interfaces.Mappers;
using Shared.Application.Services;
using Shared.Domain.Interfaces;

namespace BlogService.Application.Services;

public class StoryService<Id> : BaseService<Id, Story<Id>, StoryRequestToDto<Id>, StoryResponseToDto<Id>>, IStoryService<Id>
{
    public StoryService(IRepository<Id, Story<Id>> repository,
        IRequestMapper<StoryRequestToDto<Id>, Story<Id>> userRequestMapper,
        IResponseMapper<Story<Id>, StoryResponseToDto<Id>> userResponseMapper) : base(repository, userRequestMapper, userResponseMapper){ }

    public override async Task<StoryResponseToDto<Id>> CreateAsync(StoryRequestToDto<Id> request)
    {
        var entity = _requestMapper.Map(request);
        entity.Created = DateTime.UtcNow;
        await _repository.AddAsync(entity); 
        return _responseMapper.Map(entity);
    }
}