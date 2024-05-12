using System;
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
using Bunifu.Framework.UI;

namespace Voting_system
{
    public partial class Form15 : KryptonForm
    {
        private int electionId;
        public Form15(int electionId)
        {
            InitializeComponent();
            this.electionId = electionId;
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            ReadData();
            ReadElecInfo();

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

        private void ReadData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "SELECT position_name FROM tbl_positionsforElection" + electionId + " WHERE elec_id = @electionId";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.Parameters.AddWithValue("@electionId", electionId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string positionName = reader.GetString("position_name");

                    // Create a new FlowLayoutPanel for each position
                    FlowLayoutPanel positionPanel = new FlowLayoutPanel();
                    positionPanel.Width = 475; // Set the width (adjust as needed)
                    positionPanel.Height = 50; // Set the height (adjust as needed)
                    positionPanel.BackColor = Color.Transparent; // Set transparent background

                    // Create a new BunifuLabel for the position title
                    // Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox positionTextbox = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();

                    Bunifu.Framework.UI.BunifuMaterialTextbox positionTextbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
                    positionTextbox.Text = positionName;
                    positionTextbox.Width = 374;
                    positionTextbox.Height = 44;
                    positionTextbox.Font = new Font("Tahoma", 11, FontStyle.Regular); // Set font size and style
                    positionTextbox.ForeColor = Color.Black;
                    positionTextbox.BackColor = Color.SeaShell;
                    positionTextbox.LineFocusedColor = Color.Maroon; // Color when active
                    positionTextbox.LineMouseHoverColor = Color.Maroon;
                    positionTextbox.HintText = "Edit Position Name";



                    Bunifu.UI.WinForms.BunifuButton.BunifuButton positionbutton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
                    positionbutton.Text = "DELETE";
                    positionbutton.Font = new Font("Impact", 14, FontStyle.Bold); // Set font size and style
                    positionbutton.ForeColor = Color.White;
                    positionbutton.Width = 86;
                    positionbutton.Height = 44;
                    positionbutton.IdleFillColor = Color.Maroon;
                    positionbutton.IdleBorderColor = Color.DarkRed;
                    positionbutton.IdleBorderThickness = 1;


                    positionbutton.Click += (sender, e) => DeletePosition(positionName, positionPanel);

                    // Add the BunifuLabel to the positionPanel
                    positionPanel.Controls.Add(positionTextbox);

                    positionPanel.Controls.Add(positionbutton);


                    // Add the positionPanel to the main FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(positionPanel);
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

        private void DeletePosition(string positionName, FlowLayoutPanel positionPanel)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "DELETE FROM tbl_positionsforElection" + electionId + " WHERE elec_id = @electionId AND position_name = @positionName";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.Parameters.AddWithValue("@electionId", electionId);
                cmd.Parameters.AddWithValue("@positionName", positionName);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Remove the positionPanel from the main FlowLayoutPanel
                    flowLayoutPanel1.Controls.Remove(positionPanel);
                }
                else
                {
                    MessageBox.Show("Position not found or deletion failed.");
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

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void ReadElecInfo()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "SELECT * FROM tbl_elections WHERE elec_id = @electionId";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.Parameters.AddWithValue("@electionId", electionId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string election_name = reader.GetString("elec_name");
                    DateTime last_voting = reader.GetDateTime("last_voting");

                    bunifuTextBox1.Text = election_name;
                    bunifuDatepicker1.Value = last_voting;

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



        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            Lyceum form1 = new Lyceum();
            form1.Show();
            this.Close();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Close();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.Show();
            this.Close();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Close();
        }


        private void bunifuCustomDataGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            FlowLayoutPanel positionPanel = new FlowLayoutPanel();
            positionPanel.Width = 475; // Set the width (adjust as needed)
            positionPanel.Height = 50; // Set the height (adjust as needed)
            positionPanel.BackColor = Color.Transparent; // Set transparent background

            // Create a new BunifuLabel for the position title
            Bunifu.Framework.UI.BunifuMaterialTextbox positionTextbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            positionTextbox.Text = "";
            positionTextbox.Width = 374;
            positionTextbox.Height = 44;
            positionTextbox.Font = new Font("Tahoma", 11, FontStyle.Regular); // Set font size and style
            positionTextbox.ForeColor = Color.Black;
            positionTextbox.BackColor = Color.SeaShell;
            positionTextbox.LineFocusedColor = Color.Maroon; // Color when active
            positionTextbox.LineMouseHoverColor = Color.Maroon;
            positionTextbox.HintText = "Edit Position Name";



            Bunifu.UI.WinForms.BunifuButton.BunifuButton positionbutton = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            positionbutton.Text = "DELETE";
            positionbutton.Font = new Font("Impact", 14, FontStyle.Bold); // Set font size and style
            positionbutton.ForeColor = Color.White;
            positionbutton.Width = 86;
            positionbutton.Height = 44;
            positionbutton.IdleFillColor = Color.Maroon;
            positionbutton.IdleBorderColor = Color.DarkRed;
            positionbutton.IdleBorderThickness = 1;


            positionbutton.Click += (s, args) => DeletePositionPanel(positionPanel);

            // Add the BunifuLabel to the positionPanel
            positionPanel.Controls.Add(positionTextbox);

            positionPanel.Controls.Add(positionbutton);


            // Add the positionPanel to the main FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(positionPanel);
        }

        private void DeletePositionPanel(FlowLayoutPanel positionPanel)
        {
            // Remove the positionPanel from the main FlowLayoutPanel
            flowLayoutPanel1.Controls.Remove(positionPanel);
        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();

                // Get election name and last voting date
                string electionName = bunifuTextBox1.Text;
                DateTime lastVotingDate = bunifuDatepicker1.Value;

                // Update election details in tbl_elections
                string queryUpdateElection = "UPDATE tbl_elections SET elec_name = @electionName, last_voting = @lastVotingDate WHERE elec_id = @electionId";
                MySqlCommand cmdUpdateElection = new MySqlCommand(queryUpdateElection, mySqlConnection);
                cmdUpdateElection.Parameters.AddWithValue("@electionName", electionName);
                cmdUpdateElection.Parameters.AddWithValue("@lastVotingDate", lastVotingDate);
                cmdUpdateElection.Parameters.AddWithValue("@electionId", electionId);

                int rowsAffectedElection = cmdUpdateElection.ExecuteNonQuery();

                // Check if election details were updated successfully
                if (rowsAffectedElection > 0)
                {
                    MessageBox.Show("Election details updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update election details.");
                }

                foreach (FlowLayoutPanel panel in flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>())
                {
                    foreach (Control control in panel.Controls)
                    {
                        if (control is BunifuMaterialTextbox textbox)
                        {
                            string newPositionName = textbox.Text;

                            // Check if the position already exists in the database
                            string queryCheckExistence = "SELECT COUNT(*) FROM tbl_positionsforElection" + electionId + " WHERE elec_id = @electionId AND position_name = @newPositionName";
                            MySqlCommand cmdCheckExistence = new MySqlCommand(queryCheckExistence, mySqlConnection);
                            cmdCheckExistence.Parameters.AddWithValue("@electionId", electionId);
                            cmdCheckExistence.Parameters.AddWithValue("@newPositionName", newPositionName);

                            int existingPositionCount = Convert.ToInt32(cmdCheckExistence.ExecuteScalar());

                            if (existingPositionCount == 0)
                            {
                                // Position doesn't exist, insert it into the database
                                string queryInsert = "INSERT INTO tbl_positionsforElection" + electionId + " (elec_id, position_name) VALUES (@electionId, @newPositionName)";
                                MySqlCommand cmdInsert = new MySqlCommand(queryInsert, mySqlConnection);
                                cmdInsert.Parameters.AddWithValue("@electionId", electionId);
                                cmdInsert.Parameters.AddWithValue("@newPositionName", newPositionName);

                                int rowsInserted = cmdInsert.ExecuteNonQuery();

                                if (rowsInserted > 0)
                                {
                                    MessageBox.Show("Position '" + newPositionName + "' inserted into the database.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to insert position '" + newPositionName + "' into the database.");
                                }
                            }
                            else
                            {
                                // Position exists, update its name in the database
                                string queryUpdate = "UPDATE tbl_positionsforElection" + electionId + " SET position_name = @newPositionName WHERE elec_id = @electionId AND position_name = @oldPositionName";
                                MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, mySqlConnection);
                                cmdUpdate.Parameters.AddWithValue("@newPositionName", newPositionName);
                                cmdUpdate.Parameters.AddWithValue("@electionId", electionId);
                                cmdUpdate.Parameters.AddWithValue("@oldPositionName", newPositionName);

                                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                                // Handle success or failure
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Successfully updated position names.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to update position names.");
                                }
                            }
                        }
                    }
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

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Form18 form18 = new Form18(electionId);
            form18.Show();
            this.Close();
        }
    }
}
