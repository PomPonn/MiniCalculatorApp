using System;

namespace testform1
{
    static internal class CalcManager
    {
        static public string histPlaceholder = "There is no history yet...";
        static public char[] nums =
        {
            '0', '1', '2', '3', '4',
            '5', '6', '7', '8', '9'
        };
        static public char[] operators =
        {
            '+', '-', '*', '/', '='
        };

        static public double Calculate(double firstNum, double secondNum, char operation)
        {
            double res = 0;

            switch (operation)
            {
                case '+':
                    res = firstNum + secondNum;
                    break;
                case '-':
                    res = firstNum - secondNum;
                    break;
                case '*':
                    res = firstNum * secondNum;
                    break;
                case '/':
                    res = firstNum / secondNum;
                    break;
            }

            return res;
        }
    }
}
