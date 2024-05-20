using System.Net;
using LivroMente.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryBookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryBookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IMediator mediator)
        {
            var request = new GetAllCategoryBook();

            var stories = await mediator.Send(request);
            return Ok(stories);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatedAsync([FromBody] CategoryBookRequest command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtRoute("GetCategoryBook", new { id = response.Id }, response);
        }
            
    }

//     System.InvalidOperationException: Unable to resolve service for type 'MediatR.IMediator' while attempting to activate 'LivroMente.API.Controllers.CategoryBookController'.
//    at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.ThrowHelperUnableToResolveService(Type type, Type requiredBy)
//    at lambda_method10(Closure, IServiceProvider, Object[])
//    at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
// --- End of stack trace from previous location ---
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
//    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
//    at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
//    at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
//    at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
//    at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
//    at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
}
