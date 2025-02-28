namespace MxFace.SDK.Face.API.Shared.Models;

public class UploadFaceRequest
{
    public int? MinimumDistanceBetweenEyeThreshold { get; set; }
    public string encoded_image { get; set; }

    public bool attributes { get; set; }

    public bool? GetCroppedFace { get; set; }

    public string? Algorithm { get; set; }
}
