using System;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using MetroFramework.Controls;


namespace Model.Grids
{
    public class PaymentsGrid : Grid<PaymentEntity>
    {
        public decimal Total = 0;
        public DateTime? DateFrom;
        public DateTime? DateTo;

        public PaymentsGrid(DataBase db) : base(db)
        {
            Name = "studentsGrid";
            RefreshGrid();
            GridId = DataBase.GridsIds.PaymentsGridId;
            var columns = Db.GetColumnsSettingsList(DataBase.GridsIds.PaymentsGridId).OrderBy(c => c.OrderNr);
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
/*
            Columns.Add(new CustomColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Visible = false
            });

            Columns.Add(new CustomColumn
            {
                Name = "CustomerName",
                HeaderText = "Студент",
                DataPropertyName = "CustomerName",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Width = 150,
                Visible = !IsDetailsGrid,
                IsDefaultForSearch = true
            });

            Columns.Add(new CustomColumn
            {
                Name = "PaymentDateString",
                HeaderText = "Дата оплаты",
                DataPropertyName = "PaymentDateString",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true
            });

            Columns.Add(new CustomColumn
            {
                Name = "DateFromString",
                HeaderText = "Оплачено от",
                DataPropertyName = "DateFromString",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });
            Columns.Add(new CustomColumn
            {
                Name = "DateToString",
                HeaderText = "Оплачено До",
                DataPropertyName = "DateToString",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });
            var numericStyle = DefaultCellStyle;
            numericStyle.Format = "0.00";
            Columns.Add(new CustomColumn
            {
                Name = "Costs",
                HeaderText = "Сумма",
                DataPropertyName = "Costs",
                DefaultCellStyle = numericStyle,
                SortMode = DataGridViewColumnSortMode.Programmatic
            });*/
        }

        protected override void AdditionalMenuItems(bool isRowSelected, string id)
        {
            Menu.MenuItems[0].Visible = IsDetailsGrid;
        }

        protected override void UpdateClick(object sender, EventArgs e, string id)
        {
            var payment = Db.Payments.FirstOrDefault(c => c.Id == id);

            var dialog = EditForm;
            dialog.EditId = id;
            dialog.EditGrid = this;
            dialog.DetailsId = DetailsId;

            (dialog.Controls["gbPaymentDates"].Controls["dtPaymentsStartDate"] as DateTimePicker).Value =
                payment.DateFrom;
            (dialog.Controls["gbPaymentDates"].Controls["dtPaymentsEndDate"] as DateTimePicker).Value = payment.DateTo;
            (dialog.Controls["dtPaymentDate"] as DateTimePicker).Value = payment.PaymentDate;
            (dialog.Controls["tbPaymentsComments"] as TextBox).Text = payment.Comments;
            (dialog.Controls["tbCosts"] as TextBox).Text = payment.Costs.ToString("#.00");

            dialog.ShowDialog();
        }

        protected override void Delete(string id)
        {
            if (id != null)
            {
                var payment = Db.Payments.FirstOrDefault(c => c.Id == id);
                if (payment != null)
                {
                    Db.Payments.Remove(payment);
                    Db.SubmitChanges();
                    RefreshGrid();
                }
            }
        }

        protected override BindingListView<PaymentEntity> GetDataSource()
        {
            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);
            if (DateFrom == null)
                DateFrom = startMonth;
            if (DateTo == null)
                DateTo = endMonth;

            return Db.GetPaymentsList(IsDetailsGrid, DetailsId, DateFrom.Value, DateTo.Value);
        }

        private BindingListView<PaymentEntity> GetDataSource(DateTime datefrom, DateTime dateto)
        {
            return Db.GetPaymentsList(IsDetailsGrid, DetailsId, datefrom, dateto);
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
                (SecondaryPlaceholder.Controls["pnDates"].Controls["lbTotal"] as MetroLabel).Text = Total.ToString() + " грн.";
        }
    }
}
