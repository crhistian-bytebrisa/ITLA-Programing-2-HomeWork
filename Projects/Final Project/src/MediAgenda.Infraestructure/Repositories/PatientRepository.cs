using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class PatientRepository : BaseRepositoryIdInt<PatientModel>, IPatientRepository
    {
        public PatientRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<PatientModel>, int)> GetAllAsync(PatientRequest request)
        {
            IQueryable<PatientModel> query = _context.Set<PatientModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.User.NameComplete.Contains(request.Name));
            }

            if (request.OlderAge is not null)
            {
                query = query.Where(x =>
                (DateTime.Today.Year - x.DateOfBirth.Year) -
                (DateTime.Today < x.DateOfBirth.AddYears(DateTime.Today.Year - x.DateOfBirth.Year) ? 1 : 0)
                > request.OlderAge);
            }

            if (request.InsuranceId is not null)
            {
                query = query.Where(x => x.InsuranceId == request.InsuranceId);
            }

            if (!string.IsNullOrWhiteSpace(request.Identification))
            {
                query = query.Where(x => x.Identification == request.Identification);
            }

            if (request.Bloodtype is not null)
            {
                query = query.Where(x => x.Bloodtype == request.Bloodtype);
            }

            if (request.Gender is not null)
            {
                query = query.Where(x => x.Gender == request.Gender);
            }

            if (request.IncludeNote is true)
            {
                query = query.Include(x => x.Notes);
            }

            if (request.IncludeMedicalDocuments is true)
            {
                query = query.Include(x => x.MedicalDocuments);
            }

            if (request.IncludeCurrentMedicaments is true)
            {
                query = query
                    .Include(x => x.CurrentMedicaments)
                    .ThenInclude(cm => cm.Medicine);
            }

            if (request.IncludeConsultations is true)
            {
                query = query.Include(x => x.Consultations);
            }

            if (request.IncludeUser is true)
            {
                query = query.Include(x => x.User);
            }

            if (request.IncludeInsurance is true)
            {
                query = query.Include(x => x.Insurance);
            }

            return await query.PaginateAsync(request);
        }
    }
}