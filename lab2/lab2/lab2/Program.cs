using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("SqlServerConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            int menu = -1;
            Tasks t = new Tasks();
            while (menu != 0)
            {
                Console.WriteLine("1. Вывести все данные из таблицы Posts");
                Console.WriteLine("2. Вывести все данные из таблицы Posts, id которых больше заданного");
                Console.WriteLine("3. Вывести работников на определённой должности");
                Console.WriteLine("4. Вывести id работника, id и название его должности");
                Console.WriteLine("5. Вывести id работника, id и название его должности по названию долнжности");
                Console.WriteLine("6. Добавление новой должности");
                Console.WriteLine("7. Добавление нового сотрудника");
                Console.WriteLine("8. Удаление должности по её номеру");
                Console.WriteLine("9. Удаление сотрудника по номеру");
                Console.WriteLine("10.Обновление информации о должности по её номеру");
                Console.WriteLine("0. Выход");
                menu = int.Parse(Console.ReadLine());
                switch (menu)
                {
                    case 1:
                        t.Task1(options);
                        break;
                    case 2:
                        Console.WriteLine("Введите id");
                        int i = int.Parse(Console.ReadLine());
                        t.Task2(i, options);
                        break;
                    case 3:
                        t.Task3(options);
                        break;
                    case 4:
                        t.Task4(options);
                        break;
                    case 5:
                        Console.WriteLine("Введите название должности");
                        string name = Console.ReadLine();
                        t.Task5(name, options);
                        break;
                    case 6:
                        Console.WriteLine("Введите название должности");
                        name = Console.ReadLine();
                        t.Task6(name, options);
                        break;
                    case 7:
                        Console.WriteLine("Введите номер должности");
                        int postId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите номер поезда");
                        int trainId = int.Parse(Console.ReadLine());
                        t.Task7(postId, trainId, options);
                        break;
                    case 8:
                        Console.WriteLine("Введите номер должности");
                        postId = int.Parse(Console.ReadLine());
                        t.Task8(postId, options);
                        break;
                    case 9:
                        Console.WriteLine("Введите номер сотрудника");
                        int staffId = int.Parse(Console.ReadLine());
                        t.Task9(staffId, options);
                        break;
                    case 10:
                        Console.WriteLine("Введите номер должности");
                        postId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите новое название");
                        string nameOfPost = Console.ReadLine();
                        t.Task10(postId, nameOfPost, options);
                        break;
                }
            }
        }
    }
}
