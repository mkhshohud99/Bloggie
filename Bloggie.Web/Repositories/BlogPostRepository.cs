using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        // CREATE
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        // READ ALL
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts
                .Include(x => x.Tags)
                .ToListAsync();
        }

        // READ ONE
        public async Task<BlogPost?> GetAsync(int id)
        {
            return await bloggieDbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // UPDATE
        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existing = await bloggieDbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existing == null)
            {
                return null;
            }

            existing.Heading = blogPost.Heading;
            existing.PageTitle = blogPost.PageTitle;
            existing.Contant = blogPost.Contant;
            existing.ShortDescription = blogPost.ShortDescription;
            existing.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existing.PublishedDate = blogPost.PublishedDate;
            existing.UrlHandle = blogPost.UrlHandle;
            existing.Author = blogPost.Author;
            existing.Visible = blogPost.Visible;

            // Update tags
            existing.Tags = blogPost.Tags;

            await bloggieDbContext.SaveChangesAsync();

            return existing;
        }

        // DELETE
        public async Task<BlogPost?> DeleteAsync(int id)
        {
            var existing = await bloggieDbContext.BlogPosts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
            {
                return null;
            }

            bloggieDbContext.BlogPosts.Remove(existing);
            await bloggieDbContext.SaveChangesAsync();

            return existing;
        }
    }
}