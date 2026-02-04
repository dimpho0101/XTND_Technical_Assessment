using System.ComponentModel.DataAnnotations;

namespace XTND_Technical_Assessment.API.Contracts;


public sealed class GetTaskByIdResponse
{

    [Display(Name = "Task ID")]
    public int Id { get; set; }


    [Display(Name = "Title")]
    public string Title { get; set; } = "";


    [Display(Name = "User ID")]
    public int TaskUserId { get; set; }


    [Display(Name = "User Display Name")]
    public string UserDisplayName { get; set; } = "";

    [Display(Name = "Status ID")]
    public int TaskStatusId { get; set; }

    [Display(Name = "Status Name")]
    public string StatusName { get; set; } = "";

    [Display(Name = "Created At")]
    public DateTime TaskCreatedAtUtc { get; set; }

    [Display(Name = "Updated At")]
    public DateTime TaskUpdatedAtUtc { get; set; }
}
