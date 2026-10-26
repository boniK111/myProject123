
using System;

namespace TestSystem
{
    public class MainMenu
    {
        public void Show()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("       СИСТЕМА ТЕСТИРОВАНИЯ");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Администратор");
                Console.WriteLine("2. Тестируемый");
                Console.WriteLine("0. Выход");
                Console.WriteLine("----------------------------------");
                Console.Write("Выберите пункт меню: ");

                string? choice = Console.ReadLine();
                }
            }
        }

 }

