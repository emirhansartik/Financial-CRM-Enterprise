using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // 1. TÜM KULLANICILARI LİSTELE (GET)
        [HttpGet]
        public IActionResult GetUserList()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var values = db.Users
                    .Select(x => new
                    {
                        x.UserId,
                        x.Username,
                        x.Password
                    })
                    .ToList();

                return Ok(values);
            }
        }

        // 2. YENİ KULLANICI EKLE (POST)
        [HttpPost]
        public IActionResult CreateUser(Users user)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Ok("Kullanıcı başarıyla eklendi.");
            }
        }

        // 3. KULLANICI BİLGİLERİNİ / ŞİFRESİNİ GÜNCELLE (PUT)
        [HttpPut]
        public IActionResult UpdateUser(Users user)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var existingUser = db.Users.Find(user.UserId);
                if (existingUser == null)
                {
                    return NotFound("Güncellenecek kullanıcı bulunamadı.");
                }

                existingUser.Username = user.Username;
                existingUser.Password = user.Password;

                db.SaveChanges();
                return Ok("Kullanıcı bilgileri başarıyla güncellendi.");
            }
        }

        // 4. KULLANICI SİL (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var value = db.Users.Find(id);
                if (value == null)
                {
                    return NotFound("Silinecek kullanıcı bulunamadı.");
                }

                db.Users.Remove(value);
                db.SaveChanges();
                return Ok("Kullanıcı başarıyla silindi.");
            }
        }
    }
}