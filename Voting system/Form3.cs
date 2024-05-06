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
    public partial class Form3 : KryptonForm
    {
        private int electionId;

        // Constructor with one parameter for the election ID
        public Form3(int electionId)
        {
            InitializeComponent();
            this.electionId = electionId;

            DisplayCandidates();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // You can use the electionId value here if needed

        }

        private void DisplayCandidates()
        {
            MessageBox.Show("MATULOG KANA GUMANA NA BUKAS MO NA AYUSIN");
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(electionId);
            form4.Show();
            this.Close();
        }
    }
}
