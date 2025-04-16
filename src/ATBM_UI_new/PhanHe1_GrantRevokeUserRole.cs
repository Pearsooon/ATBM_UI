using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_GrantRevokeUserRole : Form
    {
        private OracleConnection _con;


        public PhanHe1_GrantRevokeUserRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;

            this.Load += PhanHe1_GrantRevokeUserRole_Load;
        }


        private void PhanHe1_GrantRevokeUserRole_Load(object sender, EventArgs e)
        {
            dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvColumn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_GrantUserRole(_con);
            f.ShowDialog();
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_RevokeUserRole(_con);
            f.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("❗ Vui lòng nhập tên User/Role cần tìm.");
                return;
            }

            LoadGranteePrivileges(keyword);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadGranteePrivileges(""); // lấy toàn bộ
        }

        private void LoadGranteePrivileges(string grantee)
        {
            try
            {
                // Quyền toàn bảng → dgvTable
                using (OracleCommand cmd = new OracleCommand("sp_find_grantee_table_privs", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTable.DataSource = dt;
                }

                // Quyền SELECT/UPDATE theo cột (từ VIEW) → dgvColumn
                using (OracleCommand cmd = new OracleCommand("sp_find_grantee_column_privs", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvColumn.DataSource = dt;
                }

            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi tìm kiếm quyền: " + ex.Message);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
