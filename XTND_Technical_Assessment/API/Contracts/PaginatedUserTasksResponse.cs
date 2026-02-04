namespace XTND_Technical_Assessment.API.Contracts;

public sealed class PaginatedUserTasksResponse
{
    public int UserId { get; set; }
    public string UserDisplayName { get; set; } = "";
    public List<TaskSummary> Tasks { get; set; } = new();
    public int TotalCount { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public bool HasMore { get; set; }
}
