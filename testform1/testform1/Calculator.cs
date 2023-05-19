using System;
using System.Linq;
using System.Windows.Forms;

namespace testform1
{

    public partial class Calculator : Form
    {
        // vars initialization
        bool firstNumFilled = false;
        bool commaPlaced = false;
        double firstNum = 0;
        double secondNum = 0;
        char operation = '0';
        int opTextIndex;

        public Calculator()
        {
            InitializeComponent();
            TB_calc_res.Focus();
        }

        private void B_1_Click(object sender, EventArgs e)
        {
            AddChar('1');
        }
        private void B_2_Click(object sender, EventArgs e)
        {
            AddChar('2');
        }
        private void B_3_Click(object sender, EventArgs e)
        {
            AddChar('3');
        }
        private void B_4_Click(object sender, EventArgs e)
        {
            AddChar('4');
        }
        private void B_5_Click(object sender, EventArgs e)
        {
            AddChar('5');
        }
        private void B_6_Click(object sender, EventArgs e)
        {
            AddChar('6');
        }
        private void B_7_Click(object sender, EventArgs e)
        {
            AddChar('7');
        }
        private void B_8_Click(object sender, EventArgs e)
        {
            AddChar('8');
        }
        private void B_9_Click(object sender, EventArgs e)
        {
            AddChar('9');
        }
        private void B_0_Click(object sender, EventArgs e)
        {
            AddChar('0');
        }

        private void B_clear_Click(object sender, EventArgs e)
        {
            ClearTB();
        }
        private void B_delete_Click(object sender, EventArgs e)
        {
            DeleteChar();
        }
        private void B_sign_Click(object sender, EventArgs e)
        {
            ChangeFirstNumSign();
        }
        private void B_comma_Click(object sender, EventArgs e)
        {
            AddComma();
        }
        private void B_plus_Click(object sender, EventArgs e)
        {
            AddOperation('+');
        }
        private void B_minus_Click(object sender, EventArgs e)
        {
            AddOperation('-');
        }
        private void B_multi_Click(object sender, EventArgs e)
        {
            AddOperation('*');
        }
        private void B_div_Click(object sender, EventArgs e)
        {
            AddOperation('/');
        }
        private void B_res_Click(object sender, EventArgs e)
        {
            GetResult();
        }

        // Result box keyboard input handling
        private void Calculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CalcManager.nums.Contains(e.KeyChar))
            {
                AddChar(e.KeyChar);
            }
            else if (e.KeyChar == '=')
            {
                GetResult();
            }
            else if (e.KeyChar == ',')
            {
                AddComma();
            }
            else if (e.KeyChar == 'c')
            {
                ClearTB();
            }
            else if (e.KeyChar == 'h')
            {
                ChangeHistoryListVisiblity();
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                DeleteChar();
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {

                GetResult();
            }
            else if (CalcManager.operators.Contains(e.KeyChar)) // if keychar is operator...
            {
                AddOperation(e.KeyChar);
            }

        }

        private void Calculator_Shown(object sender, EventArgs e)
        {
            TB_calc_res.Focus();
            historyListBox.Items.Add(CalcManager.histPlaceholder);
        }
        private void TB_calc_res_Leave(object sender, EventArgs e)
        {
            // keeping result box in focus
            TB_calc_res.Focus();
        }

        private void B_history_show_Click(object sender, EventArgs e)
        {
            ChangeHistoryListVisiblity();
        }
        private void B_histListClear_Click(object sender, EventArgs e)
        {
            historyListBox.Items.Clear();
            historyListBox.Items.Add(CalcManager.histPlaceholder);
        }
        private void B_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"Version: {this.ProductVersion},\n" +
                $"Author: PomPon.\n" +
                $"---------\n" +
                $"Shortcuts:\n" +
                $"-> 'c' - clear result box,\n" +
                $"-> 'h' - show history.", "About");
        }

        private void ChangeHistoryListVisiblity()
        {
            if (historyListBox.Visible)
            {
                historyListBox.Visible = false;
                B_histListClear.Visible = false;
            }
            else
            {
                historyListBox.Visible = true;
                if (historyListBox.Items.Count == 0) return;
                if (!historyListBox.Items[0].ToString().Contains("history"))
                    B_histListClear.Visible = true;
            }
        }

        private void GetResult()
        {
            //input error handling
            if (opTextIndex + 1 > TB_calc_res.Text.Length) return;
            if (operation == '0' || TB_calc_res.Text.Substring(opTextIndex + 1) == "") return;

            secondNum = double.Parse(TB_calc_res.Text.Substring(opTextIndex + 1));
            TB_calc_res.Text = CalcManager.Calculate(firstNum, secondNum, operation).ToString();
            AddItemToHistory($"{firstNum} {operation} {secondNum} = " + TB_calc_res.Text);
            firstNumFilled = false;
            operation = '0';
            if (TB_calc_res.Text.Contains(','))
                commaPlaced = true;
        }

        private void AddItemToHistory(string item)
        {
            if (historyListBox.Items[0].ToString().Contains("history"))
                historyListBox.Items.RemoveAt(0);

            historyListBox.Items.Insert(0, item);
        }

        private void AddOperation(char op)
        {

            if (!firstNumFilled && TB_calc_res.Text.Length != 0)
            {
                firstNum = Double.Parse(TB_calc_res.Text);
                TB_calc_res.Text += op;
                opTextIndex = TB_calc_res.Text.Length - 1;
                operation = op;
                firstNumFilled = true;
                commaPlaced = false;
                TB_calc_res.ScrollToCaret();
            }
        }

        private void ClearTB()
        {
            TB_calc_res.Text = null;
            firstNum = 0;
            secondNum = 0;
            firstNumFilled = false;
            TB_calc_res.ScrollToCaret();
        }

        private void DeleteChar()
        {
            if (TB_calc_res.Text.Length != 0)
            {
                if (CalcManager.operators.Contains(TB_calc_res.Text[TB_calc_res.Text.Length - 1]))
                {
                    operation = '0';
                    firstNumFilled = false;
                }
                TB_calc_res.Text = TB_calc_res.Text.Substring(0, TB_calc_res.Text.Length - 1);
                TB_calc_res.ScrollToCaret();
            }
        }

        private void ChangeFirstNumSign()
        {
            if (TB_calc_res.Text == "") return;
            if (TB_calc_res.Text[0] == '-')
            {
                TB_calc_res.Text = TB_calc_res.Text.Substring(1);
            }
            else if (TB_calc_res.Text.Length != 0)
            {
                TB_calc_res.Text = '-' + TB_calc_res.Text;
            }
            if (firstNumFilled) firstNum *= -1;
            TB_calc_res.ScrollToCaret();
        }

        private void AddComma()
        {
            if (TB_calc_res.Text.Length == 0) return;
            if (CalcManager.nums.Contains(TB_calc_res.Text[TB_calc_res.Text.Length - 1]) && !commaPlaced)
            {
                AddChar(',');
                commaPlaced = true;
                TB_calc_res.ScrollToCaret();
            }
        }

        private void AddChar(char num)
        {
            TB_calc_res.Text += num;
            TB_calc_res.ScrollToCaret();
        }
    }
}