using StackExchange.Redis;

namespace Basket.Data;

public class BasketRepository(
    ILogger<BasketRepository> logger, 
    IConnectionMultiplexer redis) : IBasketRepository
{
    private readonly IDatabase _database = redis.GetDatabase();
    
    public Task<CustomerBasket> GetBasketAsync(string customerId)
    {
        logger.LogInformation("Getting basket");
        throw new NotImplementedException();
    }

    public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBasketAsync(string id)
    {
        throw new NotImplementedException();
    }
}
