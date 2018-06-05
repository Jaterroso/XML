using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Data;
using System.Data.Linq;

namespace Xml
{
    class Connect
    {

        public static DataContext db;
        public void Try()
        {
            while (true)
            {
                try
                {
                    db = new DataContext(entities.connectionString);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    
                }
            }
        }
        public void Success()
        {
            DateTime d = DateTime.Now;
            Console.WriteLine(d + "\nПодключение к Базе Данных:\n" + entities.connectionString + "\n");
        }
        public static void retry()
        {
            Console.WriteLine("Не удалось подключиться к базе данных!\n");
            Console.WriteLine("Повторное подключение через 4...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\rПовторное подключение через 3...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\rПовторное подключение через 2...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\rПовторное подключение через 1...");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("\rПовторное подключение");
        }
    }
}
