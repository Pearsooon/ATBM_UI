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
    public partial class GIAOVIEN : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public GIAOVIEN(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += GIAOVIEN_Load;
        }

        private void GIAOVIEN_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"{_username}";

            SetAllNhanVienTextBoxReadOnly();
            SetAllMoMonTextBoxReadOnly();
            SetAllSinhVienTextBoxReadOnly();
            SetAllDangKyTextBoxReadOnly();
        }

        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_NV_GV", _con))
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

                // Là chính GV đang đăng nhập → mở khóa ô ĐT
                if (txtMaNLĐ.Text.Trim().ToUpper() == _username.ToUpper())
                    txtĐT.ReadOnly = false;
                else
                    txtĐT.ReadOnly = true;
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

            if (txtMaNLĐ.Text.Trim().ToUpper() != _username.ToUpper())
            {
                MessageBox.Show("❌ Bạn chỉ có thể cập nhật số điện thoại của chính mình.");
                return;
            }

            try
            {
                using (var cmd = new OracleCommand("admin.sp_update_phone_gv", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_sdt", OracleDbType.Varchar2).Value = newPhone;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Cập nhật số điện thoại thành công!");
                btnSelectNV.PerformClick(); // reload dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật số điện thoại: " + ex.Message);
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

        private void SetAllMoMonTextBoxReadOnly()
        {
            txtMaMM.ReadOnly = true;
            txtMaHP.ReadOnly = true;
            txtMaGV.ReadOnly = true;
            txtHK.ReadOnly = true;
            txtNam.ReadOnly = true;
        }

        private void SetAllSinhVienTextBoxReadOnly()
        {
            txtMaSV.ReadOnly = true;
            txtHoTenSV.ReadOnly = true;
            txtPhaiSV.ReadOnly = true;
            txtNgSinhSV.ReadOnly = true;
            txtĐChiSV.ReadOnly = true;
            txtĐTSV.ReadOnly = true;
            txtKhoa.ReadOnly = true;
            txtTinhTrang.ReadOnly = true;
        }

        private void SetAllDangKyTextBoxReadOnly()
        {
            txtMaSV_ĐK.ReadOnly = true;
            txtMaMM_ĐK.ReadOnly = true;
            txtDiemTH.ReadOnly = true;
            txtDiemQT.ReadOnly = true;
            txtDiemCK.ReadOnly = true;
            txtDiemTK.ReadOnly = true;
        }


        private void btnSelectMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_MOMON_GV", _con))
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
                MessageBox.Show("❌ Lỗi khi lấy dữ liệu mở môn: " + ex.Message);
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
                txtĐTSV.Text = row.Cells["ĐT"].Value?.ToString();
                txtKhoa.Text = row.Cells["KHOA"].Value?.ToString();
                txtTinhTrang.Text = row.Cells["TINHTRANG"].Value?.ToString();
            }
        }

        private void btnSelectSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_SV_GV", _con))
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
                MessageBox.Show("❌ Lỗi khi lấy danh sách sinh viên: " + ex.Message);
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

        private void btnSelectDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_DANGKY_GV", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView4.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi lấy dữ liệu đăng ký: " + ex.Message);
            }
        }

        private void btnSearchTK_Click(object sender, EventArgs e)
        {
            string masv = txtMaSVSearch.Text.Trim().ToUpper();
            string mamm = txtMaMMSearch.Text.Trim().ToUpper();

            if (dataGridView4.DataSource == null)
            {
                MessageBox.Show("❗ Vui lòng tải dữ liệu đăng ký trước (bấm Select)!");
                return;
            }

            DataView dv = (dataGridView4.DataSource as DataTable).DefaultView;

            // Tạo filter theo MASV và MAMM nếu người dùng nhập
            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(masv))
                conditions.Add($"MASV LIKE '%{masv}%'");
            if (!string.IsNullOrEmpty(mamm))
                conditions.Add($"MAMM LIKE '%{mamm}%'");

            dv.RowFilter = string.Join(" AND ", conditions);
        }

    }
}
