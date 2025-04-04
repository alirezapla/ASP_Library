﻿using BookLibraryAPIDemo.Application.Commands.Authors;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.Author;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Application.Queries.Authors;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    [Route("authors")]
    public class AuthorsController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreateAuthor {Author = model}));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AuthorDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Name", [FromQuery] bool sortDescending = false,
            [FromQuery] List<string> filterProperty = null, [FromQuery] List<string> filterValue = null,
            [FromQuery] List<string> filterOperator = null)
        {
            var queryParams = BuildQueryParams(
                filterProperty, filterValue, filterOperator,
                pageNumber, pageSize, sortBy, sortDescending);

            return Ok(await Mediator.Send(new GetAllAuthors() {QueryParams = queryParams}));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> GetAuthorByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetAuthorById() {AuthorId = id}));
        }

        [HttpGet("{id}/books")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AuthorWithBooksDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorByIdWithBooksAsync([FromRoute] string id,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Title", [FromQuery] bool sortDescending = false)
        {
            return Ok(await Mediator.Send(new GetAuthorByIdWithBooks()
            {
                AuthorId = id, PaginationParams = new PaginationParams() {Number = pageNumber, Size = pageSize},
                SortParams = new SortParams() {SortBy = sortBy, SortDescending = sortDescending}
            }));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CanWrite")]
        public async Task<IActionResult> UpdateAuthorAsync([FromRoute] string id, [FromBody] UpdateAuthorDTO model)
        {
            return Ok(await Mediator.Send(new UpdateAuthor {Id = id, Author = model}));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] string id)
        {
            await Mediator.Send(new DeleteAuthor {AuthorId = id});
            return NoContent();
        }
    }
}