namespace ATBM_UI_new
{
    partial class PhanHe1_GrantRevokeUserRole
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
            btnGrant = new Button();
            btnRevoke = new Button();
            dgvColumn = new DataGridView();
            dgvTable = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            btnSelect = new Button();
            label3 = new Label();
            textBox1 = new TextBox();
            btnSearch = new Button();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTable).BeginInit();
            SuspendLayout();
            // 
            // btnGrant
            // 
            btnGrant.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGrant.Location = new Point(87, 41);
            btnGrant.Name = "btnGrant";
            btnGrant.Size = new Size(407, 60);
            btnGrant.TabIndex = 0;
            btnGrant.Text = "Cấp quyền cho User/Role";
            btnGrant.UseVisualStyleBackColor = true;
            btnGrant.Click += btnGrant_Click;
            // 
            // btnRevoke
            // 
            btnRevoke.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRevoke.Location = new Point(554, 41);
            btnRevoke.Name = "btnRevoke";
            btnRevoke.Size = new Size(407, 60);
            btnRevoke.TabIndex = 1;
            btnRevoke.Text = "Thu quyền cho User/Role";
            btnRevoke.UseVisualStyleBackColor = true;
            btnRevoke.Click += btnRevoke_Click;
            // 
            // dgvColumn
            // 
            dgvColumn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvColumn.Location = new Point(77, 768);
            dgvColumn.Name = "dgvColumn";
            dgvColumn.RowHeadersWidth = 82;
            dgvColumn.Size = new Size(1547, 503);
            // 
            // dgvTable
            // 
            dgvTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTable.Location = new Point(77, 201);
            dgvTable.Name = "dgvTable";
            dgvTable.RowHeadersWidth = 82;
            dgvTable.Size = new Size(1547, 503);
            dgvTable.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(77, 153);
            label1.Name = "label1";
            label1.Size = new Size(114, 45);
            label1.TabIndex = 5;
            label1.Text = "TABLE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(77, 720);
            label2.Name = "label2";
            label2.Size = new Size(158, 45);
            label2.TabIndex = 6;
            label2.Text = "COLUMN";
            // 
            // btnSelect
            // 
            btnSelect.Font = new Font("Segoe UI", 12F);
            btnSelect.Location = new Point(1462, 130);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(162, 65);
            btnSelect.TabIndex = 7;
            btnSelect.Text = "SELECT";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1214, 19);
            label3.Name = "label3";
            label3.Size = new Size(204, 32);
            label3.TabIndex = 8;
            label3.Text = "Tìm kiếm Grantee";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1218, 63);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(200, 39);
            textBox1.TabIndex = 9;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1462, 59);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(150, 46);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(1551, 1285);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(150, 46);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // PhanHe1_GrantRevokeUserRole
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1713, 1343);
            Controls.Add(btnBack);
            Controls.Add(btnSearch);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(btnSelect);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvTable);
            Controls.Add(dgvColumn);
            Controls.Add(btnRevoke);
            Controls.Add(btnGrant);
            Name = "PhanHe1_GrantRevokeUserRole";
            Text = "PhanHe1_GrantRevokeUserRole";
            ((System.ComponentModel.ISupportInitialize)dgvColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGrant;
        private Button btnRevoke;
        private DataGridView dgvColumn;
        private DataGridView dgvTable;
        private Label label1;
        private Label label2;
        private Button btnSelect;
        private Label label3;
        private TextBox textBox1;
        private Button btnSearch;
        private Button btnBack;
    }
}