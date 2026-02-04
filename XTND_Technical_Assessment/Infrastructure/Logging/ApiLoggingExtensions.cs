namespace XTND_Technical_Assessment.Infrastructure.Logging;

public static class ApiLoggingExtensions
{

    public static void LogOperation(this ILogger logger, string operation, string entity, int id)
    {
        logger.LogInformation("{Operation}: {Entity} with ID {Id} created successfully", operation, entity, id);
    }


    public static void LogOperationError(this ILogger logger, string operation, string entity, Exception ex)
    {
        logger.LogError("{Operation} failed: {Entity}. {Message}", operation, entity, ex.Message);
    }


    public static void LogNotFound(this ILogger logger, string entity, int id)
    {
        logger.LogInformation("{Entity} with ID {Id} not found", entity, id);
    }


    public static void LogValidationError(this ILogger logger, string entity, string message)
    {
        logger.LogWarning("Validation error for {Entity}: {Message}", entity, message);
    }


    public static void LogConstraintViolation(this ILogger logger, string entity, string message)
    {
        logger.LogWarning("Constraint violation for {Entity}: {Message}", entity, message);
    }


    public static void LogQuery(this ILogger logger, string queryName)
    {
        logger.LogDebug("Query executed: {QueryName}.", queryName);
    }


    public static void LogQueryWithFilters(this ILogger logger, string queryName, Dictionary<string, string> filters, int recordCount)
    {
        var filterString = string.Join(", ", filters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        logger.LogInformation("Query executed: {QueryName} with filters [{FilterString}]. Retrieved {RecordCount} record(s)", 
            queryName, filterString, recordCount);
    }


    public static void LogQueryError(this ILogger logger, string queryName, Exception ex)
    {
        logger.LogError(ex, "Query error in {QueryName}: {Message}", queryName, ex.Message);
    }
}

