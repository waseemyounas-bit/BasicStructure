using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> repository;
        private readonly IUnitofWork unitofWork;
        public PostService(IRepository<Post> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public void AddPost(Post post)
        {
            repository.Add(post);
            unitofWork.saveChanges();
        }

        public void DeletePost(Guid id)
        {
            repository.Delete(id);
            unitofWork.saveChanges();
        }

        public IQueryable<Post> GetAllPosts()
        {
            var posts = repository.GetAll().OrderByDescending(x => x.CreatedDate);
            return posts;
        }
        public List<Post> GetAllJobs()
        {
            var posts = repository.GetAll().Where(x=>x.IsJob==true).OrderByDescending(x => x.CreatedDate).ToList();
            return posts;
        }

        public Post GetPostbyId(Guid id)
        {
            var post = repository.GetById(id);
            return post;
        }

        public void UpdatePost(Post post)
        {
            repository.Update(post);
            unitofWork.saveChanges();
        }
    }
}
