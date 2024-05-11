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
    public partial class Form5 : KryptonForm
    {
        private string student_id;
        public Form5(string student_id)
        {
            InitializeComponent();
            this.student_id = student_id;

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            LNU form2 = new LNU(student_id);

            // Show Form3
            form2.Show();

            // Close the current form
            this.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
