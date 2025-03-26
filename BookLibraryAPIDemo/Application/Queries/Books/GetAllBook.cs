using AutoMapper;
using BookLibraryAPIDemo.Application.DTO.Book;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Books
{
    public class GetAllBook : IRequest<PagedResult<AllBooksDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetBooksHandler : IRequestHandler<GetAllBook, PagedResult<AllBooksDTO>>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBooksHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AllBooksDTO>> Handle(GetAllBook request, CancellationToken cancellationToken)
        {
            var spec = new BookWithRelationsSpecification();
            var (getAllBooks, totalCount) = await _repository.GetAllAsync(request.PageNumber, request.PageSize, spec);
            var results = _mapper.Map<List<AllBooksDTO>>(getAllBooks);
            return new PagedResult<AllBooksDTO>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

