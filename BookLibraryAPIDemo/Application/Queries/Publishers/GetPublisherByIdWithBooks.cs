using AutoMapper;
using BookLibraryAPIDemo.Application.DTO.Publisher;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Publishers
{
    public class GetPublisherByIdWithBooks : IRequest<PublisherWithBooksDto>
    {
        public string PublisherId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
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

            var paginatedBooks = publisher.Books.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            publisher.Books = paginatedBooks;
            return _mapper.Map<PublisherWithBooksDto>(publisher);
        }
    }
}