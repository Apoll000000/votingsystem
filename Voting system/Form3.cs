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
using System.IO;

namespace Voting_system
{
    public partial class Form3 : KryptonForm
    {
        private int electionId;
        private string student_id;

        // Constructor with one parameter for the election ID
        public Form3(int electionId, string student_id)
        {
            InitializeComponent();
            this.electionId = electionId;
            this.student_id = student_id;
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            ReadData();
            ReadVoting();
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
                string query = "SELECT elec_name FROM tbl_elections WHERE elec_id = @electionId";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.Parameters.AddWithValue("@electionId", electionId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string electionName = reader.GetString("elec_name");
                    bunifuLabel1.Text = electionName;

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

        public class Candidate
        {
            public string Name { get; set; }
            public string Course { get; set; }
            public byte[] Picture { get; set; } // Store image data as byte array
        }

        private List<Candidate> GetCandidatesForIndividual(string positionName)
        {
            List<Candidate> candidatesinfo = new List<Candidate>();

            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string queryCandidates = "SELECT candidate_name, candidate_course, candidate_picture FROM tbl_candidates WHERE elec_id = @electionId AND candidate_position = @position";
                MySqlCommand cmdCandidates = new MySqlCommand(queryCandidates, mySqlConnection);
                cmdCandidates.Parameters.AddWithValue("@electionId", electionId);
                cmdCandidates.Parameters.AddWithValue("@position", positionName);
                MySqlDataReader readerCandidates = cmdCandidates.ExecuteReader();

                while (readerCandidates.Read())
                {
                    Candidate candidateinfo = new Candidate();
                    candidateinfo.Name = readerCandidates.GetString("candidate_name");
                    candidateinfo.Course = readerCandidates.GetString("candidate_course");
                    candidateinfo.Picture = (byte[])readerCandidates["candidate_picture"]; // Retrieve image data as byte array
                    candidatesinfo.Add(candidateinfo);
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

            return candidatesinfo;
        }

        private List<string> GetCandidatesForPosition(string positionName)
        {
            List<string> candidates = new List<string>();

            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string queryCandidates = "SELECT candidate_name FROM tbl_candidates WHERE elec_id = @electionId AND candidate_position = @position";
                MySqlCommand cmdCandidates = new MySqlCommand(queryCandidates, mySqlConnection);
                cmdCandidates.Parameters.AddWithValue("@electionId", electionId);
                cmdCandidates.Parameters.AddWithValue("@position", positionName);
                MySqlDataReader readerCandidates = cmdCandidates.ExecuteReader();

                while (readerCandidates.Read())
                {
                    string candidateName = readerCandidates.GetString("candidate_name");
                    candidates.Add(candidateName);
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
                    positionPanel.Width = 625; // Set the width (adjust as needed)
                    positionPanel.Height = 362; // Set the height (adjust as needed)
                    positionPanel.BackColor = Color.Transparent; // Set transparent background
                    positionPanel.AutoScroll = true; // Enable auto-scroll

                    Panel positionContentPanel = new Panel();
                    positionContentPanel.Width = 625; // Set the width (adjust as needed)
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


                    List<Candidate> candidatesinfo = GetCandidatesForIndividual(positionName);

                    foreach (Candidate candidateinfo in candidatesinfo)
                    {
                        // Create a new Panel for each candidate
                        Panel candidatePanel = new Panel();
                        candidatePanel.Width = 196; // Set the width (adjust as needed)
                        candidatePanel.Height = 222; // Set the height (adjust as needed)
                        candidatePanel.BackColor = Color.Maroon; // Set background color

                        // Create a new Label for the candidate name
                        Label candidateLabel = new Label();
                        candidateLabel.Text = candidateinfo.Name;
                        candidateLabel.Font = new Font("Impact", 11); // Set font size
                        candidateLabel.TextAlign = ContentAlignment.MiddleCenter; // Center text alignment
                        candidateLabel.Dock = DockStyle.Bottom; // Dock the label to the top of the panel

                        // Add the Label to the Panel
                        candidatePanel.Controls.Add(candidateLabel);

                        // Convert byte array to image
                        Image image = ByteArrayToImage(candidateinfo.Picture);

                        // Add PictureBox for candidate picture
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Image = image;
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Dock = DockStyle.Fill;
                        candidatePanel.Controls.Add(pictureBox);

                        // Add Label for candidate course
                        Label courseLabel = new Label();
                        courseLabel.Text = candidateinfo.Course;
                        courseLabel.Font = new Font("Tahoma", 9); // Set font size
                        courseLabel.TextAlign = ContentAlignment.MiddleCenter; // Center text alignment
                        courseLabel.Dock = DockStyle.Top; // Dock the label to the bottom of the panel
                        candidatePanel.Controls.Add(courseLabel);

                        // Add the Panel for the candidate to the FlowLayoutPanel
                        positionPanel.Controls.Add(candidatePanel);
                    }


                    // Add the positionPanel to the main FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(positionPanel);
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

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }


        private void ReadVoting()
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
                    FlowLayoutPanel positionPanel1 = new FlowLayoutPanel();
                    positionPanel1.Width = 243; // Set the width (adjust as needed)
                    positionPanel1.Height = 68; // Set the height (adjust as needed)
                    positionPanel1.BackColor = Color.Transparent; // Set transparent background

                    Panel positionContentPanel1 = new Panel();
                    positionContentPanel1.Width = 229; // Set the width (adjust as needed)
                    positionContentPanel1.Height = 20; // Set the height (adjust as needed)
                    positionContentPanel1.BackColor = Color.Maroon; // Set maroon background

                    // Add the Panel to the FlowLayoutPanel
                    positionPanel1.Controls.Add(positionContentPanel1);

                    Bunifu.UI.WinForms.BunifuLabel positionLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
                    positionLabel1.Text = positionName;
                    positionLabel1.Font = new Font("Century Gothic", 11, FontStyle.Bold); // Set font size and style
                    positionLabel1.ForeColor = Color.White;
                    // positionLabel1.Dock = DockStyle.Fill; // Dock the label to fill the FlowLayoutPanel

                    // Add the BunifuLabel to the positionPanel
                    positionContentPanel1.Controls.Add(positionLabel1);


                    Bunifu.UI.WinForms.BunifuDropdown positionDropdown1 = new Bunifu.UI.WinForms.BunifuDropdown();
                    positionDropdown1.Width = 229; // Set the width (adjust as needed)
                    positionDropdown1.Height = 30; // Set the height (adjust as needed)

                    // Set the background color of the dropdown to white

                    // Set the foreground color (text color) of the dropdown to maroon
                    positionDropdown1.ForeColor = Color.White;
     

                    // Add the Bunifu Dropdown to the positionPanel
                    positionPanel1.Controls.Add(positionDropdown1);



                    List<string> candidates = GetCandidatesForPosition(positionName);

                    foreach (string candidateName in candidates)
                    {
                        positionDropdown1.Items.Add(candidateName);
                    }

                    // Create a new BunifuLabel for the position title
                   



                    // Add the positionPanel to the main FlowLayoutPanel
                    flowLayoutPanel2.Controls.Add(positionPanel1);
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



        private void Form3_Load(object sender, EventArgs e)
        {
            // You can use the electionId value here if needed

        }


        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(electionId, student_id);
            form4.Show();
            this.Close();
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {

        }

        private void bunifuLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel9_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool AreAllDropdownsSelected()
        {
            foreach (FlowLayoutPanel positionPanel in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
            {
                Bunifu.UI.WinForms.BunifuDropdown dropdown = positionPanel.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().FirstOrDefault();
                if (dropdown == null || dropdown.SelectedIndex == -1)
                {
                    return false; // If any dropdown is not selected, return false
                }
            }
            return true; // If all dropdowns are selected, return true
        }

        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            // Check if all dropdowns are selected
            bool allSelected = AreAllDropdownsSelected();

            if (!allSelected)
            {
                MessageBox.Show("Please select candidates for all positions.");
                return; // Exit the method without proceeding further
            }

            // If all dropdowns are selected, proceed with the confirmation
            Form4 form4 = new Form4(electionId, student_id); // Passing true indicates confirmation
            form4.Confirmation += Form4_Confirmation;
            form4.Back += Form4_Back;
            form4.Show();
            this.Hide();
        }

        private void Form4_Back(object sender, EventArgs e)
        {
            // Show Form3 without updating the database
            this.Show();
            Form4 form4 = (Form4)sender;
            form4.Close();
        }

        private bool HasUserVoted(string student_id, int electionId)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection connection = new MySqlConnection(mysqlCon);
            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM user_votes WHERE student_id = @studentId AND elec_id = @electionId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", student_id);
                    command.Parameters.AddWithValue("@electionId", electionId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        private void RecordUserVote(string student_id, int electionId)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection connection = new MySqlConnection(mysqlCon);

            try
            {
                connection.Open();
                string query = "INSERT INTO user_votes (student_id, elec_id) VALUES (@studentId, @electionId)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@studentId", student_id);
                    command.Parameters.AddWithValue("@electionId", electionId);
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form4_Confirmation(object sender, EventArgs e)
        {
            // Update the database and show Form4 with confirmation
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();

                if (HasUserVoted(student_id, electionId))
                {
                    MessageBox.Show("You have already voted in this election.");
                    LNU form2 = new LNU(student_id);
                    form2.Show();
                    this.Close();
                    return;
                    
                } else
                {
                    foreach (FlowLayoutPanel positionPanel1 in flowLayoutPanel2.Controls.OfType<FlowLayoutPanel>())
                    {
                        Bunifu.UI.WinForms.BunifuDropdown dropdown = positionPanel1.Controls.OfType<Bunifu.UI.WinForms.BunifuDropdown>().FirstOrDefault();

                        if (dropdown != null)
                        {
                            string selectedCandidate = dropdown.SelectedItem.ToString();

                            if (!string.IsNullOrEmpty(selectedCandidate))
                            {
                                string updateQuery = "UPDATE tbl_candidates SET number_votes = number_votes + 1 WHERE candidate_name = @candidateName";
                                using (MySqlCommand cmd = new MySqlCommand(updateQuery, mySqlConnection))
                                {
                                    cmd.Parameters.AddWithValue("@candidateName", selectedCandidate);
                                    cmd.ExecuteNonQuery();
                                }

                                // Record the user's vote
                                RecordUserVote(student_id, electionId);
                            }
                        }
                    }

                    MessageBox.Show("Number of votes updated successfully.");
                }

                // Record the user's vote in the database
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }

            // Open Form5 after all dropdowns have been processed and database is updated
            Form5 form5 = new Form5(student_id);
            form5.Show();
            this.Close();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}


