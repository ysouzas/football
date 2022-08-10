namespace F.Core.Communication;

public class ResponseResult
{
    public ResponseResult()
    {
        Errors = new ResponseErrorMessages();
    }

    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public ResponseErrorMessages Errors { get; set; }
}