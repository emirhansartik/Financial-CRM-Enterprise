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
    }
}