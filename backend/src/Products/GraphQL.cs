namespace ShopFusion.Products;

public static class GraphQL
{
    [Query]
    public static string GetHello() => "World";
}
