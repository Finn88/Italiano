using MetroFramework.Forms;

namespace OurStudents
{
    public partial class LoadingPanel : MetroForm
    {
        public LoadingPanel()
        {
            InitializeComponent();

        }

        public void UpdateLoadingText(string loadText = "Пожалуйста подождите...")
        {
            LoadingText.Text = loadText;
        }
    }
}
