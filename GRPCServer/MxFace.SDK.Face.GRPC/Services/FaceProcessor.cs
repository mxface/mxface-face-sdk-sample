using Grpc.Core;
using MxFace.SDK.Face.Grpc;
using MxFace.SDK.Face.Interfaces;
using static MxFace.SDK.Face.Grpc.FaceService;

namespace MxFace.SDK.Face.GRPC.Services;

public class FaceProcessor : FaceServiceBase
{
    private readonly IFaceProcessor _faceService;

    public FaceProcessor(IFaceProcessor faceService)
    {
        _faceService = faceService;
    }

    public override async Task<UploadFaceResponse> UploadFace(UploadFaceRequest request, ServerCallContext context)
    {
        Face.Models.UploadFaceRequest uploadReq = new Face.Models.UploadFaceRequest
        {
            MinimumDistanceBetweenEyeThreshold = request.MinimumDistanceBetweenEyeThreshold,
            encoded_image = request.EncodedImage,
            attributes = request.Attributes
        };

        try
        {
            var upload = await _faceService.Upload(uploadReq);

            if (upload != null)
            {
                var response = new UploadFaceResponse();

                // Add each face to the Faces collection
                foreach (var face in upload.Faces)
                {
                    response.Faces.Add(new Analytics
                    {
                        FaceAnalytics = new FaceAnalytics
                        {
                            Age = face.FaceAnalytics.Age,
                            Gender = face.FaceAnalytics.Gender,
                            Emotion = face.FaceAnalytics.Emotion,
                            Confidence = (float)(face.FaceAnalytics.Confidence ?? 0.0)
                        },
                        Quality = face.Quality,
                        FaceRectangle = new FaceRectangle
                        {
                            X = face.FaceRectangle.x,
                            Y = face.FaceRectangle.y,
                            Height = face.FaceRectangle.height,
                            Width = face.FaceRectangle.width
                        }
                    });
                }

                return response;
            }

            return new UploadFaceResponse();
        }
        catch(Exception ex)
        {
            return new UploadFaceResponse
            {
                ErrorMessage = ex.Message
            };
        }
    }

    public override async Task<EnrollmentResponse> EnrollFace(EnrollmentRequest request, ServerCallContext context)
    {
        try
        {
            var enroll = await _faceService.Enroll(
            request.ImageData,
            request.ExternalId,
            request.Group);

        
            if (enroll != null)
            {
                return new EnrollmentResponse
                {
                    Code = (int)enroll.Code,
                    Message = enroll.Message
                };
            }

            return new EnrollmentResponse();
        }
        catch (Exception ex)
        {
            return new EnrollmentResponse
            {
                Message = ex.Message
            };
        }
    }

    public override async Task<SearchResponse> SearchFace(SearchRequest request, ServerCallContext context)
    {
        try
        {
            var search = await _faceService.Search(
            request.FaceData,
            request.Group);

        
            if (search != null)
            {
                return new SearchResponse
                {
                    MatchingScore = search.FirstOrDefault().MatchingScore
                };
            }

            return new SearchResponse();
        }
        catch (Exception ex)
        {
            return new SearchResponse
            {
                ErrorMessage = ex.Message
            };
        }
    }

    public override async Task<MatchResponse> VerifyFace(MatchRequest request, ServerCallContext context)
    {

        try
        {
            var verify = await _faceService.Verify(
            request.FaceData1,
            request.FaceData2);

            if (verify != null)
            {
                return new MatchResponse
                {
                    Score = verify.Score
                };
            }

            return new MatchResponse();
        }
        catch (Exception ex)
        {
            return new MatchResponse
            {
                ErrorMessage = ex.Message
            };
        }
        
    }

}

