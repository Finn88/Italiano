using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model.Tables;
using Equin.ApplicationFramework;

namespace Model.Grids
{
    public enum GroupType
    {
        Common = 'C',
        Private = 'P'
    }


    public class GroupsGrid : Grid<GroupEntity>
    {
        public GroupType GroupType;
        public GroupsGrid(DataBase db, GroupType type) : base(db)
        {
            Name = "groupsGrid";
            GroupType = type;
            RefreshGrid();

            GridId = type == GroupType.Common
                             ? DataBase.GridsIds.GroupsGridId
                             : DataBase.GridsIds.PrivateGridId;


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

          /*  Columns.Add(new CustomColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Visible = false
            });

            Columns.Add(new CustomColumn
            {
                Name = "Name",
                HeaderText = "Название",
                DataPropertyName = "Name",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true
            });
            Columns.Add(new CustomColumn
            {
                Name = "Level",
                HeaderText = "Уровень",
                DataPropertyName = "Level",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });
            
            Columns.Add(new CustomColumn
            {
                Name = "TeacherName",
                HeaderText = "Преподаватель",
                Width = 200,
                DataPropertyName = "TeacherName",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true
            });
            Columns.Add(new CustomColumn
            {
                Name = "StartEducationString",
                HeaderText = "Начало обучения",
                Width = 100,
                DataPropertyName = "StartEducation",
                SortMode = DataGridViewColumnSortMode.Programmatic,
            });
            Columns.Add(new CustomColumn
            {
                Name = "LessonsDays",
                HeaderText = "Дни занятий",
                Width = 300,
                DataPropertyName = "LessonsDays",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });*/
        }


        protected override void AdditionalMenuItems(bool isRowSelected, String id)
        {
            if (!IsDetailsGrid)
            {
                var menuPayment = new MenuItem
                {
                    Text = "Список студентов",
                    Enabled = isRowSelected
                };
                menuPayment.Click += (o, args) => DetailsTabClick(o, args, id);
                Menu.MenuItems.Add(menuPayment);
            }
        }

        protected void DetailsTabClick(object o, EventArgs args, string id)
        {
            var groupName = "Группа: " + Rows[SelectedRowIndex].Cells["Name"].Value;

            if (MainTabControl.Controls.Contains(SecondaryPlaceholder))
                MainTabControl.Controls.Remove(SecondaryPlaceholder);

            SecondaryPlaceholder.Controls.Clear();
            SecondaryPlaceholder.Text = (groupName);
            MainTabControl.Controls.Add(SecondaryPlaceholder);

            MainTabControl.SelectedTab = SecondaryPlaceholder;
            PersonsDetailsGrid.DetailsId = id;
            PersonsDetailsGrid.RefreshGrid();
            SecondaryPlaceholder.Controls.Add(PersonsDetailsGrid);
        }


        protected override void UpdateClick(object sender, EventArgs e, string id)
        {
            var group = Db.Groups.FirstOrDefault(c => c.Id == id);

            var dialog = EditForm;
            dialog.EditGrid = this;
            dialog.EditId = id;
            dialog.CleanFields();

            (dialog.Controls["tbGroupName"] as TextBox).Text = group.Name;
            (dialog.Controls["cmbTeacher"] as ComboBox).SelectedValue = (group.TeacherId);
            (dialog.Controls["dtGroupStartDate"] as DateTimePicker).Value = group.StartEducation;
            (dialog.Controls["cbLevel"] as ComboBox).Text = group.Level;
            (dialog.Controls["tbGroupComments"] as TextBox).Text = group.Comments;
            (dialog.Controls["cbGroupIsActive"] as CheckBox).Checked = group.IsActive;


            var panel = (dialog.Controls["schedulerPanel"] as GroupBox);
            foreach (var item in Db.GroupsDays.Where(c => c.GroupId == id))
            {
                (panel.Controls["cbDay" + item.Day] as CheckBox).Checked = true;
                (panel.Controls["dtFrom" + item.Day] as DateTimePicker).Value = item.StartTime;
                (panel.Controls["dtTo" + item.Day] as DateTimePicker).Value = item.EndTime;
            }
            

            dialog.GetType().GetMethod("InitScheduler").Invoke(dialog, null);
            dialog.ShowDialog();
        }

        protected override void Delete(string id)
        {
            if (id != null)
            {
                var group = Db.Groups.FirstOrDefault(c => c.Id == id);
                if (group != null)
                {
                    Db.Groups.Remove(group);
                    Db.Persons.Where(c => c.GroupId == id && c.PersonType == 'S').ToList().ForEach(c => c.GroupId = null);
                    Db.SubmitChanges();
                    RefreshGrid();
                }
            }
        }

        protected override BindingListView<GroupEntity> GetDataSource()
        {
            return Db.GetGroupsList((char)GroupType);
        }
    }
}
