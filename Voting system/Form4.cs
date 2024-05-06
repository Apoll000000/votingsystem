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
    public partial class Form4 : KryptonForm
    {
        private int electionId;
        public Form4(int electionId)
        {
            InitializeComponent();
            this.electionId = electionId;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(electionId);

            // Show Form3
            form3.Show();

            // Close the current form
            this.Close();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();

            // Show Form3
            form5.Show();

            // Close the current form
            this.Close();
        }
    }
}
