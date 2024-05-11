using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;

namespace Voting_system
{
    public partial class Form13 : KryptonForm
    {

        public Form13()
        {

            InitializeComponent();
        }


        private int SaveToDatabase(string elecname, DateTime date)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string insertQuery = "INSERT INTO tbl_elections (elec_name, last_voting) VALUES (@Name, @lastVoting); SELECT LAST_INSERT_ID();";
                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, mySqlConnection))
                {
                    insertCommand.Parameters.AddWithValue("@Name", elecname);
                    insertCommand.Parameters.AddWithValue("@lastVoting", date);
                    int electionId = Convert.ToInt32(insertCommand.ExecuteScalar());

                    // Create table tbl_positionsforelection with the newly created ID
                    string createTableQuery = $"CREATE TABLE tbl_positionsforelection{electionId} (elec_id INT, position_name VARCHAR(1000), FOREIGN KEY (elec_id) REFERENCES tbl_elections(elec_id));";
                    using (MySqlCommand createTableCommand = new MySqlCommand(createTableQuery, mySqlConnection))
                    {
                        createTableCommand.ExecuteNonQuery();
                    }

                    return electionId; // Return the last inserted ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; // Return -1 indicating an error
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void ClearFields()
        {
            bunifuTextBox2.Text = "";
            bunifuTextBox1.Text = "";
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Close();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bunifuTextBox1.Text) &&
                bunifuDatepicker1 != null) // Check if an item is selected in the BunifuDropdown
            {
                SaveToDatabase(bunifuTextBox1.Text, bunifuDatepicker1.Value);
                MessageBox.Show("Election created successfully");
                ClearFields();
                Form14 form14 = new Form14();
                form14.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all the fields");
            }
        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {

        }
    }
}
