using System;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using MetroFramework.Controls;


namespace Model.Grids
{
    public class PaymentsReportGrid : Grid<PaymentsReportEntity>
    {
        public decimal Total = 0;
        public DateTime? DateFrom;
        public DateTime? DateTo;

        public PaymentsReportGrid(DataBase db)
            : base(db)
        {
            Name = "reportGrid";
            RefreshGrid();
            GridId = DataBase.GridsIds.PaymentsReportGridId;
            var columns = Db.GetColumnsSettingsList(GridId).OrderBy(c => c.OrderNr);
            MouseDown -= ClickMouseDown;
            MouseDoubleClick -= DoubleClickMouseDown;

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
        }

        protected override void Delete(string id)
        {
        }

        protected override BindingListView<PaymentsReportEntity> GetDataSource()
        {
            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);
            if (DateFrom == null)
                DateFrom = startMonth;
            if (DateTo == null)
                DateTo = endMonth;

            return Db.GetPaymentsReportList(DateFrom.Value, DateTo.Value);
        }

        private BindingListView<PaymentsReportEntity> GetDataSource(DateTime datefrom, DateTime dateto)
        {
            return Db.GetPaymentsReportList(datefrom, dateto);
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
            Total = source.Sum(c => c.TotalPerDate);
            DataSource = source;
            Update();
            Refresh();
            if (SecondaryPlaceholder!=null && SecondaryPlaceholder.Controls.Count > 0)
                (SecondaryPlaceholder.Controls["pnDates"].Controls["lbTotal"] as MetroLabel).Text = Total + " грн.";
        }
    }
}
