using ShopFusion.Basket.Host.Data;

namespace ShopFusion.Basket.Host.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddRedis("redis");

        builder.Services.AddSingleton<IBasketRepository, BasketRepository>();

        /*builder.AddRabbitMqEventBus("eventbus")
            .AddSubscription<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>()
            .ConfigureJsonOptions(options => options.TypeInfoResolverChain.Add(IntegrationEventContext.Default));*/
    }
}
