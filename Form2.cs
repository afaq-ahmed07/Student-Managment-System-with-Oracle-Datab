using Database_Project_SEM_.Properties;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project_SEM_
{
    public partial class Form2 : Form
    {
        OracleConnection con;
       
        private bool isCollapsed=true;
        public Form2()
        {
            InitializeComponent();
            tabPage1.Hide();
            tabPage2.Hide();
            bunifuPanel1.Size = bunifuPanel1.MinimumSize;
        }
        private void Form2_Load()
        {
            string conStr = @"DATA SOURCE=localhost:1521/xe;USER ID=f219298;PASSWORD=123";
            con = new OracleConnection(conStr);
            
        }
      public string emailtext { get; set; }//thsi will get name from sign in page

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (isCollapsed)
            //{
            //    bunifuPanel1.Height += 10;
            //    if (bunifuPanel1.Size == bunifuPanel1.MaximumSize)
            //    {
            //        timer1.Stop();
            //        isCollapsed = false;
            //    }
            //}
            //else
            //{
            //    bunifuPanel1.Height -= 10;
            //    if (bunifuPanel1.Size == bunifuPanel1.MinimumSize)
            //    {
            //        timer1.Stop();
            //        isCollapsed = true;
            //    }
            //}
        }


        public void GetAttendanceByEmail()
        {
            
            Form2_Load();
            con.Open();
            OracleCommand getattendence = con.CreateCommand();
            getattendence.CommandText = "SELECT a.* FROM student s, attendence a WHERE a.s_id = s.s_id AND s.email = '" + emailtext + "'";
            getattendence.CommandType = CommandType.Text;
            getattendence.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getattendence);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView2.Rows.Add(row["n_attend"].ToString(), row["total"].ToString());
                int total = Convert.ToInt32(row["total"].ToString());
                int n_attend= Convert.ToInt32(row["n_attend"].ToString());
                int percentage = (int)((float)n_attend / total * 100);
                bunifuCircleProgress1.Value =percentage;
            }
            con.Close();
        }



        public void GetStudentByEmail()
        {
            Form2_Load();
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "SELECT * FROM student where email='" + emailtext + "'";
            getstudent.CommandType = CommandType.Text;
            getstudent.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getstudent);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {

                rollno.Text = row["s_id"].ToString();
                name.Text = row["name"].ToString();
                address.Text = row["address"].ToString();
                b_group.Text = row["b_group"].ToString();
                gender.Text = row["gender"].ToString();
                contact.Text = row["contact"].ToString();
                r_date.Text = row["r_date"].ToString();
                fee.Text = row["fee"].ToString();
                c.Text = row["c_id"].ToString();
                email.Text = row["email"].ToString();
            }
            con.Close();
        }

        public void GetScheduleByEmail()
        {
            Form2_Load();
            con.Open();
            OracleCommand getschedule = con.CreateCommand();
            getschedule.CommandText = "SELECT c.* FROM class_schedule c,student s where c.c_id=s.c_id AND s.email='" + emailtext + "'";
            getschedule.CommandType = CommandType.Text;
            getschedule.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getschedule);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach(DataRow row in table.Rows)
            {
                bunifuDataGridView1.Rows.Add(row["s_time"].ToString(), row["e_time"].ToString(), row["day"].ToString(), row["loc"].ToString());
            }
            con.Close();
        }

        private void dropdown() {
            if (isCollapsed)
            {
                bunifuPanel1.Height = bunifuPanel1.MaximumSize.Height;
            }
            else
            {
                bunifuPanel1.Height = bunifuPanel1.MinimumSize.Height;
            }

            isCollapsed = !isCollapsed;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage3.Hide();
            tabPage2.Show();
            GetScheduleByEmail();
        }

      

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel12_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void rollno_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Show();
            GetAttendanceByEmail();
           // bunifuProgressBar2.Value = percentage;
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage3.Hide();
            tabPage2.Show();
            GetScheduleByEmail();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            //timer1.Start();
            dropdown();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            tabPage3.Hide();
            tabPage2.Hide();
            tabPage1.Show();
            GetStudentByEmail();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuProgressBar2_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuProgressBar.ProgressChangedEventArgs e)
        {
            
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuCircleProgress.ProgressChangedEventArgs e)
        {

        }

        private void bunifuLabel3_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // hide the current form
            Form1 form1 = new Form1(); // create a new instance of Form1
            form1.Show(); // show Form1
        }

        private void bunifuLabel3_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel3.ForeColor = Color.Gray;
        }

        private void bunifuLabel3_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel3.ForeColor = Color.Transparent;
        }

        private void b_group_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
