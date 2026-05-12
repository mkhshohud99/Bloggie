namespace Bloggie.Web.Models.ViewModels
{
    public class AuthorPostReportRequest
    {
        public string? AuthorName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<AuthorPostReport> Reports { get; set; } = [];
    }
}