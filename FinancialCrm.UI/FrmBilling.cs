using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FinancialCrm.UI
{
    public partial class FrmBilling : Form
    {
        private readonly string apiUrl = $"{ApiConfig.BaseUrl}/api/Bills";
        public FrmBilling()
        {
            InitializeComponent();
        }
        // Ortak Listeleme Metodu (Kod tekrarını önlemek için)
        private async Task LoadBillsAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<BillDto>>(jsonData);
                    dataGridView1.DataSource = values;
                }
            }
        }
        private async void FrmBilling_Load(object sender, EventArgs e)
        {
            await LoadBillsAsync();
        }

        private async void btnBillList_Click(object sender, EventArgs e)
        {
            await LoadBillsAsync();
        }

        private async void btnCreateBill_Click(object sender, EventArgs e)
        {
            var newBill = new BillDto
            {
                BillTitle = txtBillTitle.Text,
                BillAmount = decimal.Parse(txtBillAmount.Text),
                BillPeriod = txtBillPeriod.Text
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(newBill);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ödeme başarılı bir şekilde sisteme eklendi.", "Ödeme & Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBillsAsync();
                }
            }
        
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void BtnRemoveBill_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBillId.Text);

            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ödeme başarılı bir şekilde sistemden silindi.", "Ödeme & Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBillsAsync();
                }
            }
        }

        private async void btnUpdateBill_Click(object sender, EventArgs e)
        {
            var updatedBill = new BillDto
            {
                BillId = int.Parse(txtBillId.Text),
                BillTitle = txtBillTitle.Text,
                BillAmount = decimal.Parse(txtBillAmount.Text),
                BillPeriod = txtBillPeriod.Text
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(updatedBill);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ödeme başarılı bir şekilde güncellendi.", "Ödeme & Faturalar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBillsAsync();
                }
            }
        
        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.Show();
            this.Hide();
        }

        private void frmDashboardForm_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnBankProcessForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
            frm.Show();
            this.Hide();
        }
    }
    public class BillDto
    {
        public int BillId { get; set; }
        public string BillTitle { get; set; }
        public decimal BillAmount { get; set; }
        public string BillPeriod { get; set; }
    }
}
