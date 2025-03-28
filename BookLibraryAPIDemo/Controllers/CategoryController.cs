using BookLibraryAPIDemo.Application.Commands.BookLibraryAPICategory;
using BookLibraryAPIDemo.Application.Commands.Categorys;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.category;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Application.Queries.Categories;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    [Route("categories")]
    public class CategoryController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreateCategory {Category = model}));
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CategoryDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, [FromQuery] string sortBy = "Name", [FromQuery] bool sortDescending = false)
        {
            return Ok(await Mediator.Send(new GetAllCategories()
            {
                PaginationParams = new PaginationParams() {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams() {SortBy = sortBy, SortDescending = sortDescending}
            }));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetCategoryById() {CategoryId = id}));
        }

        [HttpGet("{id}/books")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CategoryWithBooksDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryByIdWithBooksAsync([FromRoute] string id,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Title", [FromQuery] bool sortDescending = false)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdWithBooks()
            {
                PaginationParams = new PaginationParams() {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams() {SortBy = sortBy, SortDescending = sortDescending}
            }));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string id, [FromBody] UpdateCategoryDTO model)
        {
            return Ok(await Mediator.Send(new UpdateCategory {Id = id, Category = model}));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] string id)
        {
            await Mediator.Send(new DeleteCategory {CategoryId = id});
            return NoContent();
        }
    }
}