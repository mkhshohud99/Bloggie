using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository,
                                        IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.DisplayName,
                    Value = t.Id.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest model)
        {
            Console.WriteLine("POST HIT");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("MODEL INVALID");

                var tags = await tagRepository.GetAllAsync();
                model.Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.DisplayName,
                    Value = t.Id.ToString()
                });

                return View(model);
            }

            var blogPost = new BlogPost
            {
                Heading = model.Heading,
                PageTitle = model.PageTitle,
                Contant = model.Contant,
                ShortDescription = model.ShortDescription,
                FeaturedImageUrl = model.FeaturedImageUrl,
                PublishedDate = model.PublishedDate,
                UrlHandle = model.UrlHandle,
                Author = model.Author,
                Visible = model.Visible,
                Tags = new List<Tag>()
            };

            if (model.SelectedTags != null)
            {
                foreach (var id in model.SelectedTags)
                {
                    if (int.TryParse(id, out int tagId))
                    {
                        var tag = await tagRepository.GetAsync(tagId);
                        if (tag != null)
                        {
                            blogPost.Tags.Add(tag);
                        }
                    }
                }
            }

            await blogPostRepository.AddAsync(blogPost);

            Console.WriteLine("SAVED TO DB");

            return RedirectToAction("Add");
        }

        public async Task<IActionResult> List()
        {
            //call the repository to get all blog posts from the database
            var blogPost = await blogPostRepository.GetAllAsync();

            
            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);

            if (blogPost == null)
            {
                return NotFound();   // easier to debug
            }

            var tags = await tagRepository.GetAllAsync();

            var model = new EditBlogPostRequest
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Contant,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                Visible = blogPost.Visible,
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.DisplayName,
                    Value = x.Id.ToString()
                }),
                SelectedTags = blogPost.Tags
                                .Select(x => x.Id.ToString())
                                .ToList()
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Report(
        string? authorName,
        DateTime? fromDate,
        DateTime? toDate)
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            // Filter by author
            if (!string.IsNullOrWhiteSpace(authorName))
            {
                blogPosts = blogPosts
                    .Where(x => x.Author.Contains(authorName))
                    .ToList();
            }

            // Filter by from date
            if (fromDate.HasValue)
            {
                blogPosts = blogPosts
                    .Where(x => x.PublishedDate >= fromDate.Value)
                    .ToList();
            }

            // Filter by to date
            if (toDate.HasValue)
            {
                blogPosts = blogPosts
                    .Where(x => x.PublishedDate <= toDate.Value)
                    .ToList();
            }

            var reports = blogPosts
                .GroupBy(x => x.Author)
                .Select(x => new AuthorPostReport
                {
                    Author = x.Key,
                    TotalPosts = x.Count()
                })
                .OrderByDescending(x => x.TotalPosts)
                .ToList();

            var model = new AuthorPostReportRequest
            {
                AuthorName = authorName,
                FromDate = fromDate,
                ToDate = toDate,
                Reports = reports
            };

            return View(model);
        }
    }
}