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

        public static void Exit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("See you next time!");
            Thread.Sleep(1200);
        }

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
                Console.Write("Insert a value (0 to exit): ");
                num = AskValueOverZero();
                if (num != 0 && num != -1)
                {
                    res = num;
                    num = 0;
                }

            } while (num != 0);

            if (res != 0)
            {
                Sqrt op = new Sqrt();
                op.Number = res;

                string param = JsonConvert.SerializeObject(op);

                SqrtRes sqrtres = JsonConvert.DeserializeObject<SqrtRes>(CallRestMethod("https://localhost:44338/Calculator/Sqrt", param));

                string operation = "√" + res.ToString() + " = " + sqrtres.Square;

                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

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
                Console.Write("Insert a value (0 to exit): ");
                num = AskValue();
                if (num != 0 && num != -1001)
                {
                    divd = num;
                    num = 0;
                }

            } while (num != 0);

            if (divd != 0)
            {
                do
                {
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Welcome to the Calculator!\n");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("-- Substracting -- "); Console.Write(" Inserted: " + divd + " % ?");
                    Console.WriteLine();
                    Console.Write("Insert a value (0 to exit): ");
                    num = AskValueOverZero();
                    if (num != 0 && num != -1)
                    {
                        divs = num;
                        num = 0;
                    }

                } while (num != 0);
            }


            if (divd != 0 && divs != 0)
            {
                
                Div op = new Div();
                op.Dividend = divd;
                op.Divisor = divs;

                string param = JsonConvert.SerializeObject(op);

                DivRes divres = JsonConvert.DeserializeObject<DivRes>(CallRestMethod("https://localhost:44338/Calculator/Div", param));



                string operation = divd.ToString() + " % " + divs.ToString() + " = " + divres.Quotient + " (Remainder: " + divres.Remainder + ")";
                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

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
                Console.Write("Insert a value (0 to get the result): ");
                num = AskValue();
                if (num != 0 && num != -1001)
                {
                    nums.Add(num);
                }

            } while (num != 0);

            if (nums.Count > 0)
            {
                
                

                Mult op = new Mult();
                op.Factors = nums;

                string param = JsonConvert.SerializeObject(op);

                MultRes multres = JsonConvert.DeserializeObject<MultRes>(CallRestMethod("https://localhost:44338/Calculator/Mult", param));

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
                Console.Write("Insert a value (0 to get the exit): ");
                num = AskValue();
                if (num != 0 && num != -1001)
                {
                    min = num;
                    num = 0;
                }

            } while (num != 0);

            if (min != 0)
            {
                do
                {
                    Console.Clear(); Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Welcome to the Calculator!\n");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("-- Substracting -- "); Console.Write(" Inserted: " + min + " - ?");
                    Console.WriteLine();
                    Console.Write("Insert a value (0 to exit): ");
                    num = AskValue();
                    if (num != 0 && num != -1001)
                    {
                        sub = num;
                        num = 0;
                    }

                } while (num != 0);
            }

            if (min != 0 && sub != 0)
            {
 
                Sub op = new Sub();
                op.Minuend = min;
                op.Substraend = sub;

                string param = JsonConvert.SerializeObject(op);

                SubRes subres = JsonConvert.DeserializeObject<SubRes>(CallRestMethod("https://localhost:44338/Calculator/Sub", param));

                string operation = "";
                operation += min.ToString() + " - " + sub.ToString() + " = " + subres.Difference;
                
                Console.WriteLine(operation);
                Console.ReadKey();
            }
        }

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
                Console.Write("Insert a value (0 to get the result): ");
                num = AskValue();
                if (num != 0 && num != -1001)
                {
                    nums.Add(num);
                }

            } while (num != 0);

            if (nums.Count > 0)
            {
               
                Add op = new Add();
                op.Addends = nums;

                string param = JsonConvert.SerializeObject(op);

                AddRes addres = JsonConvert.DeserializeObject<AddRes>(CallRestMethod("https://localhost:44338/Calculator/Add", param));

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


        private static int AskValue()
        {
            string key = Console.ReadLine().ToString();

            int aux = 0;
            if (int.TryParse(key, out aux))
            {
                int num = int.Parse(key);
                if (num > -1000 && num < 1000)
                {
                    return num;
                }
                else
                {
                    return -1001;
                }
            }
            else
            {
                return -1001;
            }
        }

        private static int AskValueOverZero()
        {
            string key = Console.ReadLine().ToString();

            int aux = 0;
            if (int.TryParse(key, out aux))
            {
                int num = int.Parse(key);
                if (num > 0 && num < 1000)
                {
                    return num;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        public static void Quest(string op, string param)
        {
            Task t = new Task(() => GetRes(op, param));
            t.Start();

        }


        static async void GetRes(string op, string param)
        {
            // ... Target page.
            string page = $"https://localhost:44338/Calculator/{op}?{param}";

            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {

                // ... Read the string.
                string result = await content.ReadAsStringAsync();
                Console.WriteLine(result);
                //res = result;

            }
        }


        public static string CallRestMethod(string url, string param)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "POST";
            webrequest.Accept = "application/json";
            webrequest.ContentType = "application/json";
            webrequest.Headers.Set("Origin", System.Reflection.Assembly.GetExecutingAssembly().Location);


            using (var streamWriter = new StreamWriter(webrequest.GetRequestStream()))
            {
               
                streamWriter.Write(param);
                streamWriter.Flush();
                streamWriter.Close();
            }

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
