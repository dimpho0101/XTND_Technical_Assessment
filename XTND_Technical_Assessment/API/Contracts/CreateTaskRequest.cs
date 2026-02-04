namespace XTND_Technical_Assessment.Api.Contracts;

public sealed class CreateTaskRequest
{
    public int TaskUserId { get; set; }
    public string Title { get; set; } = "";
}
