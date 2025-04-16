using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_updateUser : Form
    {
        private OracleConnection _con;

        public PhanHe1_updateUser(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtUserToAlter_TextChanged(object sender, EventArgs e) { }

        private void txtNewPassword_TextChanged(object sender, EventArgs e) { }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string username = txtUserToAlter.Text.Trim().ToUpper();
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user.");
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_alter_user", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("p_new_password", OracleDbType.Varchar2).Value = newPassword;

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Cập nhật mật khẩu user thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Quay lại form trước
        }
    }
}
