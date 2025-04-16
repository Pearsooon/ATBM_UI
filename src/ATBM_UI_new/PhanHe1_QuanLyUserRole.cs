using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_QuanLyUserRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_QuanLyUserRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;

            this.Load += PhanHe1_QuanLyUserRole_Load;
        }


        private void PhanHe1_QuanLyUserRole_Load(object sender, EventArgs e)
        {
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_createUser(_con); // truyền connection nếu cần
            f.ShowDialog();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_deleteUser(_con);
            f.ShowDialog();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_updateUser(_con);
            f.ShowDialog();
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_createRole(_con);
            f.ShowDialog();
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_deleteRole(_con);
            f.ShowDialog();
        }

        private void btnUpdateRole_Click(object sender, EventArgs e)
        {
            var f = new PhanHe1_updateRole(_con);
            f.ShowDialog();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách USERS
                using (OracleCommand cmd = new OracleCommand("SELECT USERNAME, USER_ID, CREATED FROM DBA_USERS ORDER BY USERNAME", _con))
                {
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUsers.DataSource = dt;
                }

                // Lấy danh sách ROLES
                using (OracleCommand cmd = new OracleCommand("SELECT ROLE, ROLE_ID, PASSWORD_REQUIRED FROM DBA_ROLES ORDER BY ROLE", _con))
                {
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRoles.DataSource = dt;
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi truy vấn: " + ex.Message);
            }
        }

        private void btnUserFinding_Click(object sender, EventArgs e)
        {
            string keyword = txtUserFinding.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm user.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_find_user", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = keyword;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi tìm kiếm user: " + ex.Message);
            }
        }


        private void btnRoleFinding_Click(object sender, EventArgs e)
        {
            string keyword = txtRoleFinding.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm role.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("sp_find_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_rolename", OracleDbType.Varchar2).Value = keyword;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvRoles.DataSource = dt;
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi tìm kiếm role: " + ex.Message);
            }
        }


        private void txtUserFinding_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRoleFinding_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
