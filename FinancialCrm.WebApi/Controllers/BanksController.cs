using Microsoft.AspNetCore.Mvc;
using FinancialCrm.DataAccess;
using System.Linq;

namespace FinancialCrm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBanksDashboardData()
        {
            using (var db = new FinancialCrmDbEntities1())
            {
                // 1. Banka Bakiyelerini Çekiyoruz
                var ziraatBalance = db.Banks
                    .Where(x => x.BankTitle == "Ziraat Bankası")
                    .Select(y => y.BankBalance)
                    .FirstOrDefault() ?? 0;

                var vakifBalance = db.Banks
                    .Where(x => x.BankTitle == "Vakıfbank")
                    .Select(y => y.BankBalance)
                    .FirstOrDefault() ?? 0;

                var isBankasiBalance = db.Banks
                    .Where(x => x.BankTitle == "İş Bankası")
                    .Select(y => y.BankBalance)
                    .FirstOrDefault() ?? 0;

                // 2. Son 5 Banka Hareketini (En yeniden en eskiye) Çekiyoruz
                var last5Processes = db.BankProcesses
                    .OrderByDescending(x => x.BankProcessId)
                    .Take(5)
                    .ToList()
                    .Select(x => new
                    {
                        x.Description,
                        x.Amount,
                        x.ProcessType,
                        ProcessDate = x.ProcessDate.HasValue ? x.ProcessDate.Value.ToString("dd.MM.yyyy") : ""
                    })
                    .ToList();

                var result = new
                {
                    ZiraatBalance = ziraatBalance,
                    VakifBalance = vakifBalance,
                    IsBankasiBalance = isBankasiBalance,
                    Last5Processes = last5Processes
                };

                return Ok(result);
            }
        }
    }
}