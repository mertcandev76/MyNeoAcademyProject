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
    public class AuthorManager : GenericManager<Author>, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository):base(authorRepository) 
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author?> GetAllWithBlogAsync(int id) => await _authorRepository.GetAllWithBlogAsync(id);
    }
}
