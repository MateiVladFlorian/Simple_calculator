using System;
using System.Windows.Forms;
using System.Threading;

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
                if (screen.Text == "0")
                {
                    screen.Text = "";
                    screen.Text += button.Text;
                }
                else
                    screen.Text += button.Text;
            }

            if (button.Text == "C")
            {
                res = "";
                screen.Text = "0";
                panel.Text = "0";
            }

            if (button.Text == "X")
            {
                if (screen.Text == "0")
                    screen.Text = "0";

                if (screen.Text.Length > 1)
                {
                    screen.Text = screen.Text.Substring(0, screen.Text.Length - 1);
                    res = screen.Text;
                }
                else
                {
                    screen.Text = "0";
                    res = "";
                }
            }

            if (button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/" || button.Text == "%" || button.Text == "^")
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

            if (button.Text == "=")
            {
                Calculator.SetRight(screen.Text);
                res = Calculator.GetResult(op);

                panel.Visible = false;
                screen.Text = res;
            }
        }

        private void screen_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(screen.Text, "[^0-9\\.]"))
            {
                int pos = screen.SelectionStart - 1;
                screen.Text = System.Text.RegularExpressions.Regex.Replace(screen.Text, "[^0-9\\.]", "");
                screen.SelectionStart = Math.Max(pos, 0);
            }
        }

        private void screen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsText())
                    screen.Text = Clipboard.GetText();
            }
        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char digit = e.KeyChar;

            if ((digit >= 48 && digit <= 57) || digit == '.')
            {
                if (screen.Text == "0")
                {
                    screen.Text = "";
                    screen.Text += digit;
                }
                else
                    screen.Text += digit;
            }

            if (digit == 'C')
            {
                res = "";
                screen.Text = "0";
                panel.Text = "0";
            }

            if (digit == 'X')
            {
                if (screen.Text == "0")
                    screen.Text = "0";

                if (screen.Text.Length > 1)
                {
                    screen.Text = screen.Text.Substring(0, screen.Text.Length - 1);
                    res = screen.Text;
                }
                else
                {
                    screen.Text = "0";
                    res = "";
                }
            }

            if (digit == '+' || digit == '-' || digit == '*' ||
                digit == '/' || digit == '%' || digit == '^')
            {
                if (!string.IsNullOrEmpty(res))
                {
                    Calculator.SetLeft(res);
                    panel.Text = screen.Text + digit;

                    panel.Visible = true;
                    screen.Text = "0";
                }
                else
                {
                    Calculator.SetLeft(screen.Text);
                    panel.Text = screen.Text + digit;

                    panel.Visible = true;
                    screen.Text = "0";
                }

                op = $"{digit}";
            }

            if (digit == '=')
            {
                Calculator.SetRight(screen.Text);
                res = Calculator.GetResult(op);

                panel.Visible = false;
                screen.Text = res;
            }
        }
    }      
}
