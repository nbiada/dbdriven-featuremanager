namespace DbDrivenFM.App.Data;

public class ResultResponse
{
    public ResultResponse(bool success, string message = "")
    {
        Success = success;
        Message = message;
    }
    public bool Success { get; set; } = false;
    public string? Message { get; set; }
}