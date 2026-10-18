using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login(Users user)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                // Veritabanındaki Users tablosundan eşleşme arıyoruz
       
                var loginUser = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);

                if (loginUser != null)
                {
                    return Ok("Giriş Başarılı");
                }

                // Eşleşme yoksa 401 Unauthorized (Yetkisiz) kodu döner

                return Unauthorized("Kullanıcı adı veya şifre hatalı");
            }
        }
    }
}