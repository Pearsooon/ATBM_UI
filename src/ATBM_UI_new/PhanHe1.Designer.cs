namespace ATBM_UI_new
{
    partial class PhanHe1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            DangNhapLabel = new Label();
            btnLogout = new Button();
            btnQuanLy = new Button();
            btnGrantRevokeForRoleUser = new Button();
            btnGrantRevokeRoleForUser = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F);
            label1.Location = new Point(534, 9);
            label1.Name = "label1";
            label1.Size = new Size(664, 89);
            label1.TabIndex = 0;
            label1.Text = "Admin Control Panel ";
            // 
            // DangNhapLabel
            // 
            DangNhapLabel.AutoSize = true;
            DangNhapLabel.Font = new Font("Segoe UI", 12F);
            DangNhapLabel.Location = new Point(991, 158);
            DangNhapLabel.Name = "DangNhapLabel";
            DangNhapLabel.Size = new Size(182, 45);
            DangNhapLabel.TabIndex = 1;
            DangNhapLabel.Text = "Đăng nhập:";
            // 
            // btnLogout
            // 
            btnLogout.Font = new Font("Segoe UI", 12F);
            btnLogout.Location = new Point(1339, 151);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(163, 59);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnQuanLy
            // 
            btnQuanLy.Font = new Font("Segoe UI", 12F);
            btnQuanLy.Location = new Point(89, 387);
            btnQuanLy.Name = "btnQuanLy";
            btnQuanLy.Size = new Size(365, 86);
            btnQuanLy.TabIndex = 3;
            btnQuanLy.Text = "Quản lý User/Role";
            btnQuanLy.UseVisualStyleBackColor = true;
            btnQuanLy.Click += btnQuanLy_Click;
            // 
            // btnGrantRevokeForRoleUser
            // 
            btnGrantRevokeForRoleUser.Font = new Font("Segoe UI", 12F);
            btnGrantRevokeForRoleUser.Location = new Point(531, 387);
            btnGrantRevokeForRoleUser.Name = "btnGrantRevokeForRoleUser";
            btnGrantRevokeForRoleUser.Size = new Size(511, 86);
            btnGrantRevokeForRoleUser.TabIndex = 4;
            btnGrantRevokeForRoleUser.Text = "Cấp/Thu hồi quyền cho role/user";
            btnGrantRevokeForRoleUser.UseVisualStyleBackColor = true;
            btnGrantRevokeForRoleUser.Click += btnGrantRevokeForRoleUser_Click;
            // 
            // btnGrantRevokeRoleForUser
            // 
            btnGrantRevokeRoleForUser.Font = new Font("Segoe UI", 12F);
            btnGrantRevokeRoleForUser.Location = new Point(1122, 387);
            btnGrantRevokeRoleForUser.Name = "btnGrantRevokeRoleForUser";
            btnGrantRevokeRoleForUser.Size = new Size(417, 86);
            btnGrantRevokeRoleForUser.TabIndex = 5;
            btnGrantRevokeRoleForUser.Text = "Cấp / Thu hồi role cho user";
            btnGrantRevokeRoleForUser.UseVisualStyleBackColor = true;
            btnGrantRevokeRoleForUser.Click += btnGrantRevokeRoleForUser_Click;
            // 
            // PhanHe1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1630, 649);
            Controls.Add(btnGrantRevokeRoleForUser);
            Controls.Add(btnGrantRevokeForRoleUser);
            Controls.Add(btnQuanLy);
            Controls.Add(btnLogout);
            Controls.Add(DangNhapLabel);
            Controls.Add(label1);
            Name = "PhanHe1";
            Text = "PhanHe1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label DangNhapLabel;
        private Button btnLogout;
        private Button btnQuanLy;
        private Button btnGrantRevokeForRoleUser;
        private Button btnGrantRevokeRoleForUser;
    }
}