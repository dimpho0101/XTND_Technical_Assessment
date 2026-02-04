namespace XTND_Technical_Assessment.Domain;

public class TaskItemStatus
{
    public int Id { get; set; }                 
    public string Name { get; set; } = "";
    public bool IsActive { get; set; } = true;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
