
using System;

namespace TestSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MainMenu menu = new MainMenu();
            menu.Show();
        }
    }
}
