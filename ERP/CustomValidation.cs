using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP
{
    public class CustomValidation
    {
        public static String checkAlphabetInput(string value)
        {
            char character = value[value.Length - 1];
            string result = "";
            if ((character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z')) result = value;
            else if (character == ' ') result = value;
            else
            {
                MessageBox.Show("! Only alphabetical characters allow. ");
                result = value.Remove(value.Length - 1);
            }
            return result;
            // for (char letter = 'A'; letter <= 'Z'; letter++)
            //{

            //}


            /* 
               for (char letter = 'a'; letter <= 'z'; letter++)
               {
                   if (character == letter)
                   {
                       MessageBox.Show("" + letter);
                       result = value.Remove(value.Length - 1);
                   }
                   else result = value;
               }
           */

            /*
                char last = this.textBox10.Text[this.textBox10.Text.Length - 1];
                char[] number = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                
                for (char letter = 'A'; letter <= 'Z'; letter++)
                {
                    MessageBox.Show("" + letter);
                }

                for (int i = 0; i < 9; i++) {
                    if(last == Convert.ToChar(number[i]))
                    {
                        MessageBox.Show("Only alphabetical characters allow");
                        textBox10.Text.Remove(textBox10.Text.Length - 1);
                    }
                }
             */
        }
    }
}
