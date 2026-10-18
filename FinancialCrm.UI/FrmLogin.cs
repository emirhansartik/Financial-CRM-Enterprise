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
using System.Text;

namespace FinancialCrm.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // Kullanıcı adı ve şifre kontrolü
        private async void btnGiris_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            // Verileri JSON formatına dönüştürüp paketliyoruz
            var json = JsonConvert.SerializeObject(new LoginDto { Username = username, Password = password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                // AuthController içindeki Login metoduna POST isteği atıyoruz
                var response = await client.PostAsync("https://localhost:7126/api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    // API'dan 200 döndüyse Dashboard formunu aç
                    FrmDashboard frm = new FrmDashboard();
                    frm.Show();
                    this.Hide(); // Login formunu gizle
                }
                else
                {
                    // API'den 401döndüyse hata mesajı verdiriyoruz
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
