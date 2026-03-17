using DiscussionService.Application.DTOs.Requests;
using DiscussionService.Application.DTOs.Responses;
using DiscussionService.Application.Interfaces.Services;
using DiscussionService.Application.Mappers;
using DiscussionService.Application.Services;
using DiscussionService.Domain.Entities;
using DiscussionService.Infrastructure.Cassandra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Interfaces.Mappers;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Cassandra;

namespace DiscussionService.Application;

public static class DiscussionServiceApplicationExtensions
{
    public static IServiceCollection AddApplication<Id>(this IServiceCollection services, IConfiguration configuration)
    {
        // Репозиторий
        services.AddScoped(typeof(IRepository<Id, Comment<Id>>), typeof(CassandraCommentRepository<Id, Comment<Id>>));

        // Сервисы
        services.AddScoped<ICommentService<Id>, CommentService<Id>>();
        
        // Мапперы
        services.AddScoped<IRequestMapper<CommentRequestToDto<Id>, Comment<Id>>, CommentMapper<Id>>();
        services.AddScoped<IResponseMapper<Comment<Id>, CommentResponseToDto<Id>>, CommentMapper<Id>>();

        services.AddCassandra(configuration);
        return services;
    }
}