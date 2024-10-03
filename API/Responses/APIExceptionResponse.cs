namespace API.Responses
{
    public class APIExceptionResponse
    {

        public int StateCode { get; set; }

        public string Message { get; set; }

        private string GetDefaultMessage(int stateCode)
        {
            return stateCode switch
            {
                400 => "The provided data does not meet the validation requirements",
                401 => "Invalid credentials provided. Please log in with valid credentials to access this resource",
                404 => "The page or resource you requested does not exist",
                500 => "We're experiencing technical difficulties. Our team is working to resolve the issue as quickly as possible.",
                _ => "please try again later"
            };
        }
        public APIExceptionResponse(int stateCode, string? message=default)
        {
            StateCode=stateCode;
            Message= message ?? GetDefaultMessage(stateCode);
        }
    }
}
