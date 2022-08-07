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
    public partial class QLDiem : Form
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
        public static SqlConnection sqlcon = new SqlConnection(connectionString);
        public QLDiem()
        {
            InitializeComponent();
        }
        public static string taikhoan;
        public static string MonDay;

        private void QLDiem_Load(object sender, EventArgs e)
        {
            lbUserGV.Text = "Xin chào Giáo Viên: " + taikhoan;
            label3.Text = "Danh sách điểm: " +MonDay;
            LoadMon();
        }
        private void LoadMon()
        {
            string constr = ConfigurationManager.ConnectionStrings["VNBL"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblDiem WHERE sMaMon = '" + MonDay + "'", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable("tblHS");
                        ad.Fill(tb);
                        dataGridDiem.DataSource = tb;
                    }
                }
            }
        }
    }
}
