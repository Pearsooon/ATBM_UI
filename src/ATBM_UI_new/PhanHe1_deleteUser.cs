using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_deleteUser : Form
    {
        private OracleConnection _con;

        public PhanHe1_deleteUser(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
        }

        private void txtUserToDelete_TextChanged(object sender, EventArgs e) { }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string username = txtUserToDelete.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user cần xoá.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xoá user \"{username}\"?",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_delete_user", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Đã xoá user thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form
        }
    }
}
