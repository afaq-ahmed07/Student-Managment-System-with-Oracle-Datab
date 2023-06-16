using Bunifu.UI.WinForms;
using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using Bunifu.UI.WinForms.BunifuTextbox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project_SEM_
{
    public partial class Form3 : Form
    {
        OracleConnection con;
        private bool isCollapsed=true;
        public string emailtext1 { get; set; }

        public Form3()
        {
            InitializeComponent();
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            bunifuDataGridView1.GridColor = Color.White;
            bunifuDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            string conStr = @"DATA SOURCE=localhost:1521/xe;USER ID=f219298;PASSWORD=123";
            con = new OracleConnection(conStr);
            GetteacherByEmail();
            //

        }
       

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (isCollapsed)
            //{

            //    PanelDropDown.Height += 10;
            //    if (PanelDropDown.Size == PanelDropDown.MaximumSize)
            //    {
            //        timer1.Stop();
            //        isCollapsed = false;

            //    }
            //}
            //else {
            //    PanelDropDown.Height -= 10;
            //    if (PanelDropDown.Size == PanelDropDown.MinimumSize) {
            //        timer1.Stop();
            //        isCollapsed = true;
                
            //    }
            //}
        }
       

        public void GetStudentByClass(string c_name)
        {
           
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "SELECT s.* FROM student s, class c, teacher t " +
                     "WHERE s.c_id = c.c_id AND t.email = '" + emailtext1 + "' AND c.c_name = '" + c_name + "'";
            getstudent.CommandType = CommandType.Text;
            getstudent.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getstudent);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView1.Rows.Add(row["s_id"].ToString(), row["name"].ToString(), row["email"].ToString(), row["contact"].ToString(), row["address"].ToString());
               // bunifuTextBox1.Text = row["name"].ToString();
            }


            con.Close();
        }

        public void GetteacherByEmail()
        { 
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "SELECT * FROM teacher where email='" + emailtext1 + "'";
            getstudent.CommandType = CommandType.Text;
            getstudent.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getstudent);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {

                id.Text = row["t_id"].ToString();
                name.Text = row["name"].ToString();
                address.Text = row["address"].ToString();
                b_group.Text = row["b_group"].ToString();
                gender.Text = row["gender"].ToString();
                contact.Text = row["contact"].ToString();
                r_date.Text = row["r_date"].ToString();
                email.Text = row["email"].ToString();
            }
            con.Close();
        }
        public void GetStudentinAttend(string c_name)
        {
            
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "SELECT s.* FROM student s, class c, teacher t " +
                     "WHERE s.c_id = c.c_id AND t.email = '" + emailtext1 + "' AND c.c_name = '" + c_name + "'";
            getstudent.CommandType = CommandType.Text;
            getstudent.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getstudent);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView2.Rows.Add(row["s_id"].ToString(), row["name"].ToString());
            }


            con.Close();
        }

        public void GetReportByEmail()
        {
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "select s.s_id,s.name,a.n_attend,a.total,a.percent from student s,attendence a,class c,teacher t\r\n" +
                "where s.s_id=a.s_id AND s.c_id=c.c_id AND c.t_id=t.t_id AND t.email='" + emailtext1 + "'";
            getstudent.CommandType = CommandType.Text;
            getstudent.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getstudent);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView4.Rows.Add(row["s_id"].ToString(), row["name"].ToString(), row["n_attend"].ToString(), row["total"].ToString(), row["percent"].ToString());
            }


            con.Close();
        }

        public DataTable GetClassbyEmail()
        {

            con.Open();
            OracleCommand getclass = con.CreateCommand();
            getclass.CommandText = "select c.c_name from class c,teacher t where c.t_id=t.t_id AND t.email='" + emailtext1 + "'";
            getclass.CommandType = CommandType.Text;
            getclass.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getclass);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {

                bunifuDataGridView3.Rows.Add(row["c_name"].ToString());
                comboBox2.Items.Add(row["c_name"].ToString());
            }
            con.Close();
            return table;
        }

        public void GetClassincombo()
        {
            con.Open();
            OracleCommand getclass = con.CreateCommand();
            getclass.CommandText = "select c.c_name from class c,teacher t where c.t_id=t.t_id AND t.email='" + emailtext1 + "'";
            getclass.CommandType = CommandType.Text;
            getclass.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getclass);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {

                comboBox2.Items.Add(row["c_name"].ToString());
            }
            con.Close();
        }

        public void addstudentattend()
        {

        }
        public void addAttendence(string cellvalue, DataGridViewCellEventArgs e)
        {
            con.Open();
            string columnName = bunifuDataGridView2.Columns[e.ColumnIndex].Name;
            OracleCommand insertattendence = con.CreateCommand();
            insertattendence.CommandText = "update Attendence set n_attend=n_attend + :n_attend where s_id=:s_id";
            insertattendence.Parameters.Add("n_attend", OracleDbType.Int32);
            insertattendence.Parameters.Add("s_id", OracleDbType.Int32);
            DataGridViewRow row = bunifuDataGridView2.Rows[e.RowIndex];
            if (cellvalue == "P")
            {
                row.Cells[columnName].Value = 1;
            }
            else if (cellvalue == "A")
            {
                row.Cells[columnName].Value = 0;
            }
            insertattendence.Parameters["n_attend"].Value = row.Cells[columnName].Value;
            insertattendence.Parameters["s_id"].Value = row.Cells["Rollno"].Value;
            insertattendence.ExecuteNonQuery();
            MessageBox.Show("Attendance Added");
            con.Close();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            //timer1.Start();
            dropdown();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage2.Show();
            GetClassincombo();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage2.Hide();
            tabPage1.Show();
            GetteacherByEmail();
        }

        private void dropdown()
        {
            if (isCollapsed)
            {
                PanelDropDown.Height = PanelDropDown.MaximumSize.Height;
            }
            else
            {
                PanelDropDown.Height = PanelDropDown.MinimumSize.Height;
            }

            isCollapsed = !isCollapsed;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {

            Bunifu.UI.WinForms.BunifuDataGridView dataGridView = bunifuDataGridView2; // Replace "myBunifuDataGridView" with the actual name of your DataGridView control

            string columnName = Interaction.InputBox("Enter Date:", "Add Column"); // Prompt the user for the column name

            if (!string.IsNullOrEmpty(columnName))
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn(); // Create a new column
                column.HeaderText = columnName; // Set the column header text to the entered column name
                dataGridView.Columns.Add(column); // Add the column to the DataGridView control
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            tabPage2.Hide();
            tabPage4.Hide();
            tabPage3.Hide();
            tabPage1.Hide();
            tabPage5.Show();
           DataTable table=GetClassbyEmail();
            foreach (DataRow row in table.Rows)
            {
                comboBox1.Items.Add(row["c_name"].ToString());
            }
   
            
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            tabPage2.Hide();
            tabPage4.Hide();
            tabPage1.Hide();
            tabPage5.Hide();
            tabPage3.Show();
            GetClassbyEmail();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage1.Hide();
            tabPage5.Hide();
            tabPage4.Show();
            GetReportByEmail();
        }

        private void bunifuDataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            bunifuDataGridView1.Rows.Clear();
            GetStudentByClass(selectedValue);
        }

        private void bunifuDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuDataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the edited cell is in the newly created column
            if (e.ColumnIndex == bunifuDataGridView2.Columns.Count - 1)
            {
                // Get the edited cell's value
                string cellValue = bunifuDataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                addAttendence(cellValue, e);    
            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox2.SelectedItem.ToString();
            bunifuDataGridView1.Rows.Clear();
            GetStudentinAttend(selectedValue);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void r_date_TextChanged(object sender, EventArgs e)
        {

        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            Bunifu.UI.WinForms.BunifuDataGridView dataGridView = bunifuDataGridView2; // Replace "myBunifuDataGridView" with the actual name of your DataGridView control

            string columnName = Interaction.InputBox("Enter Date:", "Add Column"); // Prompt the user for the column name

            if (!string.IsNullOrEmpty(columnName))
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn(); // Create a new column
                column.HeaderText = columnName; // Set the column header text to the entered column name
                dataGridView.Columns.Add(column); // Add the column to the DataGridView control

            }
            
        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {
            this.Hide(); // hide the current form
            Form1 form1 = new Form1(); // create a new instance of Form1
            form1.Show(); // show Form1
        }

        private void bunifuLabel10_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel10.ForeColor = Color.Gray;
        }

        private void bunifuLabel10_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel10.ForeColor = Color.Transparent;
        }
    }
}
