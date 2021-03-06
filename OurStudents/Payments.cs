﻿using System;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using Model;
using Model.Grids;
using Model.Tables;

namespace OurStudents
{
    public partial class PaymentsEditForm : EditForm<PaymentEntity>
    {
        public MainForm MainFormLocal;

        public PaymentsEditForm()
        {
            InitializeComponent();
            cmbPaymentFor.ValueMember = "Code";
            cmbPaymentFor.DisplayMember = "Name";
            cmbPaymentFor.DataSource = MainForm.Db.PaymentReportSettings;
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(EditId ))
            {
                MainForm.Db.Payments.Add(new Payments
                {
                    DateFrom = dtPaymentsStartDate.Value,
                    DateTo = dtPaymentsEndDate.Value,
                    PaymentDate = dtPaymentDate.Value,
                    Costs = Convert.ToDecimal(tbCosts.Text),
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Comments = tbPaymentsComments.Text,
                    PersonId = DetailsId,
                    PaymentType = (char)cmbPaymentFor.SelectedValue
                });
            }
            else
            {
                var payment = MainForm.Db.Payments.FirstOrDefault(c => c.Id == EditId);
                if (payment != null)
                {
                    payment.DateFrom = dtPaymentsStartDate.Value;
                    payment.DateTo = dtPaymentsEndDate.Value;
                    payment.PaymentDate = dtPaymentDate.Value;
                    payment.Costs = Convert.ToDecimal(tbCosts.Text);
                    payment.ModifiedDate = DateTime.Now;
                    payment.Comments = tbPaymentsComments.Text;
                    payment.PaymentType = (char) cmbPaymentFor.SelectedValue;
                }
            }

            MainForm.Db.SubmitChanges();
            this.Hide();
            (EditGrid as PaymentsGrid).RefreshGrid();
        }

        public override void CleanFields()
        {
            var defaultPayments = (0).ToString("0.00");
            cmbPaymentFor.ValueMember = "Code";
            cmbPaymentFor.DisplayMember = "Name";
            cmbPaymentFor.DataSource = MainForm.Db.PaymentReportSettings;
            cmbPaymentFor.SelectedValue = 'Z';
            var person=MainForm.Db.Persons.FirstOrDefault(c => c.Id == DetailsId);
            if (person != null && person.GroupId != null)
            {
                var group = MainForm.Db.Groups.FirstOrDefault(c => c.Id == person.GroupId);
                if (group != null)
                {
                    defaultPayments = (group.GroupType ==
                                       (char) GroupType.Common
                        ? AppSettings.DefaultCosts
                        : AppSettings.DefaultCostsSingle).ToString("0.00");
                    cmbPaymentFor.SelectedValue = (group.GroupType ==
                                                   (char) GroupType.Common
                        ? 'A'
                        : 'B');
                }
            }

            dtPaymentsStartDate.Value = DateTime.Today;
            dtPaymentsEndDate.Value = DateTime.Today;
            dtPaymentDate.Value = DateTime.Today;
            tbCosts.Text = defaultPayments;
            tbPaymentsComments.Text = "";
        }
    }
}
