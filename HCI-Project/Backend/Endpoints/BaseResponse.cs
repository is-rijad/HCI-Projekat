namespace Backend.Endpoints;

public class BaseResponse
{
    public int Status { get; set; } = 200;
    public string Message { get; set; }
}