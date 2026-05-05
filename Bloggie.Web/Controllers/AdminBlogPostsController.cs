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
    }
}