using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace FinancialCrm.UI
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            //  Timerı başlatma
            timer1.Start();

            
            timer1_Tick(sender, e);
        }

       

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Timer Kısmı ve api'dan veri çekme kısmı 
        private async void timer1_Tick(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                //  ÜSTTEKİ 3 KARTIN DOLDURULMASI 
                var responseStats = await client.GetAsync($"{ApiConfig.BaseUrl}/api/Statistics");
                if (responseStats.IsSuccessStatusCode)
                {
                    var jsonData = await responseStats.Content.ReadAsStringAsync();
                    var stats = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                    lblTotalBalance.Text = stats.TotalBalance.ToString("C2");
                    lblLastBankProcessAmount.Text = stats.LastBankProcessAmount.ToString("C2");

                    // c2 tl isareti koymak icin
                    lblBillAmount.Text = stats.TotalBillAmount.ToString("C2");
                }

                //  SOLDAKİ SÜTUN GRAFİĞİ 
                var responseBank = await client.GetAsync($"{ApiConfig.BaseUrl}/api/Statistics/GetBankData");
                if (responseBank.IsSuccessStatusCode)
                {
                    var jsonBank = await responseBank.Content.ReadAsStringAsync();
                    var bankData = JsonConvert.DeserializeObject<List<BankDataDto>>(jsonBank);

                    chart1.Series["Series1"].Points.Clear();
                    foreach (var item in bankData)
                    {
                        chart1.Series["Series1"].Points.AddXY(item.BankTitle, item.BankBalance);
                    }
                }

                // SAĞDAKİ PASTA GRAFİĞİ
                var responseBill = await client.GetAsync($"{ApiConfig.BaseUrl}/api/Statistics/GetBillData");
                if (responseBill.IsSuccessStatusCode)
                {
                    var jsonBill = await responseBill.Content.ReadAsStringAsync();
                    var billData = JsonConvert.DeserializeObject<List<BillDataDto>>(jsonBill);

                    chart2.Series[0].Points.Clear();
                    foreach (var item in billData)
                    {
                        chart2.Series[0].Points.AddXY(item.BillTitle, item.BillAmount);
                    }
                }
            }
        }


        private void btnBankProcessForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
            frm.Show();
            this.Hide();
        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {
            FrmCategories frm = new FrmCategories();
            frm.Show();
            this.Hide();
        }

        private void frmBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();
            frm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.Show();
            this.Hide();
        }
    }
    public class ResultStatisticsDto
    {
        public decimal TotalBalance { get; set; }
        public decimal LastBankProcessAmount { get; set; }
        public decimal TotalBillAmount { get; set; } // Fatura tutarı için değiştirildi
    }

    public class BankDataDto
    {
        public string BankTitle { get; set; }
        public decimal BankBalance { get; set; }
    }

    public class BillDataDto
    {
        public string BillTitle { get; set; }
        public decimal BillAmount { get; set; }
    }
}
