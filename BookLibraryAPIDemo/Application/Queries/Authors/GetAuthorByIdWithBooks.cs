using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.Author;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Application.Queries.Authors
{
    public class GetAuthorByIdWithBooks : IRequest<AuthorWithBooksDto>
    {
        public string AuthorId { get; set; }
        public PaginationParams PaginationParams { get; set; }
        public SortParams SortParams { get; set; }
    }

    public class GetAuthorByIdWithBooksHandler : IRequestHandler<GetAuthorByIdWithBooks, AuthorWithBooksDto>
    {
        private readonly IBaseRepository<Author> _repository;
        private readonly IMapper _mapper;

        public GetAuthorByIdWithBooksHandler(IBaseRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthorWithBooksDto> Handle(GetAuthorByIdWithBooks request,
            CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdAsync(request.AuthorId, (Author a) => a.Books);
            if (author == null)
            {
                throw new AuthorNotFoundException(request.AuthorId);
            }

            var paginatedBooks = author.Books
                .Skip((request.PaginationParams.Number - 1) * request.PaginationParams.Size)
                .Take(request.PaginationParams.Size)
                .OrderBy(b => EF.Property<object>(b, request.SortParams.SortBy))
                .ToList();
            author.Books = paginatedBooks;
            return _mapper.Map<AuthorWithBooksDto>(author);
        }
    }
}