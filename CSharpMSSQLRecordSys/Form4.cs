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

namespace CSharpMSSQLRecordSys
{
    public partial class Form4 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=ALLMANKIND\\MSSQLSERVER8; Database=db_csrecordsys; Integrated Security=True;");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string currentid = "";

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            GetRecords();
        }

        public void GetRecords()
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("select * from tbl_records", conn);
            adapter.Fill(ds, "tbl_records");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_records";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("insert into tbl_records (firstname, lastname, age, gender) VALUES " +
             "('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "')", conn);
            adapter.Fill(ds, "tbl_records");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Text = "";
            GetRecords();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();

            ds = new DataSet();
            adapter = new SqlDataAdapter("delete from tbl_records where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_records");
            GetRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();
            textBox1.Text = dataGridView1[1, i].Value.ToString();
            textBox2.Text = dataGridView1[2, i].Value.ToString();
            textBox3.Text = dataGridView1[3, i].Value.ToString();
            comboBox1.Text = dataGridView1[4, i].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("update tbl_records set firstname = '" + textBox1.Text +
                "', lastname = '" + textBox2.Text +
                "', age = '" + textBox3.Text +
                "', gender = '" + comboBox1.Text +
                "' where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_records");
            GetRecords();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Text = "";
        }
    }
}
