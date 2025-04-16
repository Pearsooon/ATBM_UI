using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms.Design;


namespace ATBM_UI_new
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbRole.Items.Clear();
            cbRole.Items.AddRange(new object[]
            {
                "ADMIN",
                "TRGĐV",
                "GV",
                "NV_PĐT",
                "NV_PKT",
                "NV_CTSV",
                "NV_TCHC",
                "NVCB",
                "SV"
            });
            cbRole.SelectedIndex = 0;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e){}

        private void txtPassword_TextChanged(object sender, EventArgs e){}

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e){}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim().ToUpper();
            string password = txtPassword.Text.Trim();
            string role = cbRole.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // 1. Nếu username == role (VD: ADMIN), bỏ qua kiểm tra trong DBA_ROLE_PRIVS
            if (username != role)
            {
                // Tạo kết nối tạm bằng tài khoản admin để kiểm tra role
                string adminConnStr = "User Id=admin;Password=06092004;Data Source=localhost:1521/DaiHocX;";
                using (var adminCon = new OracleConnection(adminConnStr))
                {
                    try
                    {
                        adminCon.Open();

                        using (var cmd = new OracleCommand("SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :uname", adminCon))
                        {
                            cmd.Parameters.Add("uname", OracleDbType.Varchar2).Value = username;

                            using (var reader = cmd.ExecuteReader())
                            {
                                bool foundRole = false;
                                while (reader.Read())
                                {
                                    string grantedRole = reader.GetString(0).ToUpper();
                                    if (grantedRole == role)
                                    {
                                        foundRole = true;
                                        break;
                                    }
                                }

                                if (!foundRole)
                                {
                                    MessageBox.Show($"❌ User '{username}' không có role '{role}'.");
                                    return;
                                }
                            }
                        }

                        adminCon.Close();
                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show("❌ Lỗi kiểm tra role bằng ADMIN: " + ex.Message);
                        return;
                    }
                }
            }

            // 2. Nếu đúng role → kết nối thật bằng username/password
            string userConnStr = $"User Id={username};Password={password};Data Source=localhost:1521/DaiHocX;";
            try
            {
                OracleConnection userCon = new OracleConnection(userConnStr);
                userCon.Open();

                MessageBox.Show("✅ Đăng nhập thành công!");
                this.Hide();

                // Mở form đúng với role
                switch (role)
                {
                    case "ADMIN":
                        new PhanHe1(username, userCon).Show(); break;
                    case "NVCB":
                        new NVCB(username, userCon, role).Show(); break;
                    case "GV":
                        new GIAOVIEN(username, userCon, role).Show(); break;
                    case "TRGĐV":
                        new TRGĐV(username, userCon, role).Show(); break;
                    case "NV_PĐT":
                        new NV_PĐT(username, userCon, role).Show(); break;
                    case "NV_PKT":
                        new NV_PKT(username, userCon, role).Show(); break;
                    case "NV_CTSV":
                        new NV_PCTSV(username, userCon, role).Show(); break;
                    case "NV_TCHC":
                        new NV_TCHC(username, userCon, role).Show(); break;
                    case "SV":
                        new SINHVIEN(username, userCon, role).Show();break;
                    default:
                        MessageBox.Show($"Role {role} chưa được hỗ trợ."); break;
                }
            }
            catch (OracleException ex)
            {
                if (ex.Number == 1017)
                    MessageBox.Show("❌ Sai tài khoản hoặc mật khẩu.");
                else
                    MessageBox.Show("❌ Lỗi kết nối: " + ex.Message);
            }
        }




    }
}
