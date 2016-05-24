using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using Model;
using MetroFramework.Forms;
using Model.Grids;
using Model.Tables;
using System.IO;

namespace OurStudents
{
    public partial class MainForm : MetroForm
    {
        private Search _searchForm;
        private MetroGrid _currentGrid;
        private MetroGrid _secondaryGrid;
   
        public TabControl _tabControl;
        public LoadingPanel _spinnerPanel;
        public TabPage _tabMainPlaceholder;
        public TabPage _tabSecondaryPlaceholder;


        public MainForm()
        {
         ShowInTaskbar = true;
         InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
                Enabled = false;
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }

        private void OnMainForm(object sender, EventArgs e)
        {
            _searchForm = new Search();
            _tabControl = new MetroTabControl { Width = Width, Height = Height, Top=ribbon1.Bottom, Style = MetroColorStyle.Green};
            _spinnerPanel = new LoadingPanel();
            _tabMainPlaceholder = new MetroTabPage();
            _tabSecondaryPlaceholder = new MetroTabPage();
            _tabControl.Controls.Add(_tabMainPlaceholder);
            Controls.Add(_tabControl);


            Db = new DataBase(Program.DBName);
           
            if (!Db.DatabaseExists())
            {
                var confirmResult = MessageBox.Show(
                    "База данных не была найдена. Вы желаете создать новую?", "Внимание",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.No)
                {
                    Application.Exit();
                }
                else
                {
                    _backgroundWorker.DoWork += (s, arg) =>
                    {
                        Invoke((MethodInvoker) (() =>
                        {
                            _spinnerPanel = new LoadingPanel();
                            _spinnerPanel.UpdateLoadingText("Создание новой базы данных...");
                            _spinnerPanel.Show();
                        }));

                        Db.CreateNewDatabase();
                        AppSettings = new AppSettings(Db);
                    };
                    _backgroundWorker.RunWorkerCompleted += (s, arg) => Invoke(new Action(() =>
                    {
                        _spinnerPanel.Close();
                        btnStudents.PerformClick();
                        ribbon1.Enabled = true;
                    }));
                    _backgroundWorker.RunWorkerAsync();
                }
            }
            else
            {
                Db.UpdateDB();
                AppSettings = new AppSettings(Db);
                btnStudents.PerformClick();
                ribbon1.Enabled = true;
            }
            Activate();
        }


        #region Payments

