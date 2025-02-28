namespace MxFace.SDK.Face.API.Shared.Models;

public class SearchResponse : BaseResponse
{
    public string ExternalId { get; set; }
    public int MatchingScore { get; set; }
}
