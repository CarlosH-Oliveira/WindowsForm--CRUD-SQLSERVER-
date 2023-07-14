using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace testApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string connectionString = "Data Source=DESKTOP-UML8NPO;Initial Catalog=testappdb;User ID=sa;Password=32300508Az.;";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd;
        SqlDataReader read;
        bool mode = true;
        string sql;

        private void refreshGridView()
        {
            try
            {
                sql = "SELECT * FROM [Students];";
                conn.Open();

                cmd = new SqlCommand(sql, conn);

                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                conn.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshGridView();
        }

        private void getID(String id)
        {
            try
            {
                sql = "SELECT * FROM [Students] WHERE id = '"+id+"';";
                conn.Open();

                cmd = new SqlCommand(sql, conn);

                read = cmd.ExecuteReader();

                while (read.Read())
                {
                    txtName.Text = read[1].ToString();
                    txtCourse.Text = read[2].ToString();
                    txtFee.Text = read[3].ToString();
                }
                conn.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if(mode == true)
            {
                sql = "INSERT INTO [Students](Name, Course, Fee) VALUES(@name, @course, @fee);";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                conn.Close();
                refreshGridView();
                txtName.Focus();
            }
            else
            {
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "UPDATE [Students] SET Name = @name, Course = @course, Fee = @fee where ID = @id;";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated");


                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                conn.Close();
                refreshGridView();
                txtName.Focus();
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (e.ColumnIndex == dataGridView1.Columns["EDIT"].Index && e.RowIndex >= 0)
            {
                button1.Text = "Update";
                mode = false;
                getID(id);
            }
            if (e.ColumnIndex == dataGridView1.Columns["DELETE"].Index && e.RowIndex >= 0)
            {
                sql = "DELETE FROM [Students] WHERE ID = @id;";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
                refreshGridView();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            mode = true;
            button1.Text = "Add";
        }
    }
}
