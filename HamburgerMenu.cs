using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_calculator
{
    public partial class HamburgerMenu : UserControl
    {
        private Color lineColor = Color.White;
        private Color hoverColor = Color.LightGray;

        public event EventHandler Clicked;

        public HamburgerMenu()
        {
            InitializeComponent();

            /*
            this.Width = 45;
            this.Height = 45;
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.Transparent;

            this.MouseEnter += (s, e) =>
            {
                lineColor = hoverColor;
                this.Invalidate();
            };

            this.MouseLeave += (s, e) =>
            {
                lineColor = Color.White;
                this.Invalidate();
            };

            this.Click += (s, e) => Clicked?.Invoke(this, EventArgs.Empty);*/
        }
        
        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new Pen(lineColor, 3))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int x1 = 5;
                int x2 = this.Width - 5;

                e.Graphics.DrawLine(pen, x1, 10, x2, 10);
                e.Graphics.DrawLine(pen, x1, 20, x2, 20);
                e.Graphics.DrawLine(pen, x1, 30, x2, 30);
            }
        }*/
    }
}
