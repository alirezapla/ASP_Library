using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogActionFilter))]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
        
        protected QueryParams BuildQueryParams(
            List<string> filterProperty,
            List<string> filterValue,
            List<string> filterOperator = null,
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "Id",
            bool sortDescending = false)
        {
            return new QueryParams
            {
                PaginationParams = new PaginationParams { Number = pageNumber, Size = pageSize },
                SortParams = new SortParams { SortBy = sortBy, SortDescending = sortDescending },
                Filters = filterProperty?
                    .Select((p, i) => new FilterParams
                    {
                        PropertyName = p,
                        PropertyValue = filterValue[i],
                        Operator = filterOperator?.Count > i ? filterOperator[i] : "=="
                    })
                    .ToList() ?? new List<FilterParams>()
            };
        }
    }
}