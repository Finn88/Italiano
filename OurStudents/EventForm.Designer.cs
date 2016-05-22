using System.Windows.Forms;
namespace OurStudents
{
    partial class EventForm
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
            this.clbPartisipents = new System.Windows.Forms.CheckedListBox();
            this.cmbCheff = new System.Windows.Forms.ComboBox();
            this.lbStudentGroup = new System.Windows.Forms.Label();
            this.lbEventDate = new System.Windows.Forms.Label();
            this.dtEventDate = new System.Windows.Forms.DateTimePicker();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.lbComments = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbEventTime = new System.Windows.Forms.Label();
            this.dtTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTimeTo = new System.Windows.Forms.DateTimePicker();
            this.tbCosts = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMenu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEventName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clbPartisipents
            // 
            this.clbPartisipents.FormattingEnabled = true;
            this.clbPartisipents.Location = new System.Drawing.Point(424, 160);
            this.clbPartisipents.Name = "clbPartisipents";
            this.clbPartisipents.ScrollAlwaysVisible = true;
            this.clbPartisipents.Size = new System.Drawing.Size(199, 94);
            this.clbPartisipents.TabIndex = 0;
            // 
            // cmbCheff
            // 
            this.cmbCheff.FormattingEnabled = true;
            this.cmbCheff.Location = new System.Drawing.Point(424, 11);
            this.cmbCheff.Name = "cmbCheff";
            this.cmbCheff.Size = new System.Drawing.Size(199, 21);
            this.cmbCheff.TabIndex = 99;
            // 
            // lbStudentGroup
            // 
            this.lbStudentGroup.AutoSize = true;
            this.lbStudentGroup.Location = new System.Drawing.Point(338, 15);
            this.lbStudentGroup.Name = "lbStudentGroup";
            this.lbStudentGroup.Size = new System.Drawing.Size(33, 13);
            this.lbStudentGroup.TabIndex = 98;
            this.lbStudentGroup.Text = "Шеф:";
            // 
            // lbEventDate
            // 
            this.lbEventDate.AutoSize = true;
            this.lbEventDate.Location = new System.Drawing.Point(20, 159);
            this.lbEventDate.Name = "lbEventDate";
            this.lbEventDate.Size = new System.Drawing.Size(99, 13);
            this.lbEventDate.TabIndex = 94;
            this.lbEventDate.Text = "Дата проведения:";
            // 
            // dtEventDate
            // 
            this.dtEventDate.CustomFormat = "yyyy/MM/dd";
            this.dtEventDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEventDate.Location = new System.Drawing.Point(169, 157);
            this.dtEventDate.Name = "dtEventDate";
            this.dtEventDate.Size = new System.Drawing.Size(87, 20);
            this.dtEventDate.TabIndex = 95;
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Location = new System.Drawing.Point(35, 272);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(76, 17);
            this.cbIsActive.TabIndex = 91;
            this.cbIsActive.Text = "Активный";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // tbComments
            // 
            this.tbComments.Location = new System.Drawing.Point(424, 49);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbComments.Size = new System.Drawing.Size(199, 94);
            this.tbComments.TabIndex = 87;
            // 
            // lbComments
            // 
            this.lbComments.AutoSize = true;
            this.lbComments.Location = new System.Drawing.Point(338, 52);
            this.lbComments.Name = "lbComments";
            this.lbComments.Size = new System.Drawing.Size(80, 13);
            this.lbComments.TabIndex = 79;
            this.lbComments.Text = "Комментарии:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(531, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 93;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(419, 305);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += btnSave_Click;
            // 
            // lbEventTime
            // 
            this.lbEventTime.AutoSize = true;
            this.lbEventTime.Location = new System.Drawing.Point(20, 192);
            this.lbEventTime.Name = "lbEventTime";
            this.lbEventTime.Size = new System.Drawing.Size(43, 13);
            this.lbEventTime.TabIndex = 100;
            this.lbEventTime.Text = "Время:";
            // 
            // dtTimeFrom
            // 
            this.dtTimeFrom.CustomFormat = "HH:mm";
            this.dtTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTimeFrom.Location = new System.Drawing.Point(91, 188);
            this.dtTimeFrom.Name = "dtTimeFrom";
            this.dtTimeFrom.ShowUpDown = true;
            this.dtTimeFrom.Size = new System.Drawing.Size(59, 20);
            this.dtTimeFrom.TabIndex = 101;
            this.dtTimeFrom.Value = new System.DateTime(2016, 2, 18, 8, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "До";
            // 
            // dtTimeTo
            // 
            this.dtTimeTo.CustomFormat = "HH:mm";
            this.dtTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTimeTo.Location = new System.Drawing.Point(196, 188);
            this.dtTimeTo.Name = "dtTimeTo";
            this.dtTimeTo.ShowUpDown = true;
            this.dtTimeTo.Size = new System.Drawing.Size(59, 20);
            this.dtTimeTo.TabIndex = 103;
            this.dtTimeTo.Value = new System.DateTime(2016, 2, 18, 13, 0, 0, 0);
            // 
            // tbCosts
            // 
            this.tbCosts.Location = new System.Drawing.Point(79, 223);
            this.tbCosts.Name = "tbCosts";
            this.tbCosts.Size = new System.Drawing.Size(70, 20);
            this.tbCosts.TabIndex = 105;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 106;
            this.label3.Text = "Цена:";
            // 
            // tbMenu
            // 
            this.tbMenu.Location = new System.Drawing.Point(93, 49);
            this.tbMenu.Multiline = true;
            this.tbMenu.Name = "tbMenu";
            this.tbMenu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMenu.Size = new System.Drawing.Size(163, 94);
            this.tbMenu.TabIndex = 87;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Меню:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "Участники:";
            // 
            // tbEventName
            // 
            this.tbEventName.Location = new System.Drawing.Point(93, 12);
            this.tbEventName.Name = "tbEventName";
            this.tbEventName.Size = new System.Drawing.Size(163, 20);
            this.tbEventName.TabIndex = 109;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 110;
            this.label6.Text = "Название:";
            // 
            // EventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 352);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbEventName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbCosts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtTimeTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbEventTime);
            this.Controls.Add(this.dtTimeFrom);
            this.Controls.Add(this.cmbCheff);
            this.Controls.Add(this.lbStudentGroup);
            this.Controls.Add(this.lbEventDate);
            this.Controls.Add(this.dtEventDate);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.tbMenu);
            this.Controls.Add(this.tbComments);
            this.Controls.Add(this.lbComments);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.clbPartisipents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventForm";
            this.Text = "Мастер-класс";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetCosts(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string.Format("{0:#,##0.00}", double.Parse((sender as TextBox).Text));
        }

        private new void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        #endregion

        public System.Windows.Forms.CheckedListBox clbPartisipents;
        private System.Windows.Forms.ComboBox cmbCheff;
        private System.Windows.Forms.Label lbStudentGroup;
        private System.Windows.Forms.Label lbEventDate;
        private System.Windows.Forms.DateTimePicker dtEventDate;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.Label lbComments;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbEventTime;
        public System.Windows.Forms.DateTimePicker dtTimeFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DateTimePicker dtTimeTo;
        private System.Windows.Forms.TextBox tbCosts;
        private System.Windows.Forms.Label label3;
        private TextBox tbMenu;
        private Label label4;
        private Label label5;
        private TextBox tbEventName;
        private Label label6;
    }
}