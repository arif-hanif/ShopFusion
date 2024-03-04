using ShopFusion.Products.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    //.AddUploadType()
    //.AddGlobalObjectIdentification()
    .AddMutationConventions()
    //.RegisterDbContext<ProductsDbContext>()
    .AddInstrumentation(o => o.RenameRootActivity = true);

var app = builder.Build();

app.MapGet("/", () => "Welcome to the products host!");
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
