var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddSubscriptionType(s => s.Name("Subscription"))
    .AddQueryType(q => q.Name("Query"))
    .AddSubscriptionDataLoaderTypes();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();

app.Run();