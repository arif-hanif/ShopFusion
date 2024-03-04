namespace ShopFusion.Shared.Errors;

public class DatabaseOperationException() 
    : Exception($"There was an error operating on the database, try again.");
