using AutoMapper;
using BookLibraryAPIDemo.Application.Commands.Authors;
using BookLibraryAPIDemo.Application.DTO.Author;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Library.UnitTests.Commands.Authors;

public class UpdateAuthorHandlerTests
{
    private readonly IBaseRepository<Author> _repository;
    private readonly IMapper _mapper;
    private readonly UpdateAuthorHandler _handler;

    public UpdateAuthorHandlerTests()
    {
        _repository = Substitute.For<IBaseRepository<Author>>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateAuthorHandler(_repository, _mapper);
    }


    [Fact]
    public async Task UpdateAuthor_ShouldThrowKeyNotFoundException_WhenAuthorNotFound()
    {
        // Arrange
        const string authorId = "non-existent-id";
        var request = new UpdateAuthor
        {
            Id = authorId,
            Author = new UpdateAuthorDTO
            {
                Bio = "Updated Bio"
            }
        };
        _repository.GetByIdAsync(request.Id)!.Returns((Author) null);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Author with id {authorId} not found");
    }

    [Fact]
    public async Task UpdateAuthor_ShouldUpdateAuthor_WhenAuthorExists()
    {
        // Arrange
        var request = new UpdateAuthor {Id = "1", Author = new UpdateAuthorDTO {Bio = "New Bio Author"}};
        var existingAuthor = new Author {Id = "1", Name = "Old Author"};
        var updatedAuthor = new Author {Id = "1", Name = "Updated Author"};
        var expectedDto = new AuthorDTO {Name = "Updated Author"};

        _repository.GetByIdAsync(request.Id).Returns(existingAuthor);
        _mapper.Map(request.Author, existingAuthor).Returns(updatedAuthor);
        _repository.UpdateAsync(existingAuthor).Returns(Task.FromResult(existingAuthor));
        _mapper.Map<AuthorDTO>(existingAuthor).Returns(expectedDto);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);
        await _repository.Received(1).UpdateAsync(existingAuthor);
    }
}