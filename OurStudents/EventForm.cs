using System.Collections.Generic;
using MetroFramework.Forms;
using Model;
using Model.Grids;
using Model.Tables;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OurStudents
{
    public partial class EventForm : EditForm<EventsEntity>
    {
        private EventsType EventType;
        public EventForm(EventsType eventType)
        {
            InitializeComponent();

            EventType = eventType;

            var cheffSource =
                MainForm.Db.Persons.Where(c => (c.PersonType == 'T' || c.PersonType == 'C') && c.IsActive)
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.MiddleName)
                    .ToList();
            cheffSource.Insert(0, new Persons
            {
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Id = null
            });
            cmbCheff.DataSource = cheffSource;
            cmbCheff.ValueMember = "Id";
            cmbCheff.DisplayMember = "FullName";

            var studentsSource =
                MainForm.Db.Persons.Where(c => (c.PersonType == 'S' || c.PersonType == 'C') && c.IsActive).OrderBy(
                    c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.MiddleName)
                    .ToList();
            clbPartisipents.DataSource = studentsSource;
            clbPartisipents.ValueMember = "Id";
            clbPartisipents.DisplayMember = "FullName";

            label4.Visible = EventType == EventsType.Master;
            tbMenu.Visible = EventType == EventsType.Master;
            lbStudentGroup.Text = EventType == EventsType.Master ? "Шеф:" : "Ведущий:";
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(EditId))
            {
                var newEvent = new Events
                {
                    EventName = tbEventName.Text,
                    MasterId = Convert.ToString(cmbCheff.SelectedValue),
                    StartDate =
                        dtEventDate.Value.Date.AddHours(dtTimeFrom.Value.Hour).AddMinutes(dtTimeFrom.Value.Minute),
                    EndDate = dtEventDate.Value.Date.AddHours(dtTimeTo.Value.Hour).AddMinutes(dtTimeTo.Value.Minute),
                    Costs = Convert.ToDecimal(tbCosts.Text),
                    Menu = tbMenu.Text,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Comments = tbComments.Text,
                    IsActive = cbIsActive.Checked,
                    EventType = (char)EventType
                };
                MainForm.Db.Events.Add(newEvent);
                MainForm.Db.SubmitChanges();
                EditId = newEvent.Id;
            }
            else
            {
                var eventEdit = MainForm.Db.Events.FirstOrDefault(c => c.Id == EditId);
                if (eventEdit != null)
                {
                    eventEdit.EventName = tbEventName.Text;
                    eventEdit.MasterId = Convert.ToString(cmbCheff.SelectedValue);
                    eventEdit.StartDate =
                        dtEventDate.Value.Date.AddHours(dtTimeFrom.Value.Hour).AddMinutes(dtTimeFrom.Value.Minute);
                    eventEdit.EndDate =
                        dtEventDate.Value.Date.AddHours(dtTimeTo.Value.Hour).AddMinutes(dtTimeTo.Value.Minute);
                    eventEdit.Costs = Convert.ToDecimal(tbCosts.Text);
                    eventEdit.Menu = tbMenu.Text;
                    eventEdit.CreateDate = DateTime.Now;
                    eventEdit.ModifiedDate = DateTime.Now;
                    eventEdit.Comments = tbComments.Text;
                    eventEdit.IsActive = cbIsActive.Checked;
                }
            }

            var selectedList = (clbPartisipents.CheckedItems.Cast<Persons>().ToList()).Select(c => c.Id);
            var tmpDeleteList =
                MainForm.Db.EventsParticipants.Where(
                    c => c.EventId == EditId && !selectedList.Contains(c.ParticipantsId)).ToList();

            foreach (var item in tmpDeleteList)
            {
                MainForm.Db.EventsParticipants.Remove(item);
            }

            var tmpAddList =
                (clbPartisipents.CheckedItems.Cast<Persons>().ToList()).Where(i => !MainForm.Db.EventsParticipants.Where(
                    c => c.EventId == EditId).Select(c => c.ParticipantsId).Contains(i.Id)).Select(c => c.Id);
            foreach (var item in tmpAddList)
            {
                MainForm.Db.EventsParticipants.Add(new EventsParticipants
                {
                    EventId = EditId,
                    ParticipantsId = item
                });
            }

            MainForm.Db.SubmitChanges();

            this.Hide();
            EditGrid.RefreshGrid();
        }

        public override void CleanFields()
        {
            tbEventName.Text = "";
            cmbCheff.SelectedValue = "";
            dtEventDate.Value = DateTime.Today;
            dtTimeFrom.Value = DateTime.Today;
            dtTimeTo.Value = DateTime.Today;
            tbCosts.Text = (0).ToString("0.00"); 
            tbMenu.Text = "";
            tbComments.Text = "";
            cbIsActive.Checked = true;

            for (int i = 0; i < clbPartisipents.Items.Count; i++)
            {
                clbPartisipents.SetItemChecked(i, false);
            }
        }
    }
}
