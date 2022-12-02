namespace API.Errors;
using System.Collections.Generic;

    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {

        }
        public IEnumerable<string> Errors {get; set;}
    }
