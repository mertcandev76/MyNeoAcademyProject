using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping.Resolvers
{
    public class BlogTagsToTagReferenceDTOResolver : IValueResolver<Blog, ResultBlogDTO, List<TagReferenceDTO>>
    {
        public List<TagReferenceDTO> Resolve(Blog source, ResultBlogDTO destination, List<TagReferenceDTO> destMember, ResolutionContext context)
        {
            return source.BlogTags != null
                ? source.BlogTags
                    .Where(bt => bt.Tag != null)
                    .Select(bt => context.Mapper.Map<TagReferenceDTO>(bt.Tag))
                    .ToList()
                : new List<TagReferenceDTO>();
        }
    }


}
