namespace BookLibraryAPIDemo.Infrastructure.Interfaces;

public interface IEntity
{
    string Id { get; set; }
    bool IsDeleted { get; set; }
}