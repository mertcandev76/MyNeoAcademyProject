using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping.Resolvers
{
    public class AppUserProfileImageResolver : IValueResolver<AppUser, ResultAppUserDTO, string?>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
         
        public AppUserProfileImageResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Resolve(AppUser source, ResultAppUserDTO destination, string? destMember, ResolutionContext context)
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
