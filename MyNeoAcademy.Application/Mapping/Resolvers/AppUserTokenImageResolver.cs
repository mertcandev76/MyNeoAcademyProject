using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.DTOs.Auth;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping.Resolvers
{
    public class AppUserTokenImageResolver : IValueResolver<AppUser, TokenResultDTO, string?>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserTokenImageResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Resolve(AppUser source, TokenResultDTO destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.ProfileImageUrl))
                return null;

            if (source.ProfileImageUrl.StartsWith("http"))
                return source.ProfileImageUrl;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return source.ProfileImageUrl;

            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{source.ProfileImageUrl.TrimStart('/')}";
        }
    }
}
