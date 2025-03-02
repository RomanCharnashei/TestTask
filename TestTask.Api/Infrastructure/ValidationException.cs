using Microsoft.AspNetCore.Identity;

namespace TestTask.Api.Infrastructure
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(string error) : base(error)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IdentityResult identityResult) : this()
        {
            Errors = identityResult.Errors
                .GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
