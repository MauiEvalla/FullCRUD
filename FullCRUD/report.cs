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
using CrystalDecisions.Shared;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;


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
            // Retrieve data into the dataTable
            dataTable.Clear();
            dataTable = ReportBL.viewData();

            // Create a new instance of the report
            customerReport report = new customerReport();

            // Set the DataTable as the data source for the report
            report.Database.Tables["customers"].SetDataSource(dataTable);

            // Set the report source for the CrystalReportViewer
            crystalReportViewer1.ReportSource = report;

            // Refresh the report data
            report.Refresh();

            // Export the report to PDF
            ExportOptions exportOptions = new ExportOptions();
            exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat; // PDF format
            exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            exportOptions.DestinationOptions = new DiskFileDestinationOptions
            {
                DiskFileName = "C:\\Users\\Omen\\Desktop\\hi\\customers.pdf" // Specify the path and file name for the exported PDF file.
            };

            report.ExportToDisk(ExportFormatType.PortableDocFormat, "C:\\Users\\Omen\\Desktop\\hi\\customers.pdf");

            MessageBox.Show("Report exported to PDF successfully.");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load("C:\\Users\\Omen\\source\\repos\\FullCRUD\\FullCRUD\\customerReport.rpt");
            reportDocument.SetDatabaseLogon("root", "", "users", "customers");




            ExportOptions exportOptions = new ExportOptions();
            exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat; // PDF format
            exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            exportOptions.DestinationOptions = new DiskFileDestinationOptions
            {
                DiskFileName = "C:\\Users\\Omen\\Desktop\\hi\\customers.pdf" // Specify the path and file name for the exported PDF file.
            };
            reportDocument.Export(exportOptions);

            MessageBox.Show("Report exported to PDF successfully.");

        }

        private void btnCharts_Click(object sender, EventArgs e)
        {

        }
    }
}
