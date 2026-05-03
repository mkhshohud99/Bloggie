namespace Bloggie.Web.Models.Domain
{
    public class Tag
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public string DisplayName { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
