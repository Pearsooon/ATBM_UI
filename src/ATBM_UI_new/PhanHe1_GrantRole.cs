using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_GrantRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_GrantRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtUserRoleToGrant_TextChanged(object sender, EventArgs e)
        {
            // Tùy chọn xử lý khi text thay đổi, nếu cần validate
        }

        private void txtRoleToGrant_TextChanged(object sender, EventArgs e)
        {
            // Tùy chọn xử lý khi text thay đổi, nếu cần validate
        }

        private void chkGrantOption_CheckedChanged(object sender, EventArgs e)
        {
            // Có thể dùng để thay đổi trạng thái hiển thị hoặc gợi ý
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            string grantee = txtUserRoleToGrant.Text.Trim().ToUpper();
            string role = txtRoleToGrant.Text.Trim().ToUpper();
            bool withGrant = chkGrantOption.Checked;

            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("❌ Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                // Kiểm tra xem grantee có tồn tại không (USER hoặc ROLE)
                using (var checkCmd = new OracleCommand("sp_check_user_or_role", _con))
                {
                    checkCmd.CommandType = CommandType.StoredProcedure;
                    checkCmd.Parameters.Add("p_name", OracleDbType.Varchar2).Value = grantee;

                    var resultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 20)
                    {
                        Direction = ParameterDirection.Output
                    };
                    checkCmd.Parameters.Add(resultParam);

                    checkCmd.ExecuteNonQuery();
                    string result = resultParam.Value.ToString();

                    if (result == "NONE")
                    {
                        MessageBox.Show("❌ Không tồn tại user/role: " + grantee);
                        return;
                    }
                }

                // Gọi procedure cấp role
                using (var cmd = new OracleCommand("sp_grant_role_to_user_or_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                    cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = role;
                    cmd.Parameters.Add("p_with_option", OracleDbType.Boolean).Value = withGrant;

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Cấp role thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi khi cấp role: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // hoặc quay lại Form trước
        }
    }
}
