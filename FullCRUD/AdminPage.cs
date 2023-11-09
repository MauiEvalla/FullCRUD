using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using BusinessObject;
using BusinessLogic;
using System.IO;

namespace FullCRUD
{
    public partial class AdminPage : Form
    {
        UserBL userBL = new UserBL();

        private MySqlConnection connection;

        private bool editMode = false;

        public AdminPage()
        {
            InitializeComponent();

            connection = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ToString());

            // Set up the DataGridView
            dataGridView1.AutoGenerateColumns = false;

            //ID column details
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "id";
            col1.DataPropertyName = "id";
            col1.HeaderText = "ID";

            //Username column Details
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "user_name";
            col2.DataPropertyName = "user_name";
            col2.HeaderText = "Username";

            //Phone column details
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "phone";
            col3.DataPropertyName = "phone"; // 
            col3.HeaderText = "Phone";

            //Email column details.
            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Name = "email";
            col5.DataPropertyName = "email"; // 
            col5.HeaderText = "Email";

           //Edit Button details in the grid view table
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true; 
            editButtonColumn.Name = "EditButton";

           //Delete Button details in the grid view table
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true; 
            deleteButtonColumn.Name = "DeleteButton";

            // Adds the column to the grid view
            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);
            dataGridView1.Columns.Add(col5);
            dataGridView1.Columns.Add(editButtonColumn);
            dataGridView1.Columns.Add(deleteButtonColumn);

            // Bind data to the DataGridView
            LoadData();
        }

        void GridView(string searchQuery) //Sends a SQL Command requesting for a specific username based on user input 
        {

            using (MySqlDataReader drUser = userBL.SearchUsersByUsername(searchQuery))
            {
                if (drUser.HasRows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(drUser);
                    this.dataGridView1.DataSource = dataTable;
                    this.dataGridView1.Visible = true;
                }

            }

        }

        private void LoadData() //Gets all the data and displays them on grid view upon initialization of Admin Page form.
        {

            using (MySqlDataReader drUser = userBL.GetAllUser())
            {
                if (drUser.HasRows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(drUser);
                    this.dataGridView1.DataSource = dataTable;
                    this.dataGridView1.Visible = true;
                }

            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["EditButton"].Index && e.RowIndex >= 0) //Click edit button so you can edit the data
            {
                if (!editMode)
                {
                    // Enable editing for the selected row
                    foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                    {
                        if (cell.OwningColumn.Name != "EditButton" && cell.OwningColumn.Name != "DeleteButton")
                        {
                            cell.ReadOnly = false;
                        }
                    }

                    editMode = true;
                }
                else
                { 
                   DataRow row = ((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
                   User editedUser = new User(
                   Convert.ToInt32(row["id"]),
                   row["user_name"].ToString(),
                   row["phone"].ToString(),
                   row["email"].ToString()
                    );

                    int rowsAffected = userBL.UpdateUserById(editedUser);

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Changes saved successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to save changes.");
                            }
                        foreach (DataGridViewCell cell in dataGridView1.Rows[e.RowIndex].Cells)
                        {
                            if (cell.OwningColumn.Name != "EditButton" && cell.OwningColumn.Name != "DeleteButton")
                            {
                                cell.ReadOnly = true;
                            }
                        }
                        editMode = false;
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["DeleteButton"].Index && e.RowIndex >= 0) //DELETE button logic
            {
                if (MessageBox.Show("Are you sure you want to delete this row?", "Delete Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Get the ID of the record to be deleted
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                    int drUser = userBL.DeleteUserById(id);
                    {
                        if (drUser > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");

                            // Get the data source from DataGridView (assuming it's a DataTable)
                            DataTable dataSource = (DataTable)dataGridView1.DataSource;

                            // Remove the row at the specified index
                            dataSource.Rows.RemoveAt(e.RowIndex);

                            // Reset the data source of DataGridView to reflect the changes
                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = dataSource;
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the record.");
                        }

                    }
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;
            GridView(searchQuery);
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            InsertForm insertform = new InsertForm();
            insertform.Show();
            this.Hide();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string fileName = "User_Data.xlsx"; // Provide a desired file name with extension
            userBL.ExportToExcel(dataGridView1, fileName);
            MessageBox.Show("Data exported to Excel successfully!");
        }


        private void AdminPage_Load(object sender, EventArgs e)
        {
        }

        private void BtnCustomer_Click(object sender, EventArgs e)
        {
            addCustomer addCustomer = new addCustomer();
            addCustomer.Show();
            this.Hide();
        }

        private void btnExportDatabase_Click(object sender, EventArgs e)
        {
            string filePath = "Users.csv";

            using (MySqlDataReader drUser = userBL.GetAllUser())
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the column headers
                    writer.WriteLine("id,Username,Phone,Email");

                    // Write the data
                    while (drUser.Read())
                    {
                        writer.Write(drUser["id"] + ",");
                        writer.Write(drUser["user_name"].ToString() + ",");
                        writer.Write(drUser["phone"].ToString() + ",");
                        writer.Write(drUser["email"].ToString());
                        writer.WriteLine();
                    }
                }

            }
            MessageBox.Show("Data exported to Users.csv");
        }
    }
}
   

