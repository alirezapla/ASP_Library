using AutoMapper;
using BookLibraryAPIDemo.Application.DTO.Author;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Authors
{
    public class GetAllAuthors : IRequest<PagedResult<AuthorDTO>>
    {
        public QueryParams QueryParams { get; set; }
    }

    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthors, PagedResult<AuthorDTO>>
    {
        private readonly IBaseRepository<Author> _repository;
        private readonly IMapper _mapper;

        public GetAllAuthorsHandler(IBaseRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AuthorDTO>> Handle(GetAllAuthors request, CancellationToken cancellationToken)
        {
            var (allAuthors, totalCount) = await _repository.GetAllAsync(BuildSpecification(request.QueryParams),
                request.QueryParams.SortParams);

            var results = _mapper.Map<List<AuthorDTO>>(allAuthors);
            return new PagedResult<AuthorDTO>(results, totalCount, request.QueryParams.PaginationParams);
        }

        private static IRichSpecification<Author> BuildSpecification(QueryParams queryParams)
        {
            var builder = new SpecificationBuilder<Author>();

            foreach (var filter in queryParams.Filters)
            {
                builder.Where(filter);
            }

            return builder.ApplyPaging(queryParams.PaginationParams)
                .Build();
        }
    }
}