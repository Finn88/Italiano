using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Model.Tables;
using OurStudents;

namespace Model.Grids
{
    public class CustomColumn : DataGridViewTextBoxColumn
    {
        public bool IsDefaultForSearch { get; set; }
    }

    public abstract class Grid<T> : MetroGrid
        where T : IEntity
    {
        protected DataBase Db;
        public EditForm<T> EditForm;
        public string DetailsId; //secondary id
        public Grid<PaymentEntity> PaymentDetailsGrid;
        public Grid<PersonEntity> PersonsDetailsGrid;
        public TabControl MainTabControl;
        public TabPage MainPlaceholder;
        public TabPage SecondaryPlaceholder;
        public bool IsDetailsGrid;
        protected ContextMenu Menu;
        protected LoadingPanel LoadingPanel;
        protected int SelectedRowIndex;
        public MetroForm MainForm;
        protected int GridId;

        protected Grid(DataBase db)
        {
            Top = 0;
            Left = 25;
            Padding = new Padding(0, 0, 10, 0);
            Db = db;
            MultiSelect = false;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            AutoGenerateColumns = false;
            AllowUserToAddRows = false;
            MouseDown += ClickMouseDown;
            MouseDoubleClick += DoubleClickMouseDown;
            ColumnHeaderMouseClick += SortEvent;
            ColumnWidthChanged += ColumnSizeChanged;
            ReadOnly = true;
            Style = MetroColorStyle.Green;
            Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DataBindingComplete += (sender, args) => SetRowNumber(sender);
            RowHeadersWidth = 60;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void ColumnSizeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            var column = Db.GridsSettings.FirstOrDefault(c => c.ColumnName == e.Column.Name && c.GridId == GridId);
            if(column== null) return;
            column.Width = e.Column.Width;
            Db.SubmitChanges();
        }

        private void SetRowNumber(object sender)
        {
            var gridView = sender as DataGridView;
            if (null != gridView)
            {
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                }
            }
        }


        private void SortEvent(object sender, DataGridViewCellMouseEventArgs e)
        {
            var columnIndex = e.ColumnIndex;
            var currColumn = Columns[columnIndex];
            if (Columns[currColumn.Name] != null)
                Sort(Columns[currColumn.Name],
                    (SortOrder == SortOrder.Ascending) || (currColumn != SortedColumn)
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending);
        }

        protected abstract void UpdateClick(object sender, EventArgs e, string id);

        protected abstract void Delete(string id);

        protected void DeleteClick(object sender, EventArgs e, string id)
        {
            var confirmResult = MessageBox.Show("Вы хотите удалить запись?", "Внимание", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Delete(id);
            }
        }

        protected void RefreshClick(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void DoubleClickMouseDown(object sender, MouseEventArgs e)
        {
            var grid = (DataGridView)sender;
            int rowIndex = grid.HitTest(e.X, e.Y).RowIndex;
            SelectedRowIndex = rowIndex;
            if (rowIndex == -1)
            {
                return;
            }
            grid.ClearSelection();
            grid.Rows[rowIndex].Selected = true;

            var isSelected = grid.SelectedRows.Count > 0;
            if (isSelected)
            {
                var row = grid.SelectedRows[0];
                string id = (row.Cells["Id"].Value).ToString();
                UpdateClick(null, null, id);
            }
        }

        protected virtual void ClickMouseDown(object sender, MouseEventArgs e)
        {
            var grid = (DataGridView) sender;
            int rowIndex = grid.HitTest(e.X, e.Y).RowIndex;
            SelectedRowIndex = rowIndex;
            if (rowIndex == -1)
                grid.ClearSelection();
            else
                grid.Rows[rowIndex].Selected = true;

            if (e.Button == MouseButtons.Right)
            {
                string id = "";
                var isSelected = grid.SelectedRows.Count > 0;
                if (isSelected)
                {
                    var row = grid.SelectedRows[0];
                    id = row.Cells["Id"].Value.ToString();
                }

                Menu = new ContextMenu();
                var menuNew = new MenuItem
                {
                    Text = "Новая запись"
                };
                menuNew.Click += NewClick;

                var menuEdit = new MenuItem
                {
                    Text = "Редактировать запись",
                    Enabled = isSelected
                };
                menuEdit.Click += (o, args) => UpdateClick(o, args, id);

                var menuDelete = new MenuItem
                {
                    Text = "Удалить запись",
                    Enabled = isSelected
                };
                menuDelete.Click += (o, args) => DeleteClick(o, args, id);

                var menuRefresh = new MenuItem
                {
                    Text = "Обновить список"
                };
                menuRefresh.Click += RefreshClick;

                var menuSearch = new MenuItem
                {
                    Text = "Поиск"
                };
                menuSearch.Click += (o, args) => MainForm.GetType().GetMethod("SearchInGrid").Invoke(MainForm, null);

                Menu.MenuItems.Add(menuNew);
                Menu.MenuItems.Add(menuEdit);
                Menu.MenuItems.Add(menuDelete);
                Menu.MenuItems.Add(menuRefresh);
                Menu.MenuItems.Add(menuSearch);

                AdditionalMenuItems(isSelected, id);

                ContextMenu = Menu;
                ContextMenu.Show(this, e.Location);
            }
        }

        protected virtual void AdditionalMenuItems(bool isRowSelected, string id)
        {
        }

        protected void NewClick(object sender, EventArgs e)
        {
            EditForm.CleanFields();
            var dialog = EditForm;
            dialog.EditId = "";
            dialog.DetailsId = DetailsId;
            dialog.EditGrid = this;
            dialog.CleanFields();
            dialog.ShowDialog();
        }

        public virtual void RefreshGrid()
        {
            DataSource = GetDataSource();
            Update();
            Refresh();
        }

        protected abstract BindingListView<T> GetDataSource();

    }
}
