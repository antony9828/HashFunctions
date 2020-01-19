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
            List<string> list = GenerateStringList(100, 10);
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnoprstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static List<string> GenerateStringList(int listSize, int stringSize)
        {
            List<string> list = new List<string>();
            for(int i = 0; i < listSize; i++)
            {
                list.Add(RandomString(stringSize));
            }
            return list;
        }
    }
}
