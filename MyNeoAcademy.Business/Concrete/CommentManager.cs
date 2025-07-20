using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentManager(ICommentRepository commentRepository):base(commentRepository) 
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comment>> GetAllByBlogIdAsync(int blogId)=>await _commentRepository.GetAllByBlogIdAsync(blogId);

        public async Task<List<Comment>> GetAllWithBlogAsync()=>await _commentRepository.GetAllWithBlogAsync();

        public async Task<Comment?> GetByIdWithBlogAsync(int id) => await _commentRepository.GetByIdWithBlogAsync(id);
    }
}
