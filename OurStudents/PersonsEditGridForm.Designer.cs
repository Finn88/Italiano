using System;
using System.Drawing;
using System.Linq;
using Model.Tables;

namespace OurStudents
{
    partial class PersonsEditGridForm
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
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.lbFirstName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbLastName = new System.Windows.Forms.Label();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.lbPhone = new System.Windows.Forms.Label();
            this.lbComments = new System.Windows.Forms.Label();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.tbPhone = new System.Windows.Forms.MaskedTextBox();
            this.lbEmail = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.rbMail = new System.Windows.Forms.RadioButton();
            this.rbFemail = new System.Windows.Forms.RadioButton();
            this.lbSex = new System.Windows.Forms.Label();
            this.dtSecondaryDate = new System.Windows.Forms.DateTimePicker();
            this.lbSecondaryDate = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.lbMName = new System.Windows.Forms.Label();
            this.tbMName = new System.Windows.Forms.TextBox();
            this.lbBirthDate = new System.Windows.Forms.Label();
            this.dtBirthDate = new System.Windows.Forms.DateTimePicker();
            this.lbAgeValueStrudent = new System.Windows.Forms.Label();
            this.lbAgeStrudent = new System.Windows.Forms.Label();
            this.lbStudentGroup = new System.Windows.Forms.Label();
            this.cmbStudenGroup = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            this.Shown += PersonShow;
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(92, 12);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(199, 20);
            this.tbFirstName.TabIndex = 0;

            // 
            
