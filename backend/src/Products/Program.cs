using ShopFusion.Products.Data;

var builder = WebApplication.CreateBuilder(args);

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

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
