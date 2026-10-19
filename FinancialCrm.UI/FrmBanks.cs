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
        

        private void FrmBanks_Load(object sender, EventArgs e)
        {
           
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
    // API'den gelen JSON paketini karşılayacak DTO Kalıpları
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
