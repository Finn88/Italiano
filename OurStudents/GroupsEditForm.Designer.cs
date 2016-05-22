namespace OurStudents
{
    partial class GroupsEditForm
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
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnGroupCancel = new System.Windows.Forms.Button();
            this.btnGroupSave = new System.Windows.Forms.Button();
            this.tbGroupComments = new System.Windows.Forms.TextBox();
            this.lbGroupComments = new System.Windows.Forms.Label();
            this.lbGroupStartDate = new System.Windows.Forms.Label();
            this.dtGroupStartDate = new System.Windows.Forms.DateTimePicker();
            this.cbGroupIsActive = new System.Windows.Forms.CheckBox();
            this.cmbTeacher = new System.Windows.Forms.ComboBox();
            this.lbTeacher = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.lbLevel = new System.Windows.Forms.Label();
            this.lbGroupName = new System.Windows.Forms.Label();
            this.tbGroupName = new System.Windows.Forms.TextBox();
            this.dtFrom1 = new System.Windows.Forms.DateTimePicker();
            this.dtTo1 = new System.Windows.Forms.DateTimePicker();
            this.cbDay1 = new System.Windows.Forms.CheckBox();
            this.schedulerPanel = new System.Windows.Forms.GroupBox();
            this.cbDay4 = new System.Windows.Forms.CheckBox();
            this.cbDay7 = new System.Windows.Forms.CheckBox();
            this.cbDay3 = new System.Windows.Forms.CheckBox();
            this.cbDay6 = new System.Windows.Forms.CheckBox();
            this.cbDay2 = new System.Windows.Forms.CheckBox();
            this.dtTo4 = new System.Windows.Forms.DateTimePicker();
            this.dtTo7 = new System.Windows.Forms.DateTimePicker();
            this.dtTo3 = new System.Windows.Forms.DateTimePicker();
            this.dtTo6 = new System.Windows.Forms.DateTimePicker();
            this.dtTo2 = new System.Windows.Forms.DateTimePicker();
            this.dtFrom4 = new System.Windows.Forms.DateTimePicker();
            this.dtFrom7 = new System.Windows.Forms.DateTimePicker();
            this.dtFrom3 = new System.Windows.Forms.DateTimePicker();
            this.dtFrom6 = new System.Windows.Forms.DateTimePicker();
            this.dtFrom2 = new System.Windows.Forms.DateTimePicker();
            this.cbDay5 = new System.Windows.Forms.CheckBox();
            this.dtTo5 = new System.Windows.Forms.DateTimePicker();
            this.dtFrom5 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.schedulerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGroupCancel
            // 
            this.btnGroupCancel.Location = new System.Drawing.Point(527, 263);
            this.btnGroupCancel.Name = "btnGroupCancel";
            this.btnGroupCancel.Size = new System.Drawing.Size(75, 23);
            this.btnGroupCancel.TabIndex = 87;
            this.btnGroupCancel.Text = "Отменить";
            this.btnGroupCancel.UseVisualStyleBackColor = true;
            btnGroupCancel.Click += btnCancel_Click;
            // 
            // btnGroupSave
            // 
            this.btnGroupSave.Location = new System.Drawing.Point(415, 263);
            this.btnGroupSave.Name = "btnGroupSave";
            this.btnGroupSave.Size = new System.Drawing.Size(75, 23);
            this.btnGroupSave.TabIndex = 86;
            this.btnGroupSave.Text = "Сохранить";
            this.btnGroupSave.UseVisualStyleBackColor = true;
            this.btnGroupSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbGroupComments
            // 
            this.tbGroupComments.Location = new System.Drawing.Point(403, 19);
            this.tbGroupComments.Multiline = true;
            this.tbGroupComments.Name = "tbGroupComments";
            this.tbGroupComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGroupComments.Size = new System.Drawing.Size(199, 94);
            this.tbGroupComments.TabIndex = 85;
            // 
            // lbGroupComments
            // 
            this.lbGroupComments.AutoSize = true;
            this.lbGroupComments.Location = new System.Drawing.Point(323, 22);
            this.lbGroupComments.Name = "lbGroupComments";
            this.lbGroupComments.Size = new System.Drawing.Size(80, 13);
            this.lbGroupComments.TabIndex = 84;
            this.lbGroupComments.Text = "Комментарии:";
            // 
            // lbGroupStartDate
            // 
            this.lbGroupStartDate.AutoSize = true;
            this.lbGroupStartDate.Location = new System.Drawing.Point(17, 76);
            this.lbGroupStartDate.Name = "lbGroupStartDate";
            this.lbGroupStartDate.Size = new System.Drawing.Size(96, 13);
            this.lbGroupStartDate.TabIndex = 82;
            this.lbGroupStartDate.Text = "Начало обучения:";
            // 
            // dtGroupStartDate
            // 
            this.dtGroupStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtGroupStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtGroupStartDate.Location = new System.Drawing.Point(116, 72);
            this.dtGroupStartDate.Name = "dtGroupStartDate";
            this.dtGroupStartDate.Size = new System.Drawing.Size(87, 20);
            this.dtGroupStartDate.TabIndex = 83;
            // 
            // cbGroupIsActive
            // 
            this.cbGroupIsActive.AutoSize = true;
            this.cbGroupIsActive.Checked = true;
            this.cbGroupIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGroupIsActive.Location = new System.Drawing.Point(22, 267);
            this.cbGroupIsActive.Name = "cbGroupIsActive";
            this.cbGroupIsActive.Size = new System.Drawing.Size(76, 17);
            this.cbGroupIsActive.TabIndex = 81;
            this.cbGroupIsActive.Text = "Активный";
            this.cbGroupIsActive.UseVisualStyleBackColor = true;
            // 
            // cmbTeacher
            // 
            this.cmbTeacher.FormattingEnabled = true;
            this.cmbTeacher.Location = new System.Drawing.Point(116, 45);
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Size = new System.Drawing.Size(199, 21);
            this.cmbTeacher.TabIndex = 80;
            // 
            // lbTeacher
            // 
            this.lbTeacher.AutoSize = true;
            this.lbTeacher.Location = new System.Drawing.Point(16, 48);
            this.lbTeacher.Name = "lbTeacher";
            this.lbTeacher.Size = new System.Drawing.Size(89, 13);
            this.lbTeacher.TabIndex = 79;
            this.lbTeacher.Text = "Преподаватель:";
            // 
            // cbLevel
            // 
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "A1",
            "A2",
            "B1",
            "B2",
            "C1",
            "C2"});
            this.cbLevel.Location = new System.Drawing.Point(116, 98);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(87, 21);
            this.cbLevel.TabIndex = 78;
            // 
            // lbLevel
            // 
            this.lbLevel.AutoSize = true;
            this.lbLevel.Location = new System.Drawing.Point(16, 101);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(54, 13);
            this.lbLevel.TabIndex = 77;
            this.lbLevel.Text = "Уровень:";
            // 
            // lbGroupName
            // 
            this.lbGroupName.AutoSize = true;
            this.lbGroupName.Location = new System.Drawing.Point(16, 22);
            this.lbGroupName.Name = "lbGroupName";
            this.lbGroupName.Size = new System.Drawing.Size(60, 13);
            this.lbGroupName.TabIndex = 76;
            this.lbGroupName.Text = "Название:";
            // 
            // tbGroupName
            // 
            this.tbGroupName.Location = new System.Drawing.Point(116, 19);
            this.tbGroupName.Name = "tbGroupName";
            this.tbGroupName.Size = new System.Drawing.Size(199, 20);
            this.tbGroupName.TabIndex = 75;
            // 
            // dtFrom1
            // 
            this.dtFrom1.CustomFormat = "HH:mm";
            this.dtFrom1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom1.Location = new System.Drawing.Point(56, 16);
            this.dtFrom1.Name = "dtFrom1";
            this.dtFrom1.ShowUpDown = true;
            this.dtFrom1.Size = new System.Drawing.Size(51, 20);
            this.dtFrom1.TabIndex = 88;
            this.dtFrom1.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtTo1
            // 
            this.dtTo1.CustomFormat = "HH:mm";
            this.dtTo1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo1.Location = new System.Drawing.Point(113, 16);
            this.dtTo1.Name = "dtTo1";
            this.dtTo1.ShowUpDown = true;
            this.dtTo1.Size = new System.Drawing.Size(51, 20);
            this.dtTo1.TabIndex = 88;
            this.dtTo1.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // cbDay1
            // 
            this.cbDay1.AutoSize = true;
            this.cbDay1.Location = new System.Drawing.Point(6, 19);
            this.cbDay1.Name = "cbDay1";
            this.cbDay1.Size = new System.Drawing.Size(40, 17);
            this.cbDay1.TabIndex = 89;
            this.cbDay1.Text = "Пн";
            this.cbDay1.UseVisualStyleBackColor = true;
            // 
            // schedulerPanel
            // 
            this.schedulerPanel.Controls.Add(this.cbDay4);
            this.schedulerPanel.Controls.Add(this.cbDay7);
            this.schedulerPanel.Controls.Add(this.cbDay3);
            this.schedulerPanel.Controls.Add(this.cbDay6);
            this.schedulerPanel.Controls.Add(this.cbDay2);
            this.schedulerPanel.Controls.Add(this.dtTo4);
            this.schedulerPanel.Controls.Add(this.dtTo7);
            this.schedulerPanel.Controls.Add(this.dtTo3);
            this.schedulerPanel.Controls.Add(this.dtTo6);
            this.schedulerPanel.Controls.Add(this.dtTo2);
            this.schedulerPanel.Controls.Add(this.dtFrom4);
            this.schedulerPanel.Controls.Add(this.dtFrom7);
            this.schedulerPanel.Controls.Add(this.dtFrom3);
            this.schedulerPanel.Controls.Add(this.dtFrom6);
            this.schedulerPanel.Controls.Add(this.dtFrom2);
            this.schedulerPanel.Controls.Add(this.cbDay5);
            this.schedulerPanel.Controls.Add(this.cbDay1);
            this.schedulerPanel.Controls.Add(this.dtTo5);
            this.schedulerPanel.Controls.Add(this.dtFrom5);
            this.schedulerPanel.Controls.Add(this.dtTo1);
            this.schedulerPanel.Controls.Add(this.dtFrom1);
            this.schedulerPanel.Location = new System.Drawing.Point(17, 125);
            this.schedulerPanel.Name = "schedulerPanel";
            this.schedulerPanel.Size = new System.Drawing.Size(354, 126);
            this.schedulerPanel.TabIndex = 90;
            this.schedulerPanel.TabStop = false;
            this.schedulerPanel.Text = "Расписание";
            // 
            // cbDay4
            // 
            this.cbDay4.AutoSize = true;
            this.cbDay4.Location = new System.Drawing.Point(6, 97);
            this.cbDay4.Name = "cbDay4";
            this.cbDay4.Size = new System.Drawing.Size(39, 17);
            this.cbDay4.TabIndex = 92;
            this.cbDay4.Text = "Чт";
            this.cbDay4.UseVisualStyleBackColor = true;
            // 
            // cbDay7
            // 
            this.cbDay7.AutoSize = true;
            this.cbDay7.Location = new System.Drawing.Point(183, 69);
            this.cbDay7.Name = "cbDay7";
            this.cbDay7.Size = new System.Drawing.Size(39, 17);
            this.cbDay7.TabIndex = 92;
            this.cbDay7.Text = "Вс";
            this.cbDay7.UseVisualStyleBackColor = true;
            // 
            // cbDay3
            // 
            this.cbDay3.AutoSize = true;
            this.cbDay3.Location = new System.Drawing.Point(6, 71);
            this.cbDay3.Name = "cbDay3";
            this.cbDay3.Size = new System.Drawing.Size(39, 17);
            this.cbDay3.TabIndex = 92;
            this.cbDay3.Text = "Ср";
            this.cbDay3.UseVisualStyleBackColor = true;
            // 
            // cbDay6
            // 
            this.cbDay6.AutoSize = true;
            this.cbDay6.Location = new System.Drawing.Point(183, 43);
            this.cbDay6.Name = "cbDay6";
            this.cbDay6.Size = new System.Drawing.Size(39, 17);
            this.cbDay6.TabIndex = 92;
            this.cbDay6.Text = "Сб";
            this.cbDay6.UseVisualStyleBackColor = true;
            // 
            // cbDay2
            // 
            this.cbDay2.AutoSize = true;
            this.cbDay2.Location = new System.Drawing.Point(6, 45);
            this.cbDay2.Name = "cbDay2";
            this.cbDay2.Size = new System.Drawing.Size(38, 17);
            this.cbDay2.TabIndex = 92;
            this.cbDay2.Text = "Вт";
            this.cbDay2.UseVisualStyleBackColor = true;
            // 
            // dtTo4
            // 
            this.dtTo4.CustomFormat = "HH:mm";
            this.dtTo4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo4.Location = new System.Drawing.Point(113, 94);
            this.dtTo4.Name = "dtTo4";
            this.dtTo4.ShowUpDown = true;
            this.dtTo4.Size = new System.Drawing.Size(51, 20);
            this.dtTo4.TabIndex = 90;
            this.dtTo4.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtTo7
            // 
            this.dtTo7.CustomFormat = "HH:mm";
            this.dtTo7.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo7.Location = new System.Drawing.Point(292, 66);
            this.dtTo7.Name = "dtTo7";
            this.dtTo7.ShowUpDown = true;
            this.dtTo7.Size = new System.Drawing.Size(51, 20);
            this.dtTo7.TabIndex = 90;
            this.dtTo7.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtTo3
            // 
            this.dtTo3.CustomFormat = "HH:mm";
            this.dtTo3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo3.Location = new System.Drawing.Point(113, 68);
            this.dtTo3.Name = "dtTo3";
            this.dtTo3.ShowUpDown = true;
            this.dtTo3.Size = new System.Drawing.Size(51, 20);
            this.dtTo3.TabIndex = 90;
            this.dtTo3.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtTo6
            // 
            this.dtTo6.CustomFormat = "HH:mm";
            this.dtTo6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo6.Location = new System.Drawing.Point(292, 40);
            this.dtTo6.Name = "dtTo6";
            this.dtTo6.ShowUpDown = true;
            this.dtTo6.Size = new System.Drawing.Size(51, 20);
            this.dtTo6.TabIndex = 90;
            this.dtTo6.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtTo2
            // 
            this.dtTo2.CustomFormat = "HH:mm";
            this.dtTo2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo2.Location = new System.Drawing.Point(113, 42);
            this.dtTo2.Name = "dtTo2";
            this.dtTo2.ShowUpDown = true;
            this.dtTo2.Size = new System.Drawing.Size(51, 20);
            this.dtTo2.TabIndex = 90;
            this.dtTo2.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtFrom4
            // 
            this.dtFrom4.CustomFormat = "HH:mm";
            this.dtFrom4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom4.Location = new System.Drawing.Point(56, 94);
            this.dtFrom4.Name = "dtFrom4";
            this.dtFrom4.ShowUpDown = true;
            this.dtFrom4.Size = new System.Drawing.Size(51, 20);
            this.dtFrom4.TabIndex = 91;
            this.dtFrom4.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtFrom7
            // 
            this.dtFrom7.CustomFormat = "HH:mm";
            this.dtFrom7.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom7.Location = new System.Drawing.Point(235, 66);
            this.dtFrom7.Name = "dtFrom7";
            this.dtFrom7.ShowUpDown = true;
            this.dtFrom7.Size = new System.Drawing.Size(51, 20);
            this.dtFrom7.TabIndex = 91;
            this.dtFrom7.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtFrom3
            // 
            this.dtFrom3.CustomFormat = "HH:mm";
            this.dtFrom3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom3.Location = new System.Drawing.Point(56, 68);
            this.dtFrom3.Name = "dtFrom3";
            this.dtFrom3.ShowUpDown = true;
            this.dtFrom3.Size = new System.Drawing.Size(51, 20);
            this.dtFrom3.TabIndex = 91;
            this.dtFrom3.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtFrom6
            // 
            this.dtFrom6.CustomFormat = "HH:mm";
            this.dtFrom6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom6.Location = new System.Drawing.Point(235, 40);
            this.dtFrom6.Name = "dtFrom6";
            this.dtFrom6.ShowUpDown = true;
            this.dtFrom6.Size = new System.Drawing.Size(51, 20);
            this.dtFrom6.TabIndex = 91;
            this.dtFrom6.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtFrom2
            // 
            this.dtFrom2.CustomFormat = "HH:mm";
            this.dtFrom2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom2.Location = new System.Drawing.Point(56, 42);
            this.dtFrom2.Name = "dtFrom2";
            this.dtFrom2.ShowUpDown = true;
            this.dtFrom2.Size = new System.Drawing.Size(51, 20);
            this.dtFrom2.TabIndex = 91;
            this.dtFrom2.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // cbDay5
            // 
            this.cbDay5.AutoSize = true;
            this.cbDay5.Location = new System.Drawing.Point(184, 17);
            this.cbDay5.Name = "cbDay5";
            this.cbDay5.Size = new System.Drawing.Size(39, 17);
            this.cbDay5.TabIndex = 89;
            this.cbDay5.Text = "Пт";
            this.cbDay5.UseVisualStyleBackColor = true;
            // 
            // dtTo5
            // 
            this.dtTo5.CustomFormat = "HH:mm";
            this.dtTo5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo5.Location = new System.Drawing.Point(292, 14);
            this.dtTo5.Name = "dtTo5";
            this.dtTo5.ShowUpDown = true;
            this.dtTo5.Size = new System.Drawing.Size(51, 20);
            this.dtTo5.TabIndex = 88;
            this.dtTo5.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // dtFrom5
            // 
            this.dtFrom5.CustomFormat = "HH:mm";
            this.dtFrom5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom5.Location = new System.Drawing.Point(235, 14);
            this.dtFrom5.Name = "dtFrom5";
            this.dtFrom5.ShowUpDown = true;
            this.dtFrom5.Size = new System.Drawing.Size(51, 20);
            this.dtFrom5.TabIndex = 88;
            this.dtFrom5.Value = new System.DateTime(2016, 2, 21, 0, 0, 0, 0);
            // 
            // GroupsEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 309);
            this.ControlBox = false;
            this.Controls.Add(this.schedulerPanel);
            this.Controls.Add(this.btnGroupCancel);
            this.Controls.Add(this.btnGroupSave);
            this.Controls.Add(this.tbGroupComments);
            this.Controls.Add(this.lbGroupComments);
            this.Controls.Add(this.lbGroupStartDate);
            this.Controls.Add(this.dtGroupStartDate);
            this.Controls.Add(this.cbGroupIsActive);
            this.Controls.Add(this.cmbTeacher);
            this.Controls.Add(this.lbTeacher);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.lbLevel);
            this.Controls.Add(this.lbGroupName);
            this.Controls.Add(this.tbGroupName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupsEditForm";
            this.Text = "Редактировать";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.schedulerPanel.ResumeLayout(false);
            this.schedulerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button btnGroupCancel;
        private System.Windows.Forms.Button btnGroupSave;
        private System.Windows.Forms.TextBox tbGroupComments;
        private System.Windows.Forms.Label lbGroupComments;
        private System.Windows.Forms.Label lbGroupStartDate;
        private System.Windows.Forms.DateTimePicker dtGroupStartDate;
        private System.Windows.Forms.CheckBox cbGroupIsActive;
        private System.Windows.Forms.ComboBox cmbTeacher;
        private System.Windows.Forms.Label lbTeacher;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label lbLevel;
        private System.Windows.Forms.Label lbGroupName;
        private System.Windows.Forms.TextBox tbGroupName;
        private System.Windows.Forms.DateTimePicker dtFrom1;
        private System.Windows.Forms.DateTimePicker dtTo1;
        private System.Windows.Forms.CheckBox cbDay1;
        private System.Windows.Forms.GroupBox schedulerPanel;
        private System.Windows.Forms.CheckBox cbDay4;
        private System.Windows.Forms.CheckBox cbDay7;
        private System.Windows.Forms.CheckBox cbDay3;
        private System.Windows.Forms.CheckBox cbDay6;
        private System.Windows.Forms.CheckBox cbDay2;
        private System.Windows.Forms.DateTimePicker dtTo4;
        private System.Windows.Forms.DateTimePicker dtTo7;
        private System.Windows.Forms.DateTimePicker dtTo3;
        private System.Windows.Forms.DateTimePicker dtTo6;
        private System.Windows.Forms.DateTimePicker dtTo2;
        private System.Windows.Forms.DateTimePicker dtFrom4;
        private System.Windows.Forms.DateTimePicker dtFrom7;
        private System.Windows.Forms.DateTimePicker dtFrom3;
        private System.Windows.Forms.DateTimePicker dtFrom6;
        private System.Windows.Forms.DateTimePicker dtFrom2;
        private System.Windows.Forms.CheckBox cbDay5;
        private System.Windows.Forms.DateTimePicker dtTo5;
        private System.Windows.Forms.DateTimePicker dtFrom5;
    }
}