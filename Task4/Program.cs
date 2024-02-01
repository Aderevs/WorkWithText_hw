using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Task4
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            PrintBill(ReadFile("bill.txt"));
        }
        static string ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }

        static void PrintBill(string text)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            double money;
            string localisedMoney;
            string[] strings = text.Split('\n');
            var reg = Price();
            for (int i = 0; i < strings.Length; i++)
            {
                if (reg.IsMatch(strings[i]))
                {
                    Match m = reg.Match(strings[i]);
                    Console.Write(m.Groups["itemName"]);
                    money = double.Parse(Regex.Replace(
                        m.Groups["price"].ToString(),
                        @"\b(?<hrivra>\d+)\.(?<kopecks>\d{2})\b",
                        "${hrivra},${kopecks}"
                        ));
                    localisedMoney = money.ToString("C", culture);
                    Console.WriteLine(localisedMoney);
                }
                else
                {
                    Console.WriteLine("Дата покупки:");
                    Console.WriteLine(strings[i]);
                }
            }
            Console.WriteLine(new string('-', 25) + "\n\nInfo from bill in culture en-US:");
            culture = new CultureInfo("en-US");
            for (int i = 0; i < strings.Length; i++)
            {
                if (reg.IsMatch(strings[i]))
                {
                    Match m = reg.Match(strings[i]);
                    Console.Write(m.Groups["itemName"]);
                    money = double.Parse(Regex.Replace(
                        m.Groups["price"].ToString(),
                        @"\b(?<hrivra>\d+)\.(?<kopecks>\d{2})\b",
                        "${hrivra},${kopecks}"
                        ));
                    money /= 37.60; // current exchange rate 
                    localisedMoney = money.ToString("C", culture);
                    Console.WriteLine(localisedMoney);
                }
                else
                {
                    Console.WriteLine("Date of purchase:");
                    Console.WriteLine(strings[i]);
                }
            }
        }


        [GeneratedRegex(@"(?<itemName>.+)\b(?<price>\d+\.\d{2})грн\b")]
        private static partial Regex Price();
    }
}
