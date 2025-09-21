namespace SchoolEntities.Domain.Interfaces;

public interface ICommunityMember
{
    string Name { get; set; }
    int Age { get; set; }
    string Lastname { get; set; }
    string Address { get; set; }
    string Phone { get; set; }
}
