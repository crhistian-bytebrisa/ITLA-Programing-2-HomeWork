namespace MediAgenda.Application.DTOs.Relations
{
    public class PrescriptionPermissionDTO
    {
        public int PrescriptionId { get; set; }
        public PrescriptionSimpleDTO Prescription { get; set; }
        public int PermissionId { get; set; }
        public PermissionSimpleDTO Permission { get; set; }
    }

    public class PrescriptionPermissionSimpleDTO
    {
        public int PrescriptionId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
    }

    public class PrescriptionPermissionCreateDTO
    {
        public int PrescriptionId { get; set; }
        public int PermissionId { get; set; }
    }
}
