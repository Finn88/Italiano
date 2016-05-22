using System.Windows.Forms;

namespace OurStudents
{
    partial class Search
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
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.searchYes = new System.Windows.Forms.Button();
            this.selectedColumns = new System.Windows.Forms.CheckedListBox();
            this.lbCol = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(83, 65);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(183, 20);
            this.searchTextBox.TabIndex = 0;
            // 
            // searchCancel
            // 
            this.searchCancel.Location = new System.Drawing.Point(191, 245);
            this.searchCancel.Name = "searchCancel";
            this.searchCancel.Size = new System.Drawing.Size(75, 23);
            this.searchCancel.TabIndex = 1;
            this.searchCancel.Text = "Отмена";
            this.searchCancel.UseVisualStyleBackColor = true;
            this.searchCancel.Click += new System.EventHandler(this.searchCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Найти:";
            // 
            // searchYes
            // 
            this.searchYes.Location = new System.Drawing.Point(70, 245);
            this.searchYes.Name = "searchYes";
            this.searchYes.Size = new System.Drawing.Size(75, 23);
            this.searchYes.TabIndex = 3;
            this.searchYes.Text = "OK";
            this.searchYes.UseVisualStyleBackColor = true;
            this.searchYes.Click += new System.EventHandler(this.searchYes_Click);
            // 
            // selectedColumns
            // 
            this.selectedColumns.FormattingEnabled = true;
            this.selectedColumns.Location = new System.Drawing.Point(85, 91);
            this.selectedColumns.Name = "selectedColumns";
            this.selectedColumns.Size = new System.Drawing.Size(181, 139);
            this.selectedColumns.TabIndex = 4;
            this.selectedColumns.SelectedIndexChanged += new System.EventHandler(this.selectedColumns_SelectedIndexChanged);
            this.selectedColumns.CheckOnClick = true;
            // 
            // lbCol
            // 
            this.lbCol.AutoSize = true;
            this.lbCol.Location = new System.Drawing.Point(23, 91);
            this.lbCol.Name = "lbCol";
            this.lbCol.Size = new System.Drawing.Size(56, 13);
            this.lbCol.TabIndex = 5;
            this.lbCol.Text = "Искать в:";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 289);
            this.ControlBox = false;
            this.Controls.Add(this.lbCol);
            this.Controls.Add(this.selectedColumns);
            this.Controls.Add(this.searchYes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchCancel);
            this.Controls.Add(this.searchTextBox);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Search";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Поиск";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button searchYes;
        private CheckedListBox selectedColumns;
        private Label lbCol;
    }
}