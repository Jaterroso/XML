using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Data;
using System.Data.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace Xml
{
    class Program
    {
        private static Table<vacancies> vacs;
        private static Table<applicant> ap;
        private static string path;
        static void Main(string[] args)
        {
            string key;
            Connect first = new Connect();
            first.Try();
            List<applicant> List = new List<applicant>();
            List<vacancies> ListV = new List<vacancies>();
            while (true)
            {
                try
                {
                    Console.WriteLine("Просмотреть:\n1. Соискателей\n2. Вакансии");
                    switch (key = Console.ReadLine())
                    {
                        case "1":
                            ap = Connect.db.GetTable<applicant>();
                            first.Success();
                            SQLList.TableToList(List, ap);
                            foreach (var l in List)
                            {
                                Console.WriteLine(l.idapplicant + ".\t" + l.FIO + " / " + l.position + " / " + l.salary + " / " + l.hired);
                            }
                            break;
                        case "2":
                            vacs = Connect.db.GetTable<vacancies>();
                            first.Success();
                            SQLList.TableToList(ListV, vacs);
                            foreach (var l in ListV)
                            {
                                Console.WriteLine(l.Idvacant + ".\t" + l.position + " / " + l.salary + " / " + l.dateopen);
                            }
                            break;
                    }                    
                    break;
                }
                catch (Exception e)
                {
                    Connect.retry();
                }
            }
            Console.WriteLine("\nЭкспортировать в XML? (y/n)");
            while (true)
            {
                if (Console.ReadLine() == "y")
                {
                    try
                    {
                        path = "C:/XML/" + key + ".xml";

                        if (key == "1")
                            SQLList.ListToXML(List, path);
                        else
                            SQLList.ListToXML(ListV, path);
                        Console.WriteLine("Экспорт " + path + " успешно завершен!");
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\nОшибка экспорта!\nТекст ошибки: " + e.Message + "\nЭкспортировать повторно? (y/n)");
                    }
                    finally
                    {
                        Console.WriteLine("\nПереконвертировать в формат Excel? (y/n)");
                        while(true)
                        {
                            if (Console.ReadLine() == "y")
                            {
                                try
                                {
                                    //Конвертация см класс XMLtoEXCEL
                                    XMLtoEXCEL convert = new XMLtoEXCEL(path);
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            } 
            Console.WriteLine("Для выхода нажмите Enter");
            Console.ReadLine();
            Console.WriteLine("Закрытие консоли");
            Environment.Exit(0);
        }  
    }
}
//Доделать чтение XML
//Сделать прогресс через возврат каретки