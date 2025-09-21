using SchoolEntities.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEntities.Domain.Entities
{
    public class Exstudent : IStudent
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Grade { get; set; }
        public bool isStudent { get; } = false;

        public Exstudent(string name, string lastname, int age, string address, string phone, string grade)
        {
            Name = name;
            Lastname = lastname;
            Age = age;
            Address = address;
            Phone = phone;
            Grade = grade;
        }
    }
}
