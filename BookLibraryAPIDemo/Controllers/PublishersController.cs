using BookLibraryAPIDemo.Application.Commands.Publishers;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.Publisher;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Application.Queries.Publishers;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    [Route("publishers")]
    public class PublishersController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePublisherAsync([FromBody] CreatePublisherDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreatePublisher {Publisher = model}));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<PublisherDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPublishersAsync([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, [FromQuery] string sortBy = "PublisherName", [FromQuery] bool sortDescending = false)

        {
            return Ok(await Mediator.Send(new GetAllPublisher()
            {
                PaginationParams = new PaginationParams() {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams() {SortBy = sortBy, SortDescending = sortDescending}
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> GetPublisherByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetPublisherById() {PublisherId = id}));
        }

        [HttpGet("{id}/books")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<PublisherWithBooksDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPublisherByIdWithBooksAsync([FromRoute] string id,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Title", [FromQuery] bool sortDescending = false)
        {
            return Ok(await Mediator.Send(new GetPublisherByIdWithBooks()
            {
                PublisherId = id, PaginationParams = new PaginationParams() {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams() {SortBy = sortBy, SortDescending = sortDescending}
            }));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CanWrite")]
        public async Task<IActionResult> UpdatePublisherAsync([FromRoute] string id,
            [FromBody] UpdatePublisherDTO model)
        {
            return Ok(await Mediator.Send(new UpdatePublisher {Id = id, Publisher = model}));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePublisherAsync([FromRoute] string id)
        {
            await Mediator.Send(new DeletePublisher {publisherId = id});
            return NoContent();
        }
    }
}