using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class PhanHe1_GrantUserRole : Form
    {
        private OracleConnection _con;

        public PhanHe1_GrantUserRole(OracleConnection con)
        {
            InitializeComponent();
            _con = con;
            this.Load += PhanHe1_GrantUserRole_Load;
        }

        private void PhanHe1_GrantUserRole_Load(object sender, EventArgs e)
        {
            cbPrivilege.Items.AddRange(new object[] { "SELECT", "INSERT", "UPDATE", "DELETE" });

            try
            {
                using (var cmd = new OracleCommand("sp_get_user_tables", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(cursorParam);

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                        cbTable.Items.Add(row["TABLE_NAME"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi load bảng: " + ex.Message);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string name = txtGrantee.Text.Trim().ToUpper();
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

                    if (result == "USER") lblResult.Text = "✅ Đây là User";
                    else if (result == "ROLE") lblResult.Text = "✅ Đây là Role";
                    else lblResult.Text = "❌ Không tồn tại User/Role!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi kiểm tra: " + ex.Message);
            }
        }

        private void cbPrivilege_SelectedIndexChanged(object sender, EventArgs e)
        {
            string priv = cbPrivilege.SelectedItem?.ToString();
            clbColumns.Items.Clear();
            clbColumns.Enabled = (priv == "SELECT" || priv == "UPDATE");
        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string table = cbTable.SelectedItem?.ToString();
            string priv = cbPrivilege.SelectedItem?.ToString();

            if (priv == "SELECT" || priv == "UPDATE")
            {
                clbColumns.Items.Clear();
                clbColumns.Enabled = true;

                try
                {
                    using (var cmd = new OracleCommand("sp_get_columns_of_table", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_table_name", OracleDbType.Varchar2).Value = table;

                        var p_cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(p_cursor);

                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable colTable = new DataTable();
                        da.Fill(colTable);

                        foreach (DataRow row in colTable.Rows)
                        {
                            clbColumns.Items.Add(row["COLUMN_NAME"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi load cột: " + ex.Message);
                }
            }
            else
            {
                clbColumns.Items.Clear();
                clbColumns.Enabled = false;
            }
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            string grantee = txtGrantee.Text.Trim().ToUpper();
            string table = cbTable.Text.Trim().ToUpper();
            string privilege = cbPrivilege.Text.Trim().ToUpper();
            bool withGrant = chkGrantOption.Checked;

            if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(table) || string.IsNullOrEmpty(privilege))
            {
                MessageBox.Show("❌ Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                // Nếu là SELECT hoặc UPDATE theo cột thì tạo view rồi cấp quyền
                if (privilege == "SELECT" || privilege == "UPDATE")
                {
                    var selectedCols = clbColumns.CheckedItems.Cast<string>().ToList();
                    if (selectedCols.Count == 0)
                    {
                        MessageBox.Show("❗ Hãy chọn ít nhất một cột.");
                        return;
                    }

                    string columnList = string.Join(",", selectedCols);

                    using (var cmd = new OracleCommand("sp_grant_column_view", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                        cmd.Parameters.Add("p_table", OracleDbType.Varchar2).Value = table;
                        cmd.Parameters.Add("p_columns", OracleDbType.Varchar2).Value = columnList;
                        cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;
                        cmd.ExecuteNonQuery();
                    }
                }
                else // INSERT / DELETE trên bảng
                {
                    using (var cmd = new OracleCommand("sp_grant_table_priv", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                        cmd.Parameters.Add("p_table", OracleDbType.Varchar2).Value = table;
                        cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;
                        cmd.Parameters.Add("p_with_grant_opt", OracleDbType.Boolean).Value = withGrant;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("✅ Cấp quyền thành công!");
                this.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("❌ Lỗi khi cấp quyền: " + ex.Message);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGrantee_TextChanged(object sender, EventArgs e) { }
        private void clbColumns_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
