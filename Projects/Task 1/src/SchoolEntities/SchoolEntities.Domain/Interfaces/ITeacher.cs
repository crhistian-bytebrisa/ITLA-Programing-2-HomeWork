namespace SchoolEntities.Domain.Interfaces;

public interface ITeacher : IEmployee
{
    List<string> Asignatures { get; set; } 
    bool isAdministrator { get;}
}
