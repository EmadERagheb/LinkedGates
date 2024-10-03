namespace API.Responses
{
    public class APIValidationErrorResponse(IEnumerable<string> errors) : APIExceptionResponse(400)
    {
        public IEnumerable<string> Errors { get; } = errors;
    }
}
