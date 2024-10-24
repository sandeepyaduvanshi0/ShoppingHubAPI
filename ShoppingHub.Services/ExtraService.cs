using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.Services
{
   public class ExtraService
    {        

    }

    public static class ImageUploader
    {
        public static IActionResult Upload(IFormFile imageFile, object entity, string propertyName)
        {
            // Supported image file types
            var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp" };

            // Check if the uploaded file is of a valid image type
            if (imageFile == null || !supportedTypes.Contains(imageFile.ContentType))
            {
                return new BadRequestObjectResult("Only image files (JPEG, PNG, GIF, BMP) are allowed.");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copy the image file into the memory stream
                    imageFile.CopyTo(memoryStream);

                    // Convert the image to a byte array
                    byte[] imageBytes = memoryStream.ToArray();
                    // Use reflection to set the byte array property dynamically
                    var entityProperty = entity.GetType().GetProperty(propertyName);
                  //  var entityContantType = entity.GetType().GetProperty("ContentType");
                    if (entityProperty != null && entityProperty.PropertyType == typeof(byte[]))
                    {
                        entityProperty.SetValue(entity, imageBytes);
                        //entityContantType.SetValue(entity, imageFile.ContentType);
                    }
                    else
                    {
                        return new BadRequestObjectResult("Invalid property name or type.");
                    }
                }

                return new OkObjectResult("Image uploaded successfully.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Internal server error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }

}
