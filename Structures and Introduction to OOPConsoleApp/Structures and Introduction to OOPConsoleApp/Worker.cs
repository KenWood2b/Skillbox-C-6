using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures_and_Introduction_to_OOPConsoleApp
{
    struct Worker
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public override string ToString()
        {
            return $"{Id}#{DateAdded:dd.MM.yyyy HH:mm}#{FIO}#{Age}#{Height}#{BirthDate:dd.MM.yyyy}#{BirthPlace}";
        }
    }
}
