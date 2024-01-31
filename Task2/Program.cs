using System.Text.RegularExpressions;

namespace Task2
{
    internal partial class Program
    {
        static async Task Main()
        {
            string htmlCode = await GetHtmlAsync("https://ideainyou.com/");
            if (htmlCode.Any())
            {
                var siteDataFile = new FileInfo("siteData.txt");
                using (var stream = siteDataFile.Open(FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        var arr = GetReferences(htmlCode);
                        writer.WriteLine("References:");
                        foreach (var item in arr)
                        {
                            writer.WriteLine(item);
                        }
                        writer.WriteLine(new string('-', 100));

                        arr = GetPhoneNumres(htmlCode);
                        writer.WriteLine("phone numbers:");
                        foreach (var item in arr)
                        {
                            writer.WriteLine(item);
                        }
                        writer.WriteLine(new string('-', 100));

                        writer.WriteLine("emailes:");
                        arr = GetEmailes(htmlCode);
                        foreach (var item in arr)
                        {
                            writer.WriteLine(item);
                        }

                        Console.WriteLine("The file was successfully written:)");
                    }
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("cant get html code");
            }
        }
        static async Task<string> GetHtmlAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                int remainingAttemptsCounter = 5;
                while (remainingAttemptsCounter >= 0)
                {
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(url);

                        response.EnsureSuccessStatusCode();

                        string htmlCode = await response.Content.ReadAsStringAsync();

                        return htmlCode;
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Error during getting page: {e.Message}");
                        Console.WriteLine($"remaining attempts: {--remainingAttemptsCounter}");

                    }
                }
            }
            return null;
        }
        static string[] GetReferences(string html)
        {
            List<string> references = new List<string>();
            Regex regex = FindLinks();
            for (Match m = regex.Match(html); m.Success; m = m.NextMatch())
            {
                references.Add(m.Groups["link"].ToString());
            }
            return references.ToArray();
        }
        static string[] GetPhoneNumres(string html)
        {
            List<string> numbers = new List<string>();
            Regex regex = FindPhoneNumber();
            for (Match m = regex.Match(html); m.Success; m = m.NextMatch())
            {
                numbers.Add(m.Groups["number"].ToString());
            }
            return numbers.ToArray();
        }
        static string[] GetEmailes(string html)
        {
            List<string> numbers = new List<string>();
            Regex regex = FindEmailes();
            for (Match m = regex.Match(html); m.Success; m = m.NextMatch())
            {
                numbers.Add(m.Groups["mail"].ToString());
            }
            return numbers.ToArray();
        }

        [GeneratedRegex(@"href=""(?<link>\S+)""")]
        private static partial Regex FindLinks();

        [GeneratedRegex(@"\D*(?<number>\+\d{1,4}([- ]{0,1}\d){9,11})\D*")]
        private static partial Regex FindPhoneNumber();

        [GeneratedRegex(@"\W*(?<mail>\w+@\w+\.\w+)\W*")]
        private static partial Regex FindEmailes();
    }
}
