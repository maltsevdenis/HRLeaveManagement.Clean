
namespace HRLeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> ValidationsErrors { get; set; }

        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(string message, FluentValidation.Results.ValidationResult validationResult) : base(message)
        {
            ValidationsErrors = new();

            foreach(var error in validationResult.Errors)
            {
                ValidationsErrors.Add(error.ErrorMessage);
            }
        }
    }
}
