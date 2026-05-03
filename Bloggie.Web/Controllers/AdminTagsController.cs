using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        // GET: Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Add
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
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

            await tagRepository.AddTagAsync(tag);

            return RedirectToAction("List");
        }

        // GET: List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await tagRepository.GetTagAsync(id);

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

        // POST: Edit ✅ FIXED
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(editTagRequest);
            }

            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateTagAsync(tag);

            if (updatedTag != null)
            {
                // success
            }
            else
            {
                // error
            }

            return RedirectToAction("List");
        }

        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTag = await tagRepository.DeleteTagAsync(id);

            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("List");
        }
    }
}