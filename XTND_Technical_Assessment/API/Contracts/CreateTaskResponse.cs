namespace XTND_Technical_Assessment.Api.Contracts;

public sealed class CreateTaskResponse
{
    public Guid Id { get; set; }
    public Guid TaskUserId { get; set; }
    public string Title { get; set; } = "";
    public DateTime TaskCreatedAtUtc { get; set; }
    public DateTime TaskUpdatedAtUtc { get; set; }
}
