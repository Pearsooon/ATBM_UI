namespace ATBM_UI_new
{
    partial class PhanHe1_GrantRevokeRoleForUser
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
            dgvRole = new DataGridView();
            btnSelect = new Button();
            btnRevokeRole = new Button();
            btnGrantRole = new Button();
            btnSearchUser = new Button();
            txtUserFinding = new TextBox();
            label3 = new Label();
            btnRoleSearch = new Button();
            txtRoleFinding = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRole).BeginInit();
            SuspendLayout();
            // 
            // dgvRole
            // 
            dgvRole.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRole.Location = new Point(82, 160);
            dgvRole.Name = "dgvRole";
            dgvRole.RowHeadersWidth = 82;
            dgvRole.Size = new Size(1618, 1017);
            dgvRole.TabIndex = 0;
            // 
            // btnSelect
            // 
            btnSelect.Font = new Font("Segoe UI", 12F);
            btnSelect.Location = new Point(1532, 94);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(168, 58);
            btnSelect.TabIndex = 1;
            btnSelect.Text = "SELECT";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnRevokeRole
            // 
            btnRevokeRole.Font = new Font("Segoe UI", 12F);
            btnRevokeRole.Location = new Point(358, 47);
            btnRevokeRole.Name = "btnRevokeRole";
            btnRevokeRole.Size = new Size(242, 107);
            btnRevokeRole.TabIndex = 2;
            btnRevokeRole.Text = " Thu hồi Role cho User/Role";
            btnRevokeRole.UseVisualStyleBackColor = true;
            btnRevokeRole.Click += btnRevokeRole_Click;
            // 
            // btnGrantRole
            // 
            btnGrantRole.Font = new Font("Segoe UI", 12F);
            btnGrantRole.Location = new Point(82, 47);
            btnGrantRole.Name = "btnGrantRole";
            btnGrantRole.Size = new Size(237, 107);
            btnGrantRole.TabIndex = 3;
            btnGrantRole.Text = "Cấp Role cho User/Role ";
            btnGrantRole.UseVisualStyleBackColor = true;
            btnGrantRole.Click += btnGrantRole_Click;
            // 
            // btnSearchUser
            // 
            btnSearchUser.Location = new Point(887, 108);
            btnSearchUser.Name = "btnSearchUser";
            btnSearchUser.Size = new Size(150, 46);
            btnSearchUser.TabIndex = 13;
            btnSearchUser.Text = "Tìm kiếm";
            btnSearchUser.UseVisualStyleBackColor = true;
            btnSearchUser.Click += btnSearchUser_Click;
            // 
            // txtUserFinding
            // 
            txtUserFinding.Location = new Point(664, 112);
            txtUserFinding.Name = "txtUserFinding";
            txtUserFinding.Size = new Size(200, 39);
            txtUserFinding.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(660, 68);
            label3.Name = "label3";
            label3.Size = new Size(167, 32);
            label3.TabIndex = 11;
            label3.Text = "Tìm kiếm User";
            // 
            // btnRoleSearch
            // 
            btnRoleSearch.Location = new Point(1304, 108);
            btnRoleSearch.Name = "btnRoleSearch";
            btnRoleSearch.Size = new Size(150, 46);
            btnRoleSearch.TabIndex = 16;
            btnRoleSearch.Text = "Tìm kiếm";
            btnRoleSearch.UseVisualStyleBackColor = true;
            btnRoleSearch.Click += btnRoleSearch_Click;
            // 
            // txtRoleFinding
            // 
            txtRoleFinding.Location = new Point(1077, 108);
            txtRoleFinding.Name = "txtRoleFinding";
            txtRoleFinding.Size = new Size(200, 39);
            txtRoleFinding.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1073, 64);
            label1.Name = "label1";
            label1.Size = new Size(166, 32);
            label1.TabIndex = 14;
            label1.Text = "Tìm kiếm Role";
            // 
            // PhanHe1_GrantRevokeRoleForUser
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1729, 1213);
            Controls.Add(btnRoleSearch);
            Controls.Add(txtRoleFinding);
            Controls.Add(label1);
            Controls.Add(btnSearchUser);
            Controls.Add(txtUserFinding);
            Controls.Add(label3);
            Controls.Add(btnGrantRole);
            Controls.Add(btnRevokeRole);
            Controls.Add(btnSelect);
            Controls.Add(dgvRole);
            Name = "PhanHe1_GrantRevokeRoleForUser";
            Text = "PhanHe1_GrantRevokeRoleForUser";
            ((System.ComponentModel.ISupportInitialize)dgvRole).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvRole;
        private Button btnSelect;
        private Button btnRevokeRole;
        private Button btnGrantRole;
        private Button btnSearchUser;
        private TextBox txtUserFinding;
        private Label label3;
        private Button btnRoleSearch;
        private TextBox txtRoleFinding;
        private Label label1;
    }
}