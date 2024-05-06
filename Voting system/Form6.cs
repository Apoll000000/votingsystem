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


namespace Voting_system
{
    public partial class Form6 : KryptonForm
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string user = "admin";
            string pass = "admin";

            string enteredUser = bunifuTextBox1.Text;
            string enteredPass = bunifuTextBox2.Text;

            if (enteredUser == user && enteredPass == pass)
            {
                Form8 form8 = new Form8();
                form8.Show();
                this.Close();
            } else
            {
                Form7 form7 = new Form7();
                form7.Show();
                this.Close();
            }
             
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Lyceum form1 = new Lyceum();
            form1.Show();
            this.Close();
        }
    }
}
