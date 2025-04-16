namespace ATBM_UI_new
{
    partial class PhanHe1_createRole
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
            label2 = new Label();
            txtNewRole = new TextBox();
            txtNewRolePass = new TextBox();
            btnCreateRole = new Button();
            label3 = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(100, 101);
            label1.Name = "label1";
            label1.Size = new Size(86, 45);
            label1.TabIndex = 0;
            label1.Text = "Role";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(100, 203);
            label2.Name = "label2";
            label2.Size = new Size(163, 45);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // txtNewRole
            // 
            txtNewRole.Font = new Font("Segoe UI", 12F);
            txtNewRole.Location = new Point(320, 108);
            txtNewRole.Name = "txtNewRole";
            txtNewRole.Size = new Size(316, 50);
            txtNewRole.TabIndex = 2;
            txtNewRole.TextChanged += txtNewRole_TextChanged;
            // 
            // txtNewRolePass
            // 
            txtNewRolePass.Font = new Font("Segoe UI", 12F);
            txtNewRolePass.Location = new Point(320, 200);
            txtNewRolePass.Name = "txtNewRolePass";
            txtNewRolePass.Size = new Size(316, 50);
            txtNewRolePass.TabIndex = 3;
            txtNewRolePass.TextChanged += txtNewRolePass_TextChanged;
            // 
            // btnCreateRole
            // 
            btnCreateRole.Font = new Font("Segoe UI", 12F);
            btnCreateRole.Location = new Point(551, 282);
            btnCreateRole.Name = "btnCreateRole";
            btnCreateRole.Size = new Size(154, 61);
            btnCreateRole.TabIndex = 4;
            btnCreateRole.Text = "Create ";
            btnCreateRole.UseVisualStyleBackColor = true;
            btnCreateRole.Click += btnCreateRole_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(100, 375);
            label3.Name = "label3";
            label3.Size = new Size(641, 45);
            label3.TabIndex = 5;
            label3.Text = "Bỏ trống password nếu không muốn tạo pass";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(710, 472);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 12;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // PhanHe1_createRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(872, 530);
            Controls.Add(btnBack);
            Controls.Add(label3);
            Controls.Add(btnCreateRole);
            Controls.Add(txtNewRolePass);
            Controls.Add(txtNewRole);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_createRole";
            Text = "PhanHe1_createRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtNewRole;
        private TextBox txtNewRolePass;
        private Button btnCreateRole;
        private Label label3;
        private Button btnBack;
    }
}