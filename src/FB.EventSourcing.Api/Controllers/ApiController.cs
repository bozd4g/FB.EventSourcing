using FB.EventSourcing.Application;
using Microsoft.AspNetCore.Mvc;

namespace FB.EventSourcing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        [NonAction]
        protected static ApiResponse ApiResponse()
        {
            var model = new ApiResponse();
            return model;
        }

        [NonAction]
        protected static ApiResponse ApiResponse<T>(T obj)
            where T : class, new()
        {
            var model = new ApiResponse<T>(obj);
            return model;
        }
    }
}