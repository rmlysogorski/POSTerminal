using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POSTerminal
{
    class UserInput
    {
        public static int GetUserInputAsInteger(string message)
        {
            Display(message);
            string input = Console.ReadLine();

            if (input == string.Empty)
            {
                return GetUserInputAsInteger(message);
            }
            try
            {
                return int.Parse(input);
            }
            catch(Exception)
            {
                Display("A whole number is needed like 1, but not like 1.0!");
                return GetUserInputAsInteger(message);
            }
        }

        public static int GetUserInputAsIntegerOrReturnOne(string message)
        {
            Display(message);
            string input = Console.ReadLine();

            if (input == string.Empty)
            {
                return 1;
            }
            try
            {
                return int.Parse(input);
            }
            catch (Exception)
            {
                Display("A whole number is needed like 1, but not like 1.0!");
                return GetUserInputAsInteger(message);
            }
        }


        public static int[] GetUserMultipleInputAsInteger(string message, char splitter=' ')
        {
            Display(message);
            string input = Console.ReadLine();

            if (input == string.Empty)
            {
                return GetUserMultipleInputAsInteger(message);
            }
            try
            {
                string[] numberString = input.Split(splitter);
                int[] numbers = new int[numberString.Length];
                int counter = 0;

                foreach(string number in numberString)
                {
                     numbers[counter] = int.Parse(number);
                    counter++;
                }

                return numbers;
            }
            catch (Exception)
            {
                Display(string.Format("Enter numbers separated by {0}", 
                    splitter == ' '? "space" : splitter.ToString()));
                return GetUserMultipleInputAsInteger(message);
            }
        }

        public static string GetUserInputAsDate(string message, bool strict = true)
        {
            Display(message);
            string input = Console.ReadLine();

            if (input == string.Empty && strict)
            {
                return GetUserInputAsDate(message);
            }
            else
            {
                try
                {
                    if (input != String.Empty)
                    {
                        DateTime.Parse(input);
                    }
                }
                catch (Exception)
                {
                    Display("Ooops! Date needs to be like this MM/DD/YYYY");
                    return GetUserInputAsDate(message);
                }
                return input;
            }
        }

        public static string GetUserInput(string message, bool strict = true)
        {
            Display(message);
            string input = Console.ReadLine();

            if (input == string.Empty && strict)
            {
                return GetUserInput(message);
            }
            return input;
        }



        /// <summary>
        /// Displays a message
        /// </summary>
        /// <param name="message"></param>
        public static void Display(string message)
        {
            Console.Write("\n\n" + message);
        }

        /// <summary>
        /// Return true if user input equals trueOption. trueOption set to "Y" by default.  
        /// </summary>
        /// <param name="message"></param>  
        public static bool UserConfirmationPrompt(string message, string trueOption = "Y", string falseOption = "N")
        {

            string input = UserInput.GetUserInput(message);

            if (new Regex($"{trueOption}", RegexOptions.IgnoreCase).IsMatch(input))
            {
                return true;
            }

            if (new Regex($"{falseOption}", RegexOptions.IgnoreCase).IsMatch(input))
            {
                return false;
            }
            else
            {
                return UserConfirmationPrompt(message);
            }
        }

        public static void DisplayExceptionDetail(Exception e)
        {
            Display(e.Message);
            if(e.InnerException != null)
            {
                Display(e.InnerException.Message);
                Display(e.Source);
            }
        }
    }
}
