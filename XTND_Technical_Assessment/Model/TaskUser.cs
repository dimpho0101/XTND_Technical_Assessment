namespace XTND_Technical_Assessment.Domain;

public class TaskUser
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = "";

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
