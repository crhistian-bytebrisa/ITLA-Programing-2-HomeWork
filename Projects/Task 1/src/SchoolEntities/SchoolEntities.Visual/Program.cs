using SchoolEntities.Domain.Entities;
using SchoolEntities.Domain.Interfaces;

namespace SchoolEntities.Visual
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStudent Pedrito = new Student("Pedrito", "Perez", 15, "Los mina", "8098845678", "4to secundaria");
            IStudent Juanito = new Exstudent("Juanito", "Guzman", 17, "Los alcarrizo", "8098679856", "6to secundaria");
            IEmployee Maria = new Administrative("Maria", "Peralta", 23, "Calle mexico", "8498675432", "Administrativa", 45000);
            ITeacher Pedro = new Teacher("Pedro", "Gutierrez", 28, "Villa Mella", "8498865765", "Profesor", 38000, new List<string>{"Matematicas","Musica"});
            ITeacher Juan = new Administrator("Juan", "Yanabi", 28, "Villa Mella", "8498465765", "Director y profesor", 65000, new List<string> { "Español", "Sociales" });


        }
    }
}
