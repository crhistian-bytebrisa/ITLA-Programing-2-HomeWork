using MediAgenda.API.DTOs.Relations;

namespace MediAgenda.API.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PrescriptionPermissionDTO> PrescriptionPermissions { get; set; }
    }

    public class PermissionSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PermissionCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
