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
    public partial class Form17 : KryptonForm
    {
        private int electionId;

        public Form17(int electionId)
        {
            InitializeComponent();
            this.electionId = electionId;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {


            Form16 form16 = new Form16(electionId);
             form16.Show();
            this.Close();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();

                // Check if the election is already tallied
                string queryCheckTallied = "SELECT is_tallied FROM tbl_elections WHERE elec_id = @electionId";
                MySqlCommand cmdCheckTallied = new MySqlCommand(queryCheckTallied, mySqlConnection);
                cmdCheckTallied.Parameters.AddWithValue("@electionId", electionId);
                object result = cmdCheckTallied.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int isTallied = Convert.ToInt32(result);
                    if (isTallied == 1)
                    {
                        MessageBox.Show("You have already finalized the election results.");
                        return;
                    }
                }

                // Update the election status
                string queryUpdate = "UPDATE tbl_elections SET is_tallied = 1, elec_isactive = 0 WHERE elec_id = @electionId";
                MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, mySqlConnection);
                cmdUpdate.Parameters.AddWithValue("@electionId", electionId);

                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Election Tallied Successfully. Election Results are official.");

                    // Close the current form
                    this.Close();

                    // Show Form8
                    Form8 form8 = new Form8();
                    form8.Show();
                }
                else
                {
                    MessageBox.Show("No election found with the specified ID.");
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

        
    }
}
