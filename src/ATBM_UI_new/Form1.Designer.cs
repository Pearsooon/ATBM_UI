namespace ATBM_UI_new
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            cbRole = new ComboBox();
            btnLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(569, 222);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 50);
            txtUsername.TabIndex = 0;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(569, 327);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 50);
            txtPassword.TabIndex = 1;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // cbRole
            // 
            cbRole.Font = new Font("Segoe UI", 12F);
            cbRole.FormattingEnabled = true;
            cbRole.Location = new Point(569, 432);
            cbRole.Name = "cbRole";
            cbRole.Size = new Size(242, 53);
            cbRole.TabIndex = 2;
            cbRole.SelectedIndexChanged += cbRole_SelectedIndexChanged;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 12F);
            btnLogin.Location = new Point(790, 541);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(155, 62);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F);
            label1.Location = new Point(124, 50);
            label1.Name = "label1";
            label1.Size = new Size(991, 89);
            label1.TabIndex = 0;
            label1.Text = "HỆ THỐNG QUẢN LÝ SINH VIÊN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(290, 225);
            label2.Name = "label2";
            label2.Size = new Size(245, 45);
            label2.TabIndex = 4;
            label2.Text = "Nhập username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(294, 332);
            label3.Name = "label3";
            label3.Size = new Size(241, 45);
            label3.TabIndex = 5;
            label3.Text = "Nhập password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(423, 435);
            label4.Name = "label4";
            label4.Size = new Size(112, 45);
            label4.TabIndex = 6;
            label4.Text = "Vai trò";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1188, 761);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Controls.Add(cbRole);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cbRole;
        private Button btnLogin;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
