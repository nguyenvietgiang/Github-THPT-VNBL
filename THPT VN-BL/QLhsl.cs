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
    public partial class QLhsl : Form
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
        public static SqlConnection sqlcon = new SqlConnection(connectionString);
        public QLhsl()
        {
            InitializeComponent();
        }
        public static string taikhoan;
        public static string LopQL;

        private void QLhsl_Load(object sender, EventArgs e)
        {
            showHSL();
            lbUserGV.Text = "Xin chào Giáo Viên: " + taikhoan;
            label3.Text = "Danh sách chi tiết học sinh lớp " + LopQL;
            Siso();
        }
        private void showHSL()
        {
            string constr = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblHS WHERE sMaLop = '" + LopQL + "'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblHS");
                        ad.Fill(tb);
                        dataGridHS.DataSource = tb;
                    }
                }
            }
        }
        private void Siso()
        {
            label14.Text = $"Sĩ Số: {dataGridHS.RowCount - 1}";
        }

        private void btnThemHS_Click(object sender, EventArgs e)
        {
            string c;
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "spHocsinh_INSERT";

                    try
                    {
                        cmd.Parameters.AddWithValue("@sMaHS", txtMa.Text);
                        cmd.Parameters.AddWithValue("@sMaLop", txtLop.Text);
                        cmd.Parameters.AddWithValue("@sMaHK", txtHK.Text);
                        cmd.Parameters.AddWithValue("@sHoTenHS", txtTen.Text);
                        cmd.Parameters.AddWithValue("@dNgaysinhHS", txtNS.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@bUuTien", txtUT.Text);
                        cmd.Parameters.AddWithValue("@sGioiTinh", txtGT.Text);
                        cmd.Parameters.AddWithValue("@sDiaChi", txtDC.Text);
                        cmd.Parameters.AddWithValue("@sDanToc", txtDT.Text);
                        cmd.ExecuteNonQuery();
                        c = $"{ dataGridHS.RowCount}";
                        MessageBox.Show("Đã Thêm Học Sinh thứ: " + c + " vào danh sách");
                        cnn.Close();
                        showHSL();
                        Siso();
                    }
                    catch
                    {
                        MessageBox.Show("Bạn điền sai hoặc thiếu thông tin, kiểm tra lại !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
    }
}
