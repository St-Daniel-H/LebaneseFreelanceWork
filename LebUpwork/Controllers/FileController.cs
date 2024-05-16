using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LebUpwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("Image")]
        public IActionResult Get(string ImageName)
        {
            var imagePath = "../LebUpWork/Uploads/ProfilePicture/" + ImageName;
            if (System.IO.File.Exists(imagePath))
            {
                var imageStream = System.IO.File.OpenRead(imagePath);
                return File(imageStream, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("Pdf")]
        public IActionResult GetPdf(string pdfName)
        {
            var pdfPath = "../LebUpWork/Uploads/CV/" + pdfName;
            if (System.IO.File.Exists(pdfPath))
            {
                var pdfStream = System.IO.File.OpenRead(pdfPath);
                return File(pdfStream, "application/pdf");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
