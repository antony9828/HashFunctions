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
            //int csum = ControlSum(password);
            //Console.WriteLine(csum);
            string aa = GetHash(password, 8);
            Console.WriteLine(aa);

            Console.ReadKey();

            List<string> list = GenerateStringList(100, 10);
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }

        private static string GetHash(string str, int hashLength)
        {
            string hash = "";

            if (hashLength > 3)
            {
                int minLen = 2;
                int realMinLen = 0;

                int originalSalt = ControlSum(str);
                int originalLengthStr = str.Length;

                while (minLen <= hashLength)
                    realMinLen = (minLen *= 2);

                while (minLen < originalLengthStr)
                    minLen *= 2;

                if ((minLen - originalLengthStr) < minLen)
                    minLen *= 2;

                int addCount = minLen - originalLengthStr;

                for (int i = 0; i < addCount; i++)
                    str += ReceivingExistCodes(str[i] + str[i + 1]);

                int maxSalt = ControlSum(str);
                int maxLengthStr = str.Length;

                while (str.Length != realMinLen)
                {
                    int center = str.Length / 2;
                    hash = "";
                    for (int i = 0; i < center; i++)
                    {
                        hash += ReceivingExistCodes(str[center - i] + str[center + 1]);
                        str = hash;
                    }
                }

                int rem = realMinLen - hashLength;
                int countCompress = realMinLen / rem;
                hash = "";

                for (int i = 0; hash.Length < hashLength - 4; i++)
                {
                    if (i % countCompress == 0)
                        hash += ReceivingExistCodes(str[i] + str[i + 1]);
                    else
                        hash += str[i];
                }

                hash += ReceivingExistCodes(originalSalt);
                hash += ReceivingExistCodes(originalLengthStr);

                hash += ReceivingExistCodes(maxSalt);
                hash += ReceivingExistCodes(maxLengthStr);
            }

            return hash;
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

        public static int ControlSum(string str)
        {
            if (str.Length % 2 != 0)
                str += "s";

            int sum = 0;

            for (int i = 0; i < str.Length; i += 2)
            {
                int mult = str[i] * str[i + 1];
                int div = str[i] / str[i + 1];

                sum += mult;
                sum += div;
            }
            return sum;
        }

        public static int ReceivingExistCodes(int x)
        {
            x += 256;
            while(!(((x <= 57) && (x >= 48)) || ((x <= 90) && (x <= 122)) || ((x <= 122) && (x >= 97))))
                if (x < 48) { x += 24; }
                else { x -= 47; }

            return x;
        }
    }
}
