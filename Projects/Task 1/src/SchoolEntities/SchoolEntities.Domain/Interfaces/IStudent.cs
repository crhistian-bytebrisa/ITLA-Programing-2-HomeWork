using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEntities.Domain.Interfaces
{
    public interface IStudent : ICommunityMember
    {
        string Grade { get; set; }
        bool isStudent { get; }
    }
}
