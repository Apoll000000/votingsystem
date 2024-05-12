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
    public partial class Form11 : KryptonForm
    {

        private byte[] imageData;
        public Form11()
        {

            InitializeComponent();
            PopulateElectionDropdown();
        }


        private void PopulateElectionDropdown()
        {

            string connectionString = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            try
            {
                // Open database connection
                mySqlConnection.Open();

                // Create SQL command to fetch active elections
                string query = "SELECT elec_id, elec_name FROM tbl_elections WHERE elec_isactive = 1";
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);

                // Execute SQL command and fetch data
                MySqlDataReader reader = command.ExecuteReader();

                // Clear existing items in the dropdown
                bunifuDropdown1.Items.Clear();

                // Add fetched election names and IDs to the dropdown
                while (reader.Read())
                {
                    string electionName = reader["elec_name"].ToString();
                    int electionId = Convert.ToInt32(reader["elec_id"]);
                    bunifuDropdown1.Items.Add(new KeyValuePair<int, string>(electionId, electionName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close database connection
                mySqlConnection.Close();
            }
        }

        private void SaveToDatabase(string name, string position, string course, byte[] imageData, int elecId)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "INSERT INTO tbl_candidates (candidate_name, candidate_position, candidate_course, candidate_picture, elec_id) VALUES (@Name, @Position, @Course, @ImageData, @ElecId)";
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Position", position);
                    command.Parameters.AddWithValue("@Course", course);
                    command.Parameters.AddWithValue("@ImageData", imageData);
                    command.Parameters.AddWithValue("@ElecId", elecId);
                    command.ExecuteNonQuery();
                }
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

        private void ClearFields()
        {
            bunifuTextBox1.Text = "";
            bunifuTextBox4.Text = "";
            imageData = null;
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown1.selectedIndex >= 0)
            {
                int selectedIndex = bunifuDropdown1.selectedIndex;
                KeyValuePair<int, string> selectedElection = (KeyValuePair<int, string>)bunifuDropdown1.Items[selectedIndex];
                int elecId = selectedElection.Key;

                PopulatePositionsDropdown(elecId);
            }
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
            if (bunifuDropdown2.selectedIndex >= 0 &&
                !string.IsNullOrEmpty(bunifuTextBox1.Text) &&
                bunifuDropdown3.selectedIndex >= 0 &&
                imageData != null &&
                bunifuDropdown1.selectedIndex >= 0) // Check if an item is selected in the BunifuDropdown
                    {
                        // Get the selected election ID from the BunifuDropdown
                        int selectedIndex = bunifuDropdown1.selectedIndex;

                        // Get the selected item from the Items collection
                        KeyValuePair<int, string> selectedElection = (KeyValuePair<int, string>)bunifuDropdown1.Items[selectedIndex];
                        int elecId = selectedElection.Key;

                // Cast SelectedItem to string for bunifuDropdown2 and bunifuDropdown3
                        string selectedPosition = bunifuDropdown2.selectedValue?.ToString();
                        string selectedCourse = bunifuDropdown3.selectedValue?.ToString();

                        // Save voter details and image to database
                        SaveToDatabase(bunifuTextBox1.Text, selectedPosition, selectedCourse, imageData, elecId);
                        MessageBox.Show("Candidate saved successfully.");
                        ClearFields();
                        Form12 form12 = new Form12();
                        form12.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please fill all fields, select an image, and choose an election.");
                    }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageData = File.ReadAllBytes(openFileDialog.FileName);
                    bunifuTextBox4.Text = "Image Selected";
                }
            }
        }

        private void bunifuDropdown2_onItemSelected(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {

        }

        private void PopulatePositionsDropdown(int electionId)
        {
            string connectionString = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            try
            {
                // Open database connection
                mySqlConnection.Open();

                // Create SQL command to fetch positions for the selected election
                string query = "SELECT position_name FROM tbl_positionsforelection" + electionId + " WHERE elec_id = @ElectionId";
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                command.Parameters.AddWithValue("@ElectionId", electionId);

                // Execute SQL command and fetch data
                MySqlDataReader reader = command.ExecuteReader();

                // Clear existing items in the dropdown
                bunifuDropdown2.Items.Clear();

                // Add fetched positions to the dropdown
                while (reader.Read())
                {
                    string positionName = reader["position_name"].ToString();
                    bunifuDropdown2.Items.Add(positionName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close database connection
                mySqlConnection.Close();
            }
        }
    }
}
