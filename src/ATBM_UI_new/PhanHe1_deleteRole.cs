using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_deleteRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_deleteRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtRoleToDelete_TextChanged(object sender, EventArgs e) { }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            string rolename = txtRoleToDelete.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(rolename))
            {
                MessageBox.Show("Vui lòng nhập tên role cần xoá.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xoá role \"{rolename}\"?",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_delete_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_rolename", OracleDbType.Varchar2).Value = rolename;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Đã xoá role thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form và quay về
        }
    }
}
