namespace SubscriptionDataLoader;

[ExtendObjectType("Query")]
public class ProductQuery
{
    public IEnumerable<Product> Products => Data.Products;
    
    public Task<Product?> GetProductById(int id, [Service] ProductByIdDataLoader productDataLoader)
    {
        return productDataLoader.LoadAsync(id, CancellationToken.None);
    }
}