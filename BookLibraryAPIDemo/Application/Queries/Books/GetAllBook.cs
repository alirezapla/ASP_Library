using AutoMapper;
using BookLibraryAPIDemo.Application.DTO.Book;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Books
{
    public class GetAllBook : IRequest<PagedResult<AllBooksDTO>>
    {
        public QueryParams QueryParams { get; set; }
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
            var (getAllBooks, totalCount) =
                await _repository.GetAllAsync(BuildSpec(request.QueryParams), request.QueryParams.SortParams);
            var results = _mapper.Map<List<AllBooksDTO>>(getAllBooks);
            return new PagedResult<AllBooksDTO>(results, totalCount, request.QueryParams.PaginationParams);
        }

        private static IRichSpecification<Book> BuildSpec(QueryParams queryParams)
        {
            var builder = new SpecificationBuilder<Book>();

            foreach (var filter in queryParams.Filters)
            {
                builder.Where(filter);
            }

            return builder.Includes(new BookWithRelationsSpecification().Includes)
                .ApplyPaging(queryParams.PaginationParams)
                .Build();
        }
    }
}