namespace ShopFusion.Shared.Errors;

public class InvalidNameException(string name) 
    : Exception($"The {name} name can not be null, empty, or whitespace")
{
    public string Name { get; } = name;
}

