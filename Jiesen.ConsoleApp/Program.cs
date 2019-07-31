using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Contract;
using Jiesen.EntityFramework;

namespace Jiesen.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (JiesenDbContext jiesenDbContext = new JiesenDbContext())
            {
                Person person = new Person() { Name = "test" };
                jiesenDbContext.Persons.Add(person);
                jiesenDbContext.SaveChanges();

                var result = jiesenDbContext.Persons.Select(x => x.Name.Contains("te"));
            }
        }
    }
}
