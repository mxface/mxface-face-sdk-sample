namespace MxFace.SDK.Face.API.Shared.Models;

public class BaseResponse
{
    public int? ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public int? Code { get; set; }
    public string Error { get; set; }
    public BaseResponse()
    {

    }
    public BaseResponse(int code, string message)
    {
        ErrorCode = code;
        ErrorMessage = message;
        Code = code;
        Error = message;
    }
}
