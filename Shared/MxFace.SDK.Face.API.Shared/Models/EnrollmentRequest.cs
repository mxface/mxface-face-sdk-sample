namespace MxFace.SDK.Face.API.Shared.Models;

public class EnrollmentRequest
{
    public string ImageData { get; set; }
    public string? ExternalId { get; set; } = Guid.NewGuid().ToString();
    public string? Group { get; set; }
}