        private void PaymentsMenuClick(object sender, EventArgs e)
        {
            _paymentsGrid = new PaymentsGrid(Db)
            {
                MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250),
                EditForm = new PaymentsEditForm { AppSettings = AppSettings },
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder
            };

            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = "Платежи";

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    _spinnerPanel = new LoadingPanel();
                    _spinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    _spinnerPanel.Show();
                }));

                _paymentsGrid.Top = 0;
                _paymentsGrid.Width = (_tabMainPlaceholder.Width / 2);
                _paymentsGrid.Height = _tabMainPlaceholder.Height - 150;

                this.Invoke((MethodInvoker)(() =>
                {

                    var datesPanel = new MetroPanel
                    {
                        Name = "pnDates",
                        Width = 500,
                        Height = 300,
                        Top = 0,
                        Left = (_tabMainPlaceholder.Width / 2) + 50,
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

                    _paymentsGrid.RefreshGrid();

                    datesPanel.Controls.Add(new MetroLabel
                    {
                        Text = "Вырученная сумма:",
                        Top = 100,
                        Left = 30,
                        Width = 200,
                    });

                    var total = new MetroLabel
                                    {
                                        Name = "lbTotal",
                                        Text = _paymentsGrid.Total.ToString() + " грн.",
                                        Top = 100,
                                        Left = 240,
                                        Width = 100,
                                    };

                    datesPanel.Controls.Add(total);

                    dateTo.ValueChanged += (send, args) =>
                                               {
                                                   _paymentsGrid.DateTo = dateTo.Value;
                                                   _paymentsGrid.RefreshGrid();
                                                   total.Text = _paymentsGrid.Total.ToString() + " грн.";
                                               };
                    dateFrom.ValueChanged += (send, args) =>
                                                 {
                                                     _paymentsGrid.DateFrom = dateFrom.Value;
                                                     _paymentsGrid.RefreshGrid();
                                                     total.Text = _paymentsGrid.Total.ToString() + " грн.";
                                                 };


                    _tabMainPlaceholder.Controls.Clear();
                    _tabMainPlaceholder.Controls.Add(datesPanel);
                    _tabMainPlaceholder.Controls.Add(_paymentsGrid);
                    _currentGrid = _paymentsGrid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => this.Invoke(new Action(() =>
            {
                _spinnerPanel.Close();
                this.Activate();
            }));
            backgroundLoader.RunWorkerAsync();
        }

        private void EarningsMenuClick(object sender, EventArgs e)
        {
            LoadBudgetGrid(sender, true);
        }

        private void ExpensesMenuClick(object sender, EventArgs e)
        {
            LoadBudgetGrid(sender, false);
        }

        private void LoadBudgetGrid(object sender, bool isEarning)
        {
            _budgetGrid = new BudgetGrid(Db, isEarning)
            {
                MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250),
                EditForm = new BudgetEditFrom { AppSettings = AppSettings },
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder
            };

            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = isEarning ? "Доходы" : "Расходы";

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    _spinnerPanel = new LoadingPanel();
                    _spinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    _spinnerPanel.Show();
                }));

                _budgetGrid.Top = 0;
                _budgetGrid.Width = (_tabMainPlaceholder.Width / 2);
                _budgetGrid.Height = _tabMainPlaceholder.Height - 150;

                this.Invoke((MethodInvoker)(() =>
                {

                    var datesPanel = new MetroPanel
                    {
                        Name = "pnDates",
                        Width = 500,
                        Height = 300,
                        Top = 0,
                        Left = (_tabMainPlaceholder.Width / 2) + 50,
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

                    _budgetGrid.RefreshGrid();

                    datesPanel.Controls.Add(new MetroLabel
                    {
                        Text = "Общая сумма:",
                        Top = 100,
                        Left = 30,
                        Width = 200,
                    });

                    var total = new MetroLabel
                    {
                        Name = "lbTotal",
                        Text = _budgetGrid.Total + " грн.",
                        Top = 100,
                        Left = 240,
                        Width = 100,
                    };

                    datesPanel.Controls.Add(total);

                    dateTo.ValueChanged += (send, args) =>
                    {
                        _budgetGrid.DateTo = dateTo.Value;
                        _budgetGrid.RefreshGrid();
                        total.Text = _budgetGrid.Total + " грн.";
                    };
                    dateFrom.ValueChanged += (send, args) =>
                    {
                        _budgetGrid.DateFrom = dateFrom.Value;
                        _budgetGrid.RefreshGrid();
                        total.Text = _budgetGrid.Total + " грн.";
                    };

                    _tabMainPlaceholder.Controls.Clear();
                    _tabMainPlaceholder.Controls.Add(datesPanel);
                    _tabMainPlaceholder.Controls.Add(_budgetGrid);
                    _currentGrid = _budgetGrid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => this.Invoke(new Action(() =>
            {
                _spinnerPanel.Close();
                this.Activate();
            }));
            backgroundLoader.RunWorkerAsync();
        }

        private void PaymentReportMenuClick(object sender, EventArgs e)
        {
            _reportGrid = new PaymentsReportGrid(Db)
            {
                MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder
            };

            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = "Отчет";

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    _spinnerPanel = new LoadingPanel();
                    _spinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    _spinnerPanel.Show();
                }));

                _reportGrid.Top = 0;
                _reportGrid.Width = (_tabMainPlaceholder.Width / 2)+250;
                _reportGrid.Height = _tabMainPlaceholder.Height - 150;

                this.Invoke((MethodInvoker)(() =>
                {

                    var datesPanel = new MetroPanel
                    {
                        Name = "pnDates",
                        Width = 500,
                        Height = 300,
                        Top = 0,
                        Left = (_tabMainPlaceholder.Width / 2) + 300,
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

                    _reportGrid.RefreshGrid();

                    datesPanel.Controls.Add(new MetroLabel
                    {
                        Text = "Общая сумма:",
                        Top = 100,
                        Left = 30,
                        Width = 200,
                    });

                    var total = new MetroLabel
                    {
                        Name = "lbTotal",
                        Text = _reportGrid.Total + " грн.",
                        Top = 100,
                        Left = 240,
                        Width = 100,
                    };

                    datesPanel.Controls.Add(total);

                    dateTo.ValueChanged += (send, args) =>
                    {
                        _reportGrid.DateTo = dateTo.Value;
                        _reportGrid.RefreshGrid();
                        total.Text = _reportGrid.Total + " грн.";
                    };
                    dateFrom.ValueChanged += (send, args) =>
                    {
                        _reportGrid.DateFrom = dateFrom.Value;
                        _reportGrid.RefreshGrid();
                        total.Text = _reportGrid.Total + " грн.";
                    };

                    _tabMainPlaceholder.Controls.Clear();
                    _tabMainPlaceholder.Controls.Add(datesPanel);
                    _tabMainPlaceholder.Controls.Add(_reportGrid);
                    _currentGrid = _reportGrid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => this.Invoke(new Action(() =>
            {
                _spinnerPanel.Close();
                this.Activate();
            }));
            backgroundLoader.RunWorkerAsync();
        }

        #endregion

        #region Persons

        private void StudentsMenuClick(object sender, EventArgs e)
        {

            var duplicateKeys = Db.Persons.GroupBy(x => x.Id)
              .Where(group => group.Count() > 1)
              .Select(group => group.Key).ToList();
            var duplicateKeys2 = Db.Payments.GroupBy(x => x.Id)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
            var duplicateKeys3 = Db.Groups.GroupBy(x => x.Id)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
            var duplicateKeys4 = Db.Events.GroupBy(x => x.Id)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key).ToList();
            var ddd = Guid.NewGuid().ToString();
            var z = duplicateKeys;
            var z2 = duplicateKeys2;
            var z3 = duplicateKeys3;
            var z4 = duplicateKeys4;
            var tt = ddd;
            _secondaryGrid = new PaymentsGrid(Db)
                                 {
                                     IsDetailsGrid = true,
                                     Name = "paymentsGrid",
                                     Width = _tabMainPlaceholder.Width - 50,
                                     Height = _tabMainPlaceholder.Height - 350,
                                     MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250),
                                     EditForm = new PaymentsEditForm {AppSettings = AppSettings},
                                     MainTabControl = _tabControl,
                                     MainPlaceholder = _tabMainPlaceholder,
                                     SecondaryPlaceholder = _tabSecondaryPlaceholder,
                                     MainForm = this
                                 };

            _studentsGrid = new PersonsGrid(Db, PersonType.Student)
            {
                EditForm = new PersonsEditGridForm(PersonType.Student),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                PaymentDetailsGrid = (_secondaryGrid as PaymentsGrid),
                MainForm = this
            };

            SetGrid(sender, "Студенты", _studentsGrid);
        }

        private void TeachersMenuItemClick(object sender, EventArgs e)
        {
            _teachersGrid = new PersonsGrid(Db, PersonType.Teacher)
            {
                Name = "teachersGrid",
                EditForm = new PersonsEditGridForm(PersonType.Teacher),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                MainForm = this
            };

            SetGrid(sender, "Преподаватели", _teachersGrid);
        }

        private void ContactsMenuClick(object sender, EventArgs e)
        {
            _contactsGrid = new PersonsGrid(Db, PersonType.Contact)
            {
                EditForm = new PersonsEditGridForm(PersonType.Contact),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                MainForm = this
            };

            SetGrid(sender, "Контакты", _contactsGrid);
        }

        #endregion

        #region Lessons

        private void GroupsMenuItemClick(object sender, EventArgs e)
        {
            _secondaryGrid = new PersonsGrid(Db, PersonType.Student)
                                 {
                                     IsDetailsGrid = true,
                                     Name = "studetsDetailsGrid",
                                     Width = _tabMainPlaceholder.Width - 50,
                                     Height = _tabMainPlaceholder.Height - 150,
                                     MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250),
                                     EditForm = new PersonsEditGridForm(PersonType.Student) {IsDetails = true},
                                     MainTabControl = _tabControl,
                                     MainPlaceholder = _tabMainPlaceholder,
                                     SecondaryPlaceholder = _tabSecondaryPlaceholder,
                                     MainForm = this
                                 };
            _groupsGrid = new GroupsGrid(Db, GroupType.Common)
            {
                EditForm = new GroupsEditForm(GroupType.Common),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                PersonsDetailsGrid = _secondaryGrid as PersonsGrid,
                MainForm = this
            };

            SetGrid(sender, "Группы", _groupsGrid);
        }

        private void PrivateClick(object sender, EventArgs e)
        {
            _secondaryGrid = new PersonsGrid(Db, PersonType.Student)
                                 {
                                     IsDetailsGrid = true,
                                     Name = "studetsDetailsGrid",
                                     Width = _tabMainPlaceholder.Width - 50,
                                     Height = _tabMainPlaceholder.Height - 150,
                                     MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250),
                                     EditForm = new PersonsEditGridForm(PersonType.Student) {IsDetails = true},
                                     MainTabControl = _tabControl,
                                     MainPlaceholder = _tabMainPlaceholder,
                                     SecondaryPlaceholder = _tabSecondaryPlaceholder,
                                     MainForm = this
                                 };

            _privateGrid = new GroupsGrid(Db, GroupType.Private)
            {
                EditForm = new GroupsEditForm(GroupType.Private),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                PersonsDetailsGrid = _secondaryGrid as PersonsGrid,
                MainForm = this
            };

            SetGrid(sender, "Индивидуальные", _privateGrid);
        }

        private void MasterEventClick(object sender, EventArgs e)
        {
            _masterEventGrid = new EventsGrid(Db, EventsType.Master)
            {
                EditForm = new EventForm(EventsType.Master),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                MainForm = this
            };

            SetGrid(sender, "Мастер-классы", _masterEventGrid);
        }

        private void EventClick(object sender, EventArgs e)
        {
            _eventGrid = new EventsGrid(Db, EventsType.Event)
            {
                EditForm = new EventForm(EventsType.Event),
                MainTabControl = _tabControl,
                MainPlaceholder = _tabMainPlaceholder,
                SecondaryPlaceholder = _tabSecondaryPlaceholder,
                MainForm = this
            };

            SetGrid(sender, "События", _eventGrid);
        }

        #endregion

        #region Settings

        private void SettingsClick(object sender, EventArgs e)
        {
            _currentGrid = null;
            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = "Настройки";
            _tabMainPlaceholder.Controls.Clear();

            var settingsPanel = new MetroPanel
            {
                Name = "pnSettings",
                Width = 500,
                Height = 500,
                Top = 0,
                Left = 25,
            };
            settingsPanel.Controls.Add(new MetroLabel
            {
                Text = "Оплата за месяц по-умолчанию:",
                Top = 20,
                Left = 20,
                Width = 250
            });
            settingsPanel.Controls.Add(new TextBox
            {
                Name = "tbDefaultCosts",
                Text = AppSettings.DefaultCosts.ToString("0.00"),
                Top = 20,
                Left = 270,
                Width = 100
            });
            settingsPanel.Controls.Add(new MetroLabel
            {
                Text = "Оплата за занятие по-умолчанию:",
                Top = 60,
                Left = 20,
                Width = 250
            });
            settingsPanel.Controls.Add(new TextBox
            {
                Name = "tbDefaultCostsSingle",
                Text = AppSettings.DefaultCostsSingle.ToString("0.00"),
                Top = 60,
                Left = 270,
                Width = 100
            });

            var saveButton = new Button
            {
                Text = "Сохранить",
                Top = 100,
                Left = 20,
                Width = 100
            };

            saveButton.Click += (s, args) =>
                                    {
                                        AppSettings.DefaultCosts =
                                            Convert.ToDecimal(
                                                (_tabMainPlaceholder.Controls["pnSettings"].Controls["tbDefaultCosts"]
                                                 as TextBox).Text);
                                        AppSettings.DefaultCostsSingle =
                                            Convert.ToDecimal(
                                                (_tabMainPlaceholder.Controls["pnSettings"].Controls[
                                                    "tbDefaultCostsSingle"] as TextBox).Text);

                                        MessageBox.Show("Данные успешно сохранены.");
                                    };

            settingsPanel.Controls.Add(saveButton);

            var cancelButton = new Button
            {
                Text = "Отменить",
                Top = 100,
                Left = 170,
                Width = 100
            };
            cancelButton.Click += (s, args) =>
                                      {
                                          (_tabMainPlaceholder.Controls["pnSettings"].Controls["tbDefaultCosts"] as
                                           TextBox).Text =
                                              AppSettings.DefaultCosts.ToString("0.00");
                                          (_tabMainPlaceholder.Controls["pnSettings"].Controls["tbDefaultCostsSingle"]
                                           as TextBox).Text =
                                              AppSettings.DefaultCostsSingle.ToString("0.00");
                                      };

            _tabMainPlaceholder.Controls.Add(new MetroLabel
                                             {
                                                 Text = "Выберите типы оплат, которые будут учтены в отчете.",
                                                 Top = 20,
                                                 Left = 500,
                                                 AutoSize = true
                                             });

            var chlbPaymentsSettings = new CheckedListBox
                                     {
                                         Name = "chlbPaymentsSettings",
                                         Top = 50,
                                         Width = 250,
                                         Height = 300,
                                         Left = 500,
                                         ValueMember = "Code",
                                         DisplayMember = "Name"
                                     };

            foreach (var item in Db.PaymentReportSettings)
                chlbPaymentsSettings.Items.Add(item, item.ShouldBeCount);

            chlbPaymentsSettings.ItemCheck += PaymentCostChanged;
            _tabMainPlaceholder.Controls.Add(chlbPaymentsSettings);

            settingsPanel.Controls.Add(cancelButton);
           _tabMainPlaceholder.Controls.Add(settingsPanel);
        }

        private void GridsClick(object sender, EventArgs e)
        {
            _currentGrid = null;
            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = "Таблицы";
            _tabMainPlaceholder.Controls.Clear();

            var chlbGrids = new CheckedListBox
                                {
                                    Name = "chlbGrids",
                                    Top = 50,
                                    Width = 250,
                                    Height = 300,
                                    Left = 420,
                                    ValueMember = "ColumnName",
                                    DisplayMember = "ColumnHeader"
                                };
            chlbGrids.ItemCheck += ColumnVisibilityChanged;

            foreach(var item in Db.GetColumnsSettingsList(DataBase.GridsIds.StrudentsGridId))
            {
                chlbGrids.Items.Add(item, item.IsVisible);
            }

            var btnUp = new MetroButton
                            {
                                Top = 50,
                                Width = 70,
                                Height = 30,
                                Left = 340,
                                Text = "Выше"
                            };
            btnUp.Click += UpClick;

            var btnDown = new MetroButton
                              {
                                  Top = 90,
                                  Width = 70,
                                  Height = 30,
                                  Left = 340,
                                  Text = "Ниже"
                              };
            btnDown.Click += DownClick;

            _tabMainPlaceholder.Controls.Add(new MetroLabel
                                                 {
                                                     Text =
                                                         "Выберите колонки, которые будут отображены в таблице и их поочередность.",
                                                     Top = 20,
                                                     Left = 420,
                                                     AutoSize = true
                                                 });

            _tabMainPlaceholder.Controls.Add(new MetroLabel
                                                 {
                                                     Text = "Выберите таблицу.",
                                                     Top = 20,
                                                     Left = 20,
                                                     AutoSize = true
                                                 });

            var lbGrids = new ListBox
            {
                Name = "lbGrids",
                Top = 50,
                Width = 250,
                Height = 300,
                Left = 20,
                ValueMember = "Item1",
                DisplayMember = "Item2"
            };
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.StrudentsGridId, "Студенты"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.TeachersGridId, "Преподаватели"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.ContactsGridId, "Контакты"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.PaymentsGridId, "Платежи"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.EarningsGridId, "Доходы"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.ExpensesGridId, "Расходы"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.PaymentsReportGridId, "Отчет"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.GroupsGridId, "Группы"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.PrivateGridId, "Индивидуальные"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.MasterGridId, "Мастер-классы"));
            lbGrids.Items.Add(new Tuple<int, string>(DataBase.GridsIds.EventsGridId, "События"));

            lbGrids.SelectedIndex = 0;
            lbGrids.SelectionMode = SelectionMode.One;
            lbGrids.SelectedIndexChanged += delegate
                                                {
                                                    chlbGrids.Items.Clear();
                                                    var selectedGrid = lbGrids.SelectedItem as Tuple<int, string>;
                                                    if (selectedGrid == null) return;
                                                    foreach (var item in Db.GetColumnsSettingsList(selectedGrid.Item1))
                                                    {
                                                        chlbGrids.Items.Add(item, item.IsVisible);
                                                    }
                                                };
            _tabMainPlaceholder.Controls.Add(lbGrids);
            _tabMainPlaceholder.Controls.Add(chlbGrids);
            _tabMainPlaceholder.Controls.Add(btnUp);
            _tabMainPlaceholder.Controls.Add(btnDown);
        }

        private void PaymentCostChanged(object sender, ItemCheckEventArgs e)
        {
            var chlbPaymentsSettings = (CheckedListBox)_tabMainPlaceholder.Controls["chlbPaymentsSettings"];
            if (chlbPaymentsSettings == null) return;

            var elem = chlbPaymentsSettings.Items[e.Index] as PaymentReportSettings;
            if (elem == null) return;

            var edit =
                Db.PaymentReportSettings.FirstOrDefault(c => c.Code == elem.Code);
            if (edit == null)
                return;
            edit.ShouldBeCount = Convert.ToBoolean(e.NewValue);
            Db.SubmitChanges();
        }

        private void ColumnVisibilityChanged(object sender, ItemCheckEventArgs e)
        {
            var chlbGrids = (CheckedListBox)_tabMainPlaceholder.Controls["chlbGrids"];
            if (chlbGrids == null) return;

            var elem = chlbGrids.Items[e.Index] as GridsSettings;
            if (elem == null) return;

            var selectedGrid = _tabMainPlaceholder.Controls["lbGrids"] as ListBox;
            if (selectedGrid == null) return;

            var edit =
                Db.GridsSettings.FirstOrDefault(
                    c =>
                    c.ColumnName == elem.ColumnName &&
                    c.GridId == (selectedGrid.SelectedItem as Tuple<int, string>).Item1);
            if (edit == null)
                return;
            edit.IsVisible = Convert.ToBoolean(e.NewValue);
            Db.SubmitChanges();
        }

        private void UpClick(object sender, EventArgs e)
        {
            var chlbGrids = (CheckedListBox)_tabMainPlaceholder.Controls["chlbGrids"];
            if(chlbGrids == null)
                return;
            var index = chlbGrids.SelectedIndex;
            if (index == 0 || index == -1) return;

            var selectedGrid = _tabMainPlaceholder.Controls["lbGrids"] as ListBox;
            if (selectedGrid == null) return;
            
            var elem1 = chlbGrids.Items[index];
            var state1 = chlbGrids.GetItemCheckState(index);
            var elem2 = chlbGrids.Items[index - 1];
            var state2 = chlbGrids.GetItemCheckState(index - 1);

            Db.SwapColumnsOrder((selectedGrid.SelectedItem as Tuple<int, string>).Item1, (chlbGrids.Items[index - 1] as GridsSettings).ColumnName,
                    (chlbGrids.Items[index] as GridsSettings).ColumnName);

            chlbGrids.Items[index] = elem2;
            chlbGrids.SetItemCheckState(index, state2);
            chlbGrids.Items[index - 1] = elem1;
            chlbGrids.SetItemCheckState(index - 1, state1);
            chlbGrids.SelectedIndex--;
        }

        private void DownClick(object sender, EventArgs e)
        {
            var chlbGrids = (CheckedListBox)_tabMainPlaceholder.Controls["chlbGrids"];
            if (chlbGrids == null)
                return;
            var index = chlbGrids.SelectedIndex;
            if (index == -1 || index == chlbGrids.Items.Count - 1) return;

            var selectedGrid = _tabMainPlaceholder.Controls["lbGrids"] as ListBox;
            if (selectedGrid == null) return;


            var elem1 = chlbGrids.Items[index];
            var state1 = chlbGrids.GetItemCheckState(index);
            var elem2 = chlbGrids.Items[index + 1];
            var state2 = chlbGrids.GetItemCheckState(index + 1);

            Db.SwapColumnsOrder((selectedGrid.SelectedItem as Tuple<int, string>).Item1, (chlbGrids.Items[index] as GridsSettings).ColumnName,
                                (chlbGrids.Items[index + 1] as GridsSettings).ColumnName);

            chlbGrids.Items[index] = elem2;
            chlbGrids.SetItemCheckState(index, state2);
            chlbGrids.Items[index + 1] = elem1;
            chlbGrids.SetItemCheckState(index + 1, state1);
            chlbGrids.SelectedIndex++;
        }

        private void DBSettingsClick(object sender, EventArgs e)
        {
            _currentGrid = null;
            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = "База данных";
            _tabMainPlaceholder.Controls.Clear();


            var lbCurrentDB = new MetroLabel
                                  {
                                      Top = 20,
                                      Width = 100,
                                      Left = 40,
                                      Text = "Текущая база: "
                                  };

            var lbCurrentDBName = new MetroLabel
                                      {
                                          Name = "lbCurrentDBName",
                                          Top = 20,
                                          Width = 200,
                                          Left = 140,
                                          Text = Program.DBName,
                                          FontWeight = MetroLabelWeight.Regular
                                      };

            var buttonSave = new MetroButton
                                 {
                                     Top = 100,
                                     Width = 200,
                                     Height = 30,
                                     Left = 40,
                                     Text = "Сделать резервную копию"
                                 };
            buttonSave.Click += MakeBackUp;

            var buttonAddDB = new MetroButton
                                  {
                                      Top = 60,
                                      Width = 200,
                                      Height = 30,
                                      Left = 40,
                                      Text = "Выбрать базу данных"
                                  };
            buttonAddDB.Click += AddDB;

            var buttonClearDB = new MetroButton
            {
                Top = 140,
                Width = 200,
                Height = 30,
                Left = 40,
                Text = "Очистить базу данных"
            };
            buttonClearDB.Click += ClearDB;

            _tabMainPlaceholder.Controls.Add(buttonSave);
            _tabMainPlaceholder.Controls.Add(lbCurrentDB);
            _tabMainPlaceholder.Controls.Add(lbCurrentDBName);
            _tabMainPlaceholder.Controls.Add(buttonAddDB);
            _tabMainPlaceholder.Controls.Add(buttonClearDB);
        }

        private void ClearDB(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Вы действительно желаете очистить базу данных? Все данные будут утрачены.", "Внимание",
                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                Db.CleanDatabase();
                Db = new DataBase(Program.DBName);
            }           
        }

        private void AddDB(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML(*.xml) | *.xml";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Program.DBName = openFileDialog.SafeFileName.Replace(".xml", "");
                    (_tabMainPlaceholder.Controls["lbCurrentDBName"] as MetroLabel).Text = openFileDialog.SafeFileName.Replace(".xml", ""); 

                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    const string extention = ".xml";
                    var originFile = path + "/DB/" + Program.DBName + extention;

                    if(!File.Exists(originFile))
                        File.Copy(openFileDialog.FileName, originFile, false);

                    Db = new DataBase(Program.DBName);
                }
            }
        }

        public void MakeBackUp(object sender, EventArgs eventArgs)
        {
            var noExceptions = true;
            var count = 1;
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                const string extention = ".xml";
                var originFile = path + "/DB/" + Program.DBName + extention;
                var newFile = path + "/BackUp/bk_" + Program.DBName + "_" +
                              DateTime.Today.ToShortDateString() + extention;

                var newFullPath = newFile;
                var fileNameOnly = Path.GetFileNameWithoutExtension(newFile);

                while (File.Exists(newFullPath))
                {
                    var tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    newFullPath = Path.Combine(path + "/BackUp/", tempFileName + extention);
                }
                File.Copy(originFile, newFullPath, true);
            }
            catch (UnauthorizedAccessException)
            {
                noExceptions = false;
                MessageBox.Show("У Вас недостаточно прав для записи файла.", "Внимание");
            }
            catch (DirectoryNotFoundException)
            {
                noExceptions = false;
                MessageBox.Show("Папка 'BackUp' не найдена.", "Внимание");
            }
            catch (IOException ex)
            {
                noExceptions = false;
                MessageBox.Show("Ошибка записи.", "Внимание");
            }
            if (noExceptions)
                MessageBox.Show("База данных успешно скопирована.", "Внимание");

        }

        #endregion
       
        #region TabChanged

        private void PeopleActiveChanged(object sender, EventArgs e)
        {
            if ((sender as RibbonTab).Active)
            {
                btnStudents.PerformClick();
            }
        }

        private void SchoolActivate(object sender, EventArgs e)
        {
            if ((sender as RibbonTab).Active)
            {
                btnGroups.PerformClick();
            }
        }

        private void MoneyActivate(object sender, EventArgs e)
        {
            if ((sender as RibbonTab).Active)
            {
                btnMoney.PerformClick();
            }
        }

        private void SettingsActivate(object sender, EventArgs e)
        {
            if ((sender as RibbonTab).Active)
            {
                btnSettings.PerformClick();
            }
        }

        private void ShedulerActivate(object sender, EventArgs e)
        {
            if ((sender as RibbonTab).Active)
            {
                btnSheduler.PerformClick();
            }
        }

        private void SetGrid(object sender, string tabText, MetroGrid grid)
        {
            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = tabText;

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    _spinnerPanel = new LoadingPanel();
                    _spinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    _spinnerPanel.Show();
                }));

                grid.Top = 0;
                grid.Width = (_tabMainPlaceholder.Width - 50);
                grid.Height = _tabMainPlaceholder.Height - 150;
                grid.MaximumSize = new Size(_tabMainPlaceholder.Width - 50, _tabMainPlaceholder.Height - 250);

                this.Invoke((MethodInvoker)(() =>
                {
                    _tabMainPlaceholder.Controls.Clear();
                    _tabMainPlaceholder.Controls.Add(grid);
                    _currentGrid = grid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => this.Invoke(new Action(() =>
            {
                _spinnerPanel.Close();
                this.Activate();
            }));
            backgroundLoader.RunWorkerAsync();
        }

        private void SelectButton(object sender)
        {
            btnStudents.Checked = false;
            btnTeachers.Checked = false;
            btnContacts.Checked = false;
            btnGroups.Checked = false;
            btnPrivate.Checked = false;
            btnMaster.Checked = false;
            btnMoney.Checked = false;
            btnEvents.Checked = false;
            btnSettings.Checked = false;
            btnGrids.Checked = false;
            btnDB.Checked = false;
            btnEarnings.Checked = false;
            btnExpenses.Checked = false;
            btnPaymentReport.Checked = false;

            (sender as RibbonButton).Checked = true;
        }

        public void HideSecondaryTab()
        {
            _tabSecondaryPlaceholder.Controls.Clear();
            _tabControl.Controls.Remove(_tabSecondaryPlaceholder);
        }

        #endregion

        #region Scheduler

        private void ShcedulerClick(object sender, EventArgs e)
        {
            _currentGrid = null;
            SelectButton(sender);
            HideSecondaryTab();
            _tabMainPlaceholder.Text = "Расписание";

            _tabMainPlaceholder.Controls.Clear();
            SchedulerInit(DateTime.Today);

            var lbScheduler = new MetroLabel()
            {
                Width = 200,
                Left = 50,
                Top = 10,
                Text = "Выберите неделю"
            };
            var dtScheduler = new DateTimePicker
                                  {
                                      Width = 200,
                                      Left = 200,
                                      Top = 10
                                  };
            dtScheduler.ValueChanged += DateSchedulerChanged;

            var lbmonday = new MetroLabel
                               {
                                   Top = 40,
                                   Left = 20,
                                   Text = "Понедельник"
                               };
            var monday = new ListView
                             {
                                 Name = "monday",
                                 Left = 20,
                                 Top = 60,
                                 View = View.Details,
                                 FullRowSelect = true,
                                 Size = new Size(300, 150),
                                 Scrollable = true
                             };
            monday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            monday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            
            monday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday)));
            
            var lbtuestday = new MetroLabel
            {
                Top = 40,
                Left = 320,
                Text = "Вторник"
            };
            var tuestday = new ListView
            {
                Name = "tuestday",
                Left = 320,
                Top = 60,
                View = View.Details,
                FullRowSelect = true,
                Size = new Size(300, 150),
                Scrollable = true
            };
            tuestday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            tuestday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            tuestday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday).AddDays(1)));

            var lbwednesday = new MetroLabel
            {
                Top = 40,
                Left = 620,
                Text = "Среда"
            };
            var wednesday = new ListView
            {
                Name = "wednesday",
                Left = 620,
                Top = 60,
                View = View.Details,
                FullRowSelect = true,
                Size = new Size(300, 150),
                Scrollable = true
            };
            wednesday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            wednesday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            wednesday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday).AddDays(2)));

            var lbthursday = new MetroLabel
            {
                Top = 40,
                Left = 920,
                Text = "Четверг"
            };
            var thursday = new ListView
            {
                Name = "thursday",
                Left = 920,
                Top = 60,
                View = View.Details,
                FullRowSelect = true,
                Size = new Size(300, 150),
                Scrollable = true
            };
            thursday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            thursday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            thursday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday).AddDays(3)));

            var lbfriday = new MetroLabel
            {
                Top = 220,
                Left = 20,
                Text = "Пятница"
            };
            var friday = new ListView
            {
                Name = "friday",
                Left = 20,
                Top = 240,
                View = View.Details,
                FullRowSelect = true,
                Size = new Size(300, 150),
                Scrollable = true
            };
            friday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            friday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            friday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday).AddDays(4)));

            var lbsaturnday = new MetroLabel
            {
                Top = 220,
                Left = 320,
                Text = "Суббота"
            };
            var saturnday = new ListView
            {
                Name = "saturnday",
                Left = 320,
                Top = 240,
                View = View.Details,
                FullRowSelect = true,
                Size = new Size(300, 150),
                Scrollable = true
            };
            saturnday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            saturnday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            saturnday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday).AddDays(5)));

            var lbsunday = new MetroLabel
            {
                Top = 220,
                Left = 620,
                Text = "Воскресение"
            };
            var sunday = new ListView
            {
                Name = "sunday",
                Left = 620,
                Top = 240,
                View = View.Details,
                FullRowSelect = true,
                Size = new Size(300, 150),
                Scrollable = true
            };
            sunday.Columns.Add("Группа", -2, HorizontalAlignment.Left);
            sunday.Columns.Add("Время", -2, HorizontalAlignment.Left);
            sunday.Items.AddRange(Db.GetScheduler(DateTime.Today.StartOfWeek(DayOfWeek.Monday).AddDays(6)));

            _tabMainPlaceholder.Controls.Add(dtScheduler);
            _tabMainPlaceholder.Controls.Add(monday);
            _tabMainPlaceholder.Controls.Add(tuestday);
            _tabMainPlaceholder.Controls.Add(wednesday);
            _tabMainPlaceholder.Controls.Add(thursday);
            _tabMainPlaceholder.Controls.Add(friday);
            _tabMainPlaceholder.Controls.Add(saturnday);
            _tabMainPlaceholder.Controls.Add(sunday);
            _tabMainPlaceholder.Controls.Add(lbmonday);
            _tabMainPlaceholder.Controls.Add(lbthursday);
            _tabMainPlaceholder.Controls.Add(lbwednesday);
            _tabMainPlaceholder.Controls.Add(lbtuestday);
            _tabMainPlaceholder.Controls.Add(lbfriday);
            _tabMainPlaceholder.Controls.Add(lbsaturnday);
            _tabMainPlaceholder.Controls.Add(lbsunday);
            _tabMainPlaceholder.Controls.Add(lbScheduler);
            
        }

        private void DateSchedulerChanged(object sender, EventArgs e)
        {
            SchedulerInit((sender as DateTimePicker).Value);
        }

        private void SchedulerInit(DateTime date)
        {
            if ((_tabMainPlaceholder.Controls["monday"] as ListView) != null)
            {
                (_tabMainPlaceholder.Controls["monday"] as ListView).Items.Clear();
                (_tabMainPlaceholder.Controls["tuestday"] as ListView).Items.Clear();
                (_tabMainPlaceholder.Controls["wednesday"] as ListView).Items.Clear();
                (_tabMainPlaceholder.Controls["thursday"] as ListView).Items.Clear();
                (_tabMainPlaceholder.Controls["friday"] as ListView).Items.Clear();
                (_tabMainPlaceholder.Controls["saturnday"] as ListView).Items.Clear();
                (_tabMainPlaceholder.Controls["sunday"] as ListView).Items.Clear();

                (_tabMainPlaceholder.Controls["monday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(0)));
                (_tabMainPlaceholder.Controls["tuestday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(1)));
                (_tabMainPlaceholder.Controls["wednesday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(2)));
                (_tabMainPlaceholder.Controls["thursday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(3)));
                (_tabMainPlaceholder.Controls["friday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(4)));
                (_tabMainPlaceholder.Controls["saturnday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(5)));
                (_tabMainPlaceholder.Controls["sunday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(6)));
            }
        }

        #endregion

        #region HotKeys

        void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F && _currentGrid != null)
            {
                e.SuppressKeyPress = true;
                SearchInGrid();
            }
        }

        public void SearchInGrid()
        {
            _searchForm.Grid = (_tabControl.SelectedTab == _tabMainPlaceholder) ? _currentGrid : _secondaryGrid;
            _searchForm.searchTextBox.Text = String.Empty;
            _searchForm.searchTextBox.Select();
            _searchForm.SetColumnsList();
            _searchForm.ShowDialog();
        }

        #endregion

    }
}
