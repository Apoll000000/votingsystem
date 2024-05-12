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

namespace Voting_system
{
    public partial class Form16 : KryptonForm
    {
        private int electionId;
        public Form16(int electionId)
        {

            InitializeComponent();
            this.electionId = electionId;
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            ReadData();


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

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private List<(string candidateName, string candidateCourse, int numberVotes)> GetCandidatesForPosition(string positionName)
        {
            List<(string candidateName, string candidateCourse, int numberVotes)> candidates = new List<(string, string, int)>();

            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string queryCandidates = "SELECT candidate_name, candidate_course, number_votes FROM tbl_candidates WHERE elec_id = @electionId AND candidate_position = @position";
                MySqlCommand cmdCandidates = new MySqlCommand(queryCandidates, mySqlConnection);
                cmdCandidates.Parameters.AddWithValue("@electionId", electionId);
                cmdCandidates.Parameters.AddWithValue("@position", positionName);
                MySqlDataReader readerCandidates = cmdCandidates.ExecuteReader();

                while (readerCandidates.Read())
                {
                    string candidateName = readerCandidates.GetString("candidate_name");
                    string candidateCourse = readerCandidates.GetString("candidate_course");
                    int numberVotes = readerCandidates.GetInt32("number_votes");
                    candidates.Add((candidateName, candidateCourse, numberVotes));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }

            return candidates;
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
                    positionPanel.Width = 490; // Set the width (adjust as needed)
                    positionPanel.Height = 296; // Set the height (adjust as needed)
                    positionPanel.BackColor = Color.Transparent; // Set transparent background
                    positionPanel.AutoScroll = true; // Enable auto-scroll

                    Panel positionContentPanel = new Panel();
                    positionContentPanel.Width = 470; // Set the width (adjust as needed)
                    positionContentPanel.Height = 35; // Set the height (adjust as needed)
                    positionContentPanel.BackColor = Color.Maroon; // Set maroon background

                    // Add the Panel to the FlowLayoutPanel
                    positionPanel.Controls.Add(positionContentPanel);

                    // Create a new BunifuLabel for the position title
                    Bunifu.UI.WinForms.BunifuLabel positionLabel = new Bunifu.UI.WinForms.BunifuLabel();
                    positionLabel.Text = positionName;
                    positionLabel.Font = new Font("Century Gothic", 22, FontStyle.Bold); // Set font size and style
                    positionLabel.ForeColor = Color.White;
                    positionLabel.Dock = DockStyle.Fill; // Dock the label to fill the FlowLayoutPanel

                    // Add the BunifuLabel to the positionPanel
                    positionContentPanel.Controls.Add(positionLabel);

                    // Create DataGridView to display candidate information
                    DataGridView dataGridView = new DataGridView();
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    // dataGridView.Dock = DockStyle.Fill;
                    //dataGridView.Width = 400;
                    //dataGridView.Height = 200;
                    dataGridView.Bounds = new Rectangle(50, 50, 460, 290);

                    // Add columns to the DataGridView
                    dataGridView.Columns.Add("Candidate Name", "Candidate Name");
                    dataGridView.Columns.Add("Course", "Course");
                    dataGridView.Columns.Add("Total Votes", "Total Votes");
                    dataGridView.Columns.Add("Remarks", "Remarks");

                    // Add DataGridView to the positionPanel
                    positionPanel.Controls.Add(dataGridView);

                    // Add the positionPanel to the main FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(positionPanel);

                    // Fetch candidates for this position
                    List<(string candidateName, string candidateCourse, int numberVotes)> candidates = GetCandidatesForPosition(positionName);

                    // Populate DataGridView with candidate information
                    foreach ((string candidateName, string candidateCourse, int numberVotes) in candidates)
                    {
                        dataGridView.Rows.Add(candidateName, candidateCourse, numberVotes, "");
                    }

                    // Sort DataGridView based on Total Votes column in descending order
                    dataGridView.Sort(dataGridView.Columns["Total Votes"], ListSortDirection.Descending);

                    // Set the "Winner" remark for the candidate with the highest number of votes after sorting the DataGridView
                    if (dataGridView.Rows.Count > 0)
                    {
                        dataGridView.Rows[0].Cells["Remarks"].Value = "Winner";
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form17 form17 = new Form17(electionId);
            form17.Show();
            this.Close();
        }
    }
}
