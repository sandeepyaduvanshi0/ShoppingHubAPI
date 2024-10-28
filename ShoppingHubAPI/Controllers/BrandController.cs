using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.DataAccess.Migrations;
using ShoppingHub.Models;
using ShoppingHub.Services;
using ShoppinHub.DataAccess.Repositery;
using ShoppinHub.DataAccess.Repositery.IRepositery;
using System;
using System.Drawing; // For working with images
using System.Drawing.Drawing2D;
using System.Drawing.Imaging; // For specifying the image format (jpeg, png, etc.)
using System.IO; // For file and memory stream operations

namespace ShoppingHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        public BrandController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            this._unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
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
            if (brand == null)
            {
                return BadRequest("Brand Not Found...!");
            }
            return Ok(brand);
        }
        [HttpPost]
        //[Route("Upsert")]
        public IActionResult Upsert(IFormFile files, Brand brnd)
        {
            string msg = "";
            if (brnd == null || string.IsNullOrWhiteSpace(brnd.Name))
            {
                return BadRequest("Brand is null or name is required.");
            }
            // Images File Paths
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
            if (files != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                // string brndpath =  @"images\Brand";

                if (!string.IsNullOrEmpty(brnd.LogoImage))
                {
                    var url = brnd.LogoImage;
                    // re- assign path without localApipath 
                    brnd.LogoImage = Path.GetFileName(new Uri(url).LocalPath);
                    var oldImagePath = Path.Combine(localFilePath, brnd.LogoImage);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(localFilePath, fileName), FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
                var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/images/{fileName}";
                brnd.LogoImage = urlFilePath;
                // brnd.LogoImage = @"\images\brands\" + fileName;
            }
            if (brnd.BrandId != null && brnd.BrandId != 0)
            {
                var cretaetd = _unitOfWork.Brand.Get(c => c.BrandId == brnd.BrandId);
                if (cretaetd != null)
                {
                    brnd.CreatedAt = cretaetd.CreatedAt;
                }
                brnd.UpdatedAt = DateTime.Now;
                _unitOfWork.Brand.Update(brnd);
                msg = "Brand Update Successfuly";
            }
            else
            {
                _unitOfWork.Brand.Add(brnd);
                msg = "One More Brand Added successfully";
            }
            _unitOfWork.Save();
            return Ok(msg);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteBrand(int id)
        {
            var brand = _unitOfWork.Brand.Get(c => c.BrandId == id);
            if (brand == null)
            {
                return BadRequest("Brand Not Found...!");
            }
            if (!string.IsNullOrEmpty(brand.LogoImage))
            {
                var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                var url = brand.LogoImage;
                // re- assign path without localApipath 
                brand.LogoImage = Path.GetFileName(new Uri(url).LocalPath);
                var oldImagePath = Path.Combine(localFilePath, brand.LogoImage);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Brand.Remove(brand);
            _unitOfWork.Save();
            return Ok("Brand is Deleted...!");
        }
    }
}
