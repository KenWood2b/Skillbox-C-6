using System;
using System.IO;
using System.Linq;

namespace Structures_and_Introduction_to_OOPConsoleApp
{
    internal class Repository
    {
        private readonly string filePath;

        public Repository(string filePath)
        {
            this.filePath = filePath;
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        // Просмотр всех записей
        public Worker[] GetAllWorkers()
        {
            return File.ReadAllLines(filePath)
                       .Select(ParseWorker)
                       .ToArray();
        }

        // Просмотр одной записи по ID
        public Worker GetWorkerById(int id)
        {
            return GetAllWorkers().FirstOrDefault(w => w.Id == id);
        }

        // Добавление новой записи
        public void AddWorker(Worker worker)
        {
            worker.Id = GetNextId();
            worker.DateAdded = DateTime.Now;
            File.AppendAllText(filePath, worker.ToString() + Environment.NewLine);
        }

        // Удаление записи по ID
        public void DeleteWorker(int id)
        {
            var workers = GetAllWorkers().Where(w => w.Id != id).ToArray();
            SaveWorkers(workers);
        }

        // Загрузка записей в диапазоне дат
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            return GetAllWorkers()
                   .Where(w => w.DateAdded >= dateFrom && w.DateAdded <= dateTo)
                   .ToArray();
        }

        // Вспомогательный метод для получения следующего ID
        private int GetNextId()
        {
            var workers = GetAllWorkers();
            return workers.Any() ? workers.Max(w => w.Id) + 1 : 1;
        }

        // Вспомогательный метод для парсинга строки в Worker
        private Worker ParseWorker(string line)
        {
            var data = line.Split('#');
            if (data.Length < 7)
            {
                throw new FormatException("Неверный формат данных.");
            }

            if (!int.TryParse(data[0], out int id))
            {
                throw new FormatException("Некорректный ID.");
            }

            if (!DateTime.TryParse(data[1], out DateTime dateAdded))
            {
                throw new FormatException("Некорректная дата добавления.");
            }

            string fio = data[2];

            if (!int.TryParse(data[3], out int age))
            {
                throw new FormatException("Некорректный возраст.");
            }

            if (!int.TryParse(data[4], out int height))
            {
                throw new FormatException("Некорректный рост.");
            }

            if (!DateTime.TryParse(data[5], out DateTime birthDate))
            {
                throw new FormatException("Некорректная дата рождения.");
            }

            string birthPlace = data[6];

            return new Worker
            {
                Id = id,
                DateAdded = dateAdded,
                FIO = fio,
                Age = age,
                Height = height,
                BirthDate = birthDate,
                BirthPlace = birthPlace
            };
        }

        // Сохранение всех работников в файл (например, после удаления)
        private void SaveWorkers(Worker[] workers)
        {
            File.WriteAllLines(filePath, workers.Select(w => w.ToString()));
        }
    }
}
