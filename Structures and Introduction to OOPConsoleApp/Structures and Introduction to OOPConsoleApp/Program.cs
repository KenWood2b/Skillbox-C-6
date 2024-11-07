using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures_and_Introduction_to_OOPConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "employees.txt";
            var repository = new Repository(filePath);

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Просмотреть все записи");
                Console.WriteLine("2 - Просмотреть запись по ID");
                Console.WriteLine("3 - Добавить новую запись");
                Console.WriteLine("4 - Удалить запись");
                Console.WriteLine("5 - Загрузить записи в диапазоне дат");
                Console.WriteLine("6 - Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var workers = repository.GetAllWorkers();
                        foreach (var selectedWorker in workers)
                        {
                            Console.WriteLine(selectedWorker);
                        }
                        break;
                    case "2":
                        Console.Write("Введите ID записи: ");
                        int id = int.Parse(Console.ReadLine());
                        var workerById = repository.GetWorkerById(id);
                        Console.WriteLine(workerById.Equals(default(Worker)) ? "Запись не найдена" : workerById.ToString());
                        break;
                    case "3":
                        var worker = CreateWorker();
                        repository.AddWorker(worker);
                        Console.WriteLine("Запись добавлена.");
                        break;
                    case "4":
                        Console.Write("Введите ID записи для удаления: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        repository.DeleteWorker(deleteId);
                        Console.WriteLine("Запись удалена.");
                        break;
                    case "5":
                        Console.Write("Введите начальную дату (дд.мм.гггг): ");
                        DateTime dateFrom = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите конечную дату (дд.мм.гггг): ");
                        DateTime dateTo = DateTime.Parse(Console.ReadLine());
                        var workersInRange = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                        foreach (var workerInRange in workersInRange)
                        {
                            Console.WriteLine(workerInRange);
                        }
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        // Метод для создания нового работника через ввод данных
        private static Worker CreateWorker()
        {
            Console.Write("Введите ФИО: ");
            string fio = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите рост: ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Введите дату рождения (дд.мм.гггг): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите место рождения: ");
            string birthPlace = Console.ReadLine();

            return new Worker
            {
                FIO = fio,
                Age = age,
                Height = height,
                BirthDate = birthDate,
                BirthPlace = birthPlace
            };
        }
    }
}
