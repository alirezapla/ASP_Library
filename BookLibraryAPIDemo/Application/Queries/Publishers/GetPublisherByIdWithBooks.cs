using AutoMapper;
using BookLibraryAPIDemo.Application.DTO.Publisher;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Publishers
{
    public class GetPublisherByIdWithBooks : IRequest<PublisherWithBooksDto>
    {
        public string PublisherId { get; set; }
        public PaginationParams PaginationParams { get; set; }
        public SortParams SortParams { get; set; }
    }

    public class GetPublisherByIdWithBooksHandler : IRequestHandler<GetPublisherByIdWithBooks, PublisherWithBooksDto>
    {
        private readonly IBaseRepository<Publisher> _repository;
        private readonly IMapper _mapper;

        public GetPublisherByIdWithBooksHandler(IBaseRepository<Publisher> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PublisherWithBooksDto> Handle(GetPublisherByIdWithBooks request,
            CancellationToken cancellationToken)
        {
            var publisher = await _repository.GetByIdAsync(request.PublisherId
                , (Publisher p) => p.Books);
            if (publisher == null)
            {
                throw new PublisherNotFoundException(request.PublisherId);
            }

            var paginatedBooks = publisher.Books
                .Skip((request.PaginationParams.Number - 1) * request.PaginationParams.Size)
                .Take(request.PaginationParams.Size);
            
            var sortBy = request.SortParams.SortBy;
            paginatedBooks = sortBy switch
            {
                "Title" => paginatedBooks.OrderBy(b => b.Title),
                "CreatedDate" => paginatedBooks.OrderBy(b => b.CreatedDate),
                "UpdatedDate" => paginatedBooks.OrderBy(b => b.UpdatedDate),
                "PublisherId" => paginatedBooks.OrderBy(b => b.PublisherId),
                _ => paginatedBooks
            };
            
            publisher.Books = paginatedBooks.ToList();
            return _mapper.Map<PublisherWithBooksDto>(publisher);
        }
    }
}