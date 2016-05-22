using System;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using MetroFramework.Controls;
using Model.Tables;

namespace Model.Grids
{
    public enum PersonType
    {
        Student = 'S',
        Teacher = 'T',
        Contact = 'C'
    }

    public class PersonsGrid : Grid<PersonEntity>
    {
        public PersonType GridType { get; set; }

        public PersonsGrid(DataBase db, PersonType gridType)
            : base(db)
        {
            GridType = gridType;
            Name = "studentsGrid";
            RefreshGrid();

            GridId = gridType == PersonType.Student
                             ? DataBase.GridsIds.StrudentsGridId
                             : gridType == PersonType.Teacher
                                   ? DataBase.GridsIds.TeachersGridId
                                   : DataBase.GridsIds.ContactsGridId;

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
            /*
            Columns.Add(new CustomColumn
            {
                Name = "Id",
                HeaderText = "Пин",
                DataPropertyName = "Id",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Visible = IsColumnVisible(settings, 1, gridId) ?? false
            });

            Columns.Add(new CustomColumn
            {
                Name = "LastName",
                HeaderText = "Фамилия",
                Width = 150,
                DataPropertyName = "LastName",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true,
                Visible = IsColumnVisible(settings, 5, gridId) ?? true
            });

            Columns.Add(new CustomColumn
            {
                Name = "FirstName",
                HeaderText = "Имя",
                DataPropertyName = "FirstName",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true,
                Visible = IsColumnVisible(settings, 10, gridId) ?? true
            });

            Columns.Add(new CustomColumn
            {
                Name = "MiddleName",
                HeaderText = "Отчество",
                Width = 150,
                DataPropertyName = "MiddleName",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Visible = IsColumnVisible(settings, 15, gridId) ?? false
            });
            Columns.Add(new CustomColumn
            {
                Name = "Email",
                HeaderText = "Email",
                Width = 100,
                DataPropertyName = "Email",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                Visible = IsColumnVisible(settings, 20, gridId) ?? true
            });
            Columns.Add(new CustomColumn
            {
                Name = "Phone",
                HeaderText = "Телефон",
                Width = 120,
                DataPropertyName = "Phone",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                IsDefaultForSearch = true,
                Visible = IsColumnVisible(settings, 25, gridId) ?? true
            });
            
            var secondaryText = "Обучается с";
            if (GridType == PersonType.Teacher)
                secondaryText = "Работает с";

            if (GridType != PersonType.Contact)
                Columns.Add(new CustomColumn
                {
                    Name = "SecondDate",
                    HeaderText = secondaryText,
                    Width = 100,
                    DataPropertyName = "SecondDate",
                    SortMode = DataGridViewColumnSortMode.Programmatic
                });

            if (GridType == PersonType.Teacher)
            Columns.Add(new CustomColumn
            {
                Name = "BirthDate",
                HeaderText = "Дата рождения",
                Width = 150,
                DataPropertyName = "BirthDate",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });

            Columns.Add(new CustomColumn
            {
                Name = "SexString",
                HeaderText = "Пол",
                Width = 100,
                DataPropertyName = "SexString",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });
            if (GridType == PersonType.Student)
                Columns.Add(new CustomColumn
                {
                    Name = "Group",
                    HeaderText = "Группа",
                    Width = 100,
                    DataPropertyName = "Group",
                    SortMode = DataGridViewColumnSortMode.Programmatic
                });
            */
        }

        protected override void AdditionalMenuItems(bool isRowSelected, string id)
        {
            if (GridType == PersonType.Student && !IsDetailsGrid)
            {
                var menuPayment = new MenuItem
                {
                    Text = "Архив платежей",
                    Enabled = isRowSelected
                };
                menuPayment.Click += (o, args) => DetailsTabClick(o, args, id);
                Menu.MenuItems.Add(menuPayment);
            }
            if (!IsDetailsGrid)
            {
                var menuSwap = new MenuItem
                {
                    Text = "Изменить тип",
                    Enabled = isRowSelected
                };
                var menuSwapToStudent = new MenuItem
                {
                    Text = "Студент",
                    Visible = GridType!=PersonType.Student
                };
                var menuSwapToTeacher = new MenuItem
                {
                    Text = "Преподаватель",
                    Visible = GridType != PersonType.Teacher
                };
               
                var menuSwapToContact = new MenuItem
                {
                    Text = "Контакт",
                    Visible = GridType!=PersonType.Contact
                };

                menuSwap.MenuItems.Add(menuSwapToStudent);
                menuSwap.MenuItems.Add(menuSwapToTeacher);
                menuSwap.MenuItems.Add(menuSwapToContact);

                menuSwapToStudent.Click += (o, args) => ChangePersonType(o, args, id, PersonType.Student);
                menuSwapToTeacher.Click += (o, args) => ChangePersonType(o, args, id, PersonType.Teacher);
                menuSwapToContact.Click += (o, args) => ChangePersonType(o, args, id, PersonType.Contact);

                Menu.MenuItems.Add(menuSwap);
            }
        }

        private void ChangePersonType(object o, EventArgs args, string id, PersonType personType)
        {
            var person = Db.Persons.FirstOrDefault(c => c.Id == id);
            if (person != null)
            {
                person.PersonType = (char) personType;
                Db.SubmitChanges();
                RefreshGrid();
            }
        }

