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
    public partial class SINHVIEN : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public SINHVIEN(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += SINHVIEN_Load;
        }

        private void SINHVIEN_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"{_username}";

            // Giới hạn quyền truy cập tab theo role
            if (_role == "SINHVIEN")
            {
                tabControl.TabPages.Remove(tabNhanVien);
            }
        }

        private void btnSelectMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_MOMON_SV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lấy môn mở: " + ex.Message);
            }
        }


        private void btnSearchMoMon_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_SV_SELF", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("masv", OracleDbType.Varchar2).Value = _username;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi xem thông tin: " + ex.Message);
            }
        }

        private void btnUpdateSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_SV_SELF", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = _username;
                    cmd.Parameters.Add("p_dchi", OracleDbType.Varchar2).Value = txtĐChiSV.Text.Trim();
                    cmd.Parameters.Add("p_dt", OracleDbType.Varchar2).Value = textBox5.Text.Trim();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật thông tin thành công!");
                    btnSelectSV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi cập nhật thông tin: " + ex.Message);
            }
        }


        private void btnSelectDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_DANGKY_SV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("masv", OracleDbType.Varchar2).Value = _username;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView4.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lấy đăng ký: " + ex.Message);
            }
        }

        private void btnInsertDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_INSERT_DANGKY_SV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV_ĐK.Text;
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM_ĐK.Text;
                    cmd.Parameters.Add("p_diemth", OracleDbType.Decimal).Value = string.IsNullOrWhiteSpace(txtDiemTH.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtDiemTH.Text);
                    cmd.Parameters.Add("p_diemqt", OracleDbType.Decimal).Value = string.IsNullOrWhiteSpace(txtDiemQT.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtDiemQT.Text);
                    cmd.Parameters.Add("p_diemck", OracleDbType.Decimal).Value = string.IsNullOrWhiteSpace(txtDiemCK.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtDiemCK.Text);
                    cmd.Parameters.Add("p_diemtk", OracleDbType.Decimal).Value = string.IsNullOrWhiteSpace(txtDiemTK.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtDiemTK.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Đăng ký thành công!");
                    btnSelectDK.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi đăng ký: " + ex.Message);
            }
        }

        private void btnUpdateDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_DANGKY_SV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_diemth", OracleDbType.Decimal).Value = decimal.Parse(txtDiemTH.Text.Trim());
                    cmd.Parameters.Add("p_diemqt", OracleDbType.Decimal).Value = decimal.Parse(txtDiemQT.Text.Trim());
                    cmd.Parameters.Add("p_diemck", OracleDbType.Decimal).Value = decimal.Parse(txtDiemCK.Text.Trim());
                    cmd.Parameters.Add("p_diemtk", OracleDbType.Decimal).Value = decimal.Parse(txtDiemTK.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Cập nhật thành công!");
                    btnSelectDK.PerformClick();
                }
            }
            catch (OracleException ex)
            {

                // Hiển thị lỗi trigger (nếu bị chặn do vai trò)
                if (ex.Message.Contains("ORA-20002"))
                {
                    MessageBox.Show("❌ Bạn không có quyền cập nhật điểm. Chỉ NV PKT được phép!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Contains("ORA-20001"))
                {
                    MessageBox.Show("❌ Chỉ được thao tác đăng ký trong 4 tháng đầu học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("❌ Lỗi Oracle: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi không xác định: " + ex.Message);
            }
        }

        private void btnDeleteDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_DELETE_DANGKY_SV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM_ĐK.Text.Trim();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Huỷ đăng ký thành công!");
                    btnSelectDK.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi huỷ đăng ký: " + ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                txtMaMM.Text = row.Cells["MAMM"].Value?.ToString();
                txtMaHP.Text = row.Cells["MAHP"].Value?.ToString();
                txtMaGV.Text = row.Cells["MAGV"].Value?.ToString();
                txtHK.Text = row.Cells["HK"].Value?.ToString();
                txtNam.Text = row.Cells["NAM"].Value?.ToString();

                txtMaMM.ReadOnly = true;
                txtMaHP.ReadOnly = true;
                txtMaGV.ReadOnly = true;
                txtHK.ReadOnly = true;
                txtNam.ReadOnly = true;
            }
        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                txtMaSV.Text = row.Cells["MASV"].Value?.ToString();
                txtHoTenSV.Text = row.Cells["HOTEN"].Value?.ToString();
                txtPhaiSV.Text = row.Cells["PHAI"].Value?.ToString();
                txtNgSinhSV.Text = row.Cells["NGSINH"].Value?.ToString();
                txtĐChiSV.Text = row.Cells["ĐCHI"].Value?.ToString();
                textBox5.Text = row.Cells["ĐT"].Value?.ToString();
                txtKhoa.Text = row.Cells["KHOA"].Value?.ToString();
                txtTinhTrang.Text = row.Cells["TINHTRANG"].Value?.ToString();

                txtMaSV.ReadOnly = true;
                txtHoTenSV.ReadOnly = true;
                txtPhaiSV.ReadOnly = true;
                txtNgSinhSV.ReadOnly = true;
                txtTinhTrang.ReadOnly = true;
                txtKhoa.ReadOnly = true;

                textBox5.ReadOnly = false;
                txtĐChiSV.ReadOnly = false;
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];
                txtMaSV_ĐK.Text = row.Cells["MASV"].Value?.ToString();
                txtMaMM_ĐK.Text = row.Cells["MAMM"].Value?.ToString();
                txtDiemTH.Text = row.Cells["ĐIEMTH"].Value?.ToString();
                txtDiemQT.Text = row.Cells["ĐIEMQT"].Value?.ToString();
                txtDiemCK.Text = row.Cells["ĐIEMCK"].Value?.ToString();
                txtDiemTK.Text = row.Cells["ĐIEMTK"].Value?.ToString();
            }
        }
    }
}
