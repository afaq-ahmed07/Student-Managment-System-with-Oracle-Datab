using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project_SEM_
{
    public partial class Form4 : Form
    {
        OracleConnection con;
        private bool isCollapsed=true;
        public Form4()
        {
            InitializeComponent();

        }
        private void Form4_Load()
        {
            bunifuDataGridView1.GridColor = Color.White;
            bunifuDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            string conStr = @"DATA SOURCE=localhost:1521/xe;USER ID=f219298;PASSWORD=123";
            con = new OracleConnection(conStr);
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
            //else
            //{
            //    PanelDropDown.Height -= 10;
            //    if (PanelDropDown.Size == PanelDropDown.MinimumSize)
            //    {
            //        timer1.Stop();
            //        isCollapsed = true;

            //    }
            //}
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            //timer1.Start();
        }
        public void GetTeacherByid(string t_id)
        {
            Form4_Load();
            con.Open();
            OracleCommand getteacher = con.CreateCommand();
            getteacher.CommandText = "Select * from teacher where t_id='" + t_id + "'";
            getteacher.CommandType = CommandType.Text;
            getteacher.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getteacher);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView2.Rows.Add(row["name"].ToString(), row["gender"].ToString(), row["address"].ToString(), row["contact"].ToString(), row["username"].ToString(), row["email"].ToString());
               
            }


            con.Close();
        }

        public void GetStudentByid(string s_id)
        {
            Form4_Load();
            con.Open();
            OracleCommand getstudent = con.CreateCommand();
            getstudent.CommandText = "Select * from student where s_id='" + s_id + "'";
            getstudent.CommandType = CommandType.Text;
            getstudent.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getstudent);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView3.Rows.Add(row["name"].ToString(), row["address"].ToString(), row["contact"].ToString(), row["username"].ToString(), row["email"].ToString());
               
            }


            con.Close();
        }

        public void EditTeacherByid(string t_id)
        {
            Form4_Load();

            con.Open();
            OracleCommand editteacher = con.CreateCommand();
            editteacher.CommandText = "update teacher\r\nset name=:col1,gender=:col2,address=:col3,contact=:col4,username=:col5,email=:col6\r\nwhere t_id='" + t_id + "'";
            editteacher.CommandType = CommandType.Text;
            foreach (DataGridViewRow row in bunifuDataGridView2.Rows)
            {
                editteacher.Parameters.Add(":col1", OracleDbType.Varchar2).Value = row.Cells["Column1"].Value;
                editteacher.Parameters.Add(":col2", OracleDbType.Char).Value = row.Cells["Column2"].Value;
                editteacher.Parameters.Add(":col3", OracleDbType.Varchar2).Value = row.Cells["Column3"].Value;
                editteacher.Parameters.Add(":col4", OracleDbType.Int32).Value = row.Cells["Column5"].Value;
                editteacher.Parameters.Add(":col5", OracleDbType.Varchar2).Value = row.Cells["Column6"].Value;
                editteacher.Parameters.Add(":col6", OracleDbType.Varchar2).Value = row.Cells["Column7"].Value;

            }
            editteacher.ExecuteNonQuery();
            con.Close();
        }

        public void EditStudentByid(string s_id)
        {
            Form4_Load();
            con.Open();
            OracleCommand editstudent = con.CreateCommand();
            editstudent.CommandText = "update student set name=:col1, address=:col2, contact=:col3, username=:col4, email=:col5 where s_id=:id";
            editstudent.CommandType = CommandType.Text;

            // Add the parameters to the command object outside the loop
            editstudent.Parameters.Add(":col1", OracleDbType.Varchar2);
            editstudent.Parameters.Add(":col2", OracleDbType.Varchar2);
            editstudent.Parameters.Add(":col3", OracleDbType.Int32);
            editstudent.Parameters.Add(":col4", OracleDbType.Varchar2);
            editstudent.Parameters.Add(":col5", OracleDbType.Varchar2);
            editstudent.Parameters.Add(":id", OracleDbType.Varchar2).Value = s_id;

            foreach (DataGridViewRow row in bunifuDataGridView3.Rows)
            {
                // Check if the row is not empty and update the parameter values for each row
                    editstudent.Parameters[":col1"].Value = row.Cells["Column4"].Value;
                    editstudent.Parameters[":col2"].Value = row.Cells["Column8"].Value;
                    editstudent.Parameters[":col3"].Value = row.Cells["Column9"].Value;
                    editstudent.Parameters[":col4"].Value = row.Cells["Column10"].Value;
                    editstudent.Parameters[":col5"].Value = row.Cells["Column11"].Value;

                    editstudent.ExecuteNonQuery();
            }
            con.Close();
        }

        public void addTeacher()
        {
            Form4_Load();
            con.Open();
            OracleCommand insertteach = con.CreateCommand();
            insertteach.CommandText = "INSERT INTO Teacher (t_id, username, name, address, email, b_group, gender, contact, r_date,password) " +
                                      "VALUES (t_seq.nextval, :username, :name, :address, :email, :b_group, :gender, :contact,:r_date,:password)";
           
            insertteach.Parameters.Add("username", OracleDbType.Varchar2).Value = bunifuTextBox3.Text;
            insertteach.Parameters.Add("name", OracleDbType.Varchar2).Value = bunifuTextBox1.Text;
            insertteach.Parameters.Add("address", OracleDbType.Varchar2).Value = bunifuTextBox9.Text;
            insertteach.Parameters.Add("email", OracleDbType.Varchar2).Value = bunifuTextBox4.Text;
            insertteach.Parameters.Add("b_group", OracleDbType.Varchar2).Value = bunifuTextBox7.Text;
            insertteach.Parameters.Add("gender", OracleDbType.Char).Value = comboBox7.SelectedItem.ToString();
            insertteach.Parameters.Add("contact", OracleDbType.Int32).Value = bunifuTextBox8.Text;
            insertteach.Parameters.Add("r_date", OracleDbType.Varchar2).Value = bunifuDatePicker2.Value;
            insertteach.Parameters.Add("password", OracleDbType.Varchar2).Value = bunifuTextBox2.Text;

            insertteach.ExecuteNonQuery();
            MessageBox.Show("Teacher added successfully!");
            bunifuTextBox3.Clear();
            bunifuTextBox1.Clear();
            bunifuTextBox9.Clear();
            bunifuTextBox4.Clear();
            bunifuTextBox7.Clear();
            bunifuTextBox8.Clear();
            bunifuTextBox2.Clear();

            con.Close();
        }

        public void addStudent()
        {
            //Form4_Load();
            string conStr = @"DATA SOURCE=localhost:1521/xe;USER ID=f219298;PASSWORD=123";
            con = new OracleConnection(conStr);
            con.Open();
           
            OracleCommand insertstudent = con.CreateCommand();
            OracleCommand insertattendence = con.CreateCommand();
            insertstudent.CommandText = "INSERT INTO Student (s_id, username, name, address, email, b_group, gender, contact, r_date,fee,password) " +
                                      "VALUES (s_seq.nextval, :username, :name, :address, :email, :b_group, :gender, :contact,:r_date,:fee,:password)";

            insertstudent.Parameters.Add("username", OracleDbType.Varchar2).Value = bunifuTextBox16.Text;
            insertstudent.Parameters.Add("name", OracleDbType.Varchar2).Value = bunifuTextBox18.Text;
            insertstudent.Parameters.Add("address", OracleDbType.Varchar2).Value = bunifuTextBox10.Text;
            insertstudent.Parameters.Add("email", OracleDbType.Varchar2).Value = bunifuTextBox15.Text;
            insertstudent.Parameters.Add("b_group", OracleDbType.Varchar2).Value = bunifuTextBox12.Text;
            insertstudent.Parameters.Add("gender", OracleDbType.Char).Value = comboBox5.SelectedItem.ToString();
            insertstudent.Parameters.Add("contact", OracleDbType.Int32).Value = bunifuTextBox11.Text;
            insertstudent.Parameters.Add("r_date", OracleDbType.Varchar2).Value = bunifuDatePicker1.Value;
            insertstudent.Parameters.Add("fee", OracleDbType.Char).Value = comboBox6.SelectedItem.ToString();
            insertstudent.Parameters.Add("password", OracleDbType.Varchar2).Value = bunifuTextBox5.Text;
            insertstudent.ExecuteNonQuery();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT s_seq.CURRVAL FROM dual";
            int sequenceValue = Convert.ToInt32(cmd.ExecuteScalar());

            insertattendence.CommandText = "Insert Into attendence(s_id,n_attend,total,percent) Values (:sequenceValue,0,20,0)";
            insertattendence.Parameters.Add(":sequenceValue", OracleDbType.Int32).Value = sequenceValue;
            insertattendence.ExecuteNonQuery();

            MessageBox.Show("Student added successfully!");
            bunifuTextBox18.Clear();
            bunifuTextBox16.Clear();
            bunifuTextBox10.Clear();
            bunifuTextBox15.Clear();
            bunifuTextBox12.Clear();
            bunifuTextBox5.Clear();
            bunifuTextBox11.Clear();
            con.Close();

        }

        public void addClass()
        {
            Form4_Load();
            con.Open();
            OracleCommand insertclass = con.CreateCommand();
            insertclass.CommandText = "addclass";
            insertclass.CommandType = CommandType.StoredProcedure;
            insertclass.Parameters.Add("cname", OracleDbType.Varchar2).Value = bunifuTextBox21.Text;
            insertclass.Parameters.Add("tid", OracleDbType.Varchar2).Value = comboBox1.SelectedItem.ToString();
            insertclass.ExecuteNonQuery();
            MessageBox.Show("Class added successfully!");
            con.Close();

        }

        public void GetallTeacher()
        {
            Form4_Load();
            con.Open();
            OracleCommand getteacher = con.CreateCommand();
            getteacher.CommandText = "(select t_id from teacher)";
            getteacher.CommandType = CommandType.Text;
            getteacher.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getteacher);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                comboBox1.Items.Add(row["t_id"].ToString());
                
            }
            con.Close();
        }

        public void Getallclass()
        {
            Form4_Load();
            con.Open();
            OracleCommand getteacher = con.CreateCommand();
            getteacher.CommandText = "(select * from allclass)";
            getteacher.CommandType = CommandType.Text;
            getteacher.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getteacher);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView1.Rows.Add(row["c_id"].ToString(), row["c_name"].ToString(), row["t_id"].ToString());
                comboBox2.Items.Add(row["c_id"].ToString());

            }
            con.Close();
        }

        public void Getallclasscombo()
        {
            Form4_Load();
            con.Open();
            OracleCommand getteacher = con.CreateCommand();
            getteacher.CommandText = "(select * from class)";
            getteacher.CommandType = CommandType.Text;
            getteacher.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getteacher);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                comboBox2.Items.Add(row["c_id"].ToString());
                comboBox3.Items.Add(row["c_name"].ToString());
                comboBox4.Items.Add(row["c_id"].ToString());
            }
            con.Close();
        }

        public void addStudenttoClass()
        {
            Form4_Load();
            con.Open();
            OracleCommand editstudent = con.CreateCommand();
            editstudent.CommandText = "update student set c_id=:col1 where s_id=:id";
            editstudent.CommandType = CommandType.Text;

            // Add the parameters to the command object outside the loop
            editstudent.Parameters.Add(":col1", OracleDbType.Varchar2);
            editstudent.Parameters.Add(":id", OracleDbType.Varchar2).Value =bunifuTextBox22.Text;

            editstudent.Parameters[":col1"].Value = comboBox2.SelectedItem.ToString();
                
                editstudent.ExecuteNonQuery();
            con.Close();
        }

        public void GetStudentbyClass(string select)
        {
            Form4_Load();
            con.Open();
            OracleCommand getteacher = con.CreateCommand();
            getteacher.CommandText = "SELECT s.* FROM student s, class c WHERE c.c_id = s.c_id AND c.c_name = :className";
            getteacher.Parameters.Add("className", OracleDbType.Varchar2).Value = select;
            getteacher.CommandType = CommandType.Text;
            getteacher.ExecuteNonQuery();
            OracleDataAdapter adapter = new OracleDataAdapter(getteacher);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                bunifuDataGridView4.Rows.Add(row["s_id"].ToString(), row["name"].ToString(), row["email"].ToString(), row["contact"].ToString(), row["address"].ToString());
            }
            con.Close();
        }
        public void addClass_schedule()
        {
            Form4_Load();
            con.Open();
            OracleCommand insertclass = con.CreateCommand();
            insertclass.CommandText = "insert_schedule";
            insertclass.CommandType = CommandType.StoredProcedure;
            foreach (DataGridViewRow row in bunifuDataGridView5.Rows)
            {
                insertclass.Parameters.Add("s_time", OracleDbType.Varchar2).Value = row.Cells["Column14"].Value;
                insertclass.Parameters.Add("e_time", OracleDbType.Varchar2).Value = row.Cells["Column15"].Value;
                insertclass.Parameters.Add("day", OracleDbType.Varchar2).Value = row.Cells["Column16"].Value;
                insertclass.Parameters.Add("loc", OracleDbType.Varchar2).Value = row.Cells["Column17"].Value;
                insertclass.Parameters.Add("c_id", OracleDbType.Int32).Value = comboBox4.SelectedItem.ToString();
                insertclass.ExecuteNonQuery();
                break;
            }
            MessageBox.Show("Class_schedule added successfully!");
            con.Close();

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
        private void bunifuButton20_Click(object sender, EventArgs e)
        {
            bunifuDataGridView2.Rows.Clear();
           GetTeacherByid(bunifuTextBox19.Text);

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            EditTeacherByid(bunifuTextBox19.Text);
            bunifuDataGridView2.Rows.Clear();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            bunifuDataGridView3.Rows.Clear();
            GetStudentByid(bunifuTextBox20.Text);
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            EditStudentByid(bunifuTextBox20.Text);
            bunifuDataGridView3.Rows.Clear();
        }

        private void bunifuButton17_Click(object sender, EventArgs e)
        {
            addTeacher();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage7.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage6.Show();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage6.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage7.Show();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage8.Show();
            GetallTeacher();
        }


        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage8.Hide();
            tabPage9.Show();
            Getallclass();
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Show();
            Getallclasscombo();

        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage12.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Show();
            Getallclasscombo();

        }

        private void bunifuButton13_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage5.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Show();
            Getallclasscombo();

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage3.Hide();
            tabPage2.Hide();
            tabPage4.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage5.Show();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        private void bunifuLabel6_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            tabPage4.Hide();
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage5.Hide();
            tabPage3.Show();

        }

        private void bunifuTextBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel26_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage5.Hide();
            tabPage3.Hide();
            tabPage4.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            tabPage2.Hide();
            tabPage6.Hide();
            tabPage7.Hide();
            tabPage8.Hide();
            tabPage9.Hide();
            tabPage10.Hide();
            tabPage11.Hide();
            tabPage12.Hide();
            tabPage5.Hide();
            tabPage3.Hide();
            tabPage4.Hide();
            tabPage1.Show();
        }

        private void bunifuButton18_Click(object sender, EventArgs e)
        {
            addStudent();
        }

        private void bunifuLabel6_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void PanelDropDown_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPages1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel13_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel12_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel6_Click_2(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel14_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel15_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel16_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel17_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel18_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel19_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel21_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton19_Click(object sender, EventArgs e)
        {
            addClass();
            bunifuTextBox21.Clear();
        }

        private void bunifuTextBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuSeparator9_Click(object sender, EventArgs e)
        {

        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            addStudenttoClass();
            MessageBox.Show("Student Added To Class");
        }

        private void bunifuLabel24_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel25_Click(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox3.SelectedItem.ToString();
            bunifuDataGridView4.Rows.Clear();
            GetStudentbyClass(selectedValue);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage12_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel27_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator12_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            addClass_schedule();
            bunifuDataGridView5.Rows.Clear();
        }

        private void bunifuButton15_Click(object sender, EventArgs e)
        {
            dropdown();
        }

        private void bunifuPictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView5_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            tabPage6.Hide();
            tabPage2.Show();
        }

        private void bunifuButton16_Click(object sender, EventArgs e)
        {
            tabPage7.Hide();
            tabPage2.Show();
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            tabPage8.Hide();
            tabPage2.Show();
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            tabPage9.Hide();
            tabPage5.Show();
        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            tabPage10.Hide();
            tabPage5.Show();
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            tabPage11.Hide();
            tabPage5.Show();
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            tabPage12.Hide();
            tabPage5.Show();
        }

        private void bunifuLabel29_Click(object sender, EventArgs e)
        {
            this.Hide(); // hide the current form
            Form1 form1 = new Form1(); // create a new instance of Form1
            form1.Show(); // show Form1
        }

        private void bunifuLabel29_MouseEnter(object sender, EventArgs e)
        {
            bunifuLabel29.ForeColor = Color.Gray;
        }

        private void bunifuLabel29_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel10.ForeColor = Color.Transparent;
        }
    }
}
