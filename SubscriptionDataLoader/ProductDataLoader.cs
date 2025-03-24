namespace SubscriptionDataLoader;

internal static class ProductDataLoader
{
    [DataLoader]
    public static Task<Dictionary<int, Product>> GetProductByIdAsync(
        IReadOnlyList<int> productIds,
        CancellationToken cancellationToken)
        => Task.FromResult(Data.Products
            .Where(t => productIds.Contains(t.Id))
            .ToDictionary(t => t.Id));
}