using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IPostService
    {
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Guid id);
        Post GetPostbyId(Guid id);
        IQueryable<Post> GetAllPosts();
        List<Post> GetAllJobs();
    }
}
