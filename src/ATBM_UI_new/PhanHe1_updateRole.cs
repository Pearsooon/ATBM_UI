using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_updateRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_updateRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtRoleToUpdate_TextChanged(object sender, EventArgs e) { }
        private void txtNewRolePassword_TextChanged(object sender, EventArgs e) { }

        private void btnUpdateRole_Click(object sender, EventArgs e)
        {
            string rolename = txtRoleToUpdate.Text.Trim().ToUpper();
            string password = txtNewRolePassword.Text.Trim();

            if (string.IsNullOrEmpty(rolename))
            {
                MessageBox.Show("Vui lòng nhập tên role.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_update_role_password", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_rolename", OracleDbType.Varchar2).Value = rolename;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value =
                        string.IsNullOrEmpty(password) ? DBNull.Value : password;

                    cmd.ExecuteNonQuery();
                }

                string msg = string.IsNullOrEmpty(password)
                    ? "✅ Role đã được chuyển sang không cần mật khẩu."
                    : "✅ Cập nhật mật khẩu cho role thành công!";
                MessageBox.Show(msg);

                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // quay lại
        }
    }
}
