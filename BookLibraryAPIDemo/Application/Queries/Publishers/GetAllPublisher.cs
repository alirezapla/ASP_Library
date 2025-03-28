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
        public PaginationParams PaginationParams { get; set; }
        public SortParams SortParams { get; set; }
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
            var (allPublishers, totalCount) =
                await _repository.GetAllAsync(request.PaginationParams, sortParams: request.SortParams);
            var results = _mapper.Map<List<PublisherDTO>>(allPublishers);
            return new PagedResult<PublisherDTO>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = request.PaginationParams.Number,
                PageSize = request.PaginationParams.Size
            };
        }
    }
}