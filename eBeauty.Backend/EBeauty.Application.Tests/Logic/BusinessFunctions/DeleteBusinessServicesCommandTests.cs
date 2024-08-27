using System.Linq.Expressions;
using EBeauty.Application.Interfaces;
using EBeauty.Application.Logic.BusinessFunctions.Commands;
using EBeauty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace EBeauty.Application.Tests.Logic.BusinessFunctions.Commands
{
    public class DeleteBusinessServicesCommandTests
    {
        private readonly ICurrentBusinessProvider _businessProvider;
        private readonly ICurrentAccountProvider _currentAccountProvider;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly DeleteBusinessServicesCommand.Handler _handler;

        public DeleteBusinessServicesCommandTests()
        {
            _businessProvider = Substitute.For<ICurrentBusinessProvider>();
            _currentAccountProvider = Substitute.For<ICurrentAccountProvider>();
            _applicationDbContext = Substitute.For<IApplicationDbContext>();

            _handler = new DeleteBusinessServicesCommand.Handler(
                _businessProvider, 
                _currentAccountProvider, 
                _applicationDbContext
            );
        }

        [Fact]
        public async Task Handle_Should_Delete_Service_When_Service_Exists()
        {
            // Arrange
            var businessId = 1;
            var serviceId = 1;
            var businessTypeId = 2;

            _businessProvider.GetBusinessId().Returns(businessId);

            var service = new Service { Id = serviceId, BusinessId = businessId, BusinessTypeId = businessTypeId, Name = "Test"};

            // Poprawienie konfiguracji mocka, aby używał właściwego predykatu
            _applicationDbContext.Services
                .FirstOrDefaultAsync(Arg.Is<Expression<Func<Service, bool>>>(x => x.Compile().Invoke(service)))
                .Returns(service);

            var request = new DeleteBusinessServicesCommand.Request { ServiceId = serviceId };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(businessId, result.BusinessId);
            Assert.Equal(businessTypeId, result.BusinessTypeId);

            // Sprawdzenie, czy Remove i SaveChangesAsync zostały wywołane raz.
            _applicationDbContext.Services.Received(1).Remove(service);
            await _applicationDbContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}