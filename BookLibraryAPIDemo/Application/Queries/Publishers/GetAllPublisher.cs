﻿using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Publishers
{
    public class GetAllPublisher : IRequest<PagedResult<PublisherDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
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
            var (allPublishers, totalCount) = await _repository.GetAllAsync(request.PageNumber, request.PageSize);
            var results = _mapper.Map<List<PublisherDTO>>(allPublishers);
            return new PagedResult<PublisherDTO>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}