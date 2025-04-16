namespace ATBM_UI_new
{
    partial class PhanHe1_RevokeRole
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
            btnRevoke = new Button();
            txtRoleToRevoke = new TextBox();
            txtUserRoleToRevoke = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(638, 278);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnRevoke
            // 
            btnRevoke.Font = new Font("Segoe UI", 12F);
            btnRevoke.Location = new Point(39, 253);
            btnRevoke.Name = "btnRevoke";
            btnRevoke.Size = new Size(254, 64);
            btnRevoke.TabIndex = 12;
            btnRevoke.Text = "Thu hồi Role";
            btnRevoke.UseVisualStyleBackColor = true;
            btnRevoke.Click += btnRevoke_Click;
            // 
            // txtRoleToRevoke
            // 
            txtRoleToRevoke.Font = new Font("Segoe UI", 12F);
            txtRoleToRevoke.Location = new Point(426, 174);
            txtRoleToRevoke.Name = "txtRoleToRevoke";
            txtRoleToRevoke.Size = new Size(262, 50);
            txtRoleToRevoke.TabIndex = 10;
            txtRoleToRevoke.TextChanged += txtRoleToRevoke_TextChanged;
            // 
            // txtUserRoleToRevoke
            // 
            txtUserRoleToRevoke.Font = new Font("Segoe UI", 12F);
            txtUserRoleToRevoke.Location = new Point(426, 59);
            txtUserRoleToRevoke.Name = "txtUserRoleToRevoke";
            txtUserRoleToRevoke.Size = new Size(262, 50);
            txtUserRoleToRevoke.TabIndex = 9;
            txtUserRoleToRevoke.TextChanged += txtUserRoleToRevoke_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(39, 177);
            label2.Name = "label2";
            label2.Size = new Size(335, 45);
            label2.TabIndex = 8;
            label2.Text = "Chọn Role để thu hồi";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(39, 62);
            label1.Name = "label1";
            label1.Size = new Size(343, 45);
            label1.TabIndex = 7;
            label1.Text = "User/Role cần thu hồi";
            // 
            // PhanHe1_RevokeRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 341);
            Controls.Add(btnBack);
            Controls.Add(btnRevoke);
            Controls.Add(txtRoleToRevoke);
            Controls.Add(txtUserRoleToRevoke);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_RevokeRole";
            Text = "PhanHe1_RevokeRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Button btnRevoke;
        private TextBox txtRoleToRevoke;
        private TextBox txtUserRoleToRevoke;
        private Label label2;
        private Label label1;
    }
}