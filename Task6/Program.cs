using System.Text.RegularExpressions;

namespace Task6
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            string login = "#";
            string password = "#";
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Enter your login:");
                login = Console.ReadLine();
                Regex regex = Login();
                if (regex.IsMatch(login))
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("format of login isn't valid (it can contain only letters of the Latin alphabet), so press any key to retry...");
                    Console.ReadKey();
                }
            }
            exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Enter your login:");
                Console.WriteLine(login);
                Console.WriteLine("Enter your password:");
                password = Console.ReadLine();
                Regex regex = Passwrod();
                if (regex.IsMatch(password))
                {
                    exit = true;
                    Console.WriteLine("registration succesfull");
                }
                else
                {
                    Console.WriteLine("format of password isn't valid or it's too easy " +
                        "(it has to contain only spaces, numbers and letters of the Latin alphabet), so press any key to retry...");
                    Console.ReadKey();
                }
            }

        }

        [GeneratedRegex(@"^[a-zA-Z ]+$")]
        private static partial Regex Login();

        //[GeneratedRegex(@"^((?=\s*[0-9])(?=\s*[a-zA-Z])\s*)$")]
        [GeneratedRegex(@"^[a-zA-Z 0-9]+$")]
        private static partial Regex Passwrod();
    }
}
