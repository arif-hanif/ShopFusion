var builder = WebApplication.CreateBuilder(args);

//builder.AddRedis("");
//builder.Services.AddSingleton<IBasketRepository, BasketRepository>();

var app = builder.Build();

app.MapGet("/", () => "Welcome to the basket host!");

app.Run();
