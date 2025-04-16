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
    public partial class NV_TCHC : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public NV_TCHC(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += NV_TCHC_Load;
        }

        private void NV_TCHC_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"Xin chào: {_username}";

            // Giới hạn quyền truy cập tab theo role
            if (_role == "NV TCHC")
            {
                tabControl.TabPages.Remove(tabSinhVien);
                tabControl.TabPages.Remove(tabDangKi);
                tabControl.TabPages.Remove(tabMoMon);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaNLĐ.Text = row.Cells["MANLĐ"].Value?.ToString();
                txtHoTen.Text = row.Cells["HOTEN"].Value?.ToString();
                txtPhai.Text = row.Cells["PHAI"].Value?.ToString();
                txtNgSinh.Text = Convert.ToDateTime(row.Cells["NGSINH"].Value).ToString("yyyy-MM-dd");
                txtLuong.Text = row.Cells["LUONG"].Value?.ToString();
                txtPhuCap.Text = row.Cells["PHUCAP"].Value?.ToString();
                txtĐT.Text = row.Cells["ĐT"].Value?.ToString();
                txtVaiTro.Text = row.Cells["VAITRO"].Value?.ToString();
                txtMaĐV.Text = row.Cells["MAĐV"].Value?.ToString();
            }
        }


        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.sp_select_nv_tchc", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lấy dữ liệu: " + ex.Message);
            }
        }


        private void btnInsertNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.sp_insert_nv_tchc", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_manld", OracleDbType.Varchar2).Value = txtMaNLĐ.Text;
                    cmd.Parameters.Add("p_hoten", OracleDbType.Varchar2).Value = txtHoTen.Text;
                    cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = txtPhai.Text;
                    cmd.Parameters.Add("p_ngsinh", OracleDbType.Date).Value = DateTime.Parse(txtNgSinh.Text);
                    cmd.Parameters.Add("p_luong", OracleDbType.Decimal).Value = Decimal.Parse(txtLuong.Text);
                    cmd.Parameters.Add("p_phucap", OracleDbType.Decimal).Value = Decimal.Parse(txtPhuCap.Text);
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = txtĐT.Text;
                    cmd.Parameters.Add("p_vaitro", OracleDbType.Varchar2).Value = txtVaiTro.Text;
                    cmd.Parameters.Add("p_madv", OracleDbType.Varchar2).Value = txtMaĐV.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Thêm nhân viên thành công!");
                    btnSelectNV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi thêm nhân viên: " + ex.Message);
            }
        }


        private void btnUpdateNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.sp_update_nv_tchc", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_manld", OracleDbType.Varchar2).Value = txtMaNLĐ.Text;
                    cmd.Parameters.Add("p_hoten", OracleDbType.Varchar2).Value = txtHoTen.Text;
                    cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = txtPhai.Text;
                    cmd.Parameters.Add("p_ngsinh", OracleDbType.Date).Value = DateTime.Parse(txtNgSinh.Text);
                    cmd.Parameters.Add("p_luong", OracleDbType.Decimal).Value = Decimal.Parse(txtLuong.Text);
                    cmd.Parameters.Add("p_phucap", OracleDbType.Decimal).Value = Decimal.Parse(txtPhuCap.Text);
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = txtĐT.Text;
                    cmd.Parameters.Add("p_vaitro", OracleDbType.Varchar2).Value = txtVaiTro.Text;
                    cmd.Parameters.Add("p_madv", OracleDbType.Varchar2).Value = txtMaĐV.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật nhân viên thành công!");
                    btnSelectNV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật nhân viên: " + ex.Message);
            }
        }


        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNLĐ.Text))
                {
                    MessageBox.Show("❌ Vui lòng chọn nhân viên cần xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes) return;

                using (var cmd = new OracleCommand("admin.sp_delete_nv_tchc", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_manld", OracleDbType.Varchar2).Value = txtMaNLĐ.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Xóa nhân viên thành công!");
                    btnSelectNV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }


        private void btnSearchNV_Click(object sender, EventArgs e)
        {
            string keyword = txtUserFinding.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần tìm.");
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("SELECT * FROM admin.NHANVIEN WHERE MANLĐ = :manld", _con))
                {
                    cmd.Parameters.Add("manld", OracleDbType.Varchar2).Value = keyword;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("❌ Không tìm thấy nhân viên.");
                    }

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tìm kiếm: " + ex.Message);
            }
        }


        private void txtUserFinding_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
