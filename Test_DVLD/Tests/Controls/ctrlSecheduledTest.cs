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
using Test_DVLD.Properties;
using Test_DVLD_Buisness;

namespace Test_DVLD.Tests.Controls
{
    public partial class ctrlSecheduledTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID;
        private int _TestID = -1;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseAppliction;
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }
        
        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if(_TestAppointment == null)
            {
                MessageBox.Show("Error loading Test Appointment data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseAppliction = clsLocalDrivingLicenseApplication.FindByApplicationID(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseAppliction == null)
            {
                MessageBox.Show("Error loading Local Driving License Application data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseAppliction.ApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseAppliction.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseAppliction.PersonFullName;

            lblTrial.Text = _LocalDrivingLicenseAppliction.TotalTrialsPerTest(_TestTypeID).ToString();

            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString("F2") + " USD";
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();

        }


    }
}
