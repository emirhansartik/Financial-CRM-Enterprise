using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FinancialCrm.UI
{
    public partial class FrmSettings : Form
    {
        private readonly string apiUrl = "https://localhost:7126/api/Users";
        public FrmSettings()
        {
            InitializeComponent();
        }

        // Ortak Kullanıcı Listeleme Metodu
        private async Task LoadUsersAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<UserDto>>(jsonData);
                    dataGridView1.DataSource = values;
                }
            }
        }

        private async void FrmSettings_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async void btnUserList_Click(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async void btnCreateUser_Click(object sender, EventArgs e)
        {
            var newUser = new UserDto
            {
                Username = txtUserName.Text,
                Password = txtUserPassword.Text
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Yeni kullanıcı başarıyla sisteme eklendi.", "Ayarlar & Kullanıcılar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                }
            }
        }

        private async void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {
            FrmCategories frm = new FrmCategories();
            frm.Show();
            this.Hide();
        }

        private async void btnRemoveUser_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtUserId.Text);

            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kullanıcı sistemden silindi.", "Ayarlar & Kullanıcılar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                }
            }
        }
        // Güncelleme Kısmı
        private async void btnUpdateUser_Click(object sender, EventArgs e)
        {
            var updatedUser = new UserDto
            {
                UserId = int.Parse(txtUserId.Text),
                Username = txtUserName.Text,
                Password = txtUserPassword.Text
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(updatedUser);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kullanıcı bilgileri ve şifresi başarıyla güncellendi.", "Ayarlar & Kullanıcılar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                }
            }
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void frmBankProcessForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
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
    }
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
