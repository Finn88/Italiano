using System;
using System.Linq;
using Model;
using Model.Grids;
using Model.Tables;

namespace OurStudents
{
    public partial class BudgetEditFrom: EditForm<BudgetEntity>
    {
        public MainForm MainFormLocal;

        public BudgetEditFrom()
        {
            InitializeComponent();
        }
        
        protected override void btnSave_Click(object sender, EventArgs e)
        {
            var costs = Convert.ToDecimal(tbCosts.Text);
            if (!(EditGrid as BudgetGrid).IsEarning)
            {
                if (costs > 0)
                    costs = costs*-1;
            }
            else if ((EditGrid as BudgetGrid).IsEarning)
            {
                if (costs < 0)
                    costs = costs * -1;
            }

            if (String.IsNullOrEmpty(EditId))
            {
                MainForm.Db.Budget.Add(new Budget
                {
                    PaymentDate = dtPaymentDate.Value,
                    Costs = costs,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Comments = tbPaymentsComments.Text,
                    IsEarning = (EditGrid as BudgetGrid).IsEarning
                });
            }
            else
            {
                var payment = MainForm.Db.Budget.FirstOrDefault(c => c.Id == EditId);
                if (payment != null)
                {
                    payment.PaymentDate = dtPaymentDate.Value;
                    payment.Costs = costs;
                    payment.ModifiedDate = DateTime.Now;
                    payment.Comments = tbPaymentsComments.Text;
                    payment.IsEarning = (EditGrid as BudgetGrid).IsEarning;
                }
            }

            MainForm.Db.SubmitChanges();
            Hide();
            (EditGrid as BudgetGrid).RefreshGrid();
        }

        public override void CleanFields()
        {
            var defaultPayments = (0).ToString("0.00");
            dtPaymentDate.Value = DateTime.Today;
            tbCosts.Text = defaultPayments;
            tbPaymentsComments.Text = "";
        }
    }
}
