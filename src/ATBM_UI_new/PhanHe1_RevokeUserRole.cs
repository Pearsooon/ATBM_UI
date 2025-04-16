using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
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
    public partial class PhanHe1_RevokeUserRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_RevokeUserRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
            this.Load += PhanHe1_RevokeUserRole_Load;
        }

        private void PhanHe1_RevokeUserRole_Load(object sender, EventArgs e)
        {
            cbPrivilege.Items.AddRange(new object[] { "SELECT", "INSERT", "UPDATE", "DELETE" });
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string name = txtRevoke.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(name))
            {
                lblResult.Text = "❌ Chưa nhập tên.";
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("sp_check_user_or_role", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_name", OracleDbType.Varchar2).Value = name;

                    var resultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 20)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultParam);

                    cmd.ExecuteNonQuery();
                    string result = resultParam.Value.ToString();

                    if (result == "USER" || result == "ROLE")
                    {
                        lblResult.Text = $"✅ Đây là {result}";
                        LoadGrantedObjects(name);
                    }
                    else lblResult.Text = "❌ Không tồn tại User/Role!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi kiểm tra: " + ex.Message);
            }
        }

        private void LoadGrantedObjects(string grantee)
        {
            cbTable.Items.Clear();

            try
            {
                using (var cmd = new OracleCommand("sp_find_grantee_table_privs", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        cbTable.Items.Add(row["TABLE_NAME"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi load bảng: " + ex.Message);
            }
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {

        }

        private void btnRevoke_Click_1(object sender, EventArgs e)
        {
            string grantee = txtRevoke.Text.Trim().ToUpper();
            string tableOrView = cbTable.Text.Trim().ToUpper();
            string privilege = cbPrivilege.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(tableOrView) || string.IsNullOrEmpty(privilege))
            {
                MessageBox.Show("❌ Vui lòng nhập đủ thông tin.");
                return;
            }

            try
            {
                // Gọi procedure kiểm tra view cấp quyền mức cột
                using (var cmdCheck = new OracleCommand("sp_check_view_privilege", _con))
                {
                    cmdCheck.CommandType = CommandType.StoredProcedure;
                    cmdCheck.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                    cmdCheck.Parameters.Add("p_view_name", OracleDbType.Varchar2).Value = tableOrView;
                    cmdCheck.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;

                    var resultParam = new OracleParameter("p_result", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmdCheck.Parameters.Add(resultParam);

                    cmdCheck.ExecuteNonQuery();
                    int isView = ((OracleDecimal)resultParam.Value).ToInt32();


                    if (isView == 1)
                    {
                        // Là view cấp quyền mức cột → thu hồi & xoá view
                        using (var cmd = new OracleCommand("sp_revoke_column_view", _con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                            cmd.Parameters.Add("p_table", OracleDbType.Varchar2).Value = tableOrView.Replace("V_", "").Replace("_" + grantee, "");
                            cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Là quyền mức bảng → thu hồi nếu tồn tại
                        using (var cmd = new OracleCommand("sp_revoke_privilege", _con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                            cmd.Parameters.Add("p_table", OracleDbType.Varchar2).Value = tableOrView;
                            cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("✅ Thu hồi quyền thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Thu hồi thất bại hoặc quyền không tồn tại: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
