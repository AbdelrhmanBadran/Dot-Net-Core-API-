using Services.HandleResponse;

namespace Demo.HandleResponse
{
    public class ApiValidationErrorResponse : ApiException
    {
        public ApiValidationErrorResponse()
            : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }

    }
}
