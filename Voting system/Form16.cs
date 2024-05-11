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
        public Form16()
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
    }
}
