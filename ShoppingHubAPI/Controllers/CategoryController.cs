using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.Models;
using ShoppinHub.DataAccess.Repositery.IRepositery;

namespace ShoppingHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var allCategry = _unitOfWork.Category.GetAll();
            return Ok(allCategry);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetCategory(int id)
        {
            var category = _unitOfWork.Category.Get(c => c.Id == id);
            return Ok(category);
        }
        [HttpPost]
        //[Route("create")]
        public IActionResult InsertCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category is null.");
            }

            if (category.Id == null || category.Id == 0)
            {               
                _unitOfWork.Category.Add(category);
            }
            else
            { 
                _unitOfWork.Category.Update(category);
            }
            _unitOfWork.Save();

            return Ok(category);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound("Category not found.");
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            return Ok("Category deleted successfully.");
        }


    }
}