            // 
            this.lbFirstName.AutoSize = true;
            this.lbFirstName.Location = new System.Drawing.Point(12, 15);
            this.lbFirstName.Name = "lbFirstName";
            this.lbFirstName.Size = new System.Drawing.Size(32, 13);
            this.lbFirstName.TabIndex = 1;
            this.lbFirstName.Text = "Имя:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(425, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 60;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(537, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 65;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(12, 38);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(59, 13);
            this.lbLastName.TabIndex = 5;
            this.lbLastName.Text = "Фамилия:";
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(92, 38);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(199, 20);
            this.tbLastName.TabIndex = 5;
 
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Location = new System.Drawing.Point(323, 44);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(55, 13);
            this.lbPhone.TabIndex = 5;
            this.lbPhone.Text = "Телефон:";
            // 
            // lbComments
            // 
            this.lbComments.AutoSize = true;
            this.lbComments.Location = new System.Drawing.Point(12, 139);
            this.lbComments.Name = "lbComments";
            this.lbComments.Size = new System.Drawing.Size(80, 13);
            this.lbComments.TabIndex = 7;
            this.lbComments.Text = "Комментарии:";
            // 
            // tbComments
            // 
            this.tbComments.Location = new System.Drawing.Point(92, 136);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbComments.Size = new System.Drawing.Size(199, 94);
            this.tbComments.TabIndex = 25;
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(413, 38);
            this.tbPhone.Mask = "+00 (999) 000-0000";
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(199, 20);
            this.tbPhone.TabIndex = 20;
            this.tbPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Location = new System.Drawing.Point(323, 15);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(35, 13);
            this.lbEmail.TabIndex = 11;
            this.lbEmail.Text = "Email:";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(413, 12);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(199, 20);
            this.tbEmail.TabIndex = 15;
            // 
            // rbMail
            // 
            this.rbMail.AutoSize = true;
            this.rbMail.Checked = true;
            this.rbMail.Location = new System.Drawing.Point(415, 137);
            this.rbMail.Name = "rbMail";
            this.rbMail.Size = new System.Drawing.Size(50, 17);
            this.rbMail.TabIndex = 30;
            this.rbMail.TabStop = true;
            this.rbMail.Text = "Муж.";
            this.rbMail.UseVisualStyleBackColor = true;
            // 
            // rbFemail
            // 
            this.rbFemail.AutoSize = true;
            this.rbFemail.Location = new System.Drawing.Point(415, 160);
            this.rbFemail.Name = "rbFemail";
            this.rbFemail.Size = new System.Drawing.Size(51, 17);
            this.rbFemail.TabIndex = 35;
            this.rbFemail.Text = "Жен.";
            this.rbFemail.UseVisualStyleBackColor = true;
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Location = new System.Drawing.Point(323, 139);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(30, 13);
            this.lbSex.TabIndex = 14;
            this.lbSex.Text = "Пол:";
            // 
            // dtSecondaryDate
            // 
            this.dtSecondaryDate.CustomFormat = "yyyy/MM/dd";
            this.dtSecondaryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSecondaryDate.Location = new System.Drawing.Point(413, 210);
            this.dtSecondaryDate.Name = "dtSecondaryDate";
            this.dtSecondaryDate.Size = new System.Drawing.Size(87, 20);
            this.dtSecondaryDate.TabIndex = 45;
            // 
            // lbStudyDate
            // 
            this.lbSecondaryDate.AutoSize = true;
            this.lbSecondaryDate.Location = new System.Drawing.Point(323, 215);
            this.lbSecondaryDate.Name = "lbSecondaryDate";
            this.lbSecondaryDate.Size = new System.Drawing.Size(72, 13);
            this.lbSecondaryDate.TabIndex = 18;
            this.lbSecondaryDate.Text = "Обучается с:";
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Checked = true;
            this.cbIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsActive.Location = new System.Drawing.Point(27, 261);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(76, 17);
            this.cbIsActive.TabIndex = 55;
            this.cbIsActive.Text = "Активный";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // lbMName
            // 
            this.lbMName.AutoSize = true;
            this.lbMName.Location = new System.Drawing.Point(12, 64);
            this.lbMName.Name = "lbMName";
            this.lbMName.Size = new System.Drawing.Size(57, 13);
            this.lbMName.TabIndex = 23;
            this.lbMName.Text = "Отчество:";
            // 
            // tbMName
            // 
            this.tbMName.Location = new System.Drawing.Point(92, 64);
            this.tbMName.Name = "tbMName";
            this.tbMName.Size = new System.Drawing.Size(199, 20);
            this.tbMName.TabIndex = 10;
            // 
            // lbBirthDate
            // 
            this.lbBirthDate.AutoSize = true;
            this.lbBirthDate.Location = new System.Drawing.Point(322, 188);
            this.lbBirthDate.Name = "lbBirthDate";
            this.lbBirthDate.Size = new System.Drawing.Size(90, 13);
            this.lbBirthDate.TabIndex = 66;
            this.lbBirthDate.Text = "День рождения:";
            // 
            // dtBirthDate
            // 
            this.dtBirthDate.CustomFormat = "yyyy/MM/dd";
            this.dtBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBirthDate.Location = new System.Drawing.Point(413, 184);
            this.dtBirthDate.Name = "dtBirthDate";
            this.dtBirthDate.Size = new System.Drawing.Size(87, 20);
            this.dtBirthDate.TabIndex = 67;
            this.dtBirthDate.ValueChanged += new System.EventHandler(this.dtBirthDate_ValueChanged);
            // 
            // lbAgeValueStrudent
            // 
            this.lbAgeValueStrudent.AutoSize = true;
            this.lbAgeValueStrudent.Location = new System.Drawing.Point(563, 188);
            this.lbAgeValueStrudent.Name = "lbAgeValueStrudent";
            this.lbAgeValueStrudent.Size = new System.Drawing.Size(13, 13);
            this.lbAgeValueStrudent.TabIndex = 71;
            this.lbAgeValueStrudent.Text = "0";
            // 
            // lbAgeStrudent
            // 
            this.lbAgeStrudent.AutoSize = true;
            this.lbAgeStrudent.Location = new System.Drawing.Point(511, 188);
            this.lbAgeStrudent.Name = "lbAgeStrudent";
            this.lbAgeStrudent.Size = new System.Drawing.Size(52, 13);
            this.lbAgeStrudent.TabIndex = 70;
            this.lbAgeStrudent.Text = "Возраст:";
            // 
            // lbStudentGroup
            // 
            this.lbStudentGroup.AutoSize = true;
            this.lbStudentGroup.Location = new System.Drawing.Point(323, 71);
            this.lbStudentGroup.Name = "lbStudentGroup";
            this.lbStudentGroup.Size = new System.Drawing.Size(45, 13);
            this.lbStudentGroup.TabIndex = 72;
            this.lbStudentGroup.Text = "Группа:";
            // 
            // cmbStudenGroup
            // 
            this.cmbStudenGroup.FormattingEnabled = true;
            this.cmbStudenGroup.Location = new System.Drawing.Point(413, 68);
            this.cmbStudenGroup.Name = "cmbStudenGroup";
            this.cmbStudenGroup.Size = new System.Drawing.Size(199, 21);
            this.cmbStudenGroup.TabIndex = 73;
            // 
            // StudentEditGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 293);
            this.ControlBox = false;
            this.Controls.Add(this.cmbStudenGroup);
            this.Controls.Add(this.lbStudentGroup);
            this.Controls.Add(this.lbAgeValueStrudent);
            this.Controls.Add(this.lbAgeStrudent);
            this.Controls.Add(this.lbBirthDate);
            this.Controls.Add(this.dtBirthDate);
            this.Controls.Add(this.lbMName);
            this.Controls.Add(this.tbMName);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.lbSecondaryDate);
            this.Controls.Add(this.dtSecondaryDate);
            this.Controls.Add(this.lbSex);
            this.Controls.Add(this.rbFemail);
            this.Controls.Add(this.rbMail);
            this.Controls.Add(this.lbEmail);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.tbPhone);
            this.Controls.Add(this.tbComments);
            this.Controls.Add(this.lbComments);
            this.Controls.Add(this.lbPhone);
            this.Controls.Add(this.lbLastName);
            this.Controls.Add(this.tbLastName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbFirstName);
            this.Controls.Add(this.tbFirstName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StudentEditGridForm";
            this.Text = "Редактировать:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label lbFirstName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.Label lbComments;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.MaskedTextBox tbPhone;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.RadioButton rbMail;
        private System.Windows.Forms.RadioButton rbFemail;
        private System.Windows.Forms.Label lbSex;
        private System.Windows.Forms.DateTimePicker dtSecondaryDate;
        private System.Windows.Forms.Label lbSecondaryDate;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.Label lbMName;
        private System.Windows.Forms.TextBox tbMName;
        private System.Windows.Forms.Label lbBirthDate;
        private System.Windows.Forms.DateTimePicker dtBirthDate;
        private System.Windows.Forms.Label lbAgeValueStrudent;
        private System.Windows.Forms.Label lbAgeStrudent;
        private System.Windows.Forms.Label lbStudentGroup;
        private System.Windows.Forms.ComboBox cmbStudenGroup;
    }
}