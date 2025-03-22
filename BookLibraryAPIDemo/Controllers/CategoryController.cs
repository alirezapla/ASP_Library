using BookLibraryAPIDemo.API.Controllers;
using BookLibraryAPIDemo.Application.Commands.BookLibraryAPICategory;
using BookLibraryAPIDemo.Application.Commands.Categorys;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.BookLibraryAPICategory;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    // [Authorize]
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
            [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetAllCategories() {PageNumber = pageNumber, PageSize = pageSize}));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetCategoryById() {CategoryId = id}));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string id, [FromBody] UpdateCategoryDTO model)
        {
            return Ok(await Mediator.Send(new UpdateCategory {Id = id, Category = model}));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteCategory {CategoryId = id}));
        }
    }
}