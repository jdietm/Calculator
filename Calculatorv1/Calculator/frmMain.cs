namespace Calculator
{
    public partial class frmMain : Form
    {
        private string strNumber = "";
        private string storedOperation = "";
        private float storedOperand = 0;
        private bool lastKeyisOperation = false;
        private bool updateDisplayWithStoredOperand= false;
        private bool validInput = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void txbDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetVariablesToFalse();

            if ( e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4'  || e.KeyChar == '5' ||
                 e.KeyChar == '6' || e.KeyChar == '7' || e.KeyChar == '8' || e.KeyChar == '9' || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' && (strNumber.Length < 1 || strNumber.Contains("."))) // Cheking the correct use of decimal separator (only once and after a digit)
                {
                    validInput = false;
                } else
                {
                    strNumber += e.KeyChar.ToString();
                    validInput = true;
                }
                
            }
            else
            {
                if (e.KeyChar == '+') { PerformOperation("+"); }
                if (e.KeyChar == '-') { PerformOperation("-"); }
                if (e.KeyChar == '*') { PerformOperation("*"); }
                if (e.KeyChar == '/') { PerformOperation("/"); }

                if (e.KeyChar == '=')
                {
                    if (storedOperation != "" && strNumber != "" && storedOperand != 0)
                    {
                        RunOperation();
                        storedOperation = "";
                        lastKeyisOperation = true;
                        updateDisplayWithStoredOperand = true;
                    }
                    validInput = true;
                }
            }
            if(!validInput) { e.Handled = true; }
        }

        private void txbDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateLastInput();
        }

        private void btnAc_Click(object sender, EventArgs e)
        {
            txbDisplay.Text = "";
            strNumber = "";
            storedOperation = "";
            storedOperand = 0;
            lastKeyisOperation = false;
            updateDisplayWithStoredOperand = false;
            validInput = false;
            txbDisplay.Focus();

        }

        private void PerformOperation(string operationType)
        {
            if (storedOperation != "" && strNumber != "" && storedOperand != 0) 
            {
                RunOperation();
                updateDisplayWithStoredOperand = true;
            }
            else
            {
                //if (strNumber != "" && storedOperand == 0) { storedOperand = float.Parse(strNumber); }  This was working but clearly it is not ok
                //if (strNumber != "" && storedOperand != 0) { RunOperation(); }

                if (strNumber != "")
                {
                    if (storedOperand == 0) { storedOperand = float.Parse(strNumber); }
                    else { RunOperation(); }
                }



                updateDisplayWithStoredOperand = false;
            }
            storedOperation = operationType;
            lastKeyisOperation = true;
            validInput = true;
        }

        private void RunOperation()
        {
            if (storedOperation == "+") { storedOperand += float.Parse(strNumber); }
            if (storedOperation == "-") { storedOperand -= float.Parse(strNumber); }
            if (storedOperation == "*") { storedOperand *= float.Parse(strNumber); }
            if (storedOperation == "/") { storedOperand /= float.Parse(strNumber); }
        }

        private void ValidateLastInput()
        {
            if (lastKeyisOperation)
            {
                if (updateDisplayWithStoredOperand)
                {
                    txbDisplay.Text = storedOperand.ToString();
                    strNumber = storedOperand.ToString(); ;
                }
                else
                {
                    txbDisplay.Text = "";
                    strNumber = "";
                }
            }
        }
        private void SetVariablesToFalse()
        {
            lastKeyisOperation = false;
            updateDisplayWithStoredOperand = false;
            validInput = false;
        }

        private void ClickNumbers(string number)
        {
            SetVariablesToFalse();
            strNumber += number;
            txbDisplay.Text += number;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            ClickNumbers("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            ClickNumbers("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            ClickNumbers("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            ClickNumbers("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            ClickNumbers("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            ClickNumbers("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            ClickNumbers("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            ClickNumbers("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            ClickNumbers("9");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            ClickNumbers("0");
        }

        private void btnPoint_Click(object sender, EventArgs e) 
        {
            if (strNumber.Length < 1 || strNumber.Contains(".")) // Cheking the correct use of decimal separator (only once and after a digit)
            {
                validInput = false;
            }
            else
            {
                ClickNumbers(".");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PerformOperation("+");
            ValidateLastInput();

        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            PerformOperation("-");
            ValidateLastInput();
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            PerformOperation("*");
            ValidateLastInput();
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            PerformOperation("/");
            ValidateLastInput();
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (storedOperation != "" && strNumber != "" && storedOperand != 0)
            {
                RunOperation();
                storedOperation = "";
                lastKeyisOperation = true;
                updateDisplayWithStoredOperand = true;
            }
            validInput = true;
            ValidateLastInput();
        }

        private void btnDec_Click(object sender, EventArgs e)
        {
            int onesCount = txbDisplay.Text.Split('1').Length - 1;
            int zeroesCount = txbDisplay.Text.Split('0').Length - 1;
            if ((onesCount + zeroesCount) == txbDisplay.Text.Length) // Check if the number in display is a Binary number
            {
                ConvertBinaryToDecimal();
            } else
            {
                MessageBox.Show("The number in the display panel it is not a Valid Binary number !");
            }

        }

        private void ConvertBinaryToDecimal()
        {
            double number = 0;
            int exponent = 0;
            for (int i = txbDisplay.Text.Length-1; i >= 0 ; i--)
            {
                number += int.Parse(txbDisplay.Text[i].ToString()) * Math.Pow(2,exponent);
                exponent++;
            }
            txbDisplay.Text = number.ToString();
        }

        private void btnBin_Click(object sender, EventArgs e)
        {
            bool invalidDecimalNumber = false;
            if (txbDisplay.Text.Length == 0)
            {
                invalidDecimalNumber = true;    

            } else
            {
                if (txbDisplay.Text.Contains('.'))
                {
                    invalidDecimalNumber = true;
                }
            }
            
            if (invalidDecimalNumber)
            {
                MessageBox.Show("The number in the display panel it is not a Valid Decimal number !");
            }
            else
            {
                ConvertDecimalToBinary();
            }
        }

        private void ConvertDecimalToBinary()
        {
            bool baseFit = false;
            double decNumber = float.Parse(txbDisplay.Text.ToString());
            double baseValue = float.Parse(txbDisplay.Text.ToString());
            double maxExponent = 0;
            while (!baseFit)     // Seek until find the max Exponent on base 2 that cointains the number
            {
                maxExponent = Math.Log(baseValue, 2);
                if (maxExponent % 1 == 0)
                {
                    baseFit = true;
                } else
                {
                    baseValue--;
                }
            }
            string strBinaryNumber = "";
            double  reminder = 0;
            double numberBase2 = Math.Pow(2, maxExponent);
            strBinaryNumber += "1";
            reminder = decNumber - numberBase2;
            maxExponent--;
            while (maxExponent >= 0)
            {
                numberBase2 = Math.Pow(2, maxExponent);
                if (numberBase2 > reminder)
                {
                    strBinaryNumber += "0";
                }
                if (numberBase2 <= reminder)
                {
                    strBinaryNumber += "1";
                    reminder -=  numberBase2;
                }
                maxExponent--;
            }
            txbDisplay.Text = strBinaryNumber;
            Console.WriteLine();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            /*  ASCII Code for alpahabet a..z = 97..122 */


            ConvertDecimalToBinary();
            char[] arrDisplayInverse = txbDisplay.Text.ToCharArray();
            Array.Reverse(arrDisplayInverse);    // Reverse the Binary number to process it from 2^0 to 2^n, from left to right
            string strDisplayInverse = new string(arrDisplayInverse);
            txbDisplay.Text = strDisplayInverse;
            
            string strLocNumber = "";
            for (int i = 0; i < txbDisplay.Text.Length; i++)  // Convert to LOC
            {
                if (txbDisplay.Text[i] == '1')
                    strLocNumber += (char)(i+97); /*  Since ASCII Code for alpahabet is a..z = 97..122 , then we can use 'a' as an offset*/
            }
            txbDisplay.Text = strLocNumber;
        }
    }
}

