using System.Text.Json;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace SubscriptionDataLoader.Test;

public class SubscriptionTest
{
    private const string ProductPriceSubscription = """
      subscription ProductPriceChanged($productId: Int!) {
        productPriceChanged(productId: $productId) {
          id
        }
      }
      """;

    [Fact]
    public void Test()
    {
        var client = new GraphQLHttpClient(
            new GraphQLHttpClientOptions
            {
                WebSocketEndPoint = new Uri("ws://localhost:5062/graphql")
            },
            new SystemTextJsonSerializer(new JsonSerializerOptions (SystemTextJsonSerializer.DefaultJsonSerializerOptions))
        );
        
        var subscriptionStream = client.CreateSubscriptionStream<dynamic>(
            new GraphQLRequest(ProductPriceSubscription, new { productId = 1 }),
            e => throw e);

        var subscriptionReceivedData = new ManualResetEvent(false);
        subscriptionStream.Subscribe(priceChanged =>
        {
            if (priceChanged.Data is not null)
            {
                subscriptionReceivedData.Set();
            }
        });

        if (!subscriptionReceivedData.WaitOne(TimeSpan.FromSeconds(10)))
        {
            Assert.Fail("No subscription data received within the timeout period.");
        }
    }
}