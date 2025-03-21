using BookLibraryAPIDemo.API.Controllers;
using BookLibraryAPIDemo.Application.Commands.Publishers;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.Publishers;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    // [Authorize]
    [Route("publishers")]
    public class PublishersController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreatePublisherAsync([FromBody] CreatePublisherDTO model)
        {
            return Ok(await Mediator.Send(new CreatePublisher {Publisher = model}));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<PublisherDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPublishersAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await Mediator.Send(new GetAllPublisher() {PageNumber = pageNumber, PageSize = pageSize}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetPublisherById() {PublisherId = id}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisherAsync([FromRoute] string id,
            [FromBody] UpdatePublisherDTO model)
        {
            return Ok(await Mediator.Send(new UpdatePublisher {Id = id, Publisher = model}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisherAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeletePublisher {publisherId = id}));
        }
    }
}