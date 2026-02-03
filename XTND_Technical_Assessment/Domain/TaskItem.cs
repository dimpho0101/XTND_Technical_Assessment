namespace XTND_Technical_Assessment.Domain;

public class TaskItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    // who owns/created/assigned (you can rename later)
    public Guid TaskUserId { get; set; }
    public TaskUser? TaskUser { get; set; }

    // timestamps
    public DateTime TaskCreatedAtUtc { get; set; }
    public DateTime TaskUpdatedAtUtc { get; set; }
}
