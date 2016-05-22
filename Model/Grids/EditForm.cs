using System;
using System.Windows.Forms;


namespace Model.Grids
{
    public abstract class EditForm<T> : Form
        where T : IEntity
    {
        public string EditId;
        public string DetailsId;
        public Grid<T> EditGrid;
        public AppSettings AppSettings;

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        protected abstract void btnSave_Click(object sender, EventArgs e);

        public abstract void CleanFields();
    }
}
