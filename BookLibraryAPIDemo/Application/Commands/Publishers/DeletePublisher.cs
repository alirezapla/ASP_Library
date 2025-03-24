using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Publishers
{
    public class DeletePublisher : IRequest<DeleteDTO>
    {
        public string publisherId { get; set; }


        public class DeletePublisherHandler : IRequestHandler<DeletePublisher, DeleteDTO>
        {
            private readonly IBaseRepository<Publisher> _repository;

            public DeletePublisherHandler(IBaseRepository<Publisher> repository)
            {
                _repository = repository;
            }

            public async Task<DeleteDTO> Handle(DeletePublisher request, CancellationToken cancellationToken)
            {
                var publisher = await _repository.GetByIdAsync(request.publisherId);
                if (publisher == null)
                {
                    throw new PublisherNotFoundException(request.publisherId);
                }

                await _repository.SoftDeleteAsync(publisher);

                return new DeleteDTO()
                {
                    Message = $"Publisher with Id {request.publisherId} has been deleted"
                };
            }
        }
    }
}