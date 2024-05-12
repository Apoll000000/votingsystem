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
    public partial class Form8 : KryptonForm
    {
        public Form8()
        {
            InitializeComponent();
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

        private void ReadData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "SELECT * FROM tbl_elections";
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
            Form13 form13 = new Form13();
            form13.Show();
            this.Close();
        }


        private void bunifuCustomDataGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count > 0)
            {
                // Retrieve the election ID from the selected row
                int electionId = Convert.ToInt32(bunifuCustomDataGrid1.SelectedRows[0].Cells["Column4"].Value);
                // Proceed to Form3 while passing the election ID
                Form15 form15 = new Form15(electionId);
                form15.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count > 0)
            {
                // Retrieve the election ID from the selected row
                int electionId = Convert.ToInt32(bunifuCustomDataGrid1.SelectedRows[0].Cells["Column4"].Value);

                // Check if the election is already active in the database
                string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
                MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

                try
                {
                    mySqlConnection.Open();
                    string queryCheckActive = "SELECT elec_isactive, is_tallied FROM tbl_elections WHERE elec_id = @electionId";
                    MySqlCommand cmdCheckActive = new MySqlCommand(queryCheckActive, mySqlConnection);
                    cmdCheckActive.Parameters.AddWithValue("@electionId", electionId);

                    using (var reader = cmdCheckActive.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int isActive = Convert.ToInt32(reader["elec_isactive"]);
                            int isTallied = Convert.ToInt32(reader["is_tallied"]);

                            if (isActive == 1)
                            {
                                MessageBox.Show("Election is already active.");
                            }
                            else if (isTallied == 1)
                            {
                                MessageBox.Show("Cannot activate elections that already have official results.");
                            }
                            else
                            {
                                // Update elec_isactive to 1
                                string queryUpdateActive = "UPDATE tbl_elections SET elec_isactive = 1 WHERE elec_id = @electionId";
                                MySqlCommand cmdUpdateActive = new MySqlCommand(queryUpdateActive, mySqlConnection);
                                cmdUpdateActive.Parameters.AddWithValue("@electionId", electionId);
                                int rowsAffected = cmdUpdateActive.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Election Activated Successfully.");
                                    RefreshForm();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to activate election.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Election not found.");
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
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void RefreshForm()
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Close();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count > 0)
            {
                // Retrieve the election ID from the selected row
                int electionId = Convert.ToInt32(bunifuCustomDataGrid1.SelectedRows[0].Cells["Column4"].Value);
                // Proceed to Form3 while passing the election ID
                Form16 form16 = new Form16(electionId);
                form16.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }
    }
}
