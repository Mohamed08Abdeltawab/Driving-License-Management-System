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

namespace Test_DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }

        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private DataTable _AllLocalDrivingLicenses = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {

        }


    }
}
