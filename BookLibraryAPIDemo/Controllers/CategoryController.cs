using BookLibraryAPIDemo.Application.Commands.BookLibraryAPICategory;
using BookLibraryAPIDemo.Application.Commands.Categorys;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.category;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Application.Queries.Categories;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    [Route("categories")]
    public class CategoryController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreateCategory {Category = model}));
        }


        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CategoryDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, [FromQuery] string sortBy = "Name", [FromQuery] bool sortDescending = false,
            [FromQuery] List<string> filterProperty = null, [FromQuery] List<string> filterValue = null,
            [FromQuery] List<string> filterOperator = null)
        {
            var queryParams = BuildQueryParams(
                filterProperty, filterValue, filterOperator,
                pageNumber, pageSize, sortBy, sortDescending);
            
            return Ok(await Mediator.Send(new GetAllCategories() {QueryParams = queryParams}));
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetCategoryById() {CategoryId = id}));
        }

        [HttpGet("{id}/books")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CategoryWithBooksDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryByIdWithBooksAsync([FromRoute] string id,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Title", [FromQuery] bool sortDescending = false,
            [FromQuery] List<string> filterProperty = null,
            [FromQuery] List<string> filterValue = null,
            [FromQuery] List<string> filterOperator = null)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdWithBooks()
            {
                PaginationParams = new PaginationParams() {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams() {SortBy = sortBy, SortDescending = sortDescending}
            }));
        }


        [HttpPut("{id}")]
        [Authorize(Policy = "CanWrite")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string id, [FromBody] UpdateCategoryDTO model)
        {
            return Ok(await Mediator.Send(new UpdateCategory {Id = id, Category = model}));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] string id)
        {
            await Mediator.Send(new DeleteCategory {CategoryId = id});
            return NoContent();
        }

        [HttpPut("export")]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> ExportCsvAsync()
        {
            return Ok(Task.CompletedTask);
        }
    }
}