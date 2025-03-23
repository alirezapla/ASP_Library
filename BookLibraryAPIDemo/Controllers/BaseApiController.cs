using BookLibraryAPIDemo.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    // [Authorize]
    [ServiceFilter(typeof(LogActionFilter))]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}