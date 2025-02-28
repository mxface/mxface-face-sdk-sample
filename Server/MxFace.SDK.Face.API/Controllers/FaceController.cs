using Microsoft.AspNetCore.Mvc;
using MxFace.SDK.Face.API.Shared.Models;
using MxFace.SDK.Face.Interfaces;

namespace MxFace.SDK.Face.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaceController : ControllerBase
    {
        private readonly IFaceProcessor _faceService;

        public FaceController(IFaceProcessor faceService)
        {
            _faceService = faceService;
        }

        [HttpPost("upload", Name = "uploadFace")]
        public async Task<IActionResult> Upload([FromBody] UploadFaceRequest clientRequest)
        {
            Face.Models.UploadFaceRequest request = new Face.Models.UploadFaceRequest()
            {
                MinimumDistanceBetweenEyeThreshold = clientRequest.MinimumDistanceBetweenEyeThreshold,
                encoded_image = clientRequest.encoded_image,
                attributes = clientRequest.attributes
            };
            var result = await _faceService.Upload(request);
            return Ok(result);
        }

        [HttpPost("enroll", Name = "enrollFace")]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentRequest request)
        {
            var result = await _faceService.Enroll(
                request.ImageData,
                request.ExternalId,
                request.Group);
            return Ok(result);
        }
        
        [HttpPost("search", Name = "searchFace")]
        public async Task<IActionResult> Search([FromBody] SearchRequest request)
        {
            var result = await _faceService.Search(
                request.FaceData,
                request.Group);
            return Ok(result);
        }
        
        [HttpPost("verify", Name = "verifyFace")]
        public async Task<IActionResult> Verify([FromBody] MatchRequest request)
        {
            var result = await _faceService.Verify(
                request.FaceData1,
                request.FaceData2);
            return Ok(result);
        }
    }
}
