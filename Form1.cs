using System;
using System.Windows.Forms;

namespace Simple_calculator
{
    public partial class CALCULATOR : Form
    {
        public string op = string.Empty;
        public string res = string.Empty;
        public CALCULATOR()
        {
            InitializeComponent();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            // butonul reprezinta tasta calculatorului pe care ai apasat
            Button button = (Button)sender;

            if ((button.Text[0] >= 48 && button.Text[0] <= 57) || button.Text == ".")
            {
                if (screen.Text == "0") screen.Text = button.Text;
                else screen.Text += button.Text;
            }

            if (button.Text == "C") screen.Text = "0";
            if (button.Text == "<=")
            {
                if (screen.Text == "0") screen.Text = "0";
                if (screen.Text.Length > 1) screen.Text = screen.Text.Substring(0, screen.Text.Length - 1);
                else screen.Text = "0";
            }

            if(button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/" || button.Text == "%" || button.Text == "^")
            {
                if (!string.IsNullOrEmpty(res))
                {
                    Calculator.SetLeft(res);
                    panel.Text = screen.Text + button.Text;

                    panel.Visible = true;
                    screen.Text = "0";
                }
                else
                {
                    Calculator.SetLeft(screen.Text);
                    panel.Text = screen.Text + button.Text;

                    panel.Visible = true;
                    screen.Text = "0";
                }

                op = button.Text;
            }

            if(button.Text == "=")
            {
                Calculator.SetRight(screen.Text);
                res = Calculator.GetResult(op);
                panel.Visible = false;

                panel.Text = "0";
                screen.Text = res;
            }
        }
    }
}
