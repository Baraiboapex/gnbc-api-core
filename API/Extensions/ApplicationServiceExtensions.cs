using Application.BibleStudies;
using Application.BlogPosts;
using Application.Sermons;
using Application.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddDbContext<GNBCContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            
            //Sermon Mediator Services
            services.AddMediatR(typeof(ListSermons.GetSermonsHandler).Assembly);

            //Bible Study Mediator Services
            services.AddMediatR(typeof(ListBibleStudies.GetBibleStudiesHandler).Assembly);

            //User Mediator Services
            services.AddMediatR(typeof(ListUsers.GetUsers).Assembly);

            //Blog Post Mediator Services
            services.AddMediatR(typeof(GetBlogPosts.ListBlogPostsHandler).Assembly);

            return services;
        }
    }
}