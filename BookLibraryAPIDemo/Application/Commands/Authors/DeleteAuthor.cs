using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Authors
{
    public class DeleteAuthor : IRequest<DeleteDTO>
    {
        public string AuthorId { get; set; }
    }

    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, DeleteDTO>
    {
        private readonly IBaseRepository<Author> _repository;

        public DeleteAuthorHandler(IBaseRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<DeleteDTO> Handle(DeleteAuthor request, CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdAsync(request.AuthorId);
            if (author == null || author.IsDeleted)
            {
                throw new AuthorNotFoundException(request.AuthorId);
            }

            await _repository.SoftDeleteAsync(author);

            return new DeleteDTO()
            {
                Message = $"Author with id {request.AuthorId} was successfully deleted."
            };
        }
    }
}