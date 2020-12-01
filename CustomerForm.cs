using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FinalProjectVersion2
{
    public partial class CustomerForm : Form
    {
        int selectedRow;
        long telephone;
        string firstName, lastName, address, email;
        string filename = "CustomerInformation.txt";
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void btnAddCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                firstName = txtFirstName.Text;
                lastName = txtLastName.Text;
                telephone = Convert.ToInt64(txtTelephone.Text);
                address = txtAddress.Text;
                email = txtEmail.Text;
                
                int n = dgvCustomers.Rows.Add();
                dgvCustomers.Rows[n].Cells[0].Value = firstName;
                dgvCustomers.Rows[n].Cells[1].Value = lastName;
                dgvCustomers.Rows[n].Cells[2].Value = telephone;
                dgvCustomers.Rows[n].Cells[3].Value = address;
                dgvCustomers.Rows[n].Cells[4].Value = email;
                SaveDGV();
                CreateDGV();
                ReturnAndClean();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = dgvCustomers.CurrentRow.Index;
            txtFirstName.Text = Convert.ToString(dgvCustomers[0, selectedRow].Value);
            txtLastName.Text = Convert.ToString(dgvCustomers[1, selectedRow].Value);
            txtTelephone.Text = Convert.ToString(dgvCustomers[2, selectedRow].Value);
            txtAddress.Text = Convert.ToString(dgvCustomers[3, selectedRow].Value);
            txtEmail.Text = Convert.ToString(dgvCustomers[4, selectedRow].Value);
            btnAddCustomers.Enabled = false;
            btnEditCustomers.Enabled = true;
            btnDeleteCustomers.Enabled = true;
        }

        public void CreateDGV()
        {
            dgvCustomers.Rows.Clear();

            using (var streamReader = new StreamReader(filename))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var values = line.Split(',');
                    var rowIndex = dgvCustomers.Rows.Add();
                    for (int i = 0; i < values.Length; i++)
                    {
                        dgvCustomers.Rows[rowIndex].Cells[i].Value = values[i];
                    }
                }
            }
        }

        private void btnEditCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                firstName = txtFirstName.Text;
                lastName = txtLastName.Text;
                telephone = Convert.ToInt64(txtTelephone.Text);
                address =  txtAddress.Text;
                email = txtEmail.Text;
                dgvCustomers[0, selectedRow].Value = firstName;
                dgvCustomers[1, selectedRow].Value = lastName;
                dgvCustomers[2, selectedRow].Value = telephone;
                dgvCustomers[3, selectedRow].Value = address;
                dgvCustomers[4, selectedRow].Value = email;
                ReturnAndClean();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCustomers_Click(object sender, EventArgs e)
        {
            DialogResult deleteProduct;
            deleteProduct = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (deleteProduct == DialogResult.Yes)
            {
                dgvCustomers.Rows.RemoveAt(selectedRow);
            }
            SaveDGV();
            CreateDGV();
            ReturnAndClean();
        }

        private void btnShowProducts_Click(object sender, EventArgs e)
        {
            InventoryForm showInventory = new InventoryForm();
            showInventory.Show();
            this.Close();
        }

        private void btnShowCategories_Click(object sender, EventArgs e)
        {
            SportsCategoryForm showCategories = new SportsCategoryForm();
            showCategories.Show();
            this.Close();
        }

        public void SaveDGV()
        {

            const char DELIM = ',';

            StreamWriter productFile;
            productFile = File.CreateText(filename);
            int rowcount = dgvCustomers.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                productFile.WriteLine(dgvCustomers.Rows[i].Cells[0].Value.ToString() + DELIM
                    + dgvCustomers.Rows[i].Cells[1].Value.ToString() + DELIM
                    + dgvCustomers.Rows[i].Cells[2].Value.ToString() + DELIM
                    + dgvCustomers.Rows[i].Cells[3].Value.ToString() + DELIM
                    + dgvCustomers.Rows[i].Cells[4].Value.ToString());
            }
            productFile.Close();
        }

        public void ReturnAndClean()
        {
            btnEditCustomers.Enabled = false;
            btnDeleteCustomers.Enabled = false;
            btnAddCustomers.Enabled = true;
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtTelephone.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            CreateDGV();
            ReturnAndClean();
        }
    }
}
