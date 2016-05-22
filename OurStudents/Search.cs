using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using Model.Grids;

namespace OurStudents
{
    public partial class Search : MetroForm
    {
        public MetroGrid Grid;

        public Search()
        {
            InitializeComponent();
        }

        private void searchCancel_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void searchYes_Click(object sender, System.EventArgs e)
        {
            Grid.ClearSelection();

            var selectedList = (selectedColumns.CheckedItems.Cast<ColumnItem>().ToList()).Select(c => c.Index);

            for (int i = 0; i < Grid.RowCount; i++)
            {
                for (int j = 0; j < Grid.ColumnCount; j++)
                    if (Grid.Rows[i].Cells[j].Value != null)
                        if (Grid.Rows[i].Cells[j].Visible &&
                            Grid.Rows[i].Cells[j].Value.ToString().ToLower().Contains(searchTextBox.Text.ToLower()) &&
                            selectedList.Contains(j))
                        {
                            Grid.FirstDisplayedScrollingRowIndex = i;
                            Grid.Rows[i].Selected = true;
                            this.Hide();
                            break;
                        }
            }
            if(Grid.SelectedRows.Count <= 0)
            {
                var confirmResult = MessageBox.Show(
                    "Строка не найдена.", "Поиск",
                    MessageBoxButtons.OK);
                if (confirmResult == DialogResult.OK)
                {
                    this.Hide();
                }
            }
        }

        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchYes.PerformClick();
            }
        }

        public void SetColumnsList()
        {
            selectedColumns.Items.Clear();
            selectedColumns.ValueMember = "Index";
            selectedColumns.DisplayMember = "Name";

            for (var j = 0; j < Grid.ColumnCount; j++)
            {
                if (Grid.Columns[j].Visible)
                    selectedColumns.Items.Add(new ColumnItem {Index = j, Name = Grid.Columns[j].HeaderText},
                                              (Grid.Columns[j] as CustomColumn).IsDefaultForSearch);
            }
        }

        private void selectedColumns_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           
        }
    }

    internal class ColumnItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }
}
