namespace ATBM_UI_new
{
    partial class PhanHe1_deleteUser
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
            btnDeleteUser = new Button();
            txtUserToDelete = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(638, 392);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 22;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Font = new Font("Segoe UI", 12F);
            btnDeleteUser.Location = new Point(255, 170);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(154, 61);
            btnDeleteUser.TabIndex = 21;
            btnDeleteUser.Text = "Delete";
            btnDeleteUser.UseVisualStyleBackColor = true;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // txtUserToDelete
            // 
            txtUserToDelete.Font = new Font("Segoe UI", 12F);
            txtUserToDelete.Location = new Point(337, 73);
            txtUserToDelete.Name = "txtUserToDelete";
            txtUserToDelete.Size = new Size(316, 50);
            txtUserToDelete.TabIndex = 20;
            txtUserToDelete.TextChanged += txtUserToDelete_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(74, 73);
            label1.Name = "label1";
            label1.Size = new Size(86, 45);
            label1.TabIndex = 19;
            label1.Text = "Role";
            // 
            // PhanHe1_deleteUser
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnDeleteUser);
            Controls.Add(txtUserToDelete);
            Controls.Add(label1);
            Name = "PhanHe1_deleteUser";
            Text = "PhanHe1_deleteUser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Button btnDeleteUser;
        private TextBox txtUserToDelete;
        private Label label1;
    }
}