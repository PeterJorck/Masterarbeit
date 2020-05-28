using System.Windows.Forms;

namespace ProjectorControl
{
    public partial class ProjectionForm : Form
    {
        public ProjectionForm()
        {
            InitializeComponent();
        }

        public void SetFormLocation()
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (!screen.Primary)
                {
                    this.SetBounds(screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height);
                }
            }
        }
    }
}
