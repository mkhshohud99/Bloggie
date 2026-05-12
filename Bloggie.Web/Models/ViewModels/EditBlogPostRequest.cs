using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public int Id { get; set; }

        public string Heading { get; set; } = string.Empty;

        public string PageTitle { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string FeaturedImageUrl { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        public string UrlHandle { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }
            = new List<SelectListItem>();

        public List<string> SelectedTags { get; set; }
            = new List<string>();
    }
}