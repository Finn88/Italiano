using System.Windows.Forms;

namespace OurStudents
{
    partial class BudgetEditFrom
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
            this.btnBudgetCancel = new System.Windows.Forms.Button();
            this.btnBudgetSave = new System.Windows.Forms.Button();
            this.tbPaymentsComments = new System.Windows.Forms.TextBox();
            this.lbPaymentsComments = new System.Windows.Forms.Label();
            this.lbCosts = new System.Windows.Forms.Label();
            this.tbCosts = new System.Windows.Forms.TextBox();
            this.dtPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.lbPaymentDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGroupCancel
            // 
            this.btnBudgetCancel.Location = new System.Drawing.Point(378, 131);
            this.btnBudgetCancel.Name = "btnGroupCancel";
            this.btnBudgetCancel.Size = new System.Drawing.Size(75, 23);
            this.btnBudgetCancel.TabIndex = 87;
            this.btnBudgetCancel.Text = "Отменить";
            this.btnBudgetCancel.UseVisualStyleBackColor = true;
            this.btnBudgetCancel.Click += btnCancel_Click;
            // 
            // btnGroupSave
            // 
            this.btnBudgetSave.Location = new System.Drawing.Point(266, 131);
            this.btnBudgetSave.Name = "btnGroupSave";
            this.btnBudgetSave.Size = new System.Drawing.Size(75, 23);
            this.btnBudgetSave.TabIndex = 86;
            this.btnBudgetSave.Text = "Сохранить";
            this.btnBudgetSave.UseVisualStyleBackColor = true;
            this.btnBudgetSave.Click += btnSave_Click;
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
            // lbCosts
            // 
            this.lbCosts.AutoSize = true;
            this.lbCosts.Location = new System.Drawing.Point(15, 19);
            this.lbCosts.Name = "lbCosts";
            this.lbCosts.Size = new System.Drawing.Size(44, 13);
            this.lbCosts.TabIndex = 92;
            this.lbCosts.Text = "Сумма:";
            tbCosts.KeyUp += SetCosts;
            tbCosts.KeyPress += KeyPress;
            // 
            // tbCosts
            // 
            this.tbCosts.Location = new System.Drawing.Point(93, 16);
            this.tbCosts.Name = "tbCosts";
            this.tbCosts.Size = new System.Drawing.Size(63, 20);
            this.tbCosts.TabIndex = 93;
         
            this.tbCosts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtPaymentDate
            // 
            this.dtPaymentDate.CustomFormat = "yyyy/MM/dd";
            this.dtPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPaymentDate.Location = new System.Drawing.Point(93, 65);
            this.dtPaymentDate.Name = "dtPaymentDate";
            this.dtPaymentDate.Size = new System.Drawing.Size(87, 20);
            this.dtPaymentDate.TabIndex = 94;
            // 
            // lbPaymentDate
            // 
            this.lbPaymentDate.AutoSize = true;
            this.lbPaymentDate.Location = new System.Drawing.Point(15, 70);
            this.lbPaymentDate.Name = "lbPaymentDate";
            this.lbPaymentDate.Size = new System.Drawing.Size(76, 13);
            this.lbPaymentDate.TabIndex = 95;
            this.lbPaymentDate.Text = "Дата оплаты:";
            // 
            // BudgetEditFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 168);
            this.ControlBox = false;
            this.Controls.Add(this.lbPaymentDate);
            this.Controls.Add(this.dtPaymentDate);
            this.Controls.Add(this.tbCosts);
            this.Controls.Add(this.lbCosts);
            this.Controls.Add(this.btnBudgetCancel);
            this.Controls.Add(this.btnBudgetSave);
            this.Controls.Add(this.tbPaymentsComments);
            this.Controls.Add(this.lbPaymentsComments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BudgetEditFrom";
            this.Text = "Редактировать";
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

        private System.Windows.Forms.Button btnBudgetCancel;
        private System.Windows.Forms.Button btnBudgetSave;
        private System.Windows.Forms.TextBox tbPaymentsComments;
        private System.Windows.Forms.Label lbPaymentsComments;
        private System.Windows.Forms.Label lbCosts;
        private System.Windows.Forms.TextBox tbCosts;
        private System.Windows.Forms.DateTimePicker dtPaymentDate;
        private System.Windows.Forms.Label lbPaymentDate;

    }
}