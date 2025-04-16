using System;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_UI_new
{
    public partial class PhanHe1 : Form
    {
        // Biến toàn cục để lưu thông tin username và connection
        private string currentUser;
        private OracleConnection connection;

        // Constructor mới nhận vào username và connection
        public PhanHe1(string username, OracleConnection con)
        {
            InitializeComponent();
            currentUser = username;
            connection = con;
            // Đăng ký sự kiện Load nếu chưa có
            this.Load += PhanHe1_Load;
        }

        // Sự kiện Load để hiển thị tên đăng nhập trên giao diện, giả sử bạn có Label tên lblUsername
        private void PhanHe1_Load(object sender, EventArgs e)
        {
            DangNhapLabel.Text = "Đăng nhập: " + currentUser;
        }

        // Các sự kiện xử lý khác, ví dụ button Logout, button các chức năng quản trị, v.v.
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Mở lại form đăng nhập
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }

        // Ví dụ button chuyển sang quản lý User/Role
        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            PhanHe1_QuanLyUserRole quanLyForm = new PhanHe1_QuanLyUserRole(connection);
            quanLyForm.ShowDialog();
        }


        private void btnGrantRevokeForRoleUser_Click(object sender, EventArgs e)
        {
            PhanHe1_GrantRevokeUserRole quanLyUserRole = new PhanHe1_GrantRevokeUserRole(connection);
            quanLyUserRole.ShowDialog();
        }

        private void btnGrantRevokeRoleForUser_Click(object sender, EventArgs e)
        {
            PhanHe1_GrantRevokeRoleForUser quanLyRoleCuaUser = new PhanHe1_GrantRevokeRoleForUser(connection);
            quanLyRoleCuaUser.ShowDialog();
        }
    }
}


