using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProjectorControl
{
    public partial class ControlForm : Form
    {
        private bool hidden = true;

        private ProjectionForm pForm;

        public ControlForm()
        {
            InitializeComponent();
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            pForm = new ProjectionForm();
            pForm.SetFormLocation();
            pForm.Show();
        }

        private void txtDirectory_Click(object sender, EventArgs e)
        {
            using (var ofd = new FolderBrowserDialog())
            {
                ofd.SelectedPath = @"C:\Users\Game\Masterarbeit\Bilder";
                ofd.ShowNewFolderButton = false;
                ofd.Description = "Select picture directory";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtDirectory.Text = ofd.SelectedPath;
                    DirectoryInfo dirInfo = new DirectoryInfo(ofd.SelectedPath);
                    lstFiles.Items.Clear();
                    foreach (var file in dirInfo.GetFiles("*.png"))
                    {
                        var item = new ListViewItem(new[] { file.Name }) { Tag = file.FullName };
                        lstFiles.Items.Add(item);
                    }
                }
            }
        }

        private void LstFiles_Click(object sender, EventArgs e)
        {
            var selectedItem = lstFiles.SelectedItems[0];
            var filePath = selectedItem.Tag as string;

            if (picBox.Image != null)
                picBox.Image.Dispose();

            picBox.Image = Image.FromFile(filePath);
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Status: Hidden";
            lstFiles.Enabled = true;
            hidden = !hidden;
            btnShow.Enabled = true;
            btnHide.Enabled = false;
            txtDirectory.Enabled = true;
            pForm.BackgroundImage = null;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Status: Visible";
            lstFiles.Enabled = false;
            hidden = !hidden;
            btnShow.Enabled = false;
            btnHide.Enabled = true;
            txtDirectory.Enabled = false;
            pForm.BackgroundImage = picBox.Image;
        }
    }
}
