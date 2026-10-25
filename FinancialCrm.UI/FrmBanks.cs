using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FinancialCrm.UI
{
    public partial class FrmBanks : Form
    {
        public FrmBanks()
        {
            InitializeComponent();
        }


        private async void FrmBanks_Load(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{ApiConfig.BaseUrl}/api/Banks");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<BanksDashboardDto>(jsonData);

                    // 1. Üstteki 3 Banka Kartının Bakiyelerini Yazdırıyoruz
                    lblZiraatBankBalance.Text = data.ZiraatBalance.ToString("C2");
                    lblVakifbankBalance.Text = data.VakifBalance.ToString("C2");
                    lblİsBankasiBalance.Text = data.IsBankasiBalance.ToString("C2");

                    // 2. Son 5 Banka Hareketini Ekrana Basıyoruz
                    Label[] processLabels = { lblBankProcess1, lblBankProcess2, lblBankProcess3, lblBankProcess4, lblBankProcess5 };

                    for (int i = 0; i < data.Last5Processes.Count && i < processLabels.Length; i++)
                    {
                        var item = data.Last5Processes[i];
                        processLabels[i].Text = $"{item.Description}  |  {item.ProcessDate}  |  {item.ProcessType}  |  {item.Amount:C2}";
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();
            frm.Show();
            this.Hide();    
        }

        private void btnDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {
            FrmCategories frm = new FrmCategories();
            frm.Show();
            this.Hide();
        }

        private void btnBankProcessForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
            frm.Show();
            this.Hide();
    }
    }
    // API'den gelen JSON paketini karşılayacak DTO Kalıplarıdır.
    public class BanksDashboardDto
    {
        public decimal ZiraatBalance { get; set; }
        public decimal VakifBalance { get; set; }
        public decimal IsBankasiBalance { get; set; }
        public List<BankProcessItemDto> Last5Processes { get; set; }
    }

    public class BankProcessItemDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ProcessType { get; set; }
        public string ProcessDate { get; set; }
    }
}
