using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.category;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Application.Queries.Categories
{
    public class GetCategoryByIdWithBooks : IRequest<CategoryWithBooksDTO>
    {
        public string CategoryId { get; set; }
        public PaginationParams PaginationParams { get; set; }
        public SortParams SortParams { get; set; }
    }

    public class GetCategoryByIdWithBooksHandler : IRequestHandler<GetCategoryByIdWithBooks, CategoryWithBooksDTO>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdWithBooksHandler(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryWithBooksDTO> Handle(GetCategoryByIdWithBooks request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.CategoryId,
                (Category c) => c.Books);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.CategoryId);
            }

            var paginatedBooks = category.Books
                .Skip((request.PaginationParams.Number - 1) * request.PaginationParams.Size)
                .Take(request.PaginationParams.Size)
                .OrderBy(b => EF.Property<object>(b, request.SortParams.SortBy))
                .ToList();
            category.Books = paginatedBooks;
            return _mapper.Map<CategoryWithBooksDTO>(category);
        }
    }
}