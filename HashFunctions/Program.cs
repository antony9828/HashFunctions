using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashFunctions
{
    class Program
    {
        static void Main(string[] args)
        {

            do
            {
                string password = "this is a password";

                string select;

                Console.WriteLine("Press 1 to enter your own password. Press 2 to run tests");
                do
                {
                    select = Console.ReadLine();
                    //Console.WriteLine(select);
                    //Console.WriteLine(select.GetType());
                }
                while (select != "1" && select != "2");

                if (select == "2")
                    goto SELECT2;

                Console.WriteLine("Enter new password: ");
                password = Console.ReadLine();

                string a = GetHash1(password);
                string aa = GetHash2(password);
                Console.WriteLine("Hash with the fisrt algorithm: " + a);
                Console.WriteLine("Hash with the second algorithm: " + aa);
                goto SELECT1;

                SELECT2:

                Console.WriteLine("Enter the number of tests and length of the password");
                Console.WriteLine("Number of tests: ");
                int numOfTests = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Password length: ");
                int passLength = Convert.ToInt32(Console.ReadLine());

                HashSet<string> list = GenerateStringList(numOfTests, passLength);
                HashSet<string> hashset = new HashSet<string>();
                foreach (string str in list)
                {
                    //Console.WriteLine(str);
                    //Console.WriteLine(GetHash(str,1));
                    hashset.Add(GetHash1(str));
                }

                Console.WriteLine("First algorithm");
                Console.WriteLine("Number of unique passwords: " + list.Count);
                Console.WriteLine("Number of collisions: " + (list.Count - hashset.Count));

                HashSet<string> list1 = GenerateStringList(numOfTests, passLength);
                HashSet<string> hashset1 = new HashSet<string>();
                foreach (string str in list)
                {
                    //Console.WriteLine(str);
                    //Console.WriteLine(GetHash(str,1));
                    hashset1.Add(GetHash2(str));
                }
                Console.WriteLine("Second algorithm");
                Console.WriteLine("Number of unique passwords: " + list1.Count);
                Console.WriteLine("Number of collisions: " + (list1.Count - hashset1.Count));

                SELECT1:

                Console.WriteLine("Press any key to continue. Press ESCAPE key to exit");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);


           
        }

        private static string GetHash1(string str)
        {
            string hash = "";

            long csum = ControlSum(str);
            long pass = csum;
            long hashint = 0;

            hash = hashint.ToString("x");

            for (int i = 0; i < pass.ToString().Length - 1; i++)
            {
                long mod = Math.Abs(Convert.ToInt16(pass.ToString()[i]) + Convert.ToInt16(pass.ToString()[i + 1]));
                if (mod == 0)
                {
                    mod = 1;
                }
                hashint += (pass % mod * (long)Math.Pow(10, (pass.ToString().Length - i)));
            }

            hash = hashint.ToString("x");

            return hash;
        }

        private static string GetHash2(string str)
        {
            string hash = "";

            string str1 = ControlSum(str).ToString();
            long pass = Convert.ToInt64(str1);
            long hashint = 0;
            
            for (int i = 0; i < pass.ToString().Length; i++)
            {
                hashint += (pass % Convert.ToInt16(pass.ToString()[i])) * (long)Math.Pow(10,(pass.ToString().Length - i));
            }

            hash = hashint.ToString("x");

            return hash;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnoprstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static HashSet<string> GenerateStringList(int listSize, int stringSize)
        {
            HashSet<string> list = new HashSet<string>();
            for(int i = 0; i < listSize; i++)
            {
                list.Add(RandomString(stringSize));
            }
            return list;
        }

        public static int ControlSum(string str)
        {
            if (str.Length % 2 != 0)
                str += "s";

            int sum = 0;

            for (int i = 0; i < str.Length; i += 2)
            {
                int mult = str[i] * str[i + 1];
                int div = str[i] / str[i + 1];

                sum += mult * (int)Math.Pow(10, i);
                sum += div * (int)Math.Pow(10, i);
            }
            return sum;
        }

        //public static int ReceivingExistCodes(int x)
        //{
        //    x += 256;
        //    while(!(((x <= 57) && (x >= 48)) || ((x <= 90) && (x <= 122)) || ((x <= 122) && (x >= 97))))
        //        if (x < 48) { x += 24; }
        //        else { x -= 47; }

        //    return x;
        //}
    }
}
