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
            return new Worker
            {
                Id = int.Parse(data[0]),
                DateAdded = DateTime.Parse(data[1]),
                FIO = data[2],
                Age = int.Parse(data[3]),
                Height = int.Parse(data[4]),
                BirthDate = DateTime.Parse(data[5]),
                BirthPlace = data[6]
            };
        }

        // Сохранение всех работников в файл (например, после удаления)
        private void SaveWorkers(Worker[] workers)
        {
            File.WriteAllLines(filePath, workers.Select(w => w.ToString()));
        }
    }
}
