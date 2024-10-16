using System.Linq.Expressions;
using EBeauty.Application.Exceptions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.BusinessFunctions.Commands;
using EBeauty.Application.Tests.Common;
using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;

namespace EBeauty.Application.Tests.Logic.BusinessFunctions;

public class DeleteBusinessServicesCommandTests : BaseTest<DeleteBusinessServicesCommand.Handler>
{
    public DeleteBusinessServicesCommandTests() 
        : base((businessProvider, accountProvider, dbContext) => 
            new DeleteBusinessServicesCommand.Handler(businessProvider!, accountProvider, dbContext), Substitute.For<ICurrentBusinessProvider>())
    {
    }


    [Fact]
    public async Task Handle_ShouldThrowUnauthorizedException_WhenBusinessIdIsNull()
    {
        // Arrange
        _businessProviderMock.GetBusinessId().Returns((int?)null);

        var request = new DeleteBusinessServicesCommand.Request { ServiceId = 1 };

        // Act & Assert
        await Should.ThrowAsync<UnauthorizedException>(() => _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenServiceDoesNotExist()
    {
        // Arrange
        var businessId = 1;
        _businessProviderMock.GetBusinessId().Returns(businessId);

        // Utwórz zamockowany DbSet, który zwraca null dla FirstOrDefaultAsync
        var serviceDbSet = Substitute.For<DbSet<Service>>();
        serviceDbSet.FirstOrDefaultAsync(Arg.Any<Expression<Func<Service, bool>>>())
            .Returns(Task.FromResult<Service>(null)); // Symuluje brak wyniku

        _applicationDbContextMock.Services.Returns(serviceDbSet);

        var request = new DeleteBusinessServicesCommand.Request { ServiceId = 2 };

        // Act & Assert
        await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
    }

    
    

}