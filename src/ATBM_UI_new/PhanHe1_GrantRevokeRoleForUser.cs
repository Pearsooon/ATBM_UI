using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_GrantRevokeRoleForUser : Form
    {
        private OracleConnection _con;

        public PhanHe1_GrantRevokeRoleForUser(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void btnGrantRole_Click(object sender, EventArgs e)
        {
            var form = new PhanHe1_GrantRole(_con);
            form.ShowDialog();
        }

        private void btnRevokeRole_Click(object sender, EventArgs e)
        {
            var form = new PhanHe1_RevokeRole(_con);
            form.ShowDialog();
        }

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            string username = txtUserFinding.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("❌ Vui lòng nhập username để tìm kiếm.");
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("sp_find_roles_of_user", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRole.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tìm role của user: " + ex.Message);
            }
        }

        private void btnRoleSearch_Click(object sender, EventArgs e)
        {
            string role = txtRoleFinding.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("❌ Vui lòng nhập role để tìm kiếm.");
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("sp_find_users_of_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_role", OracleDbType.Varchar2).Value = role;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRole.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tìm user của role: " + ex.Message);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("sp_get_all_role_privs", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRole.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lấy danh sách phân quyền role: " + ex.Message);
            }
        }
    }
}
