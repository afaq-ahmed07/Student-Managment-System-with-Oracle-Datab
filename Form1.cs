using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI.WinForms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project_SEM_
{
    public partial class Form1 : Form
    {
       OracleConnection con;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE=localhost:1521/xe;USER ID=f219298;PASSWORD=123";
            con = new OracleConnection(conStr);
        }

        public void checkemailstudent()
        {
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "SELECT * FROM student WHERE email=:email AND password =:password";
            getstudent.Parameters.Add("email", OracleDbType.Varchar2).Value = bunifuTextBox1.Text;
            getstudent.Parameters.Add("password", OracleDbType.Varchar2).Value = bunifuTextBox2.Text;
            getstudent.CommandType = CommandType.Text;

            int count = 0;
            using (OracleDataReader reader = getstudent.ExecuteReader())
            {
                while (reader.Read())
                {
                    count++;
                    // You can access the columns of the current row using the
                    // indexer or the GetXXX() methods of the reader object.
                }
            }

            if (count > 0)
            {
                Form2 form2 = new Form2();
                form2.emailtext = bunifuTextBox1.Text;
                this.Hide();
                form2.Show();
                form2.GetStudentByEmail();
            }
            else
            {
                MessageBox.Show("Your Password or Email is Incorrect");
            }

            con.Close();
        }

        public void checkemailteacher()
        {
            con.Open();
            OracleCommand getteacher = con.CreateCommand();
            getteacher.CommandText = "SELECT * FROM teacher WHERE email=:email AND password =:password";
            getteacher.Parameters.Add("email", OracleDbType.Varchar2).Value = bunifuTextBox1.Text;
            getteacher.Parameters.Add("password", OracleDbType.Varchar2).Value = bunifuTextBox2.Text;
            getteacher.CommandType = CommandType.Text;

            int count = 0;
            using (OracleDataReader reader = getteacher.ExecuteReader())
            {
                while (reader.Read())
                {
                    count++;
                }
            }
            if (count > 0)
            {
                Form3 form3 = new Form3();
                form3.emailtext1 = bunifuTextBox1.Text;
                this.Hide();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Your Password or Email is Incorrect");
            }

            con.Close();
        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            string studentId = bunifuTextBox1.Text;


        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.Text.Substring(0, 1) == "s")
            {
                checkemailstudent();
            }
            else if (bunifuTextBox1.Text.Substring(0, 1) == "t")
            {
                checkemailteacher();
            }
            else if (bunifuTextBox1.Text=="admin"&& bunifuTextBox2.Text=="123")
            {
                Form4 form4 = new Form4();
                this.Hide();
                form4.Show();
            }
            else {
                MessageBox.Show("INVALID INFORMATION");
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuTransition1_TransfromNeeded(object sender, Bunifu.UI.WinForms.BunifuTransition.TransfromNeededEventArg e)
        {

        }

        private void bunifuTransition1_AllAnimationsCompleted(object sender, EventArgs e)
        {

        }
    }
}
