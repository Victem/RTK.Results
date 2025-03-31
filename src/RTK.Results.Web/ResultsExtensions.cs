using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RTK.Results.Core;


namespace RTK.Results.Web
{
    public static class ResultsExtensions
    {
        
        public static ProblemDetails ToProblem(this Error error)
        {
            return new ProblemDetails
            {
                Type = error.Type,
                Detail = error.Message,
                Status = error.StatusCode,
                Title = error.Title,
                Instance = error.Instance,
                Extensions = error.Metadata,
            };
        }
    }
}
