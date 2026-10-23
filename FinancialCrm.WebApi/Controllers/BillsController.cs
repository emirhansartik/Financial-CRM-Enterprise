using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        // 1. TÜM FATURALARI / ÖDEMELERİ LİSTELE (GET)
        [HttpGet]
        public IActionResult GetBillList()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var values = db.Bills
                    .Select(x => new
                    {
                        x.BillId,
                        x.BillTitle,
                        x.BillAmount,
                        x.BillPeriod
                    })
                    .ToList();

                return Ok(values);
            }
        }

        // 2. YENİ FATURA / ÖDEME EKLE (POST)
        [HttpPost]
        public IActionResult CreateBill(Bills bill)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return Ok("Ödeme başarıyla eklendi.");
            }
        }

        // 3. FATURA / ÖDEME GÜNCELLE (PUT)
        [HttpPut]
        public IActionResult UpdateBill(Bills bill)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var existingBill = db.Bills.Find(bill.BillId);
                if (existingBill == null)
                {
                    return NotFound("Güncellenecek ödeme bulunamadı.");
                }

                existingBill.BillTitle = bill.BillTitle;
                existingBill.BillAmount = bill.BillAmount;
                existingBill.BillPeriod = bill.BillPeriod;

                db.SaveChanges();
                return Ok("Ödeme başarıyla güncellendi.");
            }
        }

        // 4. FATURA / ÖDEME SİL (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var value = db.Bills.Find(id);
                if (value == null)
                {
                    return NotFound("Silinecek ödeme bulunamadı.");
                }

                db.Bills.Remove(value);
                db.SaveChanges();
                return Ok("Ödeme başarıyla silindi.");
            }
        }
    }
}