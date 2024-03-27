using Microsoft.Extensions.Hosting;

namespace Aspire.Hosting;

public static class Extensions
{
    private static class PathNormalizer
    {
        public static string NormalizePathForCurrentPlatform(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            // Fix slashes
            path = path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

            return Path.GetFullPath(path);
        }
    }
    
    public static IResourceBuilder<NodeAppResource> AddPnpmApp(
        this IDistributedApplicationBuilder builder, 
        string name, 
        string workingDirectory, 
        string scriptName = "start", 
        string[]? args = null)
    {

        string[] allArgs = args is { Length: > 0 }
            ? ["run", scriptName, "--", .. args]
            : ["run", scriptName];

        workingDirectory = PathNormalizer.NormalizePathForCurrentPlatform(Path.Combine(builder.AppHostDirectory, workingDirectory));
        var resource = new NodeAppResource(name, "pnpm", workingDirectory, allArgs);

        return builder.AddResource(resource)
            .WithNodeDefaults();
    }
    
    private static IResourceBuilder<NodeAppResource> WithNodeDefaults(this IResourceBuilder<NodeAppResource> builder) =>
        builder.WithOtlpExporter()
            .WithEnvironment("NODE_ENV", builder.ApplicationBuilder.Environment.IsDevelopment() ? "development" : "production");
}

