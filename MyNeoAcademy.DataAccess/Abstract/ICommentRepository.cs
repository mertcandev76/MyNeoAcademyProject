using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{

    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetAllWithIncludesAsync();
        Task<Comment?> GetByIdWithIncludesAsync(int id);
        Task<List<Comment>> GetByIdWithIncludesBlogAsync(int blogId);

        Task<List<Comment>> GetPagedCommentsAsync(int skip, int take);
        Task<int> GetTotalCountAsync();

    }
}
