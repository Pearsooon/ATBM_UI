using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_createUser : Form
    {
        private OracleConnection _con;

        public PhanHe1_createUser(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtNewUsername_TextChanged(object sender, EventArgs e) { }

        private void txtNewUserPassword_TextChanged(object sender, EventArgs e) { }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text.Trim().ToUpper();
            string password = txtNewUserPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập username.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập password.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_create_user", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Tạo user thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Quay về màn hình quản lý user/role
            this.Close(); // đóng form hiện tại để quay về form trước (ShowDialog)
        }
    }
}
