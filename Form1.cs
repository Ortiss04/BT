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
namespace BTC3
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=M30\SQLEXPRESS; Initial Catalog=qlsv;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = laydl("select malop, tenlop from LOP");
            comboBox1.DisplayMember = "tenlop";
            comboBox1.ValueMember = "malop";
            
        }
        DataTable laydl(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.Fill(dt);
            return dt;
        }
        void thucthi(string sql)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenlop = comboBox1.Text;
            label2.Text = "Danh sach sinh vien lop " + tenlop;
            string malop = comboBox1.SelectedValue.ToString();
            string sql = "select * from SINHVIEN where MaLop=N'" + malop + "'";
            
            dataGridView1.DataSource = laydl(sql);
        }
    }
}
