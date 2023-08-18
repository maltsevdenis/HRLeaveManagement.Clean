
namespace HRLeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> ValidationsErrors { get; set; }

        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(string message, FluentValidation.Results.ValidationResult validationResult) : base(message)
        {
            ValidationsErrors = validationResult.ToDictionary();
        }
    }
}