        protected void DetailsTabClick(object o, EventArgs args, string pin)
        {
            var studentName = Rows[SelectedRowIndex].Cells["LastName"].Value + " " +
                                 Rows[SelectedRowIndex].Cells["FirstName"].Value;

            if (MainTabControl.Controls.Contains(SecondaryPlaceholder))
                MainTabControl.Controls.Remove(SecondaryPlaceholder);

            SecondaryPlaceholder.Controls.Clear();
            SecondaryPlaceholder.Text = ("Платежи:" + studentName);
            MainTabControl.Controls.Add(SecondaryPlaceholder);

            MainTabControl.SelectedTab = SecondaryPlaceholder;
            PaymentDetailsGrid.DetailsId = pin;
            //PaymentDetailsGrid.RefreshGrid();
            //SecondaryPlaceholder.Controls.Add(PaymentDetailsGrid);

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var datesPanel = new MetroPanel
            {
                Name = "pnDates",
                Width = 500,
                Height = 300,
                Top = 0,
                Left = (SecondaryPlaceholder.Width / 2) + 50,
            };
            var dateFrom = new DateTimePicker
            {
                Name = "dtFrom",
                Value = startMonth,
                Top = 20,
                Left = 150,
                Width = 150
            };

            var dateTo = new DateTimePicker
            {
                Name = "dtFrom",
                Value = endMonth,
                Top = 60,
                Left = 150,
                Width = 150
            };


            datesPanel.Controls.Add(new MetroLabel
            {
                Text = "Период От:",
                Top = 20,
                Left = 30,
                Width = 100
            });
            datesPanel.Controls.Add(dateFrom);

            datesPanel.Controls.Add(new MetroLabel
            {
                Text = "Период До:",
                Top = 60,
                Left = 30,
                Width = 100
            });
            datesPanel.Controls.Add(dateTo);

            (PaymentDetailsGrid as PaymentsGrid).RefreshGrid();

            datesPanel.Controls.Add(new MetroLabel
            {
                Text = "Вырученная сумма:",
                Top = 100,
                Left = 30,
                Width = 200,
            });

            var total = new MetroLabel
            {
                Name="lbTotal",
                Text = (PaymentDetailsGrid as PaymentsGrid).Total.ToString() + " грн.",
                Top = 100,
                Left = 240,
                Width = 100,
            };

            datesPanel.Controls.Add(total);

            dateTo.ValueChanged += (send, arg) =>
            {
                (PaymentDetailsGrid as PaymentsGrid).DateTo = dateTo.Value;
                (PaymentDetailsGrid as PaymentsGrid).RefreshGrid();
                total.Text = (PaymentDetailsGrid as PaymentsGrid).Total.ToString() + " грн.";
            };
            dateFrom.ValueChanged += (send, arg) =>
            {
                (PaymentDetailsGrid as PaymentsGrid).DateFrom = dateFrom.Value;
                (PaymentDetailsGrid as PaymentsGrid).RefreshGrid();
                total.Text = (PaymentDetailsGrid as PaymentsGrid).Total.ToString() + " грн.";
            };


            SecondaryPlaceholder.Controls.Clear();
            SecondaryPlaceholder.Controls.Add(datesPanel);
            SecondaryPlaceholder.Controls.Add(PaymentDetailsGrid);
        }

        protected override void UpdateClick(object sender, EventArgs e, string id)
        {
            var customer = Db.Persons.FirstOrDefault(c => c.Id == id);
            
            var dialog = EditForm;
            dialog.EditGrid = this;
            dialog.EditId = id;

            (dialog.Controls["tbFirstName"] as TextBox).Text = customer.FirstName;
            (dialog.Controls["tbLastName"] as TextBox).Text = customer.LastName;
            (dialog.Controls["tbMName"] as TextBox).Text = customer.MiddleName;
            (dialog.Controls["tbComments"] as TextBox).Text = customer.Comments;
            (dialog.Controls["dtBirthDate"] as DateTimePicker).Value = customer.BirthDate;
            (dialog.Controls["tbEmail"] as TextBox).Text = customer.Email;
            (dialog.Controls["tbPhone"] as MaskedTextBox).Text = customer.Phone;
            (dialog.Controls["cbIsActive"] as CheckBox).Checked = customer.IsActive;
            (dialog.Controls["rbMail"] as RadioButton).Checked = customer.Sex == 'M';
            (dialog.Controls["rbFemail"] as RadioButton).Checked = customer.Sex == 'F';
            (dialog.Controls["dtSecondaryDate"] as DateTimePicker).Value = customer.SecondaryDate;

            var groupsSource = Db.Groups.ToList();
            groupsSource.Insert(0, new Groups
            {
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Id = null
            });
            (dialog.Controls["cmbStudenGroup"] as ComboBox).DataSource = groupsSource;
            (dialog.Controls["cmbStudenGroup"] as ComboBox).ValueMember = "Id";
            (dialog.Controls["cmbStudenGroup"] as ComboBox).DisplayMember = "Name";
            (dialog.Controls["cmbStudenGroup"] as ComboBox).SelectedValue = customer.GroupId;

            dialog.ShowDialog();
        }

        protected override void Delete(string id)
        {
            if (id != null)
            {
                var customer = Db.Persons.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    Db.Persons.Remove(customer);
                    Db.Payments.RemoveAll((c => c.PersonId == customer.Id));
                    Db.Groups.Where(c => c.TeacherId == id).ToList().ForEach(c => c.TeacherId = null);
                    Db.EventsParticipants.RemoveAll((c => c.ParticipantsId == customer.Id));
                    Db.SubmitChanges();
                    RefreshGrid();
                }
            }
        }

        protected override BindingListView<PersonEntity> GetDataSource()
        {
            return Db.GetPersonsList((char) GridType, IsDetailsGrid, DetailsId);
        }
    }
}
