namespace ATBM_UI_new
{
    partial class PhanHe1_GrantRole
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
            txtUserRoleToGrant = new TextBox();
            txtRoleToGrant = new TextBox();
            chkGrantOption = new CheckBox();
            btnGrant = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(65, 49);
            label1.Name = "label1";
            label1.Size = new Size(363, 45);
            label1.TabIndex = 0;
            label1.Text = "User/Role cần cấp Role";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(65, 164);
            label2.Name = "label2";
            label2.Size = new Size(280, 45);
            label2.TabIndex = 1;
            label2.Text = "Chọn Role để cấp";
            // 
            // txtUserRoleToGrant
            // 
            txtUserRoleToGrant.Font = new Font("Segoe UI", 12F);
            txtUserRoleToGrant.Location = new Point(452, 46);
            txtUserRoleToGrant.Name = "txtUserRoleToGrant";
            txtUserRoleToGrant.Size = new Size(262, 50);
            txtUserRoleToGrant.TabIndex = 2;
            txtUserRoleToGrant.TextChanged += txtUserRoleToGrant_TextChanged;
            // 
            // txtRoleToGrant
            // 
            txtRoleToGrant.Font = new Font("Segoe UI", 12F);
            txtRoleToGrant.Location = new Point(452, 161);
            txtRoleToGrant.Name = "txtRoleToGrant";
            txtRoleToGrant.Size = new Size(262, 50);
            txtRoleToGrant.TabIndex = 3;
            txtRoleToGrant.TextChanged += txtRoleToGrant_TextChanged;
            // 
            // chkGrantOption
            // 
            chkGrantOption.AutoSize = true;
            chkGrantOption.Location = new Point(452, 232);
            chkGrantOption.Name = "chkGrantOption";
            chkGrantOption.Size = new Size(280, 36);
            chkGrantOption.TabIndex = 4;
            chkGrantOption.Text = "WITH GRANT OPTION";
            chkGrantOption.UseVisualStyleBackColor = true;
            chkGrantOption.CheckedChanged += chkGrantOption_CheckedChanged;
            // 
            // btnGrant
            // 
            btnGrant.Font = new Font("Segoe UI", 12F);
            btnGrant.Location = new Point(65, 303);
            btnGrant.Name = "btnGrant";
            btnGrant.Size = new Size(215, 64);
            btnGrant.TabIndex = 5;
            btnGrant.Text = "Cấp Role";
            btnGrant.UseVisualStyleBackColor = true;
            btnGrant.Click += btnGrant_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(638, 333);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // PhanHe1_GrantRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 391);
            Controls.Add(btnBack);
            Controls.Add(btnGrant);
            Controls.Add(chkGrantOption);
            Controls.Add(txtRoleToGrant);
            Controls.Add(txtUserRoleToGrant);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "PhanHe1_GrantRole";
            Text = "PhanHe1_GrantRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtUserRoleToGrant;
        private TextBox txtRoleToGrant;
        private CheckBox chkGrantOption;
        private Button btnGrant;
        private Button btnBack;
    }
}