namespace SchoolEntities.Domain.Interfaces;

public interface IEmployee : ICommunityMember
{
    string Rol { get; set; }
    int Amount { get; set; }
}   