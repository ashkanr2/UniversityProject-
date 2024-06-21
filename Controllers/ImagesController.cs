using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Interfaces;

namespace UniversityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageUrl(Guid id)
        {
            var imageUrl = await _imageService.GetImageUrlByIdAsync(id);
            if (imageUrl == null)
            {
                return NotFound();
            }
            return Ok(imageUrl);
            }
    }

}
