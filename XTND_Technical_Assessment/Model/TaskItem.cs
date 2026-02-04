namespace XTND_Technical_Assessment.Domain;

using System.ComponentModel.DataAnnotations.Schema;

public class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public int TaskUserId { get; set; }

    [ForeignKey(nameof(TaskUserId))]
    public TaskUser? TaskUser { get; set; }

    public int TaskStatusId { get; set; }
    public TaskItemStatus? TaskStatus { get; set; }

    public DateTime TaskCreatedAtUtc { get; set; }
    public DateTime TaskUpdatedAtUtc { get; set; }
}
