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
            string password = "this is a password";

            string aa = GetHash(password, 8);
            Console.WriteLine(aa);

            Console.ReadKey();

            HashSet<string> list = GenerateStringList(5000, 10);
            HashSet<string> hashset = new HashSet<string>();
            foreach (string str in list)
            {
                //Console.WriteLine(str);
                //Console.WriteLine(GetHash(str,1));
                hashset.Add(GetHash(str, 1));
            }

            Console.WriteLine("Number of test: " + list.Count);
            Console.WriteLine("Number of collisions: " + (list.Count - hashset.Count));
            Console.ReadKey();
        }

        private static string GetHash(string str, int hashLength)
        {
            string hash = "";

            long csum = ControlSum(str);
            long hashint = csum;
            hash = hashint.ToString("x");
            //Console.WriteLine(hash);
            //hash = csum + 123456789123456789;

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

                sum += mult * (10 * i);
                sum += div * (10 * i);
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
