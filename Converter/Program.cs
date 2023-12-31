using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string num; int b1, b2;
            Console.WriteLine("Tastati numarul de convertit:");
            Console.Write("num = ");
            num = Console.ReadLine();
            Console.WriteLine("Tastati baza numarului:");
            b1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Tastati baza de conversie a numarului:");
            b2 = int.Parse(Console.ReadLine());
            if (b1 < 2 || b1 > 16 || b2 < 2 || b2 > 16)
                throw new ArgumentException("Baza nu este valida! Introduceti o baza cuprinsa in intervalul [2, 16]");
            else
            {
                if(b2 == 10)
                    Console.WriteLine(ConvertToDecimal(num, b1));
                else if(b1 == 10)
                    Console.WriteLine(ConvertFromDecimal(num, b2));
                else if(b1 == 2 && b2 == 8)
                    Console.WriteLine(ConvertBinToOct(num, b1));
                else if (b1 == 2 && b2 == 16)
                    Console.WriteLine(ConvertBinToHex(num, b1));
                else
                    Console.WriteLine(ConvertAny(num, b1, b2));
            }
        }

        private static string Switcher(string res, int d)
        {
            if (d < 10)
                res += d.ToString();
            else
            {
                switch (d)
                {
                    case 10:
                        res += "A";
                        break;
                    case 11:
                        res += "B";
                        break;
                    case 12:
                        res += "C";
                        break;
                    case 13:
                        res += "D";
                        break;
                    case 14:
                        res += "E";
                        break;
                    case 15:
                        res += "F";
                        break;
                }
            }
            return res;
        }

        private static string ConvertAny(string num, int b1, int b2)
        {
            string toDecimal = ConvertToDecimal(num, b1).ToString();
            return ConvertFromDecimal(toDecimal, b2);
        }

        private static string ConvertBinToHex(string num, int b1)
        {
            string res = ""; int len = num.Length;
            while (len % 4 != 0)
            {
                num = "0" + num;
                len = num.Length;
            }
            if (len == 4)
                res = ConvertToDecimal(num, b1).ToString();
            else
            {
                string d = "";
                int i = 1;
                while (i <= len)
                {
                    Console.WriteLine(res);
                    if (i % 4 == 0)
                    {
                        d += num[i - 1];
                        res = Switcher(res, (int)ConvertToDecimal(d, b1));
                        d = "";
                    }
                    else
                        d += num[i - 1];
                    i++;
                }
            }
            return res;
        }

        private static string ConvertBinToOct(string num, int b1)
        {
            string res = ""; int len = num.Length;
            while (len % 3 != 0)
            {
                num = "0" + num;
                len = num.Length;
            }
            if(len == 3)
                res = ConvertToDecimal(num, b1).ToString();
            else
            {
                string d = "";
                int i = 1;
                while (i <= len)
                {
                    if(i % 3 == 0)
                    { 
                        d += num[i - 1];
                        res = Switcher(res, (int)ConvertToDecimal(d, b1));
                        d = "";
                    }
                    else
                        d += num[i - 1];
                    i++;
                }
            }
            return res;
        }

        private static double ConvertToDecimal(string num, int fromBase)
        {
            string digits = "0123456789ABCDEF";
            double res = 0;

            string[] halves = num.Split('.');
            string new_num = string.Join("", halves);
            double length = halves[0].Length;

            foreach(char c in new_num)
            {
                int index = digits.IndexOf(c, 0, fromBase);
                res += index * (double)Math.Pow(fromBase, length - 1);
                length--;
            }
            return res;
        }

        private static string ConvertFromDecimal(string num, int toBase)
        {
            Stack<int> integer = new Stack<int>();
            List <int> decimalNum = new List<int>();
            string res = "";
            string[] halves = num.Split('.');
            int n1 = int.Parse(halves[0]);
            double number = double.Parse(num);
            double n2 = number - n1;

            while (n1 > 0)
            {
                integer.Push(n1 % toBase);
                n1 /= toBase;
            }
            while(n2 > 0)
            {
                double result = n2 * toBase;
                decimalNum.Add((int)result);
                n2 = result - (int)result;
            }
            while(integer.Count > 0)
            {
                int d = integer.Pop();
                res = Switcher(res, d);
            }
            res += ".";
            int i = 0;
            while (i < decimalNum.Count)
            {
                int d = decimalNum[i];
                i++;
                res = Switcher(res, d);
            }
            return res;
        }
    }
}