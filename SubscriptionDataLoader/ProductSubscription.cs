using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace SubscriptionDataLoader;

[ExtendObjectType("Subscription")]
internal sealed class Subscription
{
    public async Task<ISourceStream<Product>> SubscribeToProductPriceChanged(
        int productId,
        [Service] ProductByIdDataLoader productDataLoader,
        [Service] ITopicEventReceiver receiver)
    {
        var product = await productDataLoader.LoadAsync(productId, CancellationToken.None) ??
            throw new InvalidOperationException($"Product with ID {productId} not found.");
        
        return await receiver.SubscribeAsync<Product>("ProductPriceChanged:" + product.Id);
    }

    [Subscribe(With = nameof(SubscribeToProductPriceChanged))]
    public Product ProductPriceChanged([EventMessage] Product product) => product;
}
