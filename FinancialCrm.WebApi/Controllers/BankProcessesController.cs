using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankProcessesController : ControllerBase
    {
        // 1. TÜM BANKA HAREKETLERİNİ LİSTELE
        [HttpGet]
        public IActionResult GetBankProcessList()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var values = db.BankProcesses
                    .Select(x => new
                    {
                        x.BankProcessId,
                        x.Description,
                        x.ProcessDate,
                        x.ProcessType,
                        x.Amount,
                        x.BankId,
                        BankTitle = db.Banks.Where(b => b.BankId == x.BankId).Select(b => b.BankTitle).FirstOrDefault()
                    })
                    .ToList();

                return Ok(values);
            }
        }

        // 2. COMBOBOX İÇİN BANKALARI GETİR 
        [HttpGet("GetBanks")]
        public IActionResult GetBanksForComboBox()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var banks = db.Banks
                    .Select(x => new
                    {
                        x.BankId,
                        x.BankTitle
                    })
                    .ToList();

                return Ok(banks);
            }
        }

        // 3. YENİ BANKA HAREKETİ EKLE 
        [HttpPost]
        public IActionResult CreateBankProcess(BankProcessRequestDto dto)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var newProcess = new BankProcesses
                {
                    Description = dto.Description,
                    ProcessDate = dto.ProcessDate,
                    ProcessType = dto.ProcessType,
                    Amount = dto.Amount,
                    BankId = dto.BankId
                };

                db.BankProcesses.Add(newProcess);
                db.SaveChanges();
                return Ok("Banka hareketi başarıyla eklendi.");
            }
        }

        // 4. BANKA HAREKETİ GÜNCELLE 
        [HttpPut]
        public IActionResult UpdateBankProcess(BankProcessRequestDto dto)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var existingProcess = db.BankProcesses.Find(dto.BankProcessId);
                if (existingProcess == null)
                {
                    return NotFound("Güncellenecek banka hareketi bulunamadı.");
                }

                existingProcess.Description = dto.Description;
                existingProcess.ProcessDate = dto.ProcessDate;
                existingProcess.ProcessType = dto.ProcessType;
                existingProcess.Amount = dto.Amount;
                existingProcess.BankId = dto.BankId;

                db.SaveChanges();
                return Ok("Banka hareketi başarıyla güncellendi.");
            }
        }

        // 5. BANKA HAREKETİ SİLME
        [HttpDelete("{id}")]
        public IActionResult DeleteBankProcess(int id)
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var value = db.BankProcesses.Find(id);
                if (value == null)
                {
                    return NotFound("Silinecek banka hareketi bulunamadı.");
                }

                db.BankProcesses.Remove(value);
                db.SaveChanges();
                return Ok("Banka hareketi başarıyla silindi.");
            }
        }
    }

    // Ekleme ve Güncelleme işlemlerinde Foreign Key hatasını önleyen DTO Kalıbıdır.
    public class BankProcessRequestDto
    {
        public int BankProcessId { get; set; }
        public string Description { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string ProcessType { get; set; }
        public decimal? Amount { get; set; }
        public int? BankId { get; set; }
    }
}