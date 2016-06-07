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
   
        public TabControl TabControl;
        public LoadingPanel SpinnerPanel;
        public TabPage TabMainPlaceholder;
        public TabPage TabSecondaryPlaceholder;


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
            TabControl = new MetroTabControl { Width = Width, Height = Height, Top=ribbon1.Bottom, Style = MetroColorStyle.Green};
            SpinnerPanel = new LoadingPanel();
            TabMainPlaceholder = new MetroTabPage();
            TabSecondaryPlaceholder = new MetroTabPage();
            TabControl.Controls.Add(TabMainPlaceholder);
            Controls.Add(TabControl);


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
                            SpinnerPanel = new LoadingPanel();
                            SpinnerPanel.UpdateLoadingText("Создание новой базы данных...");
                            SpinnerPanel.Show();
                        }));

                        Db.CreateNewDatabase();
                        AppSettings = new AppSettings(Db);
                    };
                    _backgroundWorker.RunWorkerCompleted += (s, arg) => Invoke(new Action(() =>
                    {
                        SpinnerPanel.Close();
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
                MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250),
                EditForm = new PaymentsEditForm { AppSettings = AppSettings },
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder
            };

            SelectButton(sender);
            HideSecondaryTab();
            TabMainPlaceholder.Text = "Платежи";

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    SpinnerPanel = new LoadingPanel();
                    SpinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    SpinnerPanel.Show();
                }));

                _paymentsGrid.Top = 0;
                _paymentsGrid.Width = (TabMainPlaceholder.Width / 2);
                _paymentsGrid.Height = TabMainPlaceholder.Height - 150;

                Invoke((MethodInvoker)(() =>
                {

                    var datesPanel = new MetroPanel
                    {
                        Name = "pnDates",
                        Width = 500,
                        Height = 330,
                        Top = 0,
                        Left = (TabMainPlaceholder.Width / 2) + 50,
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
                                        Text = _paymentsGrid.Total + " грн.",
                                        Top = 100,
                                        Left = 240,
                                        Width = 100,
                                    };

                    datesPanel.Controls.Add(total);

                    var export = new MetroButton
                    {
                        Top = 140,
                        Width = 200,
                        Height = 30,
                        Left = 30,
                        Text = "Сгенерировать отчет"
                    };
                    export.Click += (send, args) => { ExportToExel(dateFrom.Value, dateTo.Value); };

                    datesPanel.Controls.Add(export);

                    dateTo.ValueChanged += (send, args) =>
                                               {
                                                   _paymentsGrid.DateTo = dateTo.Value;
                                                   _paymentsGrid.RefreshGrid();
                                                   total.Text = _paymentsGrid.Total + " грн.";
                                               };
                    dateFrom.ValueChanged += (send, args) =>
                                                 {
                                                     _paymentsGrid.DateFrom = dateFrom.Value;
                                                     _paymentsGrid.RefreshGrid();
                                                     total.Text = _paymentsGrid.Total + " грн.";
                                                 };


                    TabMainPlaceholder.Controls.Clear();
                    TabMainPlaceholder.Controls.Add(datesPanel);
                    TabMainPlaceholder.Controls.Add(_paymentsGrid);
                    _currentGrid = _paymentsGrid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => Invoke(new Action(() =>
            {
                SpinnerPanel.Close();
                Activate();
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
                MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250),
                EditForm = new BudgetEditFrom { AppSettings = AppSettings },
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder
            };

            SelectButton(sender);
            HideSecondaryTab();
            TabMainPlaceholder.Text = isEarning ? "Доходы" : "Расходы";

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    SpinnerPanel = new LoadingPanel();
                    SpinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    SpinnerPanel.Show();
                }));

                _budgetGrid.Top = 0;
                _budgetGrid.Width = (TabMainPlaceholder.Width / 2);
                _budgetGrid.Height = TabMainPlaceholder.Height - 150;

                Invoke((MethodInvoker)(() =>
                {

                    var datesPanel = new MetroPanel
                    {
                        Name = "pnDates",
                        Width = 500,
                        Height = 300,
                        Top = 0,
                        Left = (TabMainPlaceholder.Width / 2) + 50,
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

                    TabMainPlaceholder.Controls.Clear();
                    TabMainPlaceholder.Controls.Add(datesPanel);
                    TabMainPlaceholder.Controls.Add(_budgetGrid);
                    _currentGrid = _budgetGrid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => Invoke(new Action(() =>
            {
                SpinnerPanel.Close();
                Activate();
            }));
            backgroundLoader.RunWorkerAsync();
        }

        private void PaymentReportMenuClick(object sender, EventArgs e)
        {
            _reportGrid = new PaymentsReportGrid(Db)
            {
                MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder
            };

            SelectButton(sender);
            HideSecondaryTab();
            TabMainPlaceholder.Text = "Отчет";

            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    SpinnerPanel = new LoadingPanel();
                    SpinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    SpinnerPanel.Show();
                }));

                _reportGrid.Top = 0;
                _reportGrid.Width = (TabMainPlaceholder.Width / 2)+250;
                _reportGrid.Height = TabMainPlaceholder.Height - 150;

                Invoke((MethodInvoker)(() =>
                {

                    var datesPanel = new MetroPanel
                    {
                        Name = "pnDates",
                        Width = 500,
                        Height = 300,
                        Top = 0,
                        Left = (TabMainPlaceholder.Width / 2) + 300,
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

                    TabMainPlaceholder.Controls.Clear();
                    TabMainPlaceholder.Controls.Add(datesPanel);
                    TabMainPlaceholder.Controls.Add(_reportGrid);
                    _currentGrid = _reportGrid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => Invoke(new Action(() =>
            {
                SpinnerPanel.Close();
                Activate();
            }));
            backgroundLoader.RunWorkerAsync();
        }

        #endregion

        #region Persons

        private void StudentsMenuClick(object sender, EventArgs e)
        {
            _secondaryGrid = new PaymentsGrid(Db)
                                 {
                                     IsDetailsGrid = true,
                                     Name = "paymentsGrid",
                                     Width = TabMainPlaceholder.Width - 50,
                                     Height = TabMainPlaceholder.Height - 350,
                                     MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250),
                                     EditForm = new PaymentsEditForm {AppSettings = AppSettings},
                                     MainTabControl = TabControl,
                                     MainPlaceholder = TabMainPlaceholder,
                                     SecondaryPlaceholder = TabSecondaryPlaceholder,
                                     MainForm = this
                                 };

            _studentsGrid = new PersonsGrid(Db, PersonType.Student)
            {
                EditForm = new PersonsEditGridForm(PersonType.Student),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
                PaymentDetailsGrid = ((PaymentsGrid) _secondaryGrid),
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
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
                MainForm = this
            };

            SetGrid(sender, "Преподаватели", _teachersGrid);
        }

        private void ContactsMenuClick(object sender, EventArgs e)
        {
            _contactsGrid = new PersonsGrid(Db, PersonType.Contact)
            {
                EditForm = new PersonsEditGridForm(PersonType.Contact),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
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
                                     Width = TabMainPlaceholder.Width - 50,
                                     Height = TabMainPlaceholder.Height - 150,
                                     MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250),
                                     EditForm = new PersonsEditGridForm(PersonType.Student) {IsDetails = true},
                                     MainTabControl = TabControl,
                                     MainPlaceholder = TabMainPlaceholder,
                                     SecondaryPlaceholder = TabSecondaryPlaceholder,
                                     MainForm = this
                                 };
            _groupsGrid = new GroupsGrid(Db, GroupType.Common)
            {
                EditForm = new GroupsEditForm(GroupType.Common),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
                PersonsDetailsGrid = (PersonsGrid) _secondaryGrid,
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
                                     Width = TabMainPlaceholder.Width - 50,
                                     Height = TabMainPlaceholder.Height - 150,
                                     MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250),
                                     EditForm = new PersonsEditGridForm(PersonType.Student) {IsDetails = true},
                                     MainTabControl = TabControl,
                                     MainPlaceholder = TabMainPlaceholder,
                                     SecondaryPlaceholder = TabSecondaryPlaceholder,
                                     MainForm = this
                                 };

            _privateGrid = new GroupsGrid(Db, GroupType.Private)
            {
                EditForm = new GroupsEditForm(GroupType.Private),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
                PersonsDetailsGrid = (PersonsGrid) _secondaryGrid,
                MainForm = this
            };

            SetGrid(sender, "Индивидуальные", _privateGrid);
        }

        private void MasterEventClick(object sender, EventArgs e)
        {
            _masterEventGrid = new EventsGrid(Db, EventsType.Master)
            {
                EditForm = new EventForm(EventsType.Master),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
                MainForm = this
            };

            SetGrid(sender, "Мастер-классы", _masterEventGrid);
        }

        private void EventClick(object sender, EventArgs e)
        {
            _eventGrid = new EventsGrid(Db, EventsType.Event)
            {
                EditForm = new EventForm(EventsType.Event),
                MainTabControl = TabControl,
                MainPlaceholder = TabMainPlaceholder,
                SecondaryPlaceholder = TabSecondaryPlaceholder,
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
            TabMainPlaceholder.Text = "Настройки";
            TabMainPlaceholder.Controls.Clear();

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
                                                (TabMainPlaceholder.Controls["pnSettings"].Controls["tbDefaultCosts"]
                                                 as TextBox).Text);
                                        AppSettings.DefaultCostsSingle =
                                            Convert.ToDecimal(
                                                (TabMainPlaceholder.Controls["pnSettings"].Controls[
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
                                          (TabMainPlaceholder.Controls["pnSettings"].Controls["tbDefaultCosts"] as
                                           TextBox).Text =
                                              AppSettings.DefaultCosts.ToString("0.00");
                                          (TabMainPlaceholder.Controls["pnSettings"].Controls["tbDefaultCostsSingle"]
                                           as TextBox).Text =
                                              AppSettings.DefaultCostsSingle.ToString("0.00");
                                      };

            TabMainPlaceholder.Controls.Add(new MetroLabel
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
            TabMainPlaceholder.Controls.Add(chlbPaymentsSettings);

            settingsPanel.Controls.Add(cancelButton);
           TabMainPlaceholder.Controls.Add(settingsPanel);
        }

        private void GridsClick(object sender, EventArgs e)
        {
            _currentGrid = null;
            SelectButton(sender);
            HideSecondaryTab();
            TabMainPlaceholder.Text = "Таблицы";
            TabMainPlaceholder.Controls.Clear();

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

            TabMainPlaceholder.Controls.Add(new MetroLabel
                                                 {
                                                     Text =
                                                         "Выберите колонки, которые будут отображены в таблице и их поочередность.",
                                                     Top = 20,
                                                     Left = 420,
                                                     AutoSize = true
                                                 });

            TabMainPlaceholder.Controls.Add(new MetroLabel
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
            TabMainPlaceholder.Controls.Add(lbGrids);
            TabMainPlaceholder.Controls.Add(chlbGrids);
            TabMainPlaceholder.Controls.Add(btnUp);
            TabMainPlaceholder.Controls.Add(btnDown);
        }

        private void PaymentCostChanged(object sender, ItemCheckEventArgs e)
        {
            var chlbPaymentsSettings = (CheckedListBox)TabMainPlaceholder.Controls["chlbPaymentsSettings"];
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
            var chlbGrids = (CheckedListBox)TabMainPlaceholder.Controls["chlbGrids"];
            if (chlbGrids == null) return;

            var elem = chlbGrids.Items[e.Index] as GridsSettings;
            if (elem == null) return;

            var selectedGrid = TabMainPlaceholder.Controls["lbGrids"] as ListBox;
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
            var chlbGrids = (CheckedListBox)TabMainPlaceholder.Controls["chlbGrids"];
            if(chlbGrids == null)
                return;
            var index = chlbGrids.SelectedIndex;
            if (index == 0 || index == -1) return;

            var selectedGrid = TabMainPlaceholder.Controls["lbGrids"] as ListBox;
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
            var chlbGrids = (CheckedListBox)TabMainPlaceholder.Controls["chlbGrids"];
            if (chlbGrids == null)
                return;
            var index = chlbGrids.SelectedIndex;
            if (index == -1 || index == chlbGrids.Items.Count - 1) return;

            var selectedGrid = TabMainPlaceholder.Controls["lbGrids"] as ListBox;
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
            TabMainPlaceholder.Text = "База данных";
            TabMainPlaceholder.Controls.Clear();


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

            TabMainPlaceholder.Controls.Add(buttonSave);
            TabMainPlaceholder.Controls.Add(lbCurrentDB);
            TabMainPlaceholder.Controls.Add(lbCurrentDBName);
            TabMainPlaceholder.Controls.Add(buttonAddDB);
            TabMainPlaceholder.Controls.Add(buttonClearDB);
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
                    (TabMainPlaceholder.Controls["lbCurrentDBName"] as MetroLabel).Text = openFileDialog.SafeFileName.Replace(".xml", ""); 

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
            TabMainPlaceholder.Text = tabText;

            var backgroundLoader = new BackgroundWorker();
            backgroundLoader.DoWork += (s, arg) =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    SpinnerPanel = new LoadingPanel();
                    SpinnerPanel.UpdateLoadingText("Загружаеются данные...");
                    SpinnerPanel.Show();
                }));

                grid.Top = 0;
                grid.Width = (TabMainPlaceholder.Width - 50);
                grid.Height = TabMainPlaceholder.Height - 150;
                grid.MaximumSize = new Size(TabMainPlaceholder.Width - 50, TabMainPlaceholder.Height - 250);

                this.Invoke((MethodInvoker)(() =>
                {
                    TabMainPlaceholder.Controls.Clear();
                    TabMainPlaceholder.Controls.Add(grid);
                    _currentGrid = grid;
                    _currentGrid.Select();
                }));
            };
            backgroundLoader.RunWorkerCompleted += (s, arg) => this.Invoke(new Action(() =>
            {
                SpinnerPanel.Close();
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
            TabSecondaryPlaceholder.Controls.Clear();
            TabControl.Controls.Remove(TabSecondaryPlaceholder);
        }

        #endregion

        #region Scheduler

        private void ShcedulerClick(object sender, EventArgs e)
        {
            _currentGrid = null;
            SelectButton(sender);
            HideSecondaryTab();
            TabMainPlaceholder.Text = "Расписание";

            TabMainPlaceholder.Controls.Clear();
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

            TabMainPlaceholder.Controls.Add(dtScheduler);
            TabMainPlaceholder.Controls.Add(monday);
            TabMainPlaceholder.Controls.Add(tuestday);
            TabMainPlaceholder.Controls.Add(wednesday);
            TabMainPlaceholder.Controls.Add(thursday);
            TabMainPlaceholder.Controls.Add(friday);
            TabMainPlaceholder.Controls.Add(saturnday);
            TabMainPlaceholder.Controls.Add(sunday);
            TabMainPlaceholder.Controls.Add(lbmonday);
            TabMainPlaceholder.Controls.Add(lbthursday);
            TabMainPlaceholder.Controls.Add(lbwednesday);
            TabMainPlaceholder.Controls.Add(lbtuestday);
            TabMainPlaceholder.Controls.Add(lbfriday);
            TabMainPlaceholder.Controls.Add(lbsaturnday);
            TabMainPlaceholder.Controls.Add(lbsunday);
            TabMainPlaceholder.Controls.Add(lbScheduler);
            
        }

        private void DateSchedulerChanged(object sender, EventArgs e)
        {
            SchedulerInit((sender as DateTimePicker).Value);
        }

        private void SchedulerInit(DateTime date)
        {
            if ((TabMainPlaceholder.Controls["monday"] as ListView) != null)
            {
                (TabMainPlaceholder.Controls["monday"] as ListView).Items.Clear();
                (TabMainPlaceholder.Controls["tuestday"] as ListView).Items.Clear();
                (TabMainPlaceholder.Controls["wednesday"] as ListView).Items.Clear();
                (TabMainPlaceholder.Controls["thursday"] as ListView).Items.Clear();
                (TabMainPlaceholder.Controls["friday"] as ListView).Items.Clear();
                (TabMainPlaceholder.Controls["saturnday"] as ListView).Items.Clear();
                (TabMainPlaceholder.Controls["sunday"] as ListView).Items.Clear();

                (TabMainPlaceholder.Controls["monday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(0)));
                (TabMainPlaceholder.Controls["tuestday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(1)));
                (TabMainPlaceholder.Controls["wednesday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(2)));
                (TabMainPlaceholder.Controls["thursday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(3)));
                (TabMainPlaceholder.Controls["friday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(4)));
                (TabMainPlaceholder.Controls["saturnday"] as ListView).Items.AddRange(
                    Db.GetScheduler(date.StartOfWeek(DayOfWeek.Monday).AddDays(5)));
                (TabMainPlaceholder.Controls["sunday"] as ListView).Items.AddRange(
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
            _searchForm.Grid = (TabControl.SelectedTab == TabMainPlaceholder) ? _currentGrid : _secondaryGrid;
            _searchForm.searchTextBox.Text = String.Empty;
            _searchForm.searchTextBox.Select();
            _searchForm.SetColumnsList();
            _searchForm.ShowDialog();
        }

        #endregion

        #region ExportToExel

        public void ExportToExel(DateTime dateFrom, DateTime dateTo)
        {
            var saveFileDialog = new SaveFileDialog
                                 {
                                     Filter = "xml files (*.xlsx)|*.xlsx",
                                     FilterIndex = 1,
                                     RestoreDirectory = true
                                 };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var backgroundLoader = new BackgroundWorker();
                backgroundLoader.DoWork += (s, arg) =>
                                           {
                                               Invoke((MethodInvoker) (() =>
                                                                       {
                                                                           SpinnerPanel = new LoadingPanel();
                                                                           SpinnerPanel.UpdateLoadingText(
                                                                               "Отчет генерируется...");
                                                                           SpinnerPanel.Show();
                                                                       }));

                                               var excel = new Microsoft.Office.Interop.Excel.Application();
                                               excel.Workbooks.Add();
                                               var workSheet = excel.ActiveSheet;
                                               try
                                               {
                                                   workSheet.Cells[1, 1] = "Отчетный период:";
                                                   workSheet.Cells[1, 2] =
                                                       (_paymentsGrid.DateFrom ?? DateTime.Today).ToString("yyyy/MM/dd");
                                                   workSheet.Cells[1, 3] =
                                                       (_paymentsGrid.DateTo ?? DateTime.Today).ToString("yyyy/MM/dd");

                                                   var indexColumn = 1;
                                                   foreach (DataGridViewColumn column in _paymentsGrid.Columns)
                                                   {
                                                       if (column.Visible)
                                                       {
                                                           workSheet.Cells[2, indexColumn] = column.HeaderText;
                                                           indexColumn++;
                                                       }
                                                   }

                                                   var indexRow = 3;
                                                   foreach (DataGridViewRow row in _paymentsGrid.Rows)
                                                   {
                                                       indexColumn = 1;
                                                       foreach (DataGridViewCell cell in row.Cells)
                                                       {
                                                           if (cell.Visible)
                                                           {
                                                               workSheet.Cells[indexRow, indexColumn] = cell.Value;
                                                               indexColumn++;
                                                           }
                                                       }
                                                       indexRow++;
                                                   }

                                                   workSheet.Cells[indexRow, indexColumn - 2] = "Итого:";
                                                   workSheet.Cells[indexRow, indexColumn - 1] = _paymentsGrid.Total;

                                                   workSheet.Range["A1"].AutoFormat(
                                                       Microsoft.Office.Interop.Excel.XlRangeAutoFormat
                                                           .xlRangeAutoFormatClassic1);

                                                   workSheet.SaveAs(saveFileDialog.FileName);
                                               }
                                               catch (Exception exception)
                                               {
                                                   MessageBox.Show(
                                                       "Произошла ошибка при записи Excel файла! " + exception.Message,
                                                       "Внимание",
                                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                               }
                                               finally
                                               {
                                                   excel.Quit();
                                                   System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                                                   if (workSheet != null)
                                                       System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                                                   excel = null;
                                                   workSheet = null;
                                                   GC.Collect();
                                               }
                                           };
                backgroundLoader.RunWorkerCompleted += (s, arg) => Invoke(new Action(() =>
                                                                                     {
                                                                                         SpinnerPanel.Close();
                                                                                         MessageBox.Show("Операция завершена.");
                                                                                     }));
                backgroundLoader.RunWorkerAsync();
            }
        }

        #endregion

    }
}
