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
    public partial class Lyceum : KryptonForm
    {
        public Lyceum()
        {
            InitializeComponent();
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                MessageBox.Show("CONNECTION SUCCESS");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                mySqlConnection.Close();
            }
        }

        private void ReadData()
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            string student_id = bunifuTextBox1.Text;

            MySqlCommand cmd = null; // Declare cmd variable once in the method scope

            try
            {
                mySqlConnection.Open();
                string query = "SELECT * FROM tbl_accounts WHERE student_id = @StudentID";
                cmd = new MySqlCommand(query, mySqlConnection);
                cmd.Parameters.AddWithValue("@StudentID", student_id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Match found, proceed to the next form
                    LNU form2 = new LNU(student_id);
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    // No match found, show an error message
                    MessageBox.Show("Invalid student ID. Please try again.");
                }

                reader.Close();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            ReadData();


        }

        private void Lyceum_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();

            // Show Form2
            form6.Show();

            // Optionally, you can hide MainForm
            this.Hide();
        }
    }
}
