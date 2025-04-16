namespace ATBM_UI_new
{
    partial class PhanHe1_deleteRole
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
            txtRoleToDelete = new TextBox();
            label1 = new Label();
            btnDeleteRole = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtRoleToDelete
            // 
            txtRoleToDelete.Font = new Font("Segoe UI", 12F);
            txtRoleToDelete.Location = new Point(363, 89);
            txtRoleToDelete.Name = "txtRoleToDelete";
            txtRoleToDelete.Size = new Size(316, 50);
            txtRoleToDelete.TabIndex = 16;
            txtRoleToDelete.TextChanged += txtRoleToDelete_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(100, 89);
            label1.Name = "label1";
            label1.Size = new Size(86, 45);
            label1.TabIndex = 15;
            label1.Text = "Role";
            // 
            // btnDeleteRole
            // 
            btnDeleteRole.Font = new Font("Segoe UI", 12F);
            btnDeleteRole.Location = new Point(281, 186);
            btnDeleteRole.Name = "btnDeleteRole";
            btnDeleteRole.Size = new Size(154, 61);
            btnDeleteRole.TabIndex = 17;
            btnDeleteRole.Text = "Delete";
            btnDeleteRole.UseVisualStyleBackColor = true;
            btnDeleteRole.Click += btnDeleteRole_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(603, 347);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 18;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // PhanHe1_deleteRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 405);
            Controls.Add(btnBack);
            Controls.Add(btnDeleteRole);
            Controls.Add(txtRoleToDelete);
            Controls.Add(label1);
            Name = "PhanHe1_deleteRole";
            Text = "PhanHe1_deleteRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtRoleToDelete;
        private Label label1;
        private Button btnDeleteRole;
        private Button btnBack;
    }
}