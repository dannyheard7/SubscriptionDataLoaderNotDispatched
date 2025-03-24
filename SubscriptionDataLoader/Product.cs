namespace SubscriptionDataLoader;

public record Product(int Id, string Name);

public static class Data
{
    public static IReadOnlyCollection<Product> Products =
    [
        new Product(1, "Product 1"),
        new Product(2, "Product 2"),
        new Product(3, "Product 3"),
        new Product(4, "Product 4"),
        new Product(5, "Product 5"),
    ];
}