using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Equin.ApplicationFramework;

namespace Model.Grids
{
    public enum EventsType
    {
        Master = 'M',
        Event = 'E'
    }


    public class EventsGrid : Grid<EventsEntity>
    {
        public EventsType EventType;

        public EventsGrid(DataBase db, EventsType type)
            : base(db)
        {
            Name = "masterEventGrid";
            EventType = type;
            RefreshGrid();

            var gridId = type == EventsType.Event
                 ? DataBase.GridsIds.EventsGridId
                 : DataBase.GridsIds.MasterGridId;
            GridId = gridId;
            var columns = Db.GetColumnsSettingsList(gridId).OrderBy(c => c.OrderNr);
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
            /*Columns.Add(new CustomColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Visible = false
            });
            Columns.Add(new CustomColumn
            {
                Name = "EventName",
                HeaderText = "Название",
                DataPropertyName = "EventName",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true
            });
            Columns.Add(new CustomColumn
            {
                Name = "MasterName",
                HeaderText = "Шеф/Ведущий",
                DataPropertyName = "MasterName",
                Width = 200,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch  = true
            });
            Columns.Add(new CustomColumn
            {
                Name = "Date",
                HeaderText = "Дата",
                DataPropertyName = "Date",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true
            });
            Columns.Add(new CustomColumn
            {
                Name = "StartTime",
                HeaderText = "Начало",
                DataPropertyName = "StartTime",
                SortMode = DataGridViewColumnSortMode.Programmatic,
            });
            Columns.Add(new CustomColumn
            {
                Name = "EndTime",
                HeaderText = "Конец",
                DataPropertyName = "EndTime",
                SortMode = DataGridViewColumnSortMode.Programmatic,
            });*/
        }



        protected override void UpdateClick(object sender, EventArgs e, string id)
        {
            var _event = Db.Events.FirstOrDefault(c => c.Id == id);
            var partList = (from p in Db.Persons
                join ep in Db.EventsParticipants on p.Id equals ep.ParticipantsId
                where ep.EventId == id
                select p).ToList();

            var dialog = EditForm;
            dialog.EditGrid = this;
            dialog.EditId = id;

            (dialog.Controls["tbEventName"] as TextBox).Text = _event.EventName;
            (dialog.Controls["cmbCheff"] as ComboBox).SelectedValue = (_event.MasterId);
            (dialog.Controls["dtEventDate"] as DateTimePicker).Value = _event.StartDate;
            (dialog.Controls["dtTimeFrom"] as DateTimePicker).Value = _event.StartDate;
            (dialog.Controls["dtTimeTo"] as DateTimePicker).Value = _event.EndDate;
            (dialog.Controls["cbIsActive"] as CheckBox).Checked = _event.IsActive;
            (dialog.Controls["tbComments"] as TextBox).Text = _event.Comments;
            (dialog.Controls["tbMenu"] as TextBox).Text = _event.Menu;
            (dialog.Controls["tbCosts"] as TextBox).Text = _event.Costs.ToString("#.00");

            for (int count = 0; count < (dialog.Controls["clbPartisipents"] as CheckedListBox).Items.Count; count++)
            {
                if (partList.Contains((dialog.Controls["clbPartisipents"] as CheckedListBox).Items[count]))
                {
                    (dialog.Controls["clbPartisipents"] as CheckedListBox).SetItemChecked(count, true);
                }
            }

            dialog.ShowDialog();
        }

        protected override void Delete(string id)
        {
            if (id != null)
            {
                var events = Db.Events.FirstOrDefault(c => c.Id == id);
                if (events != null)
                {
                    Db.Events.Remove(events);
                    Db.EventsParticipants.RemoveAll(c => c.ParticipantsId == id);
                    Db.SubmitChanges();
                    RefreshGrid();
                }
            }
        }

        protected override BindingListView<EventsEntity> GetDataSource()
        {
            return Db.GetEventsList((char)EventType);
        }
    }
}
