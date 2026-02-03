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
using Test_DVLD.Licenses;
using Test_DVLD.Licenses.Local_Licenses;
using Test_DVLD_Buisness;
using static DVLD_Buisness.clsLicense;

namespace Test_DVLD.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {
        private int _NewLicenseID = -1;
        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            if(rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int OldLicenseID = obj;

            lblOldLicenseID.Text = OldLicenseID.ToString();
            llShowLicenseHistory.Enabled = (OldLicenseID != -1);
            if(OldLicenseID == -1)
            {
                return;
            }

            //is license not active
            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active. You cannot replace it.", "Invalid License", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;


        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to replace this License?", "Confirm Replace License", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewReplacedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);
            if (NewReplacedLicense == null)
            {
                MessageBox.Show("License Replacement Process Failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = NewReplacedLicense.ApplicationID.ToString();
            _NewLicenseID = NewReplacedLicense.LicenseID;
            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("License Replacement Process Completed Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Demaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
                new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
