using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjWinCsFullCalculator
{
    public partial class Form1 : Form
    {
        string mainStr,currentOperation,currentSign="", oppHistory;
        Int32 counterOfDot;
        Single leftNbr=0,rightNbr=0;
        string decPoint;

        int mov;
        int movX;
        int movY;
        
        

        public Form1()
        {
            InitializeComponent();
            
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //THIS WILL MAKE FORM LOAD ON CURRENT WORKING MONITOR
            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            btnDot.Text = decPoint = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        }


        //CONCATENATED STRING 
        private void concatenateString(string anyString)
        {
            if (anyString == decPoint && mainStr == "")
            {
                counterOfDot++;
                if(counterOfDot <= 1)
                {
                    mainStr = "0" + anyString;
                    lblScreen.Text = mainStr;
                }
            }else if (anyString == decPoint )
            {
                counterOfDot++;
                if (counterOfDot <= 1)
                {
                    mainStr = mainStr + anyString;
                    lblScreen.Text = mainStr;
                }
            }
            else
            {
                mainStr = mainStr + anyString;
                lblScreen.Text = mainStr;
            }

        }


        //STORE THE NUMBER FROM mainString
        private void storeNumber()
        {
            leftNbr = 0;
            
            if (mainStr != "")
            {   
                leftNbr = Convert.ToSingle(mainStr);
                mainStr = "";
            }
        }

        private void mathOperations(Int16 xerror, Button anyButton)
        {

            if (anyButton.Text == "sqrt")
            {

                currentSign = anyButton.Text;
            }

            switch (currentSign)
            {
                case "+":
                    rightNbr = rightNbr + leftNbr;
                    break;
                case "-":
                    rightNbr = rightNbr - leftNbr;
                    break;
                case "X":
                    rightNbr = rightNbr * leftNbr;
                    break;
                case "/":
                    if (leftNbr != 0) 
                    { 
                        rightNbr = rightNbr / leftNbr; 
                    }
                    else 
                    { 
                        xerror = 1; 
                    }
                    break;
                case "sqrt":
                    if(leftNbr >= 0)
                    { 
                        rightNbr = Convert.ToSingle(Math.Sqrt(leftNbr));
                       // MessageBox.Show("sqrt case");
                    }
                    else
                    {
                        xerror = 0;
                    }
                    
                    break;
                default:
                    rightNbr = leftNbr;
                    break;
            }
        }


     
        private void arithmeticBtn(Button anyButton)
        {
            Int16 xerror = 0;
            string temp = mainStr;
            counterOfDot = 0;
            
            

            if (mainStr != "") {
                
                storeNumber();
                mathOperations(xerror,anyButton);
                
                lblScreen.Text = (xerror == 0) ? rightNbr.ToString() : "ERROR";

            }
            else
            {
                Int32 strLng = currentOperation.Length;
                // MessageBox.Show(currentOperation);
                if(currentOperation.Length != 0) { 
                currentOperation = currentOperation.Substring(0, strLng - 1);
                currentOperation.Trim();
                }
                else
                {
                    currentOperation = currentOperation + rightNbr; 
                }
            }


            currentSign = anyButton.Text;

            currentOperation = currentOperation +  temp +  currentSign;
            lblCurrentOpr.Text = currentOperation;

            if (currentSign == "=")
            {
                oppHistory = oppHistory + "\n" + currentOperation + rightNbr.ToString();
                lblHistory.Text = oppHistory;
                currentOperation = "";         
            }


        }



















        //------------------------ number BUTTONS---------------------------
        private void btnNumbers_Click(object sender, EventArgs e)
        {
            concatenateString((sender as Button).Text);
        }

        //------------------------ arithmetic BUTTONS-----------------------

        private void btnOperation_Click(object sender, EventArgs e)
        {
            arithmeticBtn(sender as Button);
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            mainStr = "";
            leftNbr = 0;
            lblScreen.Text = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainStr = "";
            currentOperation = "";
            currentSign = "";
            leftNbr = 0;
            rightNbr = 0;
            lblScreen.Text = "0";
            lblCurrentOpr.Text = "";
        }



























        //----------------close the program----------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //--------------------------window drag--------------------------------

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if(mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);

            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;

        }

    }
}
