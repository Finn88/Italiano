using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;
using Model;
using Model.Grids;
using MetroFramework;

namespace OurStudents
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonTabStudents = new System.Windows.Forms.RibbonTab();
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.tabPeople = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.btnStudents = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel10 = new System.Windows.Forms.RibbonPanel();
            this.btnTeachers = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.btnContacts = new System.Windows.Forms.RibbonButton();
            this.tabSchool = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.btnGroups = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.btnPrivate = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel6 = new System.Windows.Forms.RibbonPanel();
            this.btnMaster = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel8 = new System.Windows.Forms.RibbonPanel();
            this.btnEvents = new System.Windows.Forms.RibbonButton();
            this.tabMoney = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel7 = new System.Windows.Forms.RibbonPanel();
            this.btnMoney = new System.Windows.Forms.RibbonButton();
            this.tabScheduler = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.btnSheduler = new System.Windows.Forms.RibbonButton();
            this.tabSettings = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel9 = new System.Windows.Forms.RibbonPanel();
            this.btnSettings = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelDB = new System.Windows.Forms.RibbonPanel();
            this.btnDB = new System.Windows.Forms.RibbonButton();
            this.TablesSettings = new System.Windows.Forms.RibbonPanel();
            this.btnGrids = new System.Windows.Forms.RibbonButton();
            this._backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ribbonPanel11 = new System.Windows.Forms.RibbonPanel();
            this.btnEarnings = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel12 = new System.Windows.Forms.RibbonPanel();
            this.btnExpenses = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel13 = new System.Windows.Forms.RibbonPanel();
            this.btnPaymentReport = new System.Windows.Forms.RibbonButton();
            this.SuspendLayout();
            // 
            // ribbonTabStudents
            // 
            this.ribbonTabStudents.Text = null;
            // 
            // ribbon1
            // 
            this.ribbon1.BackColor = System.Drawing.Color.White;
            this.ribbon1.Enabled = false;
            this.ribbon1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.ribbon1.Location = new System.Drawing.Point(20, 60);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Enabled = false;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbImage = null;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon1.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.Visible = false;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.ribbon1.Size = new System.Drawing.Size(793, 165);
            this.ribbon1.TabIndex = 1;
            this.ribbon1.Tabs.Add(this.tabPeople);
            this.ribbon1.Tabs.Add(this.tabSchool);
            this.ribbon1.Tabs.Add(this.tabMoney);
            this.ribbon1.Tabs.Add(this.tabScheduler);
            this.ribbon1.Tabs.Add(this.tabSettings);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Green;
            // 
            // tabPeople
            // 
            this.tabPeople.Panels.Add(this.ribbonPanel1);
            this.tabPeople.Panels.Add(this.ribbonPanel10);
            this.tabPeople.Panels.Add(this.ribbonPanel2);
            this.tabPeople.Text = "Люди";
            this.tabPeople.ActiveChanged += new System.EventHandler(this.PeopleActiveChanged);
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.btnStudents);
            this.ribbonPanel1.Text = "Студенты";
            // 
            // btnStudents
            // 
            this.btnStudents.Image = global::OurStudents.Properties.Resources._1455418195_user5;
            this.btnStudents.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnStudents.SmallImage")));
            this.btnStudents.Tag = "Студенты";
            this.btnStudents.Text = "";
            this.btnStudents.Click += new System.EventHandler(this.StudentsMenuClick);
            // 
            // ribbonPanel10
            // 
            this.ribbonPanel10.ButtonMoreVisible = false;
            this.ribbonPanel10.Items.Add(this.btnTeachers);
            this.ribbonPanel10.Text = "Преподаватели";
            // 
            // btnTeachers
            // 
            this.btnTeachers.Image = global::OurStudents.Properties.Resources._1455418475_Teacher;
            this.btnTeachers.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnTeachers.SmallImage")));
            this.btnTeachers.Tag = "Преподаватели";
            this.btnTeachers.Text = "";
            this.btnTeachers.Click += new System.EventHandler(this.TeachersMenuItemClick);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ButtonMoreVisible = false;
            this.ribbonPanel2.Items.Add(this.btnContacts);
            this.ribbonPanel2.Text = "Контакты";
            // 
            // btnContacts
            // 
            this.btnContacts.Image = global::OurStudents.Properties.Resources._1455420010_Person_Undefined_Male_Light;
            this.btnContacts.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnContacts.SmallImage")));
            this.btnContacts.Tag = "Контакты";
            this.btnContacts.Text = "";
            this.btnContacts.Click += new System.EventHandler(this.ContactsMenuClick);
            // 
            // tabSchool
            // 
            this.tabSchool.Panels.Add(this.ribbonPanel4);
            this.tabSchool.Panels.Add(this.ribbonPanel5);
            this.tabSchool.Panels.Add(this.ribbonPanel6);
            this.tabSchool.Panels.Add(this.ribbonPanel8);
            this.tabSchool.Text = "Школа";
            this.tabSchool.ActiveChanged += new System.EventHandler(this.SchoolActivate);
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.ButtonMoreVisible = false;
            this.ribbonPanel4.Items.Add(this.btnGroups);
            this.ribbonPanel4.Text = "Группы";
            // 
            // btnGroups
            // 
            this.btnGroups.Image = global::OurStudents.Properties.Resources._1455418713_Group_Meeting_Light;
            this.btnGroups.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnGroups.SmallImage")));
            this.btnGroups.Tag = "Группы";
            this.btnGroups.Text = "";
            this.btnGroups.Click += new System.EventHandler(this.GroupsMenuItemClick);
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.ButtonMoreVisible = false;
            this.ribbonPanel5.Items.Add(this.btnPrivate);
            this.ribbonPanel5.Text = "Индивидуальные";
            // 
            // btnPrivate
            // 
            this.btnPrivate.Image = global::OurStudents.Properties.Resources._1455420292_lessons;
            this.btnPrivate.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPrivate.SmallImage")));
            this.btnPrivate.Tag = "Индивидуальные";
            this.btnPrivate.Text = "";
            this.btnPrivate.Click += new System.EventHandler(this.PrivateClick);
            // 
            // ribbonPanel6
            // 
            this.ribbonPanel6.ButtonMoreVisible = false;
            this.ribbonPanel6.Items.Add(this.btnMaster);
            this.ribbonPanel6.Text = "Мастер-классы";
            // 
            // btnMaster
            // 
            this.btnMaster.Image = global::OurStudents.Properties.Resources._1455419260_Cook_Book;
            this.btnMaster.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnMaster.SmallImage")));
            this.btnMaster.Tag = "Мастер-классы";
            this.btnMaster.Text = "";
            this.btnMaster.Click += new System.EventHandler(this.MasterEventClick);
            // 
            // ribbonPanel8
            // 
            this.ribbonPanel8.ButtonMoreVisible = false;
            this.ribbonPanel8.Items.Add(this.btnEvents);
            this.ribbonPanel8.Text = "События";
            // 
            // btnEvents
            // 
            this.btnEvents.Image = global::OurStudents.Properties.Resources._1455907230_02_calendar;
            this.btnEvents.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnEvents.SmallImage")));
            this.btnEvents.Tag = "События";
            this.btnEvents.Text = "";
            this.btnEvents.Click += new System.EventHandler(this.EventClick);
            // 
            // tabMoney
            // 
            this.tabMoney.Panels.Add(this.ribbonPanel7);
            this.tabMoney.Panels.Add(this.ribbonPanel11);
            this.tabMoney.Panels.Add(this.ribbonPanel12);
            this.tabMoney.Panels.Add(this.ribbonPanel13);
            this.tabMoney.Text = "Финансы";
            this.tabMoney.ActiveChanged += new System.EventHandler(this.MoneyActivate);
            // 
            // ribbonPanel7
            // 
            this.ribbonPanel7.ButtonMoreVisible = false;
            this.ribbonPanel7.Items.Add(this.btnMoney);
            this.ribbonPanel7.Text = "Платежи";
            // 
            // btnMoney
            // 
            this.btnMoney.Image = global::OurStudents.Properties.Resources._1455417859_coins;
            this.btnMoney.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnMoney.SmallImage")));
            this.btnMoney.Tag = "Платежи";
            this.btnMoney.Text = "";
            this.btnMoney.Click += new System.EventHandler(this.PaymentsMenuClick);
            // 
            // tabScheduler
            // 
            this.tabScheduler.Panels.Add(this.ribbonPanel3);
            this.tabScheduler.Text = "Расписание";
            this.tabScheduler.ActiveChanged += new System.EventHandler(this.ShedulerActivate);
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ButtonMoreVisible = false;
            this.ribbonPanel3.Items.Add(this.btnSheduler);
            this.ribbonPanel3.Text = "Расписание";
            // 
            // btnSheduler
            // 
            this.btnSheduler.Image = global::OurStudents.Properties.Resources._1455422814_schedule;
            this.btnSheduler.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSheduler.SmallImage")));
            this.btnSheduler.Text = "";
            this.btnSheduler.Click += new System.EventHandler(this.ShcedulerClick);
            // 
            // tabSettings
            // 
            this.tabSettings.Panels.Add(this.ribbonPanel9);
            this.tabSettings.Panels.Add(this.ribbonPanelDB);
            this.tabSettings.Panels.Add(this.TablesSettings);
            this.tabSettings.Text = "Настройки";
            this.tabSettings.ActiveChanged += new System.EventHandler(this.SettingsActivate);
            // 
            // ribbonPanel9
            // 
            this.ribbonPanel9.ButtonMoreVisible = false;
            this.ribbonPanel9.Items.Add(this.btnSettings);
            this.ribbonPanel9.Text = "Настройки";
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::OurStudents.Properties.Resources._1455422972_advancedsettings;
            this.btnSettings.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSettings.SmallImage")));
            this.btnSettings.Text = "";
            this.btnSettings.Click += new System.EventHandler(this.SettingsClick);
            // 
            // ribbonPanelDB
            // 
            this.ribbonPanelDB.ButtonMoreVisible = false;
            this.ribbonPanelDB.Items.Add(this.btnDB);
            this.ribbonPanelDB.Text = "База Данных";
            // 
            // btnDB
            // 
            this.btnDB.Image = global::OurStudents.Properties.Resources._1455906863_database_px_png;
            this.btnDB.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnDB.SmallImage")));
            this.btnDB.Text = "";
            this.btnDB.Click += new System.EventHandler(this.DBSettingsClick);
            // 
            // TablesSettings
            // 
            this.TablesSettings.ButtonMoreEnabled = false;
            this.TablesSettings.ButtonMoreVisible = false;
            this.TablesSettings.Items.Add(this.btnGrids);
            this.TablesSettings.Text = "Настройки таблиц";
            // 
            // btnGrids
            // 
            this.btnGrids.Image = ((System.Drawing.Image)(resources.GetObject("btnGrids.Image")));
            this.btnGrids.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnGrids.SmallImage")));
            this.btnGrids.Text = "";
            this.btnGrids.Click += new System.EventHandler(this.GridsClick);
            // 
            // ribbonPanel11
            // 
            this.ribbonPanel11.ButtonMoreEnabled = false;
            this.ribbonPanel11.ButtonMoreVisible = false;
            this.ribbonPanel11.Items.Add(this.btnEarnings);
            this.ribbonPanel11.Text = "Доходы";
            // 
            // btnEarnings
            // 
            this.btnEarnings.Image = ((System.Drawing.Image)(resources.GetObject("btnEarnings.Image")));
            this.btnEarnings.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnEarnings.SmallImage")));
            this.btnEarnings.Text = "";
            this.btnEarnings.Click += new System.EventHandler(this.EarningsMenuClick);
            // 
            // ribbonPanel12
            // 
            this.ribbonPanel12.ButtonMoreEnabled = false;
            this.ribbonPanel12.ButtonMoreVisible = false;
            this.ribbonPanel12.Items.Add(this.btnExpenses);
            this.ribbonPanel12.Text = "Расходы";
            // 
            // btnExpenses
            // 
            this.btnExpenses.Image = ((System.Drawing.Image)(resources.GetObject("btnExpenses.Image")));
            this.btnExpenses.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnExpenses.SmallImage")));
            this.btnExpenses.Text = "";
            this.btnExpenses.Click += new System.EventHandler(this.ExpensesMenuClick);
            // 
            // ribbonPanel13
            // 
            this.ribbonPanel13.ButtonMoreEnabled = false;
            this.ribbonPanel13.ButtonMoreVisible = false;
            this.ribbonPanel13.Items.Add(this.btnPaymentReport);
            this.ribbonPanel13.Text = "Отчет";
            // 
            // btnPaymentReport
            // 
            this.btnPaymentReport.Image = ((System.Drawing.Image)(resources.GetObject("btnPaymentReport.Image")));
            this.btnPaymentReport.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPaymentReport.SmallImage")));
            this.btnPaymentReport.Text = "";
            this.btnPaymentReport.Click += new System.EventHandler(this.PaymentReportMenuClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 365);
            this.Controls.Add(this.ribbon1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Итальянский Дом";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnMainForm);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.RibbonTab ribbonTabStudents;
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab tabPeople;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton btnStudents;
        private System.Windows.Forms.RibbonTab tabSchool;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;

        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton btnGroups;
        private System.Windows.Forms.RibbonPanel ribbonPanel5;
        private System.Windows.Forms.RibbonButton btnPrivate;
        private System.Windows.Forms.RibbonPanel ribbonPanel6;
        private System.Windows.Forms.RibbonButton btnMaster;
        private System.Windows.Forms.RibbonTab tabMoney;
        private System.Windows.Forms.RibbonPanel ribbonPanel7;
        private System.Windows.Forms.RibbonButton btnMoney;
        private System.Windows.Forms.RibbonPanel ribbonPanel8;
        private System.Windows.Forms.RibbonButton btnEvents;
        private System.Windows.Forms.RibbonTab tabSettings;
        private System.Windows.Forms.RibbonPanel ribbonPanel9;
        private System.Windows.Forms.RibbonButton btnSettings;
        private System.Windows.Forms.RibbonPanel ribbonPanel10;
        private System.Windows.Forms.RibbonButton btnTeachers;
        private System.Windows.Forms.RibbonButton btnContacts;
        private System.Windows.Forms.RibbonTab tabScheduler;
        private System.Windows.Forms.RibbonPanel ribbonPanelDB;
        private System.Windows.Forms.RibbonButton btnDB;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton btnSheduler;

        public static AppSettings AppSettings;
        public static DataBase Db;
        private static PersonsGrid _studentsGrid;
        private static PersonsGrid _teachersGrid;
        private static PersonsGrid _contactsGrid;
        private static GroupsGrid _groupsGrid;
        private static GroupsGrid _privateGrid;
        private static PaymentsGrid _paymentsGrid;
        private static EventsGrid _masterEventGrid;
        private static EventsGrid _eventGrid;

        private BackgroundWorker _backgroundWorker;
        private RibbonPanel TablesSettings;
        private RibbonButton btnGrids;
        private RibbonPanel ribbonPanel11;
        private RibbonButton btnEarnings;
        private RibbonPanel ribbonPanel12;
        private RibbonButton btnExpenses;
        private RibbonPanel ribbonPanel13;
        private RibbonButton btnPaymentReport;

    }
}

