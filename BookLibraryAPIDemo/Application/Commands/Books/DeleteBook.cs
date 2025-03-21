using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Books
{
    public class DeleteBook : IRequest<string>
    {
        public string BookId { get; set; }


        public class DeleteBookHandler : IRequestHandler<DeleteBook, string>
        {

            private readonly IBaseRepository<Book> _repository;

            public DeleteBookHandler(IBaseRepository<Book> repository)
            {
                _repository = repository;
            }

            public async Task<string> Handle(DeleteBook request, CancellationToken cancellationToken)
            {
                var book = await _repository.GetByIdAsync(request.BookId);
                if (book == null) { throw new BookNotFoundException(request.BookId); }
                await _repository.DeleteAsync(book);

                return $"Book with Id {request.BookId} has been deleted";

            }
        }
    }



}