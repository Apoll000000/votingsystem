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
    public partial class Form18 : KryptonForm
    {
        private int electionId;

        public Form18(int electionId)
        {
            InitializeComponent();
            this.electionId = electionId;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {


            Form15 form15 = new Form15(electionId);
             form15.Show();
            this.Close();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string queryDelete = "DELETE FROM tbl_elections WHERE elec_id = @electionId";
                MySqlCommand cmdDelete = new MySqlCommand(queryDelete, mySqlConnection);
                cmdDelete.Parameters.AddWithValue("@electionId", electionId);

                int rowsAffected = cmdDelete.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Election Deleted Successfully");

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
