using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Categories
{
    public class GetCategoryByIdWithBooks : IRequest<CategoryWithBooksDTO>
    {
        public string CategoryId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
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

            var paginatedBooks = category.Books.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize)
                .ToList();
            category.Books = paginatedBooks;
            return _mapper.Map<CategoryWithBooksDTO>(category);
        }
    }
}