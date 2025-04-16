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
    public partial class NVCB : Form
    {
        private string _username;
        private string _role;
        private OracleConnection _con;

        public NVCB(string username, OracleConnection con, string role)
        {
            InitializeComponent();
            _username = username;
            _con = con;
            _role = role;

            this.Load += NVCB_Load;
        }

        private void NVCB_Load(object sender, EventArgs e)
        {
            lblResult.Text = $"Xin chào {_username}";

            // Giới hạn quyền truy cập tab theo role
            if (_role == "NVCB")
            {
                tabControl.TabPages.Remove(tabSinhVien);
                tabControl.TabPages.Remove(tabDangKi);
                tabControl.TabPages.Remove(tabMoMon);
            }
        }

        private void btnSelectNV_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cmd = new OracleCommand("admin.sp_select_nv_nvcb", _con))
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

                // Vô hiệu hóa các ô trừ ĐT
                txtMaNLĐ.ReadOnly = true;
                txtHoTen.ReadOnly = true;
                txtPhai.ReadOnly = true;
                txtNgSinh.ReadOnly = true;
                txtLuong.ReadOnly = true;
                txtPhuCap.ReadOnly = true;
                txtVaiTro.ReadOnly = true;
                txtMaĐV.ReadOnly = true;
                txtĐT.ReadOnly = false; // chỉ cho phép chỉnh SĐT
            }
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form1 loginForm = new Form1(); // mở lại form đăng nhập
                loginForm.Show();
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
                using (var cmd = new OracleCommand("admin.sp_update_phone_nvcb", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_sdt", OracleDbType.Varchar2).Value = newPhone;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Cập nhật số điện thoại thành công!");
                btnSelectNV.PerformClick(); // reload lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật số điện thoại: " + ex.Message);
            }
        }

    }
}
