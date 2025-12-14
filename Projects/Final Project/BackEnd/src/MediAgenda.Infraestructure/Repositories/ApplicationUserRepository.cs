using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class ApplicationUserRepository : BaseRepositoryIdString<ApplicationUserModel>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUserModel> _userManager;

        public ApplicationUserRepository(MediContext context, UserManager<ApplicationUserModel> userManager)
            : base(context)
        {
            _userManager = userManager;
        }

        public async Task<(List<ApplicationUserModel>, int)> GetAllAsync(ApplicationUserRequest request)
        {
            IQueryable<ApplicationUserModel> query = _context.Set<ApplicationUserModel>();
            query = query.Include(x => x.Doctor)
                .Include(x => x.Patient)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                query = query.Where(x => x.NameComplete.Contains(request.FullName));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(x => x.Email != null && x.Email.Contains(request.Email));
            }

            return await query.PaginateAsync(request);
        }

        public override async Task<ApplicationUserModel> GetByIdAsync(string id)
        {
            var entity = await _context.Set<ApplicationUserModel>()
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<ApplicationUserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Set<ApplicationUserModel>()
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .ThenInclude(x => x.Insurance)
                .Include(x => x.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<IdentityRole>> GetRolesByUserIdAsync(string userId)
        {
            var roleIds = await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .ToListAsync();

            return await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .ToListAsync();
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUserModel user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task AddRolePatientInUser(ApplicationUserModel user)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }

    }
}