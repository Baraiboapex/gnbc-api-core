using Application.BibleStudies;
using Application.BlogPosts;
using Application.Sermons;
using Application.Users;
using Application.ChurchEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Infrastructure;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GNBC-API", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });
            services.AddDbContext<GNBCContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            
            //=============== Mediator Services ===============//

            //Sermon Mediator Services
            services.AddMediatR(typeof(ListSermons.GetSermonsHandler).Assembly);

            //Bible Study Mediator Services
            services.AddMediatR(typeof(ListBibleStudies.GetBibleStudiesHandler).Assembly);

            //User Mediator Services
            services.AddMediatR(typeof(ListUsers.GetUsers).Assembly);

            //Blog Post Mediator Services
            services.AddMediatR(typeof(GetBlogPosts.ListBlogPostsHandler).Assembly);

            //Church Events Mediator Services
            services.AddMediatR(typeof(ListChurchEvents.GetChurchEvents).Assembly);
            //=============== Mediator Services ===============//


            //=============== JWTToken Services ===============//
                var jwtTokenConfig = config.GetSection("jwtTokenConfig").Get<JwtConfig>();

                services.AddSingleton(jwtTokenConfig);
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                        ValidAudience = jwtTokenConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
            //=============== JWTToken Services ===============//
            return services;
        }
    }
}