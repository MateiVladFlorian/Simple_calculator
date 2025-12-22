using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace Simple_calculator
{
    public partial class CALCULATOR : Form
    {
        private enum OperationType { Binary, Unary, None }
        private string _currentOperation = string.Empty;
        private string _previousResult = string.Empty;

        private OperationType _currentOperationType = OperationType.None;
        private bool _newInputRequired = false;
        private bool _operationPerformed = false;
        private int _click = 0;

        /* mapping for display formatting */
        private static readonly Dictionary<string, string> _operationDisplayMap = new Dictionary<string, string>
        {
            ["1/x"] = "¹/ₓ",
            ["x²"] = "x²",
            ["√x"] = "√x",
            ["+"] = "+",
            ["-"] = "-",
            ["*"] = "×",
            ["/"] = "÷",
            ["%"] = "%",
            ["^"] = "^"
        };

        public CALCULATOR()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += Form1_KeyPress;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch (button.Tag?.ToString())
            {
                case "Digit":
                    HandleDigitInput(button.Text);
                    break;

                case "Decimal":
                    HandleDecimalInput();
                    break;

                case "Clear":
                    HandleClearInput(button.Text);
                    break;

                case "Backspace":
                    HandleBackspace();
                    break;

                case "BinaryOperation":
                    HandleBinaryOperation(button.Text);
                    break;

                case "UnaryOperation":
                    HandleUnaryOperation(button.Text);
                    break;

                case "Equals":
                    HandleEquals();
                    break;

                default:
                    /* legacy support for buttons without tags */
        HandleButtonByText(button.Text);
                    break;
            }
        }

        private void HandleDigitInput(string digit)
        {
            if (_newInputRequired || screen.Text == "0" || _operationPerformed)
            {
                screen.Text = digit;
                _newInputRequired = false;
                _operationPerformed = false;
            }
            else
            {
                /* prevent overflow */
                if (screen.Text.Length < 15)
                    screen.Text += digit;
            }
        }

        private void HandleDecimalInput()
        {
            if (_newInputRequired || _operationPerformed)
            {
                screen.Text = "0.";
                _newInputRequired = false;
                _operationPerformed = false;
            }
            else if (!screen.Text.Contains("."))
                screen.Text += ".";
        }

        private void HandleClearInput(string clearType)
        {
            if (clearType == "CE")
            {
                screen.Text = "0";
                _newInputRequired = false;
            }
            else
            {
                screen.Text = "0";
                panel.Text = "";
                panel.Visible = false;
                _currentOperation = string.Empty;
                _previousResult = string.Empty;
                _currentOperationType = OperationType.None;
                _newInputRequired = false;
                _operationPerformed = false;
                Calculator.SetLeft("0");
                Calculator.SetRight("0");
            }
        }

        private void HandleBackspace()
        {
            if (screen.Text.Length > 1)
                screen.Text = screen.Text.Substring(0, screen.Text.Length - 1);
            else
                screen.Text = "0";
        }

        private void HandleBinaryOperation(string operation)
        {
            try
            {
                /* if we already have an operation pending, calculate it first */
                if (!string.IsNullOrEmpty(_currentOperation) && _currentOperationType == OperationType.Binary && !_newInputRequired)
                    PerformBinaryCalculation();

                /* store current value and operation */
                Calculator.SetLeft(screen.Text);
                _currentOperation = operation;
                _currentOperationType = OperationType.Binary;

                /* update display */
                panel.Text = $"{screen.Text} {_operationDisplayMap[operation]}";
                panel.Visible = true;

                /* prepare for next input */
                _newInputRequired = true;
                _operationPerformed = false;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void HandleUnaryOperation(string operation)
        {
            try
            {
                double currentValue;
                if (!double.TryParse(screen.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out currentValue))
                {
                    throw new ArgumentException("Invalid number format");
                }

                /* store original value for display */
                string originalValue = screen.Text;

                /* perform unary operation immediately */
                string result = Calculator.GetResult(operation, currentValue);

                /* update display */
                panel.Text = $"{_operationDisplayMap[operation]}({originalValue})";
                panel.Visible = true;

                screen.Text = result;
                _previousResult = result;
                _operationPerformed = true;

                /* auto-hide panel after delay */
                Timer autoHideTimer = new Timer();
                autoHideTimer.Interval = 1500;

                autoHideTimer.Tick += (s, args) =>
                {
                    panel.Visible = false;
                    autoHideTimer.Stop();
                    autoHideTimer.Dispose();
                };

                autoHideTimer.Start();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void HandleEquals()
        {
            try
            {
                if (_currentOperationType == OperationType.Binary && !string.IsNullOrEmpty(_currentOperation))
                {
                    PerformBinaryCalculation();
                    _currentOperation = string.Empty;
                    _currentOperationType = OperationType.None;
                }
                else if (_operationPerformed)
                    screen.Text = _previousResult;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void PerformBinaryCalculation()
        {
            Calculator.SetRight(screen.Text);
            string result = Calculator.GetResult(_currentOperation);

            /* update display */
            panel.Text = $"{panel.Text} {screen.Text} =";
            screen.Text = result;
            _previousResult = result;

            /* prepare for next operation */
            Calculator.SetLeft(result);
            _newInputRequired = true;
            _operationPerformed = true;
        }

        private void HandleButtonByText(string buttonText)
        {
            /* legacy handling for buttons without tags */
            if ((buttonText[0] >= '0' && buttonText[0] <= '9') || buttonText == ".")
                HandleDigitInput(buttonText);
            else if (buttonText == "C" || buttonText == "CE")
                HandleClearInput(buttonText);
            else if (buttonText == "X")
                HandleBackspace();
            else if (buttonText == "+" || buttonText == "-" || buttonText == "*" ||
                     buttonText == "/" || buttonText == "%" || buttonText == "^")
                HandleBinaryOperation(buttonText);
            else if (buttonText == "1/x" || buttonText == "x²" || buttonText == "√x")
                HandleUnaryOperation(buttonText);
            else if (buttonText == "=")
                HandleEquals();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Calculation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            screen.Text = "0";
            panel.Visible = false;
            _currentOperation = string.Empty;
            _currentOperationType = OperationType.None;
            _newInputRequired = false;
        }

        private void screen_TextChanged(object sender, EventArgs e)
        {
            /* allow only valid numeric input */
            if (System.Text.RegularExpressions.Regex.IsMatch(screen.Text, "[^0-9\\.\\-]"))
            {
                int pos = screen.SelectionStart - 1;
                screen.Text = System.Text.RegularExpressions.Regex.Replace(screen.Text, "[^0-9\\.\\-]", "");
                screen.SelectionStart = Math.Max(pos, 0);
            }

            /* ensure proper negative number handling */
            if (screen.Text.StartsWith("-") && screen.Text.Length > 1 && screen.Text[1] == '-')
            {
                screen.Text = screen.Text.Substring(1);
            }

            /* update UI state */
            UpdateUIState();
        }

        private void UpdateUIState()
        {
            /* update button states based on current input */
            bool hasDecimal = screen.Text.Contains(".");
        }

        private void screen_KeyDown(object sender, KeyEventArgs e)
        {
            /* handle keyboard shortcuts, efficiently */
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsText())
                {
                    string clipboardText = Clipboard.GetText();

                    if (double.TryParse(clipboardText, out _))
                        screen.Text = clipboardText;
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HandleClearInput("C");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                HandleBackspace();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                HandleEquals();
                e.Handled = true;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (e.Handled) return;

            if (char.IsDigit(key))
            {
                HandleDigitInput(key.ToString());
                e.Handled = true;
            }
            else if (key == '.')
            {
                HandleDecimalInput();
                e.Handled = true;
            }
            else if (key == '+' || key == '-' || key == '*' || key == '/' || key == '%' || key == '^')
            {
                HandleBinaryOperation(key.ToString());
                e.Handled = true;
            }
            else if (key == 'r' || key == 'R')
            {
                HandleUnaryOperation("1/x");
                e.Handled = true;
            }
            else if (key == 's' || key == 'S')
            {
                HandleUnaryOperation("x²");
                e.Handled = true;
            }
            else if (key == 'q' || key == 'Q')
            {
                HandleUnaryOperation("√x");
                e.Handled = true;
            }
            else if (key == '=' || key == (char)Keys.Enter)
            {
                HandleEquals();
                e.Handled = true;
            }
            else if (key == (char)Keys.Escape || key == 'c' || key == 'C')
            {
                HandleClearInput("C");
                e.Handled = true;
            }
            else if (key == (char)Keys.Back)
            {
                HandleBackspace();
                e.Handled = true;
            }
        }

        private void sideBar1_Load(object sender, EventArgs e)
        {
            // Sidebar initialization if needed
        }

        private void hamburgerMenu_MouseClick_1(object sender, MouseEventArgs e)
        {
            _click++;
            sideBar1.Visible = (_click % 2 == 1);
        }

        private bool IsBinaryOperation(string operation)
        {
            return operation == "+" || operation == "-" || 
                operation == "*" || operation == "/" || 
                operation == "%" || operation == "^";
        }

        private bool IsUnaryOperation(string operation)
        {
            return operation == "1/x" || 
                operation == "x²" || 
                operation == "√x";
        }

        private void ValidateCurrentInput()
        {
            if (string.IsNullOrEmpty(screen.Text))
                screen.Text = "0";
        }
    }
}