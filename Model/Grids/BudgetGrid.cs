using System;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using MetroFramework.Controls;


namespace Model.Grids
{
    public class BudgetGrid : Grid<BudgetEntity>
    {
        public decimal Total = 0;
        public DateTime? DateFrom;
        public DateTime? DateTo;
        public bool IsEarning;

        public BudgetGrid(DataBase db, bool isEarning) : base(db)
        {
            Name = "budgetGrid";
            RefreshGrid();
            IsEarning = isEarning;
            GridId = isEarning ? DataBase.GridsIds.EarningsGridId : DataBase.GridsIds.ExpensesGridId;
            var columns = Db.GetColumnsSettingsList(GridId).OrderBy(c => c.OrderNr);
            foreach (var column in columns)
            {
                Columns.Add(new CustomColumn
                {
                    Name = column.ColumnName,
                    HeaderText = column.ColumnHeader,
                    DataPropertyName = column.ColumnName,
                    SortMode = DataGridViewColumnSortMode.Programmatic,
                    Visible = column.IsVisible,
                    Width = column.Width
                });
            }
        }

        protected override void UpdateClick(object sender, EventArgs e, string id)
        {
            var payment = Db.Budget.FirstOrDefault(c => c.Id == id);

            var dialog = EditForm;
            dialog.EditId = id;
            dialog.EditGrid = this;
            dialog.DetailsId = DetailsId;
            
            (dialog.Controls["dtPaymentDate"] as DateTimePicker).Value = payment.PaymentDate;
            (dialog.Controls["tbPaymentsComments"] as TextBox).Text = payment.Comments;
            (dialog.Controls["tbCosts"] as TextBox).Text = payment.Costs.ToString("#.00");

            dialog.ShowDialog();
        }

        protected override void Delete(string id)
        {
            if (id != null)
            {
                var payment = Db.Budget.FirstOrDefault(c => c.Id == id);
                if (payment != null)
                {
                    Db.Budget.Remove(payment);
                    Db.SubmitChanges();
                    RefreshGrid();
                }
            }
        }

        protected override BindingListView<BudgetEntity> GetDataSource()
        {
            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);
            if (DateFrom == null)
                DateFrom = startMonth;
            if (DateTo == null)
                DateTo = endMonth;

            return Db.GetBudgetList(IsEarning, DateFrom.Value, DateTo.Value);
        }

        private BindingListView<BudgetEntity> GetDataSource(DateTime datefrom, DateTime dateto)
        {
            return Db.GetBudgetList(IsEarning, datefrom, dateto);
        }

        public override void RefreshGrid()
        {
            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);
            if (DateFrom == null)
                DateFrom = startMonth;
            if (DateTo == null)
                DateTo = endMonth;

            var source = GetDataSource(DateFrom.Value, DateTo.Value);
            Total = source.Sum(c => c.Costs);
            DataSource = source;
            Update();
            Refresh();
            if (SecondaryPlaceholder!=null && SecondaryPlaceholder.Controls.Count > 0)
                (SecondaryPlaceholder.Controls["pnDates"].Controls["lbTotal"] as MetroLabel).Text = Total + " грн.";
        }
    }
}
