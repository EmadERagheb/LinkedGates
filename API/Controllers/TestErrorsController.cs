using API.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class TestErrorsController : BaseAPIsController
    {
        [HttpGet("not-found")]
        public IActionResult GetNotFoundResponse()
        {
            return NotFound(new APIExceptionResponse(404));
        }
        [HttpGet("serverError")]
        public IActionResult GetServerError()
        {
            string thing = "thing";
            return Ok((int.Parse(thing)));
        }
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new APIExceptionResponse(400));
        }
        [HttpGet("bad-request/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("secret-text")]
        public IActionResult GetSecretText()
        {
            return Ok("this is secret text");
        }

    }
}
