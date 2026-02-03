namespace XTND_Technical_Assessment.Domain;

public class TaskUser
{
    public Guid Id { get; set; }

    // keep it minimal for now
    public string DisplayName { get; set; } = "";

    // navigation
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
