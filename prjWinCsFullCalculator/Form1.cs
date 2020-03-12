﻿using System;
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
        string mainStr="",currentOperation,currentSign="", oppHistory="";
        Int32 counterOfDot;
        Single leftNbr=0,rightNbr=0;
        string decPoint;

        int mov;
        int movX;
        int movY;
        
        

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show(btnSq.Tag.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //THIS WILL MAKE FORM LOAD ON CURRENT WORKING MONITOR
            this.Location = Screen.AllScreens[0].WorkingArea.Location;
            btnDot.Tag = btnDot.Text = decPoint = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
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

        private void mathOperations(Int16 xerror, string anyButton)
        {


            switch (currentSign)
            {
                case "+":
                    rightNbr = rightNbr + leftNbr;
                    break;
                case "-":
                    rightNbr = rightNbr - leftNbr;
                    break;
                case "x":
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
                default:
                    rightNbr = leftNbr;
                    break;
            }


        }

//-------------------------------------------goooooooooooooooddddd-----------------------------------------------
     
        private void arithmeticBtn(String sign)
        {
            Int16 xerror = 0;
            counterOfDot = 0;
            string temp = mainStr;

         
            BasicOperators(xerror, sign);
   
            currentSign = sign;

            currentOperation = currentOperation + temp + currentSign;
            lblCurrentOpr.Text = currentOperation;


        }



        private void BasicOperators(Int16 xerror, string sign)
        {


            if (mainStr != "")
            {
                storeNumber();
                mathOperations(xerror, sign);

                lblScreen.Text = (xerror == 0) ? rightNbr.ToString() : "ERROR";

            }
            else
            {
                if(currentOperation != "")
                {
                    if (currentOperation.Length != 0)
                    {
                        currentOperation = currentOperation.Substring(0, currentOperation.Length - 1);
                    }
                    else
                    {
                        currentOperation = currentOperation + rightNbr;
                    }
                }
                
            }



        }



     





        private void btnSq_Click(object sender, EventArgs e)
        {
            string sign = (sender as Button).Tag.ToString();

            storeNumber();
            if(sign == "√")
            {
                leftNbr = Convert.ToSingle(Math.Sqrt(leftNbr));
            }
            else
            {
                leftNbr = leftNbr * leftNbr;
            }
            

            lblScreen.Text = leftNbr.ToString();
            
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            mathOperations(0, (sender as Button).Tag.ToString());
            MessageBox.Show(rightNbr.ToString());
            lblScreen.Text = rightNbr.ToString();

            oppHistory = oppHistory + "\n" + currentOperation + rightNbr.ToString();
            lblHistory.Text = oppHistory;
            currentOperation = "";
        }











        //------------------------ number BUTTONS---------------------------
        private void btnNumbers_Click(object sender, EventArgs e)
        {
            concatenateString((sender as Button).Tag.ToString());
        }

        //------------------------ arithmetic BUTTONS-----------------------

        private void btnOperation_Click(object sender, EventArgs e )
        {
            arithmeticBtn((sender as Button).Tag.ToString());
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            if(mainStr != "")
            {
                mainStr = mainStr.Substring(0, mainStr.Length - 1);
                lblScreen.Text = mainStr;
            }
            else
            {
                lblCurrentOpr.Text = "";
            }
            
        }

        private void clrAll_Click(object sender, EventArgs e)
        {
            mainStr = "";
            currentOperation = "";
            currentSign = "";
            leftNbr = 0;
            rightNbr = 0;
            lblScreen.Text = "0";
            lblCurrentOpr.Text = "";
        }

        private void clrNumber_Click(object sender, EventArgs e)
        {
            mainStr = "";
            leftNbr = 0;
            lblScreen.Text = "0";
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
