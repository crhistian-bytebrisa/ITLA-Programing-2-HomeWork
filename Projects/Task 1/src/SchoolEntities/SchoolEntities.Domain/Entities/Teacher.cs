using SchoolEntities.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEntities.Domain.Entities
{
    public class Teacher : ITeacher
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Rol { get; set; }
        public int Amount { get; set; }
        public List<string> Asignatures { get; set; }
        public bool isAdministrator { get; } = false;

        public Teacher(string name, string lastname, int age, string address, string phone, string rol, int amount, List<string> asignatures)
        {
            Name = name;
            Lastname = lastname;
            Age = age;
            Address = address;
            Phone = phone;
            Rol = rol;
            Amount = amount;
            Asignatures = asignatures;
        }
    }
}
