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
    public partial class Form3 : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source =M30\SQLEXPRESS;Initial Catalog=khoasv;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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
        private void cbngay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void xlsangmo(bool gt)
        {
            btnghi.Enabled = gt;
            btnkhong.Enabled = gt;
            btnthem.Enabled = !gt;
            btnthoat.Enabled = !gt;
        }
        void updateTreeView()
        {
            tvw.Nodes.Clear();
            DataTable dt = laydl("select makh,tenkhoa from khoa");
            cbkhoa.DataSource = dt;
            cbkhoa.DisplayMember = "tenkhoa";
            cbkhoa.ValueMember = "makh";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = dt.Rows[i][1].ToString();
                node.Tag = dt.Rows[i][0].ToString();
                tvw.Nodes.Add(node);
                string sql1 = "select * from sinhvien where makh='" + node.Tag + "'";
                DataTable DT1 = laydl(sql1);
                for (int j = 0; j < DT1.Rows.Count; j++)
                {
                    TreeNode nod = new TreeNode();
                    nod.Text = "Mã SV: " + DT1.Rows[j]["masv"].ToString();
                    nod.Tag = DT1.Rows[j]["masv"].ToString();
                    node.Nodes.Add(nod);

                }
                dataGridView1.DataSource = dt;
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            xlsangmo(false);

            updateTreeView();
            
        }

        private void tvw_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvw.SelectedNode.Parent == null)//nút gốc
            {
                string ma = tvw.SelectedNode.Tag.ToString();
                string sql = "select * from sinhvien where makh='" + ma + "'";
                dataGridView1.DataSource = laydl(sql);
            }
            else// nút con
            {
                string masv= tvw.SelectedNode.Tag.ToString();
                string sql1 = "select * from sinhvien where masv='" + masv + "'";
                DataTable dt = laydl(sql1);
                txtmasv.Text = dt.Rows[0][0].ToString();
                txttensv.Text = dt.Rows[0][1].ToString();
                if (dt.Rows[0]["Phai"].ToString() == "True")
                    txtphai.Text = "Nam";
                else txtphai.Text = "Nữ";
                cbkhoa.SelectedValue = dt.Rows[0]["makh"].ToString();
                dateTimePicker1.Value = DateTime.Parse(dt.Rows[0]["ngaysinh"].ToString());
                txtnoisinh.Text = dt.Rows[0][4].ToString();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        void setnull()
        {
            txtmasv.Clear();
            txtnoisinh.Clear();
            txtphai.Clear();
            txttensv.Clear();
        }
        private void btnkhong_Click(object sender, EventArgs e)
        {
            xlsangmo(false);
            setnull();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            xlsangmo(true);
            setnull();
            string masv = "";
            string sql = "select top 1 masv from sinhvien order by masv desc";
            SqlCommand cmd = new SqlCommand(sql, cn);
            
            cn.Open();
            masv = cmd.ExecuteScalar().ToString();
            cn.Close();
            string goc = masv.Substring(0, 2);
            string stt = (int.Parse(masv.Substring(2, 2)) + 1).ToString("00");
            txtmasv.Text = goc + stt;
        }

        private void btnghi_Click(object sender, EventArgs e)
        {
            string phai = "0";
            if (txtphai.Text == "Nam")
                phai = "1";
            string sql = "INSERT INTO SINHVIEN(MaSV, HoTen, Phai, NgaySinh, NoiSinh, MaKH)"+
                "VALUES('"+txtmasv.Text+"',N'"+txttensv.Text+"', '"+phai+"','"+dateTimePicker1.Value.ToString("MM/dd/yyyy")+"',N'"+txtnoisinh.Text+"', N'"+cbkhoa.SelectedValue.ToString()+"')";
            thucthi(sql);
            xlsangmo(false);
            setnull();
            updateTreeView();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
