using AutoMapper;
using BookLibraryAPIDemo.Application.Commands.Authors;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Library.UnitTests.Commands.Authors;

public class CreateAuthorHandlerTests
{
    private readonly IBaseRepository<Author> _repository;
    private readonly IMapper _mapper;
    private readonly CreateAuthorHandler _handler;

    public CreateAuthorHandlerTests()
    {
        _repository = Substitute.For<IBaseRepository<Author>>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateAuthorHandler(_repository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldCreateAuthor_WhenRequestIsValid()
    {
        // Arrange
        var createRequest = new CreateAuthor {Author = new CreateAuthorDTO {Name = "New Author"}};
        var authorEntity = new Author {Id = "1", Name = "New Author"};
        var expectedDto = new AuthorDTO {Id = "1", Name = "New Author"};

        _mapper.Map<Author>(createRequest.Author).Returns(authorEntity);
        _repository.CreateAsync(authorEntity).Returns(Task.FromResult(authorEntity));
        _mapper.Map<AuthorDTO>(authorEntity).Returns(expectedDto);

        // Act
        var result = await _handler.Handle(createRequest, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedDto);
        await _repository.Received(1).CreateAsync(authorEntity);
    }
}