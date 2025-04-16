using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_createRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_createRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtNewRole_TextChanged(object sender, EventArgs e) { }

        private void txtNewRolePass_TextChanged(object sender, EventArgs e) { }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            string rolename = txtNewRole.Text.Trim().ToUpper();
            string password = txtNewRolePass.Text.Trim();

            if (string.IsNullOrEmpty(rolename))
            {
                MessageBox.Show("Vui lòng nhập tên role.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_create_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_rolename", OracleDbType.Varchar2).Value = rolename;

                    if (string.IsNullOrEmpty(password))
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Tạo role thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // trở về form gọi ShowDialog()
        }
    }
}
