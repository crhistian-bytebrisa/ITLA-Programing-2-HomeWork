using SchoolEntities.Domain.Interfaces;

namespace SchoolEntities.Domain.Entities;

public class Student : IStudent
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Grade { get; set; }
    public bool isStudent { get; } = true;

    public Student(string name, string lastname, int age, string address, string phone, string grade)
    {
        Name = name;
        Lastname = lastname;
        Age = age;
        Address = address;
        Phone = phone;
        Grade = grade;
    }

}
