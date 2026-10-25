using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace FinancialCrm.UI
{
    public partial class FrmBankProcess : Form
    {
        private readonly string apiUrl = $"{ApiConfig.BaseUrl}/api/BankProcesses";
        public FrmBankProcess()
        {
            InitializeComponent();
        }
        // 1. Banka Hareketlerini Tabloya Yükleyen Ortak Metot
        private async Task LoadBankProcessesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<BankProcessDto>>(jsonData);
                    dataGridView1.DataSource = values;
                }
            }
        }

        // 2. Bankaları combobox'a Yükleyen Meto
        private async Task LoadBanksComboBoxAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{apiUrl}/GetBanks");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var banks = JsonConvert.DeserializeObject<List<BankComboBoxDto>>(jsonData);

                    cmbBankOption.DisplayMember = "BankTitle"; // Ekranda görünecek isim (Örn: Ziraat Bankası)
                    cmbBankOption.ValueMember = "BankId";      // Arka planda tutulacak ID (Örn: 1)
                    cmbBankOption.DataSource = banks;
                }
            }
        }
        private async void FrmBankProcess_Load(object sender, EventArgs e)
        {
            await LoadBanksComboBoxAsync();
            await LoadBankProcessesAsync();
        }

        private async void btnBankProcessList_Click(object sender, EventArgs e)
        {
            await LoadBankProcessesAsync();
        }
        // Yeni işlem ekleme
        private async void btnCreateBankProcess_Click(object sender, EventArgs e)
        {
            var newProcess = new
            {
                Description = txtBankDescription.Text,
                ProcessDate = DateTime.Parse(txtBankDate.Text),
                ProcessType = txtBankOption.Text,
                Amount = decimal.Parse(txtBankAmount.Text),
                BankId = Convert.ToInt32(cmbBankOption.SelectedValue)
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(newProcess);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Banka hareketi başarıyla sisteme eklendi.", "Banka Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBankProcessesAsync();
                }
            }
        }
        //Silme
        private async void BtnRemoveBankProcess_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBankProcessId.Text);

            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Banka hareketi sistemden silindi.", "Banka Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBankProcessesAsync();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnUpdateBankProcess_Click(object sender, EventArgs e)
        {
            var updatedProcess = new
            {
                BankProcessId = int.Parse(txtBankProcessId.Text),
                Description = txtBankDescription.Text,
                ProcessDate = DateTime.Parse(txtBankDate.Text),
                ProcessType = txtBankOption.Text,
                Amount = decimal.Parse(txtBankAmount.Text),
                BankId = Convert.ToInt32(cmbBankOption.SelectedValue)
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(updatedProcess);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Banka hareketi başarıyla güncellendi.", "Banka Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBankProcessesAsync();
                }
            }
        }
        

        private void frmDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();
            frm.Show();
            this.Hide();
        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {
            FrmCategories frm = new FrmCategories();
            frm.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.Show();
            this.Hide();
        }
    }
    // Tabloda (DataGridView) verileri göstermek için DTO sınıfı
    public class BankProcessDto
    {
        public int BankProcessId { get; set; }
        public string Description { get; set; }
        public DateTime ProcessDate { get; set; }
        public string ProcessType { get; set; }
        public decimal Amount { get; set; }
        public int BankId { get; set; }
        public string BankTitle { get; set; }
    }

    // ComboBox'ta bankaları listelemek için DTO sınıfı
    public class BankComboBoxDto
    {
        public int BankId { get; set; }
        public string BankTitle { get; set; }
    }
}
