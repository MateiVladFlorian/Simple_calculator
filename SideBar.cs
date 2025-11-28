using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;

namespace Simple_calculator
{
    public partial class SideBar : UserControl
    {
        private PictureBox iconPicture;
        private Panel sidebarPanel;
        private HamburgerMenu hamburger;

        public event EventHandler MenuClicked;

        public Image Icon
        {
            get => iconPicture.Image;
            set => iconPicture.Image = value;
        }

        public SideBar()
        {
            InitializeComponent();
            //InitializeSidebar();
        }

        private void InitializeSidebar()
        {
            this.Width = 100;
            this.BackColor = Color.Transparent;

            // PANEL LATERAL
            sidebarPanel = new Panel();
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Width = 70;
            sidebarPanel.BackColor = Color.FromArgb(24, 29, 43);
            this.Controls.Add(sidebarPanel);

            // ICON SUS
            iconPicture = new PictureBox();
            iconPicture.Size = new Size(40, 40);
            iconPicture.SizeMode = PictureBoxSizeMode.Zoom;
            iconPicture.Location = new Point((sidebarPanel.Width - 40) / 2, 20);
            sidebarPanel.Controls.Add(iconPicture);

            // BUTON HAMBURGER
            hamburger = new HamburgerMenu();
            hamburger.Location = new Point((sidebarPanel.Width - 40) / 2, 80);
            hamburger.Clicked += (s, e) => MenuClicked?.Invoke(this, EventArgs.Empty);
            sidebarPanel.Controls.Add(hamburger);
        }
    }
}
