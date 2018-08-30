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
    public partial class Form3 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=ALLMANKIND\\MSSQLSERVER8; Database=db_csrecordsys; Integrated Security=True;");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string currentid = "";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            GetRecords();
        }

        public void GetRecords()
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("select * from tbl_accounts", conn);
            adapter.Fill(ds, "tbl_accounts");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_accounts";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("insert into tbl_accounts (username, password, privilege) VALUES " +
             "('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')", conn);
            adapter.Fill(ds, "tbl_accounts");
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            GetRecords();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();

            ds = new DataSet();
            adapter = new SqlDataAdapter("delete from tbl_accounts where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_accounts");
            GetRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();
            textBox1.Text = dataGridView1[1, i].Value.ToString();
            textBox2.Text = dataGridView1[2, i].Value.ToString();
            comboBox1.Text = dataGridView1[3, i].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("update tbl_accounts set username = '" + textBox1.Text + 
                "', password = '" + textBox2.Text + 
                "', privilege = '" + comboBox1.Text + 
                "' where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_accounts");
            GetRecords();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
        }
    }
}
