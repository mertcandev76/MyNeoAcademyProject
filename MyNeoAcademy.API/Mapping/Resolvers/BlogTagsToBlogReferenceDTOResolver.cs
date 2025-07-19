using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping.Resolvers
{
    public class BlogTagsToBlogReferenceDTOResolver : IValueResolver<Tag, ResultTagDTO, List<BlogReferenceDTO>>
    {
        public List<BlogReferenceDTO> Resolve(Tag source, ResultTagDTO destination, List<BlogReferenceDTO> destMember, ResolutionContext context)
        {
            return source.BlogTags != null
                ? source.BlogTags
                    .Where(bt => bt.Blog != null)
                    .Select(bt => context.Mapper.Map<BlogReferenceDTO>(bt.Blog))
                    .ToList()
                : new List<BlogReferenceDTO>();
        }
    }
}
