﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PrimeiraAPI.Controllers {

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController : ControllerBase {
        [Route("/error")]
        public ActionResult HandlError() =>
            Problem();

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment) {
            if (!hostEnvironment.IsDevelopment()) {
                return NotFound();
            }

            var exceptionHandleFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandleFeature.Error.StackTrace,
                title: exceptionHandleFeature.Error.Message);
        }
    }
}
