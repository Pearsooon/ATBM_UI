using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_RevokeRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_RevokeRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtUserRoleToRevoke_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtRoleToRevoke_TextChanged(object sender, EventArgs e)
        {
        }

        private void chkGrantOption_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            string grantee = txtUserRoleToRevoke.Text.Trim().ToUpper();
            string role = txtRoleToRevoke.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("❌ Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("sp_revoke_role_from_user_or_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                    cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = role;

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Thu hồi role thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi khi thu hồi role: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // hoặc quay lại Form trước đó
        }
    }
}
