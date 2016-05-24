using System.Windows.Forms;

namespace OurStudents
{
    partial class PaymentsEditForm
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
            this.btnGroupCancel = new System.Windows.Forms.Button();
            this.btnGroupSave = new System.Windows.Forms.Button();
            this.tbPaymentsComments = new System.Windows.Forms.TextBox();
            this.lbPaymentsComments = new System.Windows.Forms.Label();
            this.lbPaymentsStartDate = new System.Windows.Forms.Label();
            this.dtPaymentsStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtPaymentsEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gbPaymentDates = new System.Windows.Forms.GroupBox();
            this.lbCosts = new System.Windows.Forms.Label();
            this.tbCosts = new System.Windows.Forms.TextBox();
            this.dtPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.lbPaymentDate = new System.Windows.Forms.Label();
            this.cmbPaymentFor = new System.Windows.Forms.ComboBox();
            this.lbPaymentFor = new System.Windows.Forms.Label();
            this.gbPaymentDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGroupCancel
            // 
            this.btnGroupCancel.Location = new System.Drawing.Point(366, 174);
            this.btnGroupCancel.Name = "btnGroupCancel";
            this.btnGroupCancel.Size = new System.Drawing.Size(75, 23);
            this.btnGroupCancel.TabIndex = 87;
            this.btnGroupCancel.Text = "Отменить";
            this.btnGroupCancel.UseVisualStyleBackColor = true;
            this.btnGroupCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGroupSave
            // 
            this.btnGroupSave.Location = new System.Drawing.Point(254, 174);
            this.btnGroupSave.Name = "btnGroupSave";
            this.btnGroupSave.Size = new System.Drawing.Size(75, 23);
            this.btnGroupSave.TabIndex = 86;
            this.btnGroupSave.Text = "Сохранить";
            this.btnGroupSave.UseVisualStyleBackColor = true;
            this.btnGroupSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbPaymentsComments
            // 
            this.tbPaymentsComments.Location = new System.Drawing.Point(267, 16);
            this.tbPaymentsComments.Multiline = true;
            this.tbPaymentsComments.Name = "tbPaymentsComments";
            this.tbPaymentsComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPaymentsComments.Size = new System.Drawing.Size(199, 94);
            this.tbPaymentsComments.TabIndex = 85;
            // 
            // lbPaymentsComments
            // 
            this.lbPaymentsComments.AutoSize = true;
            this.lbPaymentsComments.Location = new System.Drawing.Point(181, 16);
            this.lbPaymentsComments.Name = "lbPaymentsComments";
            this.lbPaymentsComments.Size = new System.Drawing.Size(80, 13);
            this.lbPaymentsComments.TabIndex = 84;
            this.lbPaymentsComments.Text = "Комментарии:";
            // 
            // lbPaymentsStartDate
            // 
            this.lbPaymentsStartDate.AutoSize = true;
            this.lbPaymentsStartDate.Location = new System.Drawing.Point(6, 26);
            this.lbPaymentsStartDate.Name = "lbPaymentsStartDate";
            this.lbPaymentsStartDate.Size = new System.Drawing.Size(23, 13);
            this.lbPaymentsStartDate.TabIndex = 82;
            this.lbPaymentsStartDate.Text = "От:";
            // 
            // dtPaymentsStartDate
            // 
            this.dtPaymentsStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtPaymentsStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPaymentsStartDate.Location = new System.Drawing.Point(35, 24);
            this.dtPaymentsStartDate.Name = "dtPaymentsStartDate";
            this.dtPaymentsStartDate.Size = new System.Drawing.Size(87, 20);
            this.dtPaymentsStartDate.TabIndex = 83;
            // 
            // dtPaymentsEndDate
            // 
            this.dtPaymentsEndDate.CustomFormat = "yyyy/MM/dd";
            this.dtPaymentsEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPaymentsEndDate.Location = new System.Drawing.Point(35, 50);
            this.dtPaymentsEndDate.Name = "dtPaymentsEndDate";
            this.dtPaymentsEndDate.Size = new System.Drawing.Size(87, 20);
            this.dtPaymentsEndDate.TabIndex = 83;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "До:";
            // 
            // gbPaymentDates
            // 
            this.gbPaymentDates.Controls.Add(this.dtPaymentsEndDate);
            this.gbPaymentDates.Controls.Add(this.label1);
            this.gbPaymentDates.Controls.Add(this.dtPaymentsStartDate);
            this.gbPaymentDates.Controls.Add(this.lbPaymentsStartDate);
            this.gbPaymentDates.Location = new System.Drawing.Point(12, 16);
            this.gbPaymentDates.Name = "gbPaymentDates";
            this.gbPaymentDates.Size = new System.Drawing.Size(139, 88);
            this.gbPaymentDates.TabIndex = 90;
            this.gbPaymentDates.TabStop = false;
            this.gbPaymentDates.Text = "Оплачено за период:";
            // 
            // lbCosts
            // 
            this.lbCosts.AutoSize = true;
            this.lbCosts.Location = new System.Drawing.Point(15, 121);
            this.lbCosts.Name = "lbCosts";
            this.lbCosts.Size = new System.Drawing.Size(44, 13);
            this.lbCosts.TabIndex = 92;
            this.lbCosts.Text = "Сумма:";
            // 
            // tbCosts
            // 
            this.tbCosts.Location = new System.Drawing.Point(93, 118);
            this.tbCosts.Name = "tbCosts";
            this.tbCosts.Size = new System.Drawing.Size(63, 20);
            this.tbCosts.TabIndex = 93;
            this.tbCosts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtPaymentDate
            // 
            this.dtPaymentDate.CustomFormat = "yyyy/MM/dd";
            this.dtPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPaymentDate.Location = new System.Drawing.Point(93, 150);
            this.dtPaymentDate.Name = "dtPaymentDate";
            this.dtPaymentDate.Size = new System.Drawing.Size(87, 20);
            this.dtPaymentDate.TabIndex = 94;
            // 
            // lbPaymentDate
            // 
            this.lbPaymentDate.AutoSize = true;
            this.lbPaymentDate.Location = new System.Drawing.Point(15, 155);
            this.lbPaymentDate.Name = "lbPaymentDate";
            this.lbPaymentDate.Size = new System.Drawing.Size(76, 13);
            this.lbPaymentDate.TabIndex = 95;
            this.lbPaymentDate.Text = "Дата оплаты:";
            // 
            // cmbPaymentFor
            // 
            this.cmbPaymentFor.FormattingEnabled = true;
            this.cmbPaymentFor.Location = new System.Drawing.Point(267, 118);
            this.cmbPaymentFor.Name = "cmbPaymentFor";
            this.cmbPaymentFor.Size = new System.Drawing.Size(135, 21);
            this.cmbPaymentFor.TabIndex = 96;
            // 
            // lbPaymentFor
            // 
            this.lbPaymentFor.AutoSize = true;
            this.lbPaymentFor.Location = new System.Drawing.Point(181, 121);
            this.lbPaymentFor.Name = "lbPaymentFor";
            this.lbPaymentFor.Size = new System.Drawing.Size(62, 13);
            this.lbPaymentFor.TabIndex = 97;
            this.lbPaymentFor.Text = "Оплата за:";
            // 
            // PaymentsEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 211);
            this.ControlBox = false;
            this.Controls.Add(this.lbPaymentFor);
            this.Controls.Add(this.cmbPaymentFor);
            this.Controls.Add(this.lbPaymentDate);
            this.Controls.Add(this.dtPaymentDate);
            this.Controls.Add(this.tbCosts);
            this.Controls.Add(this.lbCosts);
            this.Controls.Add(this.gbPaymentDates);
            this.Controls.Add(this.btnGroupCancel);
            this.Controls.Add(this.btnGroupSave);
            this.Controls.Add(this.tbPaymentsComments);
            this.Controls.Add(this.lbPaymentsComments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentsEditForm";
            this.Text = "Редактировать";
            this.gbPaymentDates.ResumeLayout(false);
            this.gbPaymentDates.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetCosts(object sender, System.Windows.Forms.KeyEventArgs e)
        {
           // string.Format("{0:#,##0.00}", double.Parse((sender as TextBox).Text));
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

        private System.Windows.Forms.Button btnGroupCancel;
        private System.Windows.Forms.Button btnGroupSave;
        private System.Windows.Forms.TextBox tbPaymentsComments;
        private System.Windows.Forms.Label lbPaymentsComments;
        private System.Windows.Forms.Label lbPaymentsStartDate;
        private System.Windows.Forms.DateTimePicker dtPaymentsStartDate;
        private System.Windows.Forms.DateTimePicker dtPaymentsEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPaymentDates;
        private System.Windows.Forms.Label lbCosts;
        private System.Windows.Forms.TextBox tbCosts;
        private System.Windows.Forms.DateTimePicker dtPaymentDate;
        private System.Windows.Forms.Label lbPaymentDate;
        private ComboBox cmbPaymentFor;
        private Label lbPaymentFor;

    }
}