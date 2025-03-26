using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Categorys
{
    public class DeleteCategory : IRequest<DeleteDTO>
    {
        public string CategoryId { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, DeleteDTO>
    {
        private readonly IBaseRepository<Category> _repository;

        public DeleteCategoryHandler(IBaseRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<DeleteDTO> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.CategoryId);
            if (category == null || category.IsDeleted)
            {
                throw new CategoryNotFoundException(request.CategoryId);
            }

            await _repository.SoftDeleteAsync(category);

            return new DeleteDTO()
            {
                Message = $"Category with id {request.CategoryId} was successfully deleted."
            };
        }
    }
}