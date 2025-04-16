namespace ATBM_UI_new
{
    partial class PhanHe1_RevokeUserRole
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
            cbTable = new ComboBox();
            label5 = new Label();
            cbPrivilege = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            btnCheck = new Button();
            lblResult = new Label();
            label2 = new Label();
            txtRevoke = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F);
            btnBack.Location = new Point(622, 483);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(97, 45);
            btnBack.TabIndex = 28;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnRevoke
            // 
            btnRevoke.Font = new Font("Segoe UI", 12F);
            btnRevoke.Location = new Point(221, 392);
            btnRevoke.Name = "btnRevoke";
            btnRevoke.Size = new Size(243, 78);
            btnRevoke.TabIndex = 27;
            btnRevoke.Text = "Thu hồi quyền";
            btnRevoke.UseVisualStyleBackColor = true;
            btnRevoke.Click += btnRevoke_Click_1;
            // 
            // cbTable
            // 
            cbTable.Font = new Font("Segoe UI", 12F);
            cbTable.FormattingEnabled = true;
            cbTable.Location = new Point(326, 201);
            cbTable.Name = "cbTable";
            cbTable.Size = new Size(286, 53);
            cbTable.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(28, 204);
            label5.Name = "label5";
            label5.Size = new Size(193, 45);
            label5.TabIndex = 24;
            label5.Text = "Bảng/View";
            // 
            // cbPrivilege
            // 
            cbPrivilege.Font = new Font("Segoe UI", 12F);
            cbPrivilege.FormattingEnabled = true;
            cbPrivilege.Location = new Point(326, 294);
            cbPrivilege.Name = "cbPrivilege";
            cbPrivilege.Size = new Size(286, 53);
            cbPrivilege.TabIndex = 23;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(25, 297);
            label4.Name = "label4";
            label4.Size = new Size(120, 45);
            label4.TabIndex = 22;
            label4.Text = "Quyền";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(151, 105);
            label3.Name = "label3";
            label3.Size = new Size(0, 32);
            label3.TabIndex = 21;
            // 
            // btnCheck
            // 
            btnCheck.Font = new Font("Segoe UI", 10F);
            btnCheck.Location = new Point(506, 141);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(106, 44);
            btnCheck.TabIndex = 20;
            btnCheck.Text = "Check";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += btnCheck_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblResult.Location = new Point(151, 141);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(35, 37);
            lblResult.TabIndex = 19;
            lblResult.Text = "...";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.125F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(28, 141);
            label2.Name = "label2";
            label2.Size = new Size(123, 37);
            label2.TabIndex = 18;
            label2.Text = "Kiểm tra:";
            // 
            // txtRevoke
            // 
            txtRevoke.Font = new Font("Segoe UI", 12F);
            txtRevoke.Location = new Point(326, 76);
            txtRevoke.Name = "txtRevoke";
            txtRevoke.Size = new Size(286, 50);
            txtRevoke.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 79);
            label1.Name = "label1";
            label1.Size = new Size(262, 45);
            label1.TabIndex = 16;
            label1.Text = "Username/Role";
            // 
            // PhanHe1_RevokeUserRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(727, 535);
            Controls.Add(btnBack);
            Controls.Add(btnRevoke);
            Controls.Add(cbTable);
            Controls.Add(label5);
            Controls.Add(cbPrivilege);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnCheck);
            Controls.Add(lblResult);
            Controls.Add(label2);
            Controls.Add(txtRevoke);
            Controls.Add(label1);
            Name = "PhanHe1_RevokeUserRole";
            Text = "PhanHe1_RevokeUserRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnBack;
        private Button btnRevoke;
        private ComboBox cbTable;
        private Label label5;
        private ComboBox cbPrivilege;
        private Label label4;
        private Label label3;
        private Button btnCheck;
        private Label lblResult;
        private Label label2;
        private TextBox txtRevoke;
        private Label label1;
    }
}