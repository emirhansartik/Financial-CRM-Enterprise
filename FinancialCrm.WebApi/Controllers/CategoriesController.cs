using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        [HttpGet]
        public IActionResult GetCategories()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var values = db.Categories.ToList();
                return Ok(values);
            }
        }

        [HttpPost]
        public IActionResult CreateCategory(FinancialCrm.DataAccess.Categories category)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return Ok("Kategori başarıyla eklendi");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var value = db.Categories.Find(id);
                db.Categories.Remove(value);
                db.SaveChanges();
                return Ok("Kategori başarıyla silindi");
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(FinancialCrm.DataAccess.Categories category)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var value = db.Categories.Find(category.CategoryId);
                value.CategoryName = category.CategoryName;
                db.SaveChanges();
                return Ok("Kategori başarıyla güncellendi");
            }
        }
    }
}