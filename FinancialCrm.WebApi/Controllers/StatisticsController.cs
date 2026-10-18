using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDashboardStatistics()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var totalBalance = db.Banks.Sum(x => x.BankBalance);
                var lastBankProcessAmount = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Select(x => x.Amount).FirstOrDefault();

                // Orta kart için faturaların toplam tutarını alıyoruz
                var totalBillAmount = db.Bills.Sum(x => x.BillAmount);

                var statistics = new
                {
                    TotalBalance = totalBalance,
                    LastBankProcessAmount = lastBankProcessAmount,
                    TotalBillAmount = totalBillAmount
                };
                return Ok(statistics);
            }
        }

        // 1. Grafik İçin: Banka Bakiyeleri
        [HttpGet("GetBankData")]
        public IActionResult GetBankData()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var data = db.Banks.Select(x => new { x.BankTitle, x.BankBalance }).ToList();
                return Ok(data);
            }
        }

        // 2. Grafik İçin: Fatura Tutarları
        [HttpGet("GetBillData")]
        public IActionResult GetBillData()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                var data = db.Bills.Select(x => new { x.BillTitle, x.BillAmount }).ToList();
                return Ok(data);
            }
        }
    }
}