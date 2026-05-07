using BlogService.Application.DTOs.Request;
using BlogService.Application.DTOs.Response;
using Microsoft.Extensions.DependencyInjection;
using BlogService.Application.Interfaces.Services;
using BlogService.Application.Mappers;
using BlogService.Application.Services;
using BlogService.Domain.Entities;
using BlogService.Infrastructure.PostgreSQL.Context;
using BlogService.Infrastructure.PostgreSQL.Repositories;
using BlogService.Infrastructure.Storage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Application.Interfaces.Mappers;
using Shared.Domain.Interfaces;

namespace BlogService.Application.DependencyInjection;

public static class BlogServiceApplicationExtensions
{
    public static IServiceCollection AddApplication<Id>(this IServiceCollection services, IConfiguration configuration) where Id : notnull
    {
        // Сервисы
        services.AddScoped<IUserService<Id>, UserService<Id>>();
        services.AddScoped<IStoryService<Id>, StoryService<Id>>();
        services.AddScoped<ICommentService<Id>, CommentService<Id>>();
        services.AddScoped<IStickerService<Id>, StickerService<Id>>();
        
        // Мапперы
        services.AddScoped<IRequestMapper<UserRequestToDto<Id>, User<Id>>, UserMapper<Id>>();
        services.AddScoped<IResponseMapper<User<Id>, UserResponseToDto<Id>>, UserMapper<Id>>();
        
        services.AddScoped<IRequestMapper<StoryRequestToDto<Id>, Story<Id>>, StoryMapper<Id>>();
        services.AddScoped<IResponseMapper<Story<Id>, StoryResponseToDto<Id>>, StoryMapper<Id>>();
        
        services.AddScoped<IRequestMapper<CommentRequestToDto<Id>, Comment<Id>>, CommentMapper<Id>>();
        services.AddScoped<IResponseMapper<Comment<Id>, CommentResponseToDto<Id>>, CommentMapper<Id>>();
        
        services.AddScoped<IRequestMapper<StickerRequestToDto<Id>, Sticker<Id>>, StickerMapper<Id>>();
        services.AddScoped<IResponseMapper<Sticker<Id>, StickerResponseToDto<Id>>, StickerMapper<Id>>();
        
        // Репозитории
        //services.AddSingleton(typeof(IRepository<,>), typeof(InMemoryRepository<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(BlogServiceEFRepository<,>));
        
        // Контекст базы данных
        var connectionString = configuration.GetConnectionString("PostgreSQLConnectionString");
        services.AddDbContext<BlogServiceDbContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}