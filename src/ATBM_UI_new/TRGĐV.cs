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
    public partial class TRGĐV : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public TRGĐV(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += TRGĐV_Load;
        }

        private void TRGĐV_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"Xin chào: {_username}";

            // Giới hạn quyền truy cập tab theo role
            if (_role == "TRGĐV")
            {
                tabControl.TabPages.Remove(tabSinhVien);
                tabControl.TabPages.Remove(tabDangKi);
            }

            SetAllNhanVienTextBoxReadOnly();
            SetAllMoMonTextBoxReadOnly();
        }

        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_NV_TRGDV", _con))
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


        private void btnSearchNV_Click(object sender, EventArgs e)
        {
            string keyword = txtUserFinding.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(keyword))
            {
                btnSelectNV.PerformClick();
                return;
            }

            // Lọc ngay trên DataGridView
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"MANLĐ LIKE '%{keyword}%'";
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

        private void btnUpdateNV_Click(object sender, EventArgs e)
        {

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

        private void btnUpdateNV_Click_1(object sender, EventArgs e)
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
                using (var cmd = new OracleCommand("admin.sp_update_phone_trgdv", _con))
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

        private void btnSelectMM_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.SP_SELECT_MOMON_TRGDV", _con))
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


    }
}
