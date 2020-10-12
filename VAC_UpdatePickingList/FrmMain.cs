using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAC_UpdatePickingList.Helpers;

namespace VAC_UpdatePickingList
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            LoadProductGroup();
        }

        private void currentTime_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        void LoadProductGroup()
        {
            string query = "select Code, [Name] from ProductGroup";
            cbProductGroup.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cbProductGroup.DisplayMember = "Name";
            cbProductGroup.ValueMember = "Code";
        }

        private void cbProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productGroup = (cbProductGroup.SelectedItem as DataRowView).Row["Code"] as string;
            string query = $"select Product from Product where ProductGroup = '{productGroup}'";
            cbProduct.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cbProduct.DisplayMember = "Product";
            cbProduct.ValueMember = "Product";
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmConfig frm = new FrmConfig();
            frm.Show();
        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string product = (cbProduct.SelectedItem as DataRowView).Row["Product"] as string;
            string query = $"select BOMNO from BOM where Product = '{product}' and MainProduct = 'Open' and Discontinue = 0";
            cbBOMVersion.DataSource = DataProvider.Instance.ExecuteQuery(query);
            cbBOMVersion.DisplayMember = "BOMNO";
            cbBOMVersion.ValueMember = "BOMNO";
        }
    }
}
