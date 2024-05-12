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
    public partial class Form9 : KryptonForm
    {

        private byte[] imageData;

        public Form9()
        {
            InitializeComponent();
        }


        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Close();
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

        private void SaveToDatabase(string studentID, string name, string course, byte[] imageData)
        {
            string mysqlCon = "server=127.0.0.1; user=root; database=db_votingsystem; password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mySqlConnection.Open();
                string query = "INSERT INTO tbl_accounts (student_id, student_name, course, picture) VALUES (@StudentID, @Name, @Course, @ImageData)";
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Course", course);
                    command.Parameters.AddWithValue("@ImageData", imageData);
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
            bunifuTextBox2.Text = "";
            bunifuTextBox1.Text = "";
            bunifuTextBox4.Text = "";
            imageData = null;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }


        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bunifuTextBox2.Text) && !string.IsNullOrEmpty(bunifuTextBox1.Text) && bunifuDropdown3.selectedIndex >= 0 && imageData != null)
            {
                string selectedCourse = bunifuDropdown3.selectedValue?.ToString();
                // Save voter details and image to database
                SaveToDatabase(bunifuTextBox2.Text, bunifuTextBox1.Text, selectedCourse, imageData);
                MessageBox.Show("Voter saved successfully.");
                ClearFields();
                Form10 form10 = new Form10();
                form10.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill all fields and select an image.");
            }
        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {

        }
    }
}