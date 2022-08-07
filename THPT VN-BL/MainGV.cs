using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace THPT_VN_BL
{
    public partial class MainGV : Form
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
        public static SqlConnection sqlcon = new SqlConnection(connectionString);
        public MainGV()
        {
            InitializeComponent();
        }

        public static string taikhoan;

        private void MainGV_Load(object sender, EventArgs e)
        {
            showTTCNGV();
            lbUserGV.Text = "Xin chào Giáo Viên: " + taikhoan;
        }
        private void showTTCNGV()
        {
            string constr = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblGV WHERE sHoTenGV = '" + taikhoan + "'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblGV");
                        ad.Fill(tb);
                        dataGVCN.DataSource = tb;
                    }
                }
            }
        }
        private void btnSuaGV_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có chắc muốn thay đổi thông tin cá nhân không?"
                    , "Thông báo"
                    , MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question);
            if (re == DialogResult.No)
            {
                return;
            }
            var cm = new SqlCommand();
            cm.Connection = sqlcon;
            cm.CommandText = "GV_Update";
            cm.CommandType = CommandType.StoredProcedure;
            try
            {
                cm.Parameters.AddWithValue("sSoCCCD", txtCCCD.Text);
                cm.Parameters.AddWithValue("sMaMon", txtMon.Text);
                cm.Parameters.AddWithValue("sHoTenGV", txtHT.Text);
                cm.Parameters.AddWithValue("sDiachi", txtDC.Text);
                cm.Parameters.AddWithValue("sGioiTinh", txtGT.Text);
                cm.Parameters.AddWithValue("sChucvu", txtCV.Text);
                cm.Parameters.AddWithValue("dNgaysinh", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                sqlcon.Open();
                cm.ExecuteNonQuery();
                sqlcon.Close();
                showTTCNGV();
                MessageBox.Show("Sửa thành công!!!");
                button1_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Sửa thất bại kiểm tra lại thông tin!!!");
            }
        }

        private void dataGVCN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGVCN.CurrentRow.Index;
            txtCCCD.Text = dataGVCN.Rows[i].Cells[0].Value.ToString();
            txtMon.Text = dataGVCN.Rows[i].Cells[1].Value.ToString();
            txtHT.Text = dataGVCN.Rows[i].Cells[2].Value.ToString();
            txtDC.Text = dataGVCN.Rows[i].Cells[3].Value.ToString();
            txtGT.Text = dataGVCN.Rows[i].Cells[4].Value.ToString();
            txtCV.Text = dataGVCN.Rows[i].Cells[5].Value.ToString();
        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtCCCD.Text))
            {
                btnSuaGV.Enabled = true;
            }
            else
            {
                btnSuaGV.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCCCD.Text = txtMon.Text = txtHT.Text = txtDC.Text = txtCV.Text = txtGT.Text= string.Empty;
            txtCCCD.Focus();
            showTTCNGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
            string sqlFindSach = "SELECT * from tblHS WHERE tblHS.sMaLop ='" + txtCV.Text + "'";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sqlFindSach, cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        dataGridView1.DataSource = tb;
                        cnn.Close();
                        cnn.Dispose();
                    }
                }
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainGV_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Đăng xuất không?", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void quảnLýHọcSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLhsl.LopQL = txtCV.Text;
            QLhsl frm = new QLhsl();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void danhSáchĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLDiem.MonDay = txtMon.Text;
            QLDiem frm = new QLDiem();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
