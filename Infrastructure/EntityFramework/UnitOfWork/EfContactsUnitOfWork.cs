using AppCore.Interfaces;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.UnitOfWork;

public class EfContactsUnitOfWork(
    IPersonRepository personRepository,
    // Rest repositories
    ContactsDbContext context
): IContactUnitOfWork
{
    public ValueTask DisposeAsync() => context.DisposeAsync();
	
    public IPersonRepository Persons => personRepository;

    public ICompanyRepository Companies { get; }
    public IOrganizationRepository Organizations { get; }

    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }
	
    public Task BeginTransactionAsync()
    {
        return context.Database.BeginTransactionAsync();
    }
	
    public Task CommitTransactionAsync()
    {
        return context.Database.CommitTransactionAsync();
    }
	
    public Task RollbackTransactionAsync()
    {
        return context.Database.RollbackTransactionAsync();
    }
}