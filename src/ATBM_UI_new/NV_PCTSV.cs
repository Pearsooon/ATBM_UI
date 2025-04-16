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
    public partial class NV_PCTSV : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public NV_PCTSV(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += NV_PCTSV_Load;
        }

        private void NV_PCTSV_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"Xin chào: {_username}";

            // Giới hạn quyền truy cập tab theo role
            if (_role == "NV_CTSV ")
            {
                tabControl.TabPages.Remove(tabDangKi);
                tabControl.TabPages.Remove(tabMoMon);
            }

            SetAllNhanVienTextBoxReadOnly();
        }

        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_NV_CTSV", _con))
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
                MessageBox.Show("❌ Lỗi khi lấy dữ liệu nhân viên: " + ex.Message);
            }
        }

        private void btnUpdateNV_Click(object sender, EventArgs e)
        {
            string newPhone = txtĐT.Text.Trim();
            if (string.IsNullOrEmpty(newPhone))
            {
                MessageBox.Show("❌ Vui lòng nhập số điện thoại mới.");
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_PHONE_CTSV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_sdt", OracleDbType.Varchar2).Value = newPhone;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Cập nhật thành công!");
                btnSelectNV.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật: " + ex.Message);
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
                txtNgSinh.Text = row.Cells["NGSINH"].Value?.ToString();
                txtLuong.Text = row.Cells["LUONG"].Value?.ToString();
                txtPhuCap.Text = row.Cells["PHUCAP"].Value?.ToString();
                txtĐT.Text = row.Cells["ĐT"].Value?.ToString();
                txtVaiTro.Text = row.Cells["VAITRO"].Value?.ToString();
                txtMaĐV.Text = row.Cells["MAĐV"].Value?.ToString();

                // Là chính TRGĐV đang đăng nhập → mở khóa ô ĐT
                if (txtMaNLĐ.Text.Trim().ToUpper() == _username.ToUpper())
                    txtĐT.ReadOnly = false;
                else
                    txtĐT.ReadOnly = true;
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                var row = dataGridView3.Rows[e.RowIndex];
                txtMaSV.Text = row.Cells["MASV"].Value?.ToString();
                txtHoTenSV.Text = row.Cells["HOTEN"].Value?.ToString();
                txtPhaiSV.Text = row.Cells["PHAI"].Value?.ToString();
                txtNgSinhSV.Text = row.Cells["NGSINH"].Value?.ToString();
                txtĐChiSV.Text = row.Cells["ĐCHI"].Value?.ToString();
                txtĐTSV.Text = row.Cells["ĐT"].Value?.ToString();
                txtKhoa.Text = row.Cells["KHOA"].Value?.ToString();
                txtTinhTrang.Text = row.Cells["TINHTRANG"].Value?.ToString();
            }
        }

        private void SetAllNhanVienTextBoxReadOnly()
        {
            txtMaNLĐ.ReadOnly = true;
            txtHoTen.ReadOnly = true;
            txtPhai.ReadOnly = true;
            txtNgSinh.ReadOnly = true;
            txtLuong.ReadOnly = true;
            txtPhuCap.ReadOnly = true;
            txtĐT.ReadOnly = true;
            txtVaiTro.ReadOnly = true;
            txtMaĐV.ReadOnly = true;
        }


        private void btnSelectSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_SINHVIEN_CTSV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lấy sinh viên: " + ex.Message);
            }
        }


        private void btnInsertSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_INSERT_SINHVIEN_CTSV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV.Text.Trim();
                    cmd.Parameters.Add("p_hoten", OracleDbType.Varchar2).Value = txtHoTenSV.Text.Trim();
                    cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = txtPhaiSV.Text.Trim();
                    cmd.Parameters.Add("p_ngsinh", OracleDbType.Date).Value = DateTime.Parse(txtNgSinhSV.Text.Trim());
                    cmd.Parameters.Add("p_dchi", OracleDbType.Varchar2).Value = txtĐChiSV.Text.Trim();  
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = txtĐTSV.Text.Trim();      
                    cmd.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = txtKhoa.Text.Trim();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Thêm sinh viên thành công!");
                    btnSelectSV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi thêm sinh viên: " + ex.Message);
            }
        }


        private void btnUpdateSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_SINHVIEN_CTSV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV.Text.Trim();
                    cmd.Parameters.Add("p_hoten", OracleDbType.Varchar2).Value = txtHoTenSV.Text.Trim();
                    cmd.Parameters.Add("p_phai", OracleDbType.Varchar2).Value = txtPhaiSV.Text.Trim();
                    cmd.Parameters.Add("p_ngsinh", OracleDbType.Date).Value = DateTime.Parse(txtNgSinhSV.Text.Trim());
                    cmd.Parameters.Add("p_dchi", OracleDbType.Varchar2).Value = txtĐChiSV.Text.Trim();
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = txtĐTSV.Text.Trim();
                    cmd.Parameters.Add("p_khoa", OracleDbType.Varchar2).Value = txtKhoa.Text.Trim();
                    cmd.Parameters.Add("p_tinhtrang", OracleDbType.Varchar2).Value = txtTinhTrang.Text.Trim();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật sinh viên thành công!");
                    btnSelectSV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật sinh viên: " + ex.Message);
            }
        }



        private void btnDeleteSV_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xoá sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            try
            {
                using (var cmd = new OracleCommand("admin.SP_DELETE_SINHVIEN_CTSV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV.Text.Trim();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Xoá sinh viên thành công!");
                btnSelectSV.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi xoá sinh viên: " + ex.Message);
            }
        }


        private void btnSearchSV_Click(object sender, EventArgs e)
        {
            string keyword = textBox10.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(keyword))
            {
                btnSelectSV.PerformClick();
                return;
            }

            if (dataGridView3.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = $"MASV LIKE '%{keyword}%'";
            }
        }

    }
}
