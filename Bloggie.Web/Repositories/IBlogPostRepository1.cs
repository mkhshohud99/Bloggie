using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostRepository1
    {
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(int id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(int id);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
    }
}