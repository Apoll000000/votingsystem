﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;

namespace Voting_system
{
    public partial class Form19 : KryptonForm
    {
        private string student_id;
        public Form19(string student_id)
        {
            InitializeComponent();
            this.student_id = student_id;
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            ReadData();
            ViewData();

            try
            {
                mySqlConnection.Open();
                MessageBox.Show("CONNECTION SUCCESS");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void ViewData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "SELECT student_name FROM tbl_accounts WHERE student_id = @studentId";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.Parameters.AddWithValue("@studentId", student_id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string studentName = reader.GetString("student_name");
                    bunifuLabel3.Text = studentName;

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void ReadData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "SELECT * FROM tbl_elections WHERE is_tallied = 1";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                bunifuCustomDataGrid1.AutoGenerateColumns = false; // Disable auto generation of columns
                bunifuCustomDataGrid1.DataSource = dataTable;

                // Manually define columns
                
                bunifuCustomDataGrid1.Columns["Column1"].DataPropertyName = "elec_name"; // Replace "Column1" with your actual column name
                bunifuCustomDataGrid1.Columns["Column2"].DataPropertyName = "elec_isactive"; // Replace "Column2" with your actual column name
                bunifuCustomDataGrid1.Columns["Column3"].DataPropertyName = "last_voting";
                bunifuCustomDataGrid1.Columns["Column4"].DataPropertyName = "elec_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void LNU_Shown(object sender, EventArgs e)
        {
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count > 0)
            {
                // Retrieve the election ID from the selected row
                int electionId = Convert.ToInt32(bunifuCustomDataGrid1.SelectedRows[0].Cells["Column4"].Value);
                // Proceed to Form3 while passing the election ID
                Form20 form20 = new Form20(electionId, student_id);
                form20.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Lyceum form1 = new Lyceum();
            form1.Show();
            this.Close();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            LNU form2 = new LNU(student_id);
            form2.Show();
            this.Close();
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
