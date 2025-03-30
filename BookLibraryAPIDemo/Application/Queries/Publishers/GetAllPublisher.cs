using AutoMapper;
using BookLibraryAPIDemo.Application.DTO.Publisher;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Publishers
{
    public class GetAllPublisher : IRequest<PagedResult<PublisherDTO>>
    {
        public QueryParams QueryParams { get; set; }
    }

    public class GetAllPublisherHandler : IRequestHandler<GetAllPublisher, PagedResult<PublisherDTO>>
    {
        private readonly IBaseRepository<Publisher> _repository;
        private readonly IMapper _mapper;

        public GetAllPublisherHandler(IBaseRepository<Publisher> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedResult<PublisherDTO>> Handle(GetAllPublisher request,
            CancellationToken cancellationToken)
        {
            var (allPublishers, totalCount) = await _repository.GetAllAsync(BuildSpecification(request.QueryParams),
                request.QueryParams.SortParams);
            var results = _mapper.Map<List<PublisherDTO>>(allPublishers);
            return new PagedResult<PublisherDTO>(results, totalCount, request.QueryParams.PaginationParams);
        }

        private static IRichSpecification<Publisher> BuildSpecification(QueryParams queryParams)
        {
            var builder = new SpecificationBuilder<Publisher>();

            foreach (var filter in queryParams.Filters)
            {
                builder.Where(filter);
            }

            return builder.Include(a => a.Books)
                .ApplyPaging(queryParams.PaginationParams)
                .Build();
        }
    }
}