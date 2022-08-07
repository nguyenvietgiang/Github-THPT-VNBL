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
    public partial class Login : Form
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
        public static SqlConnection sqlcon = new SqlConnection(connectionString);
        public Login()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Đóng Ứng Dụng Quản Lý?", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string tk = txtTaikhoan.Text;
                string mk = txtMatkhau.Text;
                if (radioButton1.Checked)
                {
                    string sql = "select * from tblTK where tblTK.sTenTK='" + tk + "'and tblTK.sMatKhau ='" + mk + "' and tblTK.sQuyen='HS'";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    SqlDataReader dta = cmd.ExecuteReader();
                    if (dta.Read() == true)
                    {
                        MainHS frm = new MainHS();
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác", "Thông Báo");
                    }
                }
                else if ((radioButton2.Checked))
                {
                    string sql = "select * from tblTK where tblTK.sTenTK='" + tk + "'and tblTK.sMatKhau ='" + mk + "' and tblTK.sQuyen='GV'";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    SqlDataReader dta = cmd.ExecuteReader();
                    if (dta.Read() == true)
                    {
                        MainGV.taikhoan = txtTaikhoan.Text;
                        QLhsl.taikhoan = txtTaikhoan.Text;
                        QLDiem.taikhoan = txtTaikhoan.Text;
                        MainGV frm = new MainGV();
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác", "Thông Báo");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập sai tài khoản, thử lại", "Thông Báo");
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }
    }
}
