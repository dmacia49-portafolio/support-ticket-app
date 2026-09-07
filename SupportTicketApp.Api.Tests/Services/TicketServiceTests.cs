using Microsoft.EntityFrameworkCore;
using SupportTicketApp.Api.Data;
using SupportTicketApp.Api.Services;

namespace SupportTicketApp.Api.Tests.Services;

public class TicketServiceTests
{
    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetAllAsync_WhenNoTicketsExist_ReturnsEmptyList()
    {
        await using ApplicationDbContext context = CreateContext();

        var service = new TicketService(context);

        var result = await service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTicketDoesNotExist_ReturnsNull()
    {
        await using ApplicationDbContext context = CreateContext();

        var service = new TicketService(context);

        var result = await service.GetByIdAsync(9999);

        Assert.Null(result);
    }
}
