namespace ATBM_UI_new
{
    partial class PhanHe1_updateRole
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
            btnUpdateRole = new Button();
            txtNewRolePassword = new TextBox();
            txtRoleToUpdate = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(719, 481);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 23;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnUpdateRole
            // 
            btnUpdateRole.Font = new Font("Segoe UI", 12F);
            btnUpdateRole.Location = new Point(333, 267);
            btnUpdateRole.Name = "btnUpdateRole";
            btnUpdateRole.Size = new Size(154, 61);
            btnUpdateRole.TabIndex = 22;
            btnUpdateRole.Text = "Update";
            btnUpdateRole.UseVisualStyleBackColor = true;
            btnUpdateRole.Click += btnUpdateRole_Click;
            // 
            // txtNewRolePassword
            // 
            txtNewRolePassword.Font = new Font("Segoe UI", 12F);
            txtNewRolePassword.Location = new Point(344, 187);
            txtNewRolePassword.Name = "txtNewRolePassword";
            txtNewRolePassword.Size = new Size(316, 50);
            txtNewRolePassword.TabIndex = 21;
            txtNewRolePassword.TextChanged += txtNewRolePassword_TextChanged;
            // 
            // txtRoleToUpdate
            // 
            txtRoleToUpdate.Font = new Font("Segoe UI", 12F);
            txtRoleToUpdate.Location = new Point(344, 85);
            txtRoleToUpdate.Name = "txtRoleToUpdate";
            txtRoleToUpdate.Size = new Size(316, 50);
            txtRoleToUpdate.TabIndex = 20;
            txtRoleToUpdate.TextChanged += txtRoleToUpdate_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(81, 187);
            label2.Name = "label2";
            label2.Size = new Size(163, 45);
            label2.TabIndex = 19;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(81, 85);
            label1.Name = "label1";
            label1.Size = new Size(169, 45);
            label1.TabIndex = 18;
            label1.Text = "Username";
            // 
            // PhanHe1_updateRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 539);
            Controls.Add(btnBack);
            Controls.Add(btnUpdateRole);
            Controls.Add(txtNewRolePassword);
            Controls.Add(txtRoleToUpdate);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_updateRole";
            Text = "PhanHe1_updateRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Button btnUpdateRole;
        private TextBox txtNewRolePassword;
        private TextBox txtRoleToUpdate;
        private Label label2;
        private Label label1;
    }
}