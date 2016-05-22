using System;
using System.Linq;
using System.Windows.Forms;
using Model;
using Model.Grids;
using Model.Tables;

namespace OurStudents
{
    public partial class PersonsEditGridForm : EditForm<PersonEntity>
    {
        public PersonType PersonType;
        public bool IsDetails;

        public PersonsEditGridForm(PersonType personType)
        {
            InitializeComponent();
            PersonType = personType;
        }

        private void PersonShow(object sender, System.EventArgs e)
        {
            cmbStudenGroup.Visible = (PersonType == PersonType.Student && !IsDetails);
            lbStudentGroup.Visible = (PersonType == PersonType.Student && !IsDetails);
            lbSecondaryDate.Text = PersonType == PersonType.Student ? "Обучается с:" : "Работает с:";
            lbSecondaryDate.Visible = (PersonType != PersonType.Contact);
            dtSecondaryDate.Visible = (PersonType != PersonType.Contact);


        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EditId))
            {
                MainForm.Db.Persons.Add(new Persons
                {
                    FirstName = tbFirstName.Text,
                    LastName = tbLastName.Text,
                    MiddleName = tbMName.Text,
                    Comments = tbComments.Text,
                    BirthDate = dtBirthDate.Value,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Email = tbEmail.Text,
                    IsActive = cbIsActive.Checked,
                    Phone = tbPhone.Text,
                    Sex = rbMail.Checked ? 'M' : 'F',
                    SecondaryDate = dtSecondaryDate.Value,
                    GroupId = (IsDetails ? DetailsId : Convert.ToString(cmbStudenGroup.SelectedValue)),
                    PersonType = (char)PersonType
                });
            }
            else
            {
                var customer = MainForm.Db.Persons.FirstOrDefault(c => c.Id == EditId);
                if (customer != null)
                {
                    customer.FirstName = tbFirstName.Text;
                    customer.LastName = tbLastName.Text;
                    customer.MiddleName = tbMName.Text;
                    customer.Comments = tbComments.Text;
                    customer.BirthDate = dtBirthDate.Value;
                    customer.ModifiedDate = DateTime.Now;
                    customer.Email = tbEmail.Text;
                    customer.IsActive = cbIsActive.Checked;
                    customer.Phone = tbPhone.Text;
                    customer.Sex = rbMail.Checked ? 'M' : 'F';
                    customer.SecondaryDate = dtSecondaryDate.Value;
                    customer.GroupId = (IsDetails ? DetailsId : Convert.ToString(cmbStudenGroup.SelectedValue));
                }
            }

            MainForm.Db.SubmitChanges();
            this.Hide();
            EditGrid.RefreshGrid();
        }

        public override void CleanFields()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbMName.Text = "";
            tbComments.Text = "";
            dtBirthDate.Value = DateTime.Today;
            tbEmail.Text = "";
            cbIsActive.Checked = true;
            tbPhone.Text = "";
            rbMail.Checked = true;
            dtSecondaryDate.Value = DateTime.Today;
            cmbStudenGroup.SelectedValue = (IsDetails ? DetailsId : "");

            var groupsSource = MainForm.Db.Groups.ToList();
            groupsSource.Insert(0, new Groups
            {
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Id = null
            });

            cmbStudenGroup.ValueMember = "Id";
            cmbStudenGroup.DisplayMember = "Name";
            cmbStudenGroup.DataSource = groupsSource;
        }

        private void dtBirthDate_ValueChanged(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            var bDate = (sender as DateTimePicker).Value.Date;
            var age = today.Year - bDate.Year;
            if (bDate > today.AddYears(-age))
                age--;
            lbAgeValueStrudent.Text = age.ToString();
        }
    }
}
