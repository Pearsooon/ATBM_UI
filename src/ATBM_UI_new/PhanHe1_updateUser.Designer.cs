namespace ATBM_UI_new
{
    partial class PhanHe1_updateUser
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
            btnBack = new Button();
            btnUpdateUser = new Button();
            txtNewPassword = new TextBox();
            txtUserToAlter = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(618, 386);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 17;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnUpdateUser
            // 
            btnUpdateUser.Font = new Font("Segoe UI", 12F);
            btnUpdateUser.Location = new Point(301, 245);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new Size(154, 61);
            btnUpdateUser.TabIndex = 16;
            btnUpdateUser.Text = "Update";
            btnUpdateUser.UseVisualStyleBackColor = true;
            btnUpdateUser.Click += btnUpdateUser_Click;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Font = new Font("Segoe UI", 12F);
            txtNewPassword.Location = new Point(312, 165);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(316, 50);
            txtNewPassword.TabIndex = 15;
            txtNewPassword.TextChanged += txtNewPassword_TextChanged;
            // 
            // txtUserToAlter
            // 
            txtUserToAlter.Font = new Font("Segoe UI", 12F);
            txtUserToAlter.Location = new Point(312, 63);
            txtUserToAlter.Name = "txtUserToAlter";
            txtUserToAlter.Size = new Size(316, 50);
            txtUserToAlter.TabIndex = 14;
            txtUserToAlter.TextChanged += txtUserToAlter_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(49, 165);
            label2.Name = "label2";
            label2.Size = new Size(240, 45);
            label2.TabIndex = 13;
            label2.Text = "New Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(49, 63);
            label1.Name = "label1";
            label1.Size = new Size(169, 45);
            label1.TabIndex = 12;
            label1.Text = "Username";
            // 
            // PhanHe1_updateUser
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 441);
            Controls.Add(btnBack);
            Controls.Add(btnUpdateUser);
            Controls.Add(txtNewPassword);
            Controls.Add(txtUserToAlter);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_updateUser";
            Text = "PhanHe1_updateUser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Button btnUpdateUser;
        private TextBox txtNewPassword;
        private TextBox txtUserToAlter;
        private Label label2;
        private Label label1;
    }
}