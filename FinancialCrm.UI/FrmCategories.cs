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
using System.Collections.Generic;

namespace FinancialCrm.UI
{
    public partial class FrmCategories : Form
    {
        public FrmCategories()
        {
            InitializeComponent();
        }

        private async void FrmCategories_Load(object sender, EventArgs e)
        {
           
        }


        private async void btnCategoryList_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                // 1. API sunucumuza istek atıyoruz
                var responseMessage = await client.GetAsync("https://localhost:7126/api/Categories");

                if (responseMessage.IsSuccessStatusCode)
                {
                    // 2. Başarılı olursa gelen JSON verisini okuyoruz
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    // 3. Metni C# Listesine çeviriyoruz (Entity katmanındaki Category sınıfını kullanarak)
                    var values = JsonConvert.DeserializeObject<List<CategoryResultDto>>(jsonData);

                    // 4. Tabloya (DataGridView) yazdırıyoruz
                    dataGridView1.DataSource = values;
                }
            }
        }

        // KATEGORİ EKLEME BUTONU
        private async void btnCreateCategory_Click(object sender, EventArgs e)
        {
            var categoryName = txtCategoriesName.Text;

            var json = JsonConvert.SerializeObject(new CategoryResultDto { CategoryName = categoryName });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("https://localhost:7126/api/Categories", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kategori başarıyla eklendi!");
                    btnCategoryList_Click(sender, e); // Tabloyu yeniler
                }
            }
        }

        // KATEGORİ SİLME BUTONU
        private async void BtnRemoveCategory_Click(object sender, EventArgs e)
        {
            var id = txtCategoriesId.Text;

            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"https://localhost:7126/api/Categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kategori başarıyla silindi!");
                    btnCategoryList_Click(sender, e); // Tabloyu yeniler
                }
            }
        }

        // KATEGORİ GÜNCELLEME BUTONU
        private async void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtCategoriesId.Text);
            var categoryName = txtCategoriesName.Text;

            var json = JsonConvert.SerializeObject(new CategoryResultDto { CategoryId = id, CategoryName = categoryName });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PutAsync("https://localhost:7126/api/Categories", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kategori başarıyla güncellendi!");
                    btnCategoryList_Click(sender, e); // Tabloyu yeniler
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDashboardForm_Click_1(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void btnBanksForm_Click_1(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnBankProcessesForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
            frm.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    // Bu sınıf, API'den gelen JSON verisini arayüzde karşılamak için oluşturduğumuz bir kalıptır.
    public class CategoryResultDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
