using ECommerceFeedback.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ECommerceFeedback.Controllers
{
  
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        internal virtual IActionResult Respond(ApiResponse apiResponse)
        {
            return apiResponse.Success ? new JsonResult(apiResponse) as IActionResult : ErrorRespond(apiResponse);
        }
 
        private ObjectResult ErrorRespond(ApiResponse apiResponse)
        {
            int httpStatusCode = Constants.IntegrationApiFailureStatusCode;
            ObjectResult response;
            if (apiResponse.ErrorDetails.HasAny(x => x.Code == Constants.BAD_REQUEST_CODE.ToString()))
            {
                httpStatusCode = Constants.BadRequestApiFailureStatusCode;
                response = new ObjectResult(new ApiResponse
                {
                    ErrorDetails = apiResponse.ErrorDetails
                });
            }
            else if (apiResponse.ErrorDetails.HasAny(x => x.Code == Constants.InternalApiCustomErrorStatusCode.ToString()))
            {
                httpStatusCode = Constants.InternalApiCustomErrorStatusCode;
                response = new ObjectResult(new ApiResponse
                {
                    ErrorDetails = apiResponse.ErrorDetails
                });
            }
            else
            {
                response = new ObjectResult(new ApiResponse
                {
                    ErrorDetails = new List<ErrorModel> {
                    new ErrorModel
                    {
                        Code = Constants.IntegrationApiFailureStatusCode.ToString(),
                        Description = Constants.INTEGRATION_API_ERROR_MESSSAGE
                    }
                }});
            }
            this.HttpContext.Response.StatusCode = httpStatusCode;
            return response;
        }

        internal virtual IActionResult BadRequest(string message)
        {
            ApiResponse response = new ApiResponse()
            {
                ErrorDetails = new List<ErrorModel>
                {
                     new ErrorModel
                     {
                         Code = Constants.DataNotFoundStatusCode,
                         Description = GetErrorMessage(message)
                     }
                }
            };
            return BadRequest(response);
        }

        private static string GetErrorMessage(string message)
        {
            var regex = new Regex(Constants.ScriptRegex);
            var isMatch = regex.IsMatch(message.ToLower());
            return isMatch ? Constants.InvalidRequestParameter : message;
        }

        internal virtual IActionResult AERespond(ApiResponse apiResponse)
        {
            if (apiResponse.Success)
            {
                return new JsonResult(apiResponse);
            }

            else if (apiResponse.ErrorDetails.Any(x => x.Code == Constants.INTERNAL_API_ERROR_CODE))
            {
                return StatusCode(Convert.ToInt32(Constants.INTERNAL_API_ERROR_CODE), apiResponse);
            }

            else if (apiResponse.ErrorDetails.Any(x => x.Code == Constants.DatabaseExceptionStatusCode.ToString()))
            {
                return StatusCode(Convert.ToInt32(Constants.DatabaseExceptionHttpStatusCode), apiResponse);
            }

            else if (apiResponse.ErrorDetails.Any(x => x.Code == Constants.NotFoundStatusCode.ToString()))
            {
                return NotFound();
            }

            else //TODO: Need to handle for more validation/business rule/error types
            {
                return BadRequest(apiResponse);
            }
        }
    }
}
