using BookLibraryAPIDemo.Application.Commands.Authors;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Library.UnitTests.Commands.Authors;

public class DeleteBookHandlerTests
{
    private readonly IBaseRepository<Author> _repository;
    private readonly DeleteAuthorHandler _handler;

    public DeleteBookHandlerTests()
    {
        _repository = Substitute.For<IBaseRepository<Author>>();
        _handler = new DeleteAuthorHandler(_repository);
    }

    [Fact]
    public async Task Handle_WhenAuthorNotFound_ShouldThrowAuthorNotFoundException()
    {
        // Arrange
        var authorId = "non-existent-id";
        var request = new DeleteAuthor { AuthorId = authorId };

        _repository.GetByIdAsync(authorId).Returns((Author)null);

        // Act
        
        
        // Assert

        await _repository.Received(1).GetByIdAsync(authorId);
    }

    [Fact]
    public async Task Handle_WhenAuthorIsAlreadyDeleted_ShouldThrowAuthorNotFoundException()
    {
        // Arrange
        var authorId = "deleted-author-id";
        var request = new DeleteAuthor { AuthorId = authorId };

        var deletedAuthor = new Author
        {
            Id = authorId,
            IsDeleted = true
        };

        _repository.GetByIdAsync(authorId).Returns(deletedAuthor);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<AuthorNotFoundException>()
            .WithMessage($"Author with id {authorId} not found");
        await _repository.Received(1).GetByIdAsync(authorId);
    }

    [Fact]
    public async Task Handle_WhenAuthorExistsAndNotDeleted_ShouldSoftDeleteAndReturnDeleteDTO()
    {
        // Arrange
        var authorId = "valid-author-id";
        var request = new DeleteAuthor { AuthorId = authorId };

        var author = new Author
        {
            Id = authorId,
            IsDeleted = false // Author is not deleted
        };

        _repository.GetByIdAsync(authorId).Returns(author);

        await _repository.SoftDeleteAsync(author);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be($"Author with id {authorId} was successfully deleted.");
    }
}