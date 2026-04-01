using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.Memory;

namespace UnitTest;

public class MemoryGenericRepositoryTest
{
    private readonly IGenericRepositoryAsync<Person> _repo = new MemoryGenericRepository<Person>();

    [Fact]
    public async Task AddPersonTestAsync()
    {
        // Arrange
        var obj = new Person()
        {
            FirstName = "Vannessa"
        };
        // Act
        var expected = await _repo.AddAsync(obj);
        // Assert
        var actual = await _repo.GetByIdAsync(expected.Id);
        Assert.Equal(expected.Id, actual?.Id);
    }

    [Fact]
    public async Task FindAllAsyncTestAsync()
    {
        // Arrange
        var person1 = new Person() { FirstName = "Joey" };
        var person2 = new Person() { FirstName = "William" };
        await _repo.AddAsync(person1);
        await _repo.AddAsync(person2);
        // Act
        var result = await _repo.FindAllAsync();
        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 2);
    }

    [Fact]
    public async Task FindPagedAsyncTestAsync()
    {
        // Arrange
        var person1 = new Person() { FirstName = "Joey" };
        var person2 = new Person() { FirstName = "William" };
        await _repo.AddAsync(person1);
        await _repo.AddAsync(person2);
        // Act
        var result = await _repo.FindPagedAsync(1, 10);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.TotalCount >= 2);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Fact]
    public async Task RemoveByIdAsyncTestAsync()
    {
        // Arrange
        var person = new Person() { FirstName = "Mike" };
        await _repo.AddAsync(person);
        var id = person.Id;
        // Act
        await _repo.RemoveByIdAsync(id);
        // Assert
        var result = await _repo.GetByIdAsync(id);
        Assert.Null(result);
    }
}