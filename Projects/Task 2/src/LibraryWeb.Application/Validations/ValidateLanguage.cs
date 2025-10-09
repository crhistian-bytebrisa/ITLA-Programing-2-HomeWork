using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories;
using Mapster;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.Validations
{
    public class ValidateLanguage
    {
        private static async Task GetName(string name, ILanguageRepository repo)
        {
            var lang = await repo.GetByName(name);

            if (lang != null)
            {
                throw new ApplicationException("Ya existe este lenguaje.");
            }
        }

        private static async Task<Language> GetId(int id, ILanguageRepository repo)
        {
            var lang = await repo.GetByIdAsync(id);
            if (lang == null)
            {
                throw new ApplicationException("No existe este lenguaje.");
            }

            return lang;
        }

        public static Func<CreateLanguageDTO, ILanguageRepository, Task<Language>> CheckAdd = async (ClangDTO, repo) =>
        {
            await GetName(ClangDTO.Name, repo);
            return ClangDTO.Adapt<Language>();
        };

        public static Func<int,CreateLanguageDTO, ILanguageRepository, Task<Language>> CheckUpdate = async (id, langDTO, repo) =>
        {
            await GetName(langDTO.Name, repo);
            await GetId(id, repo);
            var l = langDTO.Adapt<Language>();
            l.Id = id;
            return l;
        };

        public static Func<int, ILanguageRepository, Task<Language>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
