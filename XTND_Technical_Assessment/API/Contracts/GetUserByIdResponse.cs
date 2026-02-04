namespace XTND_Technical_Assessment.API.Contracts;

public sealed class GetUserByIdResponse
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = "";
}
