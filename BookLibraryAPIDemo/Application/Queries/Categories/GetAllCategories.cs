using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.category;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Categories
{
    public class GetAllCategories : IRequest<PagedResult<CategoryDTO>>
    {
        public QueryParams QueryParams { get; set; }
    }

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, PagedResult<CategoryDTO>>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CategoryDTO>> Handle(GetAllCategories request,
            CancellationToken cancellationToken)
        {
            var (allCategories, totalCount) = await _repository.GetAllAsync(BuildSpecification(request.QueryParams),
                request.QueryParams.SortParams);
            var results = _mapper.Map<List<CategoryDTO>>(allCategories);
            return new PagedResult<CategoryDTO>(results, totalCount, request.QueryParams.PaginationParams);
        }

        private static IRichSpecification<Category> BuildSpecification(QueryParams queryParams)
        {
            var builder = new SpecificationBuilder<Category>();

            foreach (var filter in queryParams.Filters)
            {
                builder.Where(filter);
            }

            return builder.ApplyPaging(queryParams.PaginationParams)
                .Build();
        }
    }
}