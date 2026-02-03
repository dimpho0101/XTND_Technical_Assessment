namespace XTND_Technical_Assessment.Api.Contracts;

public sealed class CreateTaskRequest
{
    public Guid TaskUserId { get; set; }
    public string Title { get; set; } = "";
}
