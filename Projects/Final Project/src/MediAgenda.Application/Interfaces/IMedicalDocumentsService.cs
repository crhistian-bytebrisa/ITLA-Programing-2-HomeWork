using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Http;

namespace MediAgenda.Application.Interfaces
{
    public interface IMedicalDocumentsService
    {
        Task<MedicalDocumentDTO> AddAsync(MedicalDocumentCreateDTO dtoc);
        Task DeleteAsync(MedicalDocumentModel model);
        Task<APIResponse<MedicalDocumentDTO>> GetAllAsync(MedicalDocumentRequest request);
        Task<MedicalDocumentModel> GetByIdAsync(int id);
        Task<(byte[],string,string)> GetFileByIdAsync(int id);
    }
}