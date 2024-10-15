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
    public partial class Form2 : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source =M30\SQLEXPRESS;Initial Catalog=qlsv;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }
        DataTable laydl(string sql)
        {
            DataTable dt = new DataTable()  ;
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
        private void Form2_Load(object sender, EventArgs e)
        {
            cblop.DataSource = laydl("select malop, tenlop from LOP");
            cblop.DisplayMember = "tenlop";
            cblop.ValueMember = "malop";
            for(int i=1;i<32;i++)
            {
                cbngay.Items.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                cbthang.Items.Add(i);
            }
        }

        private void cblop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenlop = cblop.Text;
            label2.Text = "Danh sach sinh vien lop " + tenlop;
            string malop = cblop.SelectedValue.ToString();
            string sql = "select * from SINHVIEN where MaLop=N'" + malop + "'";

            dataGridView1.DataSource = laydl(sql);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = dataGridView1.CurrentRow.Index;
            txtmasv.Text = dataGridView1.Rows[vt].Cells[0].Value.ToString();
            string tensv= dataGridView1.Rows[vt].Cells[1].Value.ToString();

            DateTime date = DateTime.Parse(dataGridView1.Rows[vt].Cells[3].Value.ToString());
            cbngay.Text = date.Day.ToString();
            cbthang.Text = date.Month.ToString();
            txtnam.Text = date.Year.ToString();
           
            
            txtdiachi.Text = dataGridView1.Rows[vt].Cells[4].Value.ToString();
            txtsdt.Text= dataGridView1.Rows[vt].Cells[5].Value.ToString();
            txttensv.Text = tensv;
            //MessageBox.Show(dataGridView1.Rows[vt].Cells[2].Value.ToString());
            if (dataGridView1.Rows[vt].Cells[2].Value.ToString() == "True")
                checkBox1.Checked = true;
            else checkBox1.Checked = false;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {

        }
    }
}
