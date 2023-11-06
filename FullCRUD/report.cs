using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using BusinessLogic;
using DataAccess;
using BusinessObject;

namespace FullCRUD
{
    public partial class report : Form
    {

        DataTable dataTable = new DataTable();
        MySqlConnection MySQLConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
        ReportBL ReportBL = new ReportBL();

        public report()
        {
            InitializeComponent();
        }

 
        private void btnShow_Click(object sender, EventArgs e)
        {
            dataTable.Clear ();
            customerReport report = new customerReport();
            dataTable = ReportBL.viewData();
            report.Database.Tables["customers"].SetDataSource(dataTable);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = report;
        }
    }
}
