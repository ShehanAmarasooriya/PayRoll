using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTSPayroll
{
    public partial class EmployeeDeductionList : Form
    {
        private String strEmpNo;

        public String StrEmpNo
        {
            get { return strEmpNo; }
            set { strEmpNo = value; }
        }
        public EmployeeDeductionList(String EmpNo)
        {
            InitializeComponent();
            StrEmpNo = EmpNo;
        }

        private void EmployeeDeductionList_Load(object sender, EventArgs e)
        {
            label1.Text = StrEmpNo;
        }
    }
}