using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Application.Validations;
using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories;
using Mapster;

namespace LibraryWeb.Application.Services
{
    public class LanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<List<LanguageDTO>> GetAllAsync()
        {
            var languages = await _languageRepository.GetAllAsync();
            return languages.Adapt<List<LanguageDTO>>(); 
        }

        public async Task<LanguageDTO?> GetByIdAsync(int id)
        {
            
            var language = await _languageRepository.GetByIdAsync(id);    
            return language.Adapt<LanguageDTO>();
        }

        public async Task<LanguageDTO> AddAsync(CreateLanguageDTO ClanguageDTO)
        {
            await ValidateLanguage.CheckAdd(ClanguageDTO, _languageRepository);

            var language = ClanguageDTO.Adapt<Language>();
            await _languageRepository.AddAsync(language);
            var languageDTO = language.Adapt<LanguageDTO>();
            return languageDTO;
        }

        public async Task<LanguageDTO?> UpdateAsync(LanguageDTO languageDTO)
        {
            await ValidateLanguage.CheckUpdate(languageDTO, _languageRepository);

            var language = languageDTO.Adapt<Language>();
            await _languageRepository.UpdateAsync(language);
            languageDTO = language.Adapt<LanguageDTO>();
            return languageDTO;
        }

        public async Task DeleteAsync(int id)
        {
            var language = await ValidateLanguage.CheckDelete(id, _languageRepository);
            
            await _languageRepository.DeleteAsync(language);
        }

        
        
    }
}