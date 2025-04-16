namespace ATBM_UI_new
{
    partial class PhanHe1_createUser
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
            btnCreateUser = new Button();
            txtNewUserPassword = new TextBox();
            txtNewUsername = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // btnCreateUser
            // 
            btnCreateUser.Font = new Font("Segoe UI", 12F);
            btnCreateUser.Location = new Point(333, 262);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(154, 61);
            btnCreateUser.TabIndex = 10;
            btnCreateUser.Text = "Create ";
            btnCreateUser.UseVisualStyleBackColor = true;
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // txtNewUserPassword
            // 
            txtNewUserPassword.Font = new Font("Segoe UI", 12F);
            txtNewUserPassword.Location = new Point(333, 175);
            txtNewUserPassword.Name = "txtNewUserPassword";
            txtNewUserPassword.Size = new Size(316, 50);
            txtNewUserPassword.TabIndex = 9;
            txtNewUserPassword.TextChanged += txtNewUserPassword_TextChanged;
            // 
            // txtNewUsername
            // 
            txtNewUsername.Font = new Font("Segoe UI", 12F);
            txtNewUsername.Location = new Point(333, 83);
            txtNewUsername.Name = "txtNewUsername";
            txtNewUsername.Size = new Size(316, 50);
            txtNewUsername.TabIndex = 8;
            txtNewUsername.TextChanged += txtNewUsername_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(113, 178);
            label2.Name = "label2";
            label2.Size = new Size(163, 45);
            label2.TabIndex = 7;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(113, 76);
            label1.Name = "label1";
            label1.Size = new Size(169, 45);
            label1.TabIndex = 6;
            label1.Text = "Username";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(708, 398);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // PhanHe1_createUser
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(861, 456);
            Controls.Add(btnBack);
            Controls.Add(btnCreateUser);
            Controls.Add(txtNewUserPassword);
            Controls.Add(txtNewUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_createUser";
            Text = "PhanHe1_createUser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCreateUser;
        private TextBox txtNewUserPassword;
        private TextBox txtNewUsername;
        private Label label2;
        private Label label1;
        private Button btnBack;
    }
}