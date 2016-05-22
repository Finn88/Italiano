using System;
using System.Linq;
using System.Windows.Forms;
using Model;
using Model.Grids;
using Model.Tables;

namespace OurStudents
{
    public partial class GroupsEditForm : EditForm<GroupEntity>
    {
        //private Scheduler _schedulerForm;
        public GroupType GroupType;

        public GroupsEditForm(GroupType groupType)
        {
            GroupType = groupType;
            InitializeComponent();
          //  InitScheduler();

            var teacherSource = MainForm.Db.Persons.Where(c => c.PersonType == 'T')
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.MiddleName)
                    .ToList();
            teacherSource.Insert(0, new Persons
            {
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Id = null
            });
            cmbTeacher.DataSource = teacherSource;
            cmbTeacher.ValueMember = "Id";
            cmbTeacher.DisplayMember = "FullName";
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(EditId ))
            {
                var newGroup = new Groups
                {
                    Name = tbGroupName.Text,
                    TeacherId = Convert.ToString(cmbTeacher.SelectedValue),
                    StartEducation = dtGroupStartDate.Value,
                    Level = cbLevel.Text,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = cbGroupIsActive.Checked,
                    Comments = tbGroupComments.Text,
                    GroupType = (char) GroupType
                };
                MainForm.Db.Groups.Add(newGroup);
                MainForm.Db.SubmitChanges();
                EditId = newGroup.Id;
            }
            else
            {
                var customer = MainForm.Db.Groups.FirstOrDefault(c => c.Id == EditId);
                if (customer != null)
                {
                    customer.Name = tbGroupName.Text;
                    customer.TeacherId = Convert.ToString(cmbTeacher.SelectedValue);
                    customer.StartEducation = dtGroupStartDate.Value;
                    customer.Comments = tbGroupComments.Text;
                    customer.Level = cbLevel.Text;
                    customer.ModifiedDate = DateTime.Now;
                    customer.IsActive = cbGroupIsActive.Checked;
                    customer.GroupType = (char)GroupType;
                }
            }

            MainForm.Db.GroupsDays.RemoveAll((c => c.GroupId == EditId));
            MainForm.Db.SubmitChanges();

            var defaultDate = new DateTime(2015, 1, 1, 0, 0, 0, 0);

            var panel = (Controls["schedulerPanel"] as GroupBox);
            for (var i = 1; i <= 7; i++)
            {
                if ((panel.Controls["cbDay" + i] as CheckBox).Checked)
                {
                    MainForm.Db.GroupsDays.Add(new GroupsDays
                    {
                        GroupId = EditId,
                        Day = i,
                        StartTime = defaultDate + (panel.Controls["dtFrom" + i] as DateTimePicker).Value.TimeOfDay,
                        EndTime = defaultDate + (panel.Controls["dtTo" + i] as DateTimePicker).Value.TimeOfDay
                    });
                }
            }
            
            
            /* var deleteList = _schedulerForm.GroupScheduler.Items.Select(c => (int)c.Tag);
            var tmpDeleteList =
                MainForm.Db.GroupsDays.Where(c => c.GroupId == EditId && !deleteList.Contains(c.Id)).ToList();

            foreach (var item in tmpDeleteList)
            {
                MainForm.Db.GroupsDays.DeleteOnSubmit(item);
            }


            foreach (var item in _schedulerForm.GroupScheduler.Items.Where(c => (int)c.Tag != -1))
            {
                var edit = MainForm.Db.GroupsDays.FirstOrDefault(c => c.Id == (int)item.Tag);
                if (edit != null)
                {
                    edit.StartDate = item.StartDate;
                    edit.EndDate = item.EndDate;
                }
            }

            foreach (var item in _schedulerForm.GroupScheduler.Items.Where(c => (int)c.Tag == -1))
            {
                MainForm.Db.GroupsDays.Add(new GroupsDays
                {
                    GroupId = EditId,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate
                });
            }
            */
            MainForm.Db.SubmitChanges();
            this.Hide();
            EditGrid.RefreshGrid();
        }

        public override void CleanFields()
        {
            tbGroupName.Text = "";
            cmbTeacher.SelectedValue = "";
            dtGroupStartDate.Value = DateTime.Today;
            tbGroupComments.Text = "";
            cbLevel.Text = "";
            cbGroupIsActive.Checked = true;

            for (var i = 1; i <= 7; i++)
            {
                var panel = (Controls["schedulerPanel"] as GroupBox);
                (panel.Controls["cbDay" + i] as CheckBox).Checked = false;
                (panel.Controls["dtFrom" + i] as DateTimePicker).Value = DateTime.Today;
                (panel.Controls["dtTo" + i] as DateTimePicker).Value = DateTime.Today;
            }
            

          //  if (_schedulerForm != null)
          //      _schedulerForm.GroupScheduler.Items.Clear();
        }

        public void InitScheduler()
        {
       /*     _schedulerForm = new Scheduler();
            var schedulerSource =
                MainForm.Db.GetGroupsDaysList(EditId)
                    .Select(c => new CalendarItem(_schedulerForm.GroupScheduler, c.StartDate, c.EndDate, "")
                    {
                        Tag = c.Id,
                        BackgroundColor = Color.Green
                    }).ToList();

            _schedulerForm.GroupScheduler.Items.AddRange(schedulerSource);


            var selectedMenu = new ContextMenu();
            var menuNew = new MenuItem
            {
                Text = "Добавить занятие"
            };
            var menuDelete = new MenuItem
            {
                Text = "Удалить занятие",
                Enabled = false
            };
            menuNew.Click += (o, args) => _schedulerForm.GroupScheduler.CreateItemOnSelection(String.Empty, false);
            selectedMenu.MenuItems.Add(menuNew);
            selectedMenu.MenuItems.Add(menuDelete);

            var itemMenu = new ContextMenu();
            var menuNewItem = new MenuItem
            {
                Text = "Добавить занятие",
                Enabled = false
            };
            var menuDeleteItem = new MenuItem
            {
                Text = "Удалить занятие"
            };
            menuDeleteItem.Click += (o, args) => _schedulerForm.GroupScheduler.DeleteSelectedItems();
            itemMenu.MenuItems.Add(menuNewItem);
            itemMenu.MenuItems.Add(menuDeleteItem);

            _schedulerForm.GroupScheduler.SelectedMenu = selectedMenu;
            _schedulerForm.GroupScheduler.ItemMenu = itemMenu;*/
        }

        private void ShedulerClick(object sender, EventArgs e)
        {
          //  _schedulerForm.ShowDialog();
        }
    }
}
