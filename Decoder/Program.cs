using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Decoder
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            WriteText("text.txt", Decode(ReadText("text.txt")));
            Console.WriteLine(ReadText("text.txt"));
        }
        static string ReadText(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }
        static string Decode(string text)
        {
            string[] words = text.Split(' ');
            Regex regex = Preposition();
            for(int i = 0; i < words.Length; i++)
            {
                if (regex.IsMatch(words[i]))
                {
                    words[i] = "ГАВ!";
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach(string word in words)
            {
                stringBuilder.Append(word).Append(" ");
            }
            return stringBuilder.ToString();
        }
        static void WriteText(string fileName, string textToWrite)
        {
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using(var writer = new StreamWriter(fileStream))
                {
                    writer.Write(textToWrite);
                }
            }
        }

        //instance with ukrainian words
        [GeneratedRegex(@"\b(з|по|від|у|в|на|до|за|під|по-за|над|перед|через|при|про|по-серед|навколо|навпроти|позаду|попереду)\b", RegexOptions.IgnoreCase)]
        private static partial Regex Preposition();
    }
}
