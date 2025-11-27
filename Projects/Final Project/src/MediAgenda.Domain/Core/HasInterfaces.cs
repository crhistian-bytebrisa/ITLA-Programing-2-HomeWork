using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Core
{
    public interface IHasIdInt
    {
        int Id { get; set; }
    }

    public interface IHasIdString
    {
        string Id { get; set; }
    }

    public interface IHasName
    {
        string Name { get; set; }
    }

    public interface IHasTitle
    {
        string Title { get; set; }
    }

    public interface IHasUsername
    {
        string UserName { get; set; }
    }
    
    public interface IFileName
    {
        string FileName { get; set; }
    }

    public interface IHasEmail
    {
        string Email { get; set; }
    }

    public interface IDayValidation
    {
        TimeOnly StartTime { get; set; }
        TimeOnly EndTime { get; set; }
        DateOnly Date { get; set; }
    }
}
