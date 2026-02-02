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

namespace Test_DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        //event 
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if(handler != null)
            {
                handler(LicenseID);
            }
        }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;
        public int LicenseID
        {
            get
            {
                return ctrlDriverLicenseInfo1.LicenseID;
            }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();//set text box
            ctrlDriverLicenseInfo1.LoadData(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;

            if (OnLicenseSelected != null && FilterEnabled)
            {
                OnLicenseSelected(_LicenseID);
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("please fix validation errors", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadLicenseInfo(_LicenseID);
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "License ID is required");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, "");
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //sure is a digit or control char
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            //if enter pressed
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
            }
        }

        public void txtLicenseIDFoucs()
        {
            txtLicenseID.Focus();
        }
    }
}
