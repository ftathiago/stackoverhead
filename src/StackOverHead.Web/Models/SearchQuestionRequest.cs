using Microsoft.AspNetCore.Routing;

namespace StackOverHead.Web.Models
{
    public class SearchQuestionRequest : IParameterPolicy
    {
        public SearchQuestionRequest()
        {
            Page = 1;
            PageSize = 20;
        }

        public string Question { get; set; }

        public string? Tags { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}