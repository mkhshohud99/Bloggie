using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;
        private object bloggieDbContextl;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(addTagRequest);
            }

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }

        // =========================
        // GET: Edit
        // =========================
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag == null)
            {
                return RedirectToAction("List");
            }

            var model = new EditTagRequest
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            return View(model);
        }

        // =========================
        // POST: Edit  (FIXED)
        // =========================
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var existingTag = bloggieDbContext.Tags.Find(editTagRequest.Id);

            if (existingTag != null)
            {
                existingTag.Name = editTagRequest.Name;
                existingTag.DisplayName = editTagRequest.DisplayName;

                bloggieDbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var tag = bloggieDbContext.Tags.Find(id);

            if (tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                bloggieDbContext.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}