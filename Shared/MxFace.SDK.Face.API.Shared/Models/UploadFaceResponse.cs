namespace MxFace.SDK.Face.API.Shared.Models;

public class UploadFaceResponse : BaseResponse
{
    public List<Analytics> Faces { get; set; }
}

public class Analytics : FaceDetect
{
    public FaceAnalytics FaceAnalytics { get; set; }
}
public class FaceDetect
{
    public float Quality { get; set; }
    public List<Points> Points { get; set; }
    public FaceRectangle FaceRectangle { get; set; }
    public string croppedFace { get; set; }
}

public class FaceRectangle
{
    public int x { get; set; }
    public int y { get; set; }
    public int height { get; set; }
    public int width { get; set; }
}
public class FaceAnalytics
{
    public string Age { get; set; }
    public string Gender { get; set; }
    public string Emotion { get; set; }
    public float? Confidence { get; set; }
}
public class Points
{
    public float X { get; set; }
    public float Y { get; set; }
}
