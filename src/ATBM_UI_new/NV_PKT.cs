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
    public partial class NV_PKT : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public NV_PKT(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += NV_PKT_Load;
        }

        private void NV_PKT_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"Xin chào {_username}";

            // Giới hạn quyền truy cập tab theo role
            if (_role == "NV_PKT")
            {
                tabControl.TabPages.Remove(tabSinhVien);
                tabControl.TabPages.Remove(tabMoMon);
            }
        }

        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_NV_PKT", _con))
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

        private void btnUpdateDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_PKT_UPDATE_DIEM", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_masv", OracleDbType.Varchar2).Value = txtMaSV_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_mamm", OracleDbType.Varchar2).Value = txtMaMM_ĐK.Text.Trim();
                    cmd.Parameters.Add("p_diemth", OracleDbType.Decimal).Value = Convert.ToDecimal(txtDiemTH.Text);
                    cmd.Parameters.Add("p_diemqt", OracleDbType.Decimal).Value = Convert.ToDecimal(txtDiemQT.Text);
                    cmd.Parameters.Add("p_diemck", OracleDbType.Decimal).Value = Convert.ToDecimal(txtDiemCK.Text);
                    cmd.Parameters.Add("p_diemtk", OracleDbType.Decimal).Value = Convert.ToDecimal(txtDiemTK.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Cập nhật điểm thành công!");
                    btnSearchTK.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật điểm: " + ex.Message);
            }
        }


        private void btnSearchTK_Click(object sender, EventArgs e)
        {
            string masv = txtMaSVSearch.Text.Trim();
            string mamm = txtMaMMSearch.Text.Trim();

            string query = "SELECT * FROM admin.ĐANGKY WHERE 1=1";

            if (!string.IsNullOrEmpty(masv))
                query += " AND MASV = :masv";
            if (!string.IsNullOrEmpty(mamm))
                query += " AND MAMM = :mamm";

            try
            {
                using (var cmd = new OracleCommand(query, _con))
                {
                    if (!string.IsNullOrEmpty(masv))
                        cmd.Parameters.Add("masv", OracleDbType.Varchar2).Value = masv;
                    if (!string.IsNullOrEmpty(mamm))
                        cmd.Parameters.Add("mamm", OracleDbType.Varchar2).Value = mamm;

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView4.DataSource = dt;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Không tìm thấy kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tìm kiếm: " + ex.Message);
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


                txtĐT.ReadOnly = false;
                txtMaNLĐ.ReadOnly = true;
                txtHoTen.ReadOnly = true;
                txtPhai.ReadOnly = true;
                txtNgSinh.ReadOnly = true;
                txtLuong.ReadOnly = true;
                txtPhuCap.ReadOnly = true;
                txtVaiTro.ReadOnly = true;
                txtMaĐV.ReadOnly = true;
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

        private void btnUpdateNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_UPDATE_PHONE_PKT", _con))
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

        private void btnSelectDK_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_DANGKY_PKT", _con))
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
    }
}
