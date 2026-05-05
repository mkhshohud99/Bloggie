using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        [Required]
        public string Heading { get; set; } = string.Empty;

        [Required]
        public string PageTitle { get; set; } = string.Empty;

        [Required]
        public string Contant { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public string? FeaturedImageUrl { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string UrlHandle { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        public bool Visible { get; set; }

        public IEnumerable<SelectListItem>? Tags { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}