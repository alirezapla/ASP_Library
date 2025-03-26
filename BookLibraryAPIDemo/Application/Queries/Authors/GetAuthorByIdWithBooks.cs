using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.Author;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Authors
{
    public class GetAuthorByIdWithBooks : IRequest<AuthorWithBooksDto>
    {
        public string AuthorId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
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

            var paginatedBooks = author.Books.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            author.Books = paginatedBooks;
            return _mapper.Map<AuthorWithBooksDto>(author);
        }
    }
}