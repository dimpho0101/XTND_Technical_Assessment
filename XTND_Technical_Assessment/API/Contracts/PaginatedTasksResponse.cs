namespace XTND_Technical_Assessment.API.Contracts;

public sealed class PaginatedTasksResponse
{
    public List<TaskSummary> Tasks { get; set; } = new();
    public int TotalCount { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public bool HasMore { get; set; }
}

public sealed class TaskSummary
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int TaskUserId { get; set; }
    public string UserDisplayName { get; set; } = "";
    public int TaskStatusId { get; set; }
    public string StatusName { get; set; } = "";
    public DateTime TaskCreatedAtUtc { get; set; }
    public DateTime TaskUpdatedAtUtc { get; set; }
}
