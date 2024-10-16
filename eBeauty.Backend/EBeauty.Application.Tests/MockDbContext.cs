using EBeauty.Application.Interfaces;
using EBeauty.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace EBeauty.Application.Tests;

public class MockDbContext<T>
    where T : class
{
    public static MainDbContext Create() => Create(new List<T>());

    public static MainDbContext Create(List<T> entities)
    {
        var queryable = entities.AsQueryable();
        var mockSet = Substitute.For<DbSet<T>, IQueryable<T>>();

        // Query the set
        ((IQueryable<T>)mockSet).Provider.Returns(queryable.Provider);
        ((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
        ((IQueryable<T>)mockSet).ElementType.Returns(queryable.ElementType);
        ((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());

        // Modify the set
        mockSet.When(set => set.Add(Arg.Any<T>())).Do(info => entities.Add(info.Arg<T>()));
        mockSet.When(set => set.Remove(Arg.Any<T>())).Do(info => entities.Remove(info.Arg<T>()));

        var dbContext = Substitute.For<MainDbContext>();
        dbContext.Set<T>().Returns(mockSet);
        
        return dbContext;
    }
}