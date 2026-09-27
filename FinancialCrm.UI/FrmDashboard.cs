using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Timer Kısmı
        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void frmBanksForm_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBankProcessForm_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
         
        }
    }
}
