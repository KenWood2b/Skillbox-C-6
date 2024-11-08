using System;

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
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var workerById = repository.GetWorkerById(id);
                            Console.WriteLine(workerById.Equals(default(Worker)) ? "Запись не найдена" : workerById.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод. Введите целое число.");
                        }
                        break;
                    case "3":
                        var worker = CreateWorker();
                        if (!worker.Equals(default(Worker)))
                        {
                            repository.AddWorker(worker);
                            Console.WriteLine("Запись добавлена.");
                        }
                        break;
                    case "4":
                        Console.Write("Введите ID записи для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            repository.DeleteWorker(deleteId);
                            Console.WriteLine("Запись удалена.");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод. Введите целое число.");
                        }
                        break;
                    case "5":
                        Console.Write("Введите начальную дату (дд.мм.гггг): ");
                        DateTime dateFrom;
                        if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dateFrom))
                        {
                            Console.Write("Введите конечную дату (дд.мм.гггг): ");
                            DateTime dateTo;
                            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dateTo))
                            {
                                var workersInRange = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                                foreach (var workerInRange in workersInRange)
                                {
                                    Console.WriteLine(workerInRange);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод даты.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод даты.");
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
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Некорректный ввод возраста.");
                return default;
            }

            Console.Write("Введите рост: ");
            if (!int.TryParse(Console.ReadLine(), out int height))
            {
                Console.WriteLine("Некорректный ввод роста.");
                return default;
            }

            Console.Write("Введите дату рождения (дд.мм.гггг): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                Console.WriteLine("Некорректный ввод даты рождения.");
                return default;
            }

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
