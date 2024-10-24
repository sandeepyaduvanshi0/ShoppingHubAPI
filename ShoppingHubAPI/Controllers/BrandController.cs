using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.DataAccess.Migrations;
using ShoppingHub.Models;
using ShoppingHub.Services;
using ShoppinHub.DataAccess.Repositery.IRepositery;
using System;
using System.Drawing; // For working with images
using System.Drawing.Imaging; // For specifying the image format (jpeg, png, etc.)
using System.IO; // For file and memory stream operations

namespace ShoppingHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAllBrand()
        {
            var allCategry = _unitOfWork.Brand.GetAll();
            return Ok(allCategry);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetBrand(int id)
        {
            var brand = _unitOfWork.Brand.Get(c => c.BrandId == id);

                       
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "brands");

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create the file name (you can generate a unique name or use the brand name)
            var fileName = $"Brand_logo.jpg"; // Adjust the extension based on the actual image format
            var filePath = Path.Combine(folderPath, fileName);

            // Convert the byte array to an image and save it as a file
            using (var ms = new MemoryStream(brand.LogoImage))
            {
                using (var image = Image.FromStream(ms))
                {
                    // Save the image to the file system
                    image.Save(filePath); // Use ImageFormat.Png if the image is in PNG format
                }
            }


            return Ok(brand);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> InsertBrand([FromForm] IFormFile imageFile,ShoppingHub.Models.Brand brnd)
        {
            if (brnd == null)
            {
                return BadRequest("Brand is null.");
            }
            // Validate brand name
            if (string.IsNullOrWhiteSpace(brnd.Name))
            {
                return BadRequest("Brand name is required.");
            }
            var result = ImageUploader.Upload(imageFile, brnd, nameof(brnd.LogoImage));

            if (result is BadRequestObjectResult badRequestResult)
            {
                return badRequestResult;
            }
            _unitOfWork.Brand.Add(brnd);
            _unitOfWork.Save();            
            return Ok(brnd);
        }

        //[HttpDelete]
        //[Route("delete/{id:int}")]
        //public IActionResult DeleteBrand(int id)
        //{
        //    var brand = _unitOfWork.Brand.Get(c => c.BrandId == id);

        //    if (brand == null)
        //    {
        //        return NotFound("Brand not found.");
        //    }
        //    _unitOfWork.Brand.Remove(brand);
        //    _unitOfWork.Save();
        //    return Ok("Brand deleted successfully.");
        //}


    }
}
