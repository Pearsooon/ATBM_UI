using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace ATBM_UI_new
{
    public partial class NV_PĐT : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public NV_PĐT(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += NV_PĐT_Load;
        }

        private void NV_PĐT_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"Xin chào {_username}";

            SetAllNhanVienTextBoxReadOnly();
        }

        // Xem thông tin cá nhân
        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_NV_PDT", _con))
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

        // Cập nhật SĐT của NV
        private void btnUpdateNV_Click(object sender, EventArgs e) { }

        // Xem môn mở
        private void btnSelectMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_MOMON_PDT", _con))
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

        private void btnInsertMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_INSERT_MOMON_PDT", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM.Text.Trim();
                    cmd.Parameters.Add("p_mahp", OracleDbType.Varchar2).Value = txtMaHP.Text.Trim();
                    cmd.Parameters.Add("p_magv", OracleDbType.Varchar2).Value = txtMaGV.Text.Trim();
                    cmd.Parameters.Add("p_hk", OracleDbType.Varchar2).Value = txtHK.Text.Trim();
                    cmd.Parameters.Add("p_nam", OracleDbType.Int32).Value = int.Parse(txtNam.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Thêm môn mở thành công!");
                    btnSelectMM.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thêm môn mở: " + ex.Message);
            }
        }

        private void btnUpdateMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_MOMON_PDT", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM.Text.Trim();
                    cmd.Parameters.Add("p_mahp", OracleDbType.Varchar2).Value = txtMaHP.Text.Trim();
                    cmd.Parameters.Add("p_magv", OracleDbType.Varchar2).Value = txtMaGV.Text.Trim();
                    cmd.Parameters.Add("p_hk", OracleDbType.Varchar2).Value = txtHK.Text.Trim();
                    cmd.Parameters.Add("p_nam", OracleDbType.Int32).Value = int.Parse(txtNam.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật môn mở thành công!");
                    btnSelectMM.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi cập nhật môn mở: " + ex.Message);
            }
        }

        private void btnDeleteMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_DELETE_MOMON_PDT", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM.Text.Trim();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Xoá môn mở thành công!");
                    btnSelectMM.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi xoá môn mở: " + ex.Message);
            }
        }

        // SINHVIEN
        private void btnSelectSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_SINHVIEN_PDT", _con))
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

        private void btnUpdateSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_TINHTRANG", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV.Text.Trim();
                    cmd.Parameters.Add("p_tinhtrang", OracleDbType.Varchar2).Value = txtTinhTrang.Text.Trim();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật tình trạng thành công!");
                    btnSelectSV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi cập nhật tình trạng: " + ex.Message);
            }
        }

        // ĐĂNG KÝ
        private void btnSelectDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_DANGKY_PDT", _con))
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
                MessageBox.Show("❌ Lỗi khi lấy đăng ký: " + ex.Message);
            }
        }

        private void btnInsertDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_INSERT_DANGKY_PDT", _con))
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
                using (var cmd = new OracleCommand("admin.SP_UPDATE_DANGKY_PDT", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_diemth", OracleDbType.Decimal).Value = decimal.Parse(txtDiemTH.Text.Trim());
                    cmd.Parameters.Add("p_diemqt", OracleDbType.Decimal).Value = decimal.Parse(txtDiemQT.Text.Trim());
                    cmd.Parameters.Add("p_diemck", OracleDbType.Decimal).Value = decimal.Parse(txtDiemCK.Text.Trim());
                    cmd.Parameters.Add("p_diemtk", OracleDbType.Decimal).Value = decimal.Parse(txtDiemTK.Text.Trim());

                    cmd.CommandTimeout = 10; // 10 giây
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
                using (var cmd = new OracleCommand("admin.SP_DELETE_DANGKY_PDT", _con))
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
            }
        }

        private void dataGridView4_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void btnUpdateNV_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_PHONE_PDT", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_sdt", OracleDbType.Varchar2).Value = txtĐT.Text.Trim();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật số điện thoại thành công!");
                    btnSelectNV.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btnSearchTK_Click(object sender, EventArgs e)
        {
            string masv = txtMaSVSearch.Text.Trim().ToUpper();
            string mamm = txtMaMMSearch.Text.Trim().ToUpper();

            string query = "SELECT * FROM admin.ĐANGKY WHERE 1=1";

            if (!string.IsNullOrEmpty(masv))
                query += " AND MASV = :masv";
            if (!string.IsNullOrEmpty(mamm))
                query += " AND MAMM = :mamm";

            try
            {
                using (OracleCommand cmd = new OracleCommand(query, _con))
                {
                    if (!string.IsNullOrEmpty(masv))
                        cmd.Parameters.Add(new OracleParameter("masv", masv));
                    if (!string.IsNullOrEmpty(mamm))
                        cmd.Parameters.Add(new OracleParameter("mamm", mamm));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView4.DataSource = dt;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
