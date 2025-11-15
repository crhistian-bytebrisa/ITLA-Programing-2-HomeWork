namespace MediAgenda.Application.DTOs.API
{
    //Este DTO se hizo con lo aprendido del profe
    public class APIResponse<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        {
            get
            {
                if (PageSize == 0) return 0;
                return (int)Math.Ceiling((double)TotalCount / PageSize);
            }
        }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }


        public APIResponse()
        {
            
        }

        public APIResponse(List<T> data, int totalcount, int page, int pagesize)
        {
            Page = page;
            PageSize = pagesize;
            TotalCount = totalcount;
            Data = data;
        }
    }
}
