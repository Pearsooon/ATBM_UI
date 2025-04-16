namespace ATBM_UI_new
{
    partial class PhanHe1_GrantUserRole
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
            txtGrantee = new TextBox();
            label2 = new Label();
            lblResult = new Label();
            btnCheck = new Button();
            label3 = new Label();
            label4 = new Label();
            cbPrivilege = new ComboBox();
            label5 = new Label();
            cbTable = new ComboBox();
            clbColumns = new CheckedListBox();
            btnGrant = new Button();
            btnBack = new Button();
            chkGrantOption = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(88, 90);
            label1.Name = "label1";
            label1.Size = new Size(262, 45);
            label1.TabIndex = 0;
            label1.Text = "Username/Role";
            // 
            // txtGrantee
            // 
            txtGrantee.Font = new Font("Segoe UI", 12F);
            txtGrantee.Location = new Point(386, 87);
            txtGrantee.Name = "txtGrantee";
            txtGrantee.Size = new Size(286, 50);
            txtGrantee.TabIndex = 1;
            txtGrantee.TextChanged += txtGrantee_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.125F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(88, 152);
            label2.Name = "label2";
            label2.Size = new Size(123, 37);
            label2.TabIndex = 2;
            label2.Text = "Kiểm tra:";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblResult.Location = new Point(211, 152);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(35, 37);
            lblResult.TabIndex = 3;
            lblResult.Text = "...";
            // 
            // btnCheck
            // 
            btnCheck.Font = new Font("Segoe UI", 10F);
            btnCheck.Location = new Point(566, 152);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(106, 44);
            btnCheck.TabIndex = 4;
            btnCheck.Text = "Check";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += btnCheck_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(211, 116);
            label3.Name = "label3";
            label3.Size = new Size(0, 32);
            label3.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(85, 228);
            label4.Name = "label4";
            label4.Size = new Size(120, 45);
            label4.TabIndex = 6;
            label4.Text = "Quyền";
            // 
            // cbPrivilege
            // 
            cbPrivilege.Font = new Font("Segoe UI", 12F);
            cbPrivilege.FormattingEnabled = true;
            cbPrivilege.Location = new Point(386, 225);
            cbPrivilege.Name = "cbPrivilege";
            cbPrivilege.Size = new Size(286, 53);
            cbPrivilege.TabIndex = 7;
            cbPrivilege.SelectedIndexChanged += cbPrivilege_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(88, 325);
            label5.Name = "label5";
            label5.Size = new Size(101, 45);
            label5.TabIndex = 8;
            label5.Text = "Bảng";
            // 
            // cbTable
            // 
            cbTable.Font = new Font("Segoe UI", 12F);
            cbTable.FormattingEnabled = true;
            cbTable.Location = new Point(386, 322);
            cbTable.Name = "cbTable";
            cbTable.Size = new Size(286, 53);
            cbTable.TabIndex = 9;
            cbTable.SelectedIndexChanged += cbTable_SelectedIndexChanged;
            // 
            // clbColumns
            // 
            clbColumns.FormattingEnabled = true;
            clbColumns.Location = new Point(729, 90);
            clbColumns.Name = "clbColumns";
            clbColumns.Size = new Size(240, 292);
            clbColumns.TabIndex = 10;
            clbColumns.SelectedIndexChanged += clbColumns_SelectedIndexChanged;
            // 
            // btnGrant
            // 
            btnGrant.Font = new Font("Segoe UI", 12F);
            btnGrant.Location = new Point(289, 457);
            btnGrant.Name = "btnGrant";
            btnGrant.Size = new Size(187, 78);
            btnGrant.TabIndex = 12;
            btnGrant.Text = "Cấp quyền";
            btnGrant.UseVisualStyleBackColor = true;
            btnGrant.Click += btnGrant_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F);
            btnBack.Location = new Point(901, 509);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(97, 45);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // chkGrantOption
            // 
            chkGrantOption.AutoSize = true;
            chkGrantOption.Location = new Point(386, 392);
            chkGrantOption.Name = "chkGrantOption";
            chkGrantOption.Size = new Size(280, 36);
            chkGrantOption.TabIndex = 15;
            chkGrantOption.Text = "WITH GRANT OPTION";
            chkGrantOption.UseVisualStyleBackColor = true;
            // 
            // PhanHe1_GrantUserRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1010, 566);
            Controls.Add(chkGrantOption);
            Controls.Add(btnBack);
            Controls.Add(btnGrant);
            Controls.Add(clbColumns);
            Controls.Add(cbTable);
            Controls.Add(label5);
            Controls.Add(cbPrivilege);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnCheck);
            Controls.Add(lblResult);
            Controls.Add(label2);
            Controls.Add(txtGrantee);
            Controls.Add(label1);
            Name = "PhanHe1_GrantUserRole";
            Text = "PhanHe1_GrantUserRole";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtGrantee;
        private Label label2;
        private Label lblResult;
        private Button btnCheck;
        private Label label3;
        private Label label4;
        private ComboBox cbPrivilege;
        private Label label5;
        private ComboBox cbTable;
        private CheckedListBox clbColumns;
        private Button btnGrant;
        private Button btnBack;
        private CheckBox chkGrantOption;
    }
}