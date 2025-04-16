namespace ATBM_UI_new
{
    partial class PhanHe1_QuanLyUserRole
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
            btnSelect = new Button();
            dgvUsers = new DataGridView();
            dgvRoles = new DataGridView();
            btnCreateUser = new Button();
            btnDeleteUser = new Button();
            btnUpdateUser = new Button();
            label3 = new Label();
            txtUserFinding = new TextBox();
            btnUserFinding = new Button();
            btnRoleFinding = new Button();
            txtRoleFinding = new TextBox();
            label4 = new Label();
            btnUpdateRole = new Button();
            btnDeleteRole = new Button();
            btnCreateRole = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(109, 72);
            label1.Name = "label1";
            label1.Size = new Size(117, 45);
            label1.TabIndex = 0;
            label1.Text = "USERS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(110, 706);
            label2.Name = "label2";
            label2.Size = new Size(116, 45);
            label2.TabIndex = 1;
            label2.Text = "ROLES";
            // 
            // btnSelect
            // 
            btnSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelect.Location = new Point(1130, 63);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(167, 62);
            btnSelect.TabIndex = 2;
            btnSelect.Text = "SELECT";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(110, 131);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 82;
            dgvUsers.Size = new Size(1187, 560);
            dgvUsers.TabIndex = 3;
            // 
            // dgvRoles
            // 
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Location = new Point(110, 754);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.RowHeadersWidth = 82;
            dgvRoles.Size = new Size(1187, 560);
            dgvRoles.TabIndex = 4;
            // 
            // btnCreateUser
            // 
            btnCreateUser.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreateUser.Location = new Point(1431, 131);
            btnCreateUser.Name = "btnCreateUser";
            btnCreateUser.Size = new Size(167, 62);
            btnCreateUser.TabIndex = 5;
            btnCreateUser.Text = "Tạo user";
            btnCreateUser.UseVisualStyleBackColor = true;
            btnCreateUser.Click += btnCreateUser_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeleteUser.Location = new Point(1431, 233);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(167, 62);
            btnDeleteUser.TabIndex = 6;
            btnDeleteUser.Text = "Xoá user";
            btnDeleteUser.UseVisualStyleBackColor = true;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // btnUpdateUser
            // 
            btnUpdateUser.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdateUser.Location = new Point(1431, 332);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new Size(167, 62);
            btnUpdateUser.TabIndex = 7;
            btnUpdateUser.Text = "Sửa user";
            btnUpdateUser.UseVisualStyleBackColor = true;
            btnUpdateUser.Click += btnUpdateUser_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(1361, 445);
            label3.Name = "label3";
            label3.Size = new Size(181, 37);
            label3.TabIndex = 8;
            label3.Text = "Tìm kiếm user";
            // 
            // txtUserFinding
            // 
            txtUserFinding.Font = new Font("Segoe UI", 12F);
            txtUserFinding.Location = new Point(1361, 497);
            txtUserFinding.Name = "txtUserFinding";
            txtUserFinding.Size = new Size(200, 50);
            txtUserFinding.TabIndex = 9;
            txtUserFinding.TextChanged += txtUserFinding_TextChanged;
            // 
            // btnUserFinding
            // 
            btnUserFinding.Font = new Font("Segoe UI", 10F);
            btnUserFinding.Location = new Point(1577, 497);
            btnUserFinding.Name = "btnUserFinding";
            btnUserFinding.Size = new Size(167, 50);
            btnUserFinding.TabIndex = 10;
            btnUserFinding.Text = "Tìm kiếm";
            btnUserFinding.UseVisualStyleBackColor = true;
            btnUserFinding.Click += btnUserFinding_Click;
            // 
            // btnRoleFinding
            // 
            btnRoleFinding.Font = new Font("Segoe UI", 10F);
            btnRoleFinding.Location = new Point(1577, 1120);
            btnRoleFinding.Name = "btnRoleFinding";
            btnRoleFinding.Size = new Size(167, 50);
            btnRoleFinding.TabIndex = 16;
            btnRoleFinding.Text = "Tìm kiếm";
            btnRoleFinding.UseVisualStyleBackColor = true;
            btnRoleFinding.Click += btnRoleFinding_Click;
            // 
            // txtRoleFinding
            // 
            txtRoleFinding.Font = new Font("Segoe UI", 12F);
            txtRoleFinding.Location = new Point(1361, 1120);
            txtRoleFinding.Name = "txtRoleFinding";
            txtRoleFinding.Size = new Size(200, 50);
            txtRoleFinding.TabIndex = 15;
            txtRoleFinding.TextChanged += txtRoleFinding_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(1361, 1068);
            label4.Name = "label4";
            label4.Size = new Size(178, 37);
            label4.TabIndex = 14;
            label4.Text = "Tìm kiếm role";
            // 
            // btnUpdateRole
            // 
            btnUpdateRole.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdateRole.Location = new Point(1431, 955);
            btnUpdateRole.Name = "btnUpdateRole";
            btnUpdateRole.Size = new Size(167, 62);
            btnUpdateRole.TabIndex = 13;
            btnUpdateRole.Text = "Sửa role";
            btnUpdateRole.UseVisualStyleBackColor = true;
            btnUpdateRole.Click += btnUpdateRole_Click;
            // 
            // btnDeleteRole
            // 
            btnDeleteRole.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeleteRole.Location = new Point(1431, 856);
            btnDeleteRole.Name = "btnDeleteRole";
            btnDeleteRole.Size = new Size(167, 62);
            btnDeleteRole.TabIndex = 12;
            btnDeleteRole.Text = "Xoá role";
            btnDeleteRole.UseVisualStyleBackColor = true;
            btnDeleteRole.Click += btnDeleteRole_Click;
            // 
            // btnCreateRole
            // 
            btnCreateRole.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreateRole.Location = new Point(1431, 754);
            btnCreateRole.Name = "btnCreateRole";
            btnCreateRole.Size = new Size(167, 62);
            btnCreateRole.TabIndex = 11;
            btnCreateRole.Text = "Tạo role";
            btnCreateRole.UseVisualStyleBackColor = true;
            btnCreateRole.Click += btnCreateRole_Click;
            // 
            // PhanHe1_QuanLyUserRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1756, 1337);
            Controls.Add(btnRoleFinding);
            Controls.Add(txtRoleFinding);
            Controls.Add(label4);
            Controls.Add(btnUpdateRole);
            Controls.Add(btnDeleteRole);
            Controls.Add(btnCreateRole);
            Controls.Add(btnUserFinding);
            Controls.Add(txtUserFinding);
            Controls.Add(label3);
            Controls.Add(btnUpdateUser);
            Controls.Add(btnDeleteUser);
            Controls.Add(btnCreateUser);
            Controls.Add(dgvRoles);
            Controls.Add(dgvUsers);
            Controls.Add(btnSelect);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_QuanLyUserRole";
            Text = "PhanHe1_QuanLyUserRole";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnSelect;
        private DataGridView dgvUsers;
        private DataGridView dgvRoles;
        private Button btnCreateUser;
        private Button btnDeleteUser;
        private Button btnUpdateUser;
        private Label label3;
        private TextBox txtUserFinding;
        private Button btnUserFinding;
        private Button btnRoleFinding;
        private TextBox txtRoleFinding;
        private Label label4;
        private Button btnUpdateRole;
        private Button btnDeleteRole;
        private Button btnCreateRole;
    }
}