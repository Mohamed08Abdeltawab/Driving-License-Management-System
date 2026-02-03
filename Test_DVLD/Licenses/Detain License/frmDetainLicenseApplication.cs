using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Test_DVLD.Global_Classes;
using Test_DVLD.Licenses.Local_Licenses;
using Test_DVLD_Buisness;

namespace Test_DVLD.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedlicenseID = obj;
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedlicenseID != -1);

            if (SelectedlicenseID == -1)
            {
                MessageBox.Show("No License Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("This license is already detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to detain this license?", "Confirm Detain License", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Failed to detain the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text = _DetainID.ToString();

            MessageBox.Show("License detained successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
            txtFineFees.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fine/Fees is required.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
            }

            if (!clsValidatoin.IsNumber(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fine/Fees must be a valid number.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
            }
        }
    }
}
