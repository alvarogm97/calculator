using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace CalculatorService.Client
{

    class Program
    {

        // Display the welcome / loading
        public static void Load()
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Welcome to the Calculator!");
                Console.WriteLine("Loading...");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.DarkCyan;
                for (int j = 0; j < i; j++)
                {
                    Console.Write("#");
                }
                for (int k = 0; k < 5 - i; k++)
                {
                    Console.Write(" ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("]");
                Thread.Sleep(500);
            }
        }

        // Display the exit message
        public static void Exit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("See you next time!");
            Thread.Sleep(1200);
        }

        //Display the different options
        public static int Menu()
        {
            Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Welcome to the Calculator!\n");
            Console.WriteLine("What would you like to do?:");
            Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("1. Compute the addition of two or more operands.");
            Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("2. Compute the subtraction of two or more operands.");
            Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("3. Compute the multiplication of two or more operands.");
            Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("4. Compute the division of two or more operands.");
            Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("5. Compute the square root two or more operands.");
            Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("0. Exit.");
            string key = Console.ReadKey().KeyChar.ToString();

            // Validate the input value
            int aux = 0;
            if (int.TryParse(key, out aux))
            {
                return int.Parse(key);
            }
            else
            {
                return -1;
            }

        }

        // Render to the chosen option
        public static void Exec(int opt)
        {
            switch (opt)
            {
                case 1: Add(); break;
                case 2: Sub(); break;
                case 3: Mult(); break;
                case 4: Div(); break;
                case 5: Sqrt(); break;
                default: break;
            }
        }

        // Operates the Square Root 
        private static void Sqrt()
        {
            int num, res = 0;
            do
            {
                Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Welcome to the Calculator!\n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("-- Square Root -- ");
                Console.WriteLine();

                // Ask for the value
                Console.Write("Insert a value (0 to exit): ");
                num = AskValueOverZero();

                // If not exit or invalid
                if (num != 0 && num != -1)
                {
                    res = num;
                    num = 0;
                }

            } while (num != 0);

            // If not exiting
            if (res != 0)
            {

                // Create new object and set the values
                Sqrt op = new Sqrt();
                op.Number = res;

                // Serialize it
                string param = JsonConvert.SerializeObject(op);

                // Send the petition and deserialize the response
                SqrtRes sqrtres = JsonConvert.DeserializeObject<SqrtRes>(CallRestMethod("https://localhost:44338/Calculator/Sqrt", param));

                // Build the complete operation and show it
                string operation = "√" + res.ToString() + " = " + sqrtres.Square;
                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

        // Operates the Division
        private static void Div()
        {

            int num, divd = 0, divs = 0;
            do
            {
                Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Welcome to the Calculator!\n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("-- Dividing -- ");
                Console.WriteLine();

                // Ask for the Dividend
                Console.Write("Insert a value (0 to exit): ");
                num = AskValue();

                // If it is not exit or invalid
                if (num != 0 && num != -1001)
                {
                    divd = num;
                    num = 0;
                }

            } while (num != 0);

            // If not exiting
            if (divd != 0)
            {
                do
                {
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Welcome to the Calculator!\n");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("-- Dividing -- ");

                    // Show the already inserted value
                    Console.Write(" Inserted: " + divd + " % ?");
                    Console.WriteLine();

                    // Ask for the Divisor
                    Console.Write("Insert a value (0 to exit): ");
                    num = AskValueOverZero();

                    // If it is not exit or invalid
                    if (num != 0 && num != -1)
                    {
                        divs = num;
                        num = 0;
                    }

                } while (num != 0);
            }

            // If not exiting
            if (divd != 0 && divs != 0)
            {

                // Create new object and set the values
                Div op = new Div();
                op.Dividend = divd;
                op.Divisor = divs;

                // Serialize it
                string param = JsonConvert.SerializeObject(op);

                // Send the petition and deserialize the response
                DivRes divres = JsonConvert.DeserializeObject<DivRes>(CallRestMethod("https://localhost:44338/Calculator/Div", param));

                // Build the complete operation and show it
                string operation = divd.ToString() + " % " + divs.ToString() + " = " + divres.Quotient + " (Remainder: " + divres.Remainder + ")";
                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

        // Operates the Multiply
        private static void Mult()
        {
            int num;
            List<int> nums = new List<int>();
            do
            {
                Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Welcome to the Calculator!\n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("-- Multiplying -- ");

                // Show the already inserted numbers if in case
                if (nums.Count > 0)
                {
                    Console.Write("Inserted: ");
                    foreach (int n in nums)
                    {
                        Console.Write(n + " x ");
                    }
                    Console.Write("?");
                }
                Console.WriteLine();

                // Ask for the newt value
                Console.Write("Insert a value (0 to get the result): ");
                num = AskValue();

                // If it is not exit or invalid
                if (num != 0 && num != -1001)
                {
                    nums.Add(num);
                }

            } while (num != 0);

            // If there is any inserted numbers
            if (nums.Count > 0)
            {

                // Create new object and set the values
                Mult op = new Mult();
                op.Factors = nums;

                // Serialize it
                string param = JsonConvert.SerializeObject(op);

                // Send the petition and deserialize the response
                MultRes multres = JsonConvert.DeserializeObject<MultRes>(CallRestMethod("https://localhost:44338/Calculator/Mult", param));

                // Build the complete operation and show it
                string operation = "";
                foreach (int n in nums)
                {
                    operation += n.ToString() + " x ";
                }
                operation = operation.Substring(0, operation.Length - 2);
                operation += "= " + multres.Product;

                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

        // Operates the Substraction
        private static void Sub()
        {
            int num, min = 0, sub = 0;
            do
            {
                Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Welcome to the Calculator!\n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("-- Substracting -- ");
                Console.WriteLine();

                // Ask for the Minuend
                Console.Write("Insert a value (0 to get the exit): ");
                num = AskValue();

                // If it is valid
                if (num != 0 && num != -1001)
                {
                    min = num;
                    num = 0;
                }

            } while (num != 0);

            // If exiting is not chose
            if (min != 0)
            {
                do
                {
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Welcome to the Calculator!\n");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("-- Substracting -- ");

                    // Show the already inserted value
                    Console.Write(" Inserted: " + min + " - ?");
                    Console.WriteLine();

                    // Ask for the Dividend
                    Console.Write("Insert a value (0 to exit): ");
                    num = AskValue();

                    // If it is valid
                    if (num != 0 && num != -1001)
                    {
                        sub = num;
                        num = 0;
                    }

                } while (num != 0);
            }

            // If exiting is not chose
            if (min != 0 && sub != 0)
            {

                // // Create new object and set the values
                Sub op = new Sub();
                op.Minuend = min;
                op.Substraend = sub;

                // Serialize it
                string param = JsonConvert.SerializeObject(op);

                // Send the petition and deserialize the response
                SubRes subres = JsonConvert.DeserializeObject<SubRes>(CallRestMethod("https://localhost:44338/Calculator/Sub", param));

                // Build the complete operation and show it
                string operation = "";
                operation += min.ToString() + " - " + sub.ToString() + " = " + subres.Difference;
                
                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

        // Operates the Addition
        private static void Add()
        {

            int num;
            List<int> nums = new List<int>();
            do
            {
                Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Welcome to the Calculator!\n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("-- Adding -- ");

                // Show the already inserted numbers if in case
                if (nums.Count > 0)
                {
                    Console.Write("Inserted: ");
                    foreach (int n in nums)
                    {
                        Console.Write(n + " + ");
                    }
                    Console.Write("?");
                }

                Console.WriteLine();

                // Ask for the next value
                Console.Write("Insert a value (0 to get the result): ");
                num = AskValue();

                // If it is not exit or invalid 
                if (num != 0 && num != -1001)
                {
                    nums.Add(num);
                }

            } while (num != 0);

            // If there is any inserted numbers
            if (nums.Count > 0)
            {
                // Create new object and set the values
                Add op = new Add();
                op.Addends = nums;

                // Serialize it
                string param = JsonConvert.SerializeObject(op);

                // Send the petition and deserialize the response
                AddRes addres = JsonConvert.DeserializeObject<AddRes>(CallRestMethod("https://localhost:44338/Calculator/Add", param));

                // Build the complete operation and show it
                string operation = "";
                foreach (int n in nums)
                {
                    operation += n.ToString() + " + ";
                }

                operation = operation.Substring(0, operation.Length - 2);
                operation += "= " +addres.Sum;
                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

        // Ask and provide a valid number
        private static int AskValue()
        {
            string key = Console.ReadLine().ToString();

            // Validate the input 
            // Limited between -999 and 999
            int aux = 0;
            if (int.TryParse(key, out aux))
            {
                int num = int.Parse(key);
                if (num > -1000 && num < 1000)
                {
                    return num;
                }
                // If invalid
                else
                {
                    return -1001;
                }
            }
            // If invalid
            else
            {
                return -1001;
            }
        }

        // Ask and provide a valid number
        private static int AskValueOverZero()
        {
            string key = Console.ReadLine().ToString();

            // For special cases: Divisor and Square Root
            // Validate the input 
            // Limited between 1 and 999
            int aux = 0;
            if (int.TryParse(key, out aux))
            {
                int num = int.Parse(key);
                if (num > 0 && num < 1000)
                {
                    return num;
                }
                // If invalid
                else
                {
                    return -1;
                }
            }
            // If invalid
            else
            {
                return -1;
            }
        }

        // Receibe the request url and the serializated request
        // Return the serializated response
        public static string CallRestMethod(string url, string param)
        {
            // Create an HTTP Request
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "POST";
            webrequest.Accept = "application/json";
            webrequest.ContentType = "application/json";
            
            // Stream the JSON serializated request
            using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
            {
               
                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

            // Receibe the JSON serializated response
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }


        static void Main(string[] args)
        {

            Load();
            int opt;
            do
            {
               opt = Menu();
               if(opt > 0 && opt < 6)
                {
                    Exec(opt);
                }

            } while (opt != 0);

            Exit();
    
        }
    }
}
