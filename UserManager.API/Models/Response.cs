namespace UserManager.API.Models;

public class Response
{
    public int Status { get; set; } = StatusCodes.Status200OK;
    public string Message { get; set; } = "Success";
    public object Data { get; set; } = default!;
}
