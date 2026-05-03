using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag> GetTagAsync(int id);

        Task<Tag> AddTagAsync(Tag tag);
        Task<Tag?> UpdateTagAsync(Tag tag);

        Task<Tag?> DeleteTagAsync(int id);
    }
}
