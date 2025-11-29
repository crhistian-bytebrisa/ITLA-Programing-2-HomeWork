using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class MedicalDocumentsService : IMedicalDocumentsService
    {
        private readonly IMedicalDocumentRepository _repo;

        public MedicalDocumentsService(IMedicalDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<MedicalDocumentModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<(byte[],string,string)> GetFileByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception("Documento no encontrado");
            }

            var filePath = entity.FileUrl;

            if (!File.Exists(filePath))
            {
                throw new Exception("Archivo no encontrado en el servidor");
            }

            byte[] result;
            using (var stream = new StreamReader(filePath))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.BaseStream.CopyTo(memoryStream);
                    result = memoryStream.ToArray();
                }
            }

            string contentType;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return (result,contentType,entity.FileName);
        }

        public async Task<APIResponse<MedicalDocumentDTO>> GetAllAsync(MedicalDocumentRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<MedicalDocumentDTO> listdto = list.Adapt<List<MedicalDocumentDTO>>();

            var APIR = new APIResponse<MedicalDocumentDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<MedicalDocumentDTO> AddAsync(MedicalDocumentCreateDTO dtoc)
        {
            try
            {
                string filename = dtoc.FileName;
                string extension = Path.GetExtension(dtoc.File.FileName);
                string patienName = await _repo.PatientName(dtoc.PatientId);
                patienName = "Patient_" + patienName.Replace(" ", "_");

                string filepath = Path.Combine("MedicalDocuments", $"{patienName}", $"{filename}{extension}");

                var data = new MedicalDocumentCreateWithUrlDTO
                {
                    PatientId = dtoc.PatientId,
                    FileName = filename,
                    FileUrl = filepath,
                    DocumentType = extension
                };

                string directoryPath = Path.GetDirectoryName(filepath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    await dtoc.File.CopyToAsync(fileStream);
                }

                var model = data.Adapt<MedicalDocumentModel>();
                await _repo.AddAsync(model);
                return model.Adapt<MedicalDocumentDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error cargando el archivo", ex);
            }
        }

        public async Task DeleteAsync(MedicalDocumentModel model)
        {
            string path = model.FileUrl;

            if(File.Exists(path))
            {
                File.Delete(path);

            }

            await _repo.DeleteAsync(model);
        }
    }
}
